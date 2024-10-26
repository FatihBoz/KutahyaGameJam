using UnityEngine;
public class PlayerLifeChanger : MonoBehaviour
{
    public float moveSpeed = 5f;   // Karakterin hareket hýzý
    public Camera playerCamera;     // Kullanýcýnýn kamerasý

    void Update()
    {
        // Klavye girdilerini al
        float horizontal = Input.GetAxis("Horizontal"); // A/D veya Sol/Sað ok tuþlarý
        float vertical = Input.GetAxis("Vertical");     // W/S veya Yukarý/Aþaðý ok tuþlarý

        // Kameranýn ileri ve sað vektörlerini al
        Vector3 forward = playerCamera.transform.forward; // Kameranýn ileri yönü
        Vector3 right = playerCamera.transform.right;     // Kameranýn sað yönü

        // Yalnýzca x-z düzleminde hareket ettirmek için y bileþenini sýfýrla
        forward.y = 0;
        right.y = 0;

        // Normalizasyon iþlemi
        forward.Normalize();
        right.Normalize();

        // Hareket vektörünü oluþtur
        Vector3 movement = (forward * vertical + right * horizontal).normalized * moveSpeed * Time.deltaTime;

        // Karakterin pozisyonunu güncelle
        transform.position += movement;
    }
}
