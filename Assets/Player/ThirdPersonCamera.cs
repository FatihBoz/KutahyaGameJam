using UnityEngine;

public class ThirdPersonCamera : MonoBehaviour
{
    public Transform target;       // Takip edilecek hedef (karakter)
    public float distance = 5.0f;  // Hedefle kamera aras�ndaki mesafe
    public float smoothSpeed = 10.0f;  // Kameran�n ge�i� h�z�n� ayarlar
    public float minDistance = 1.0f;   // Minimum kamera mesafesi (duvara �arpt���nda)

    private Vector3 currentVelocity;

    void LateUpdate()
    {
        // Kamera ile karakter aras�ndaki varsay�lan konumu hesapla
        Vector3 desiredPosition = target.position - target.forward * distance;

        // Kameran�n hedef ile aras�ndaki �arp��malar� kontrol etmek i�in bir ray olu�tur
        RaycastHit hit;
        if (Physics.Raycast(target.position, -target.forward, out hit, distance))
        {
            // E�er �arp��ma varsa, kameray� �arp��ma noktas�na daha yak�n konumland�r
            float hitDistance = Mathf.Clamp(hit.distance, minDistance, distance);
            desiredPosition = target.position - target.forward * hitDistance;
        }

        // Kameran�n pozisyonunu yumu�ak bir ge�i�le hedef pozisyona yakla�t�r
        transform.position = Vector3.SmoothDamp(transform.position, desiredPosition, ref currentVelocity, smoothSpeed * Time.deltaTime);

        // Kameran�n hedefe bakmas�n� sa�lar
        transform.LookAt(target);
    }
}
