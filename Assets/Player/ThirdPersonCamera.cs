using UnityEngine;

public class ThirdPersonCamera : MonoBehaviour
{
    public Transform target;       // Takip edilecek hedef (karakter)
    public float distance = 5.0f;  // Hedefle kamera arasýndaki mesafe
    public float smoothSpeed = 10.0f;  // Kameranýn geçiþ hýzýný ayarlar
    public float minDistance = 1.0f;   // Minimum kamera mesafesi (duvara çarptýðýnda)

    private Vector3 currentVelocity;

    void LateUpdate()
    {
        // Kamera ile karakter arasýndaki varsayýlan konumu hesapla
        Vector3 desiredPosition = target.position - target.forward * distance;

        // Kameranýn hedef ile arasýndaki çarpýþmalarý kontrol etmek için bir ray oluþtur
        RaycastHit hit;
        if (Physics.Raycast(target.position, -target.forward, out hit, distance))
        {
            // Eðer çarpýþma varsa, kamerayý çarpýþma noktasýna daha yakýn konumlandýr
            float hitDistance = Mathf.Clamp(hit.distance, minDistance, distance);
            desiredPosition = target.position - target.forward * hitDistance;
        }

        // Kameranýn pozisyonunu yumuþak bir geçiþle hedef pozisyona yaklaþtýr
        transform.position = Vector3.SmoothDamp(transform.position, desiredPosition, ref currentVelocity, smoothSpeed * Time.deltaTime);

        // Kameranýn hedefe bakmasýný saðlar
        transform.LookAt(target);
    }
}
