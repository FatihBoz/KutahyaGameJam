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

    // Update her karede çaðrýlýr
    private void Update()
    {
        if (moveInput.sqrMagnitude > 0.01f)
        {
            // Kamera yönünü almak
            Vector3 camForward = Camera.main.transform.forward;
            Vector3 camRight = Camera.main.transform.right;
            
            // Y eksenini sýfýrlýyoruz ki karakter sadece x-z ekseninde hareket etsin
            camForward.y = 0;
            camRight.y = 0;
            camForward.Normalize();
            camRight.Normalize();

            // Input'u kameranýn yönüne göre dönüþtürmek
            Vector3 moveDirection = (camForward * moveInput.y + camRight * moveInput.x).normalized;

            // Karakteri hareket ettirmek
            controller.Move(moveDirection * speed * Time.deltaTime);

            // Karakterin yönünü hareket yönüne çeviriyoruz
            if (moveDirection != Vector3.zero)
            {
                transform.forward = moveDirection;
            }
        }
    }

    // Yeni Input System için Input Action baðlantýsý
    public void OnMove(InputAction.CallbackContext context)
    {
        moveInput = context.ReadValue<Vector2>();
    }




}
