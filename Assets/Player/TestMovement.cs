using UnityEngine;

public class TestMovement : MonoBehaviour
{
    public float moveSpeed = 5f;
    public Transform cameraTransform;

    void Update()
    {
        // Y�n girdilerini al
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");

        // Kameran�n ileri ve sa� y�n�n� al, y ekseninde hareketi sabitle
        Vector3 forward = cameraTransform.forward;
        Vector3 right = cameraTransform.right;
        forward.y = 0f;
        right.y = 0f;

        // Hareket y�n�n� kameraya g�re ayarla
        Vector3 direction = (forward * vertical + right * horizontal).normalized;

        // Y�n girdisi varsa
        if (direction.magnitude >= 0.1f)
        {
            // Karakterin bak�� y�n�n� hareket y�n�ne g�re d�nd�r
            float targetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.Euler(-90, targetAngle, 0);

            // �leri do�ru hareket et
            transform.Translate(Vector3.forward * moveSpeed * Time.deltaTime);
        }
    }
}
