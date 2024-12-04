using Unity.Cinemachine;
using UnityEngine;

public class CircularDistanceCalculation : MonoBehaviour
{
    public Vector3 startPoint;        // Ba�lang�� noktas�
    public GameObject character;       // Karakter nesnesi (pozisyonunu alaca��z)
    float radius = 17f;        
    float offset = 0f;         // Eklenecek offset de�eri

    void Update()
    {
        Vector3 characterPosition = character.transform.position;

        // Merkez noktas�n� hesapla (ba�lang�� ve karakterin pozisyonlar�yla ayn� d�zlemde olmal�)
        Vector3 center = transform.position;

        // Ba�lang�� ve karakter pozisyonlar�na g�re merkezden uzak olan do�rultu vekt�rlerini al
        Vector3 directionToStart = (startPoint - center).normalized;
        Vector3 directionToCharacter = (characterPosition - center).normalized;

        // A�� hesapla (derece cinsinden d�ner)
        float angleInDegrees = Vector3.Angle(directionToStart, directionToCharacter);
        float angleInRadians = angleInDegrees * Mathf.Deg2Rad;

        // Yay uzunlu�unu hesapla ve offset ekle
        float arcLength = radius * angleInRadians + offset;

        GetComponent<CinemachineSplineDolly>().CameraPosition = arcLength;

        Debug.Log("Karakterin yay �zerindeki uzakl���: " + arcLength);
    }
}