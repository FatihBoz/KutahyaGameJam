using Unity.Cinemachine;
using UnityEngine;

public class CircularDistanceCalculation : MonoBehaviour
{
    public Vector3 startPoint;        // Baþlangýç noktasý
    public GameObject character;       // Karakter nesnesi (pozisyonunu alacaðýz)
    float radius = 17f;        
    float offset = 0f;         // Eklenecek offset deðeri

    void Update()
    {
        Vector3 characterPosition = character.transform.position;

        // Merkez noktasýný hesapla (baþlangýç ve karakterin pozisyonlarýyla ayný düzlemde olmalý)
        Vector3 center = transform.position;

        // Baþlangýç ve karakter pozisyonlarýna göre merkezden uzak olan doðrultu vektörlerini al
        Vector3 directionToStart = (startPoint - center).normalized;
        Vector3 directionToCharacter = (characterPosition - center).normalized;

        // Açý hesapla (derece cinsinden döner)
        float angleInDegrees = Vector3.Angle(directionToStart, directionToCharacter);
        float angleInRadians = angleInDegrees * Mathf.Deg2Rad;

        // Yay uzunluðunu hesapla ve offset ekle
        float arcLength = radius * angleInRadians + offset;

        GetComponent<CinemachineSplineDolly>().CameraPosition = arcLength;

        Debug.Log("Karakterin yay üzerindeki uzaklýðý: " + arcLength);
    }
}