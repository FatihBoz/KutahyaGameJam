using UnityEngine;

public class TestMovement : MonoBehaviour
{
    public float moveSpeed = 5f;
    public Transform cameraTransform;

    void Update()
    {
        // Yön girdilerini al
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");

        // Kameranýn ileri ve sað yönünü al, y ekseninde hareketi sabitle
        Vector3 forward = cameraTransform.forward;
        Vector3 right = cameraTransform.right;
        forward.y = 0f;
        right.y = 0f;

        // Hareket yönünü kameraya göre ayarla
        Vector3 direction = (forward * vertical + right * horizontal).normalized;

        // Yön girdisi varsa
        if (direction.magnitude >= 0.1f)
        {
            // Karakterin bakýþ yönünü hareket yönüne göre döndür
            float targetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.Euler(-90, targetAngle, 0);

            // Ýleri doðru hareket et
            transform.Translate(Vector3.forward * moveSpeed * Time.deltaTime);
        }
    }
}
