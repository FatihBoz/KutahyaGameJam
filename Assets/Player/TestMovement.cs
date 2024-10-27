using UnityEngine;
using UnityEngine.InputSystem;

public class TestMovement : MonoBehaviour
{
    public float speed = 5f;
    private CharacterController controller;
    private Vector2 moveInput;

    private void Start()
    {
        controller = GetComponent<CharacterController>();
    }

    // Update her karede �a�r�l�r
    private void Update()
    {
        if (moveInput.sqrMagnitude > 0.01f)
        {
            // Kamera y�n�n� almak
            Vector3 camForward = Camera.main.transform.forward;
            Vector3 camRight = Camera.main.transform.right;
            
            // Y eksenini s�f�rl�yoruz ki karakter sadece x-z ekseninde hareket etsin
            camForward.y = 0;
            camRight.y = 0;
            camForward.Normalize();
            camRight.Normalize();

            // Input'u kameran�n y�n�ne g�re d�n��t�rmek
            Vector3 moveDirection = (camForward * moveInput.y + camRight * moveInput.x).normalized;

            // Karakteri hareket ettirmek
            controller.Move(moveDirection * speed * Time.deltaTime);

            // Karakterin y�n�n� hareket y�n�ne �eviriyoruz
            if (moveDirection != Vector3.zero)
            {
                transform.forward = moveDirection;
            }
        }
    }

    // Yeni Input System i�in Input Action ba�lant�s�
    public void OnMove(InputAction.CallbackContext context)
    {
        moveInput = context.ReadValue<Vector2>();
    }




}
