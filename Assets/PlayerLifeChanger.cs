using UnityEngine;
public class PlayerLifeChanger : MonoBehaviour
{
    public float moveSpeed = 5f;   // Karakterin hareket h�z�
    public Camera playerCamera;     // Kullan�c�n�n kameras�

    void Update()
    {
        // Klavye girdilerini al
        float horizontal = Input.GetAxis("Horizontal"); // A/D veya Sol/Sa� ok tu�lar�
        float vertical = Input.GetAxis("Vertical");     // W/S veya Yukar�/A�a�� ok tu�lar�

        // Kameran�n ileri ve sa� vekt�rlerini al
        Vector3 forward = playerCamera.transform.forward; // Kameran�n ileri y�n�
        Vector3 right = playerCamera.transform.right;     // Kameran�n sa� y�n�

        // Yaln�zca x-z d�zleminde hareket ettirmek i�in y bile�enini s�f�rla
        forward.y = 0;
        right.y = 0;

        // Normalizasyon i�lemi
        forward.Normalize();
        right.Normalize();

        // Hareket vekt�r�n� olu�tur
        Vector3 movement = (forward * vertical + right * horizontal).normalized * moveSpeed * Time.deltaTime;

        // Karakterin pozisyonunu g�ncelle
        transform.position += movement;
    }
}
