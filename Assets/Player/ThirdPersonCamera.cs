using UnityEngine;

public class ThirdPersonCamera : MonoBehaviour
{
    public GameObject Center;    // Merkez objesi
    public GameObject Player;    // Oyuncu objesi
    public float angularSpeed = 1f;  // A��sal h�z

    private float playerAngle;       // Oyuncunun a��sal pozisyonu (radyan cinsinden)

    private Vector3 oldPos;

    void Start()
    {
        // Oyuncunun ba�lang�� a��sal pozisyonunu belirle
       // playerAngle = Mathf.Atan2(Player.transform.position.z - Center.transform.position.z, Player.transform.position.x - Center.transform.position.x);
    }

    float GetDistanceBetweenPlayerAndCenter()
    {
        // Dinamik yar��ap g�ncellemesi
        return (Player.transform.position - Center.transform.position).magnitude;
    }

    private void Update()
    {
        MoveCamera();
    }

    void MoveCamera()
    {
        if((Player.transform.position - transform.position).magnitude < 5) { return; }

    }

    //void MoveCamera(Vector3 currentPos)
    //{
    //    if(currentPos ==  oldPos) return;

    //    transform.LookAt(Player.transform.position);

    //    oldPos = Player.transform.position;

    //    Vector3 vektor1 = currentPos - Center.transform.position;
    //    Vector3 vektor2 = oldPos - Center.transform.position;

    //    // �ki vekt�r aras�ndaki a��y� bulma
    //    float Angle = Vector3.Angle(vektor1, vektor2);

    //    ///try360-x)

    //    float newPosX = transform.position.x * Mathf.Cos(Angle * Mathf.Deg2Rad) + transform.position.z * Mathf.Sin(Angle * Mathf.Deg2Rad);
    //    float newPosZ = transform.position.z * Mathf.Cos(Angle * Mathf.Deg2Rad) - transform.position.x * Mathf.Sin(Angle * Mathf.Deg2Rad);

    //    transform.position += new Vector3 (newPosX-transform.position.x,0 , newPosZ-transform.position.z);
        
    //    //newPosX = transform.position.x; 
    //    //newPosZ = transform.position.z;

    //    //float distanceTravelled = (vektor1.magnitude + vektor2.magnitude) / 2 * Angle;

    //    oldPos = currentPos;
    //}

    //void Update()
    //{
    //    // Oyuncunun a��s�n� dinamik olarak g�ncelle
    //    float currentPlayerAngle = Mathf.Atan2(Player.transform.position.z - Center.transform.position.z, Player.transform.position.x - Center.transform.position.x);

    //    // A��sal fark
    //    float deltaAngle = Mathf.DeltaAngle(Mathf.Rad2Deg * playerAngle, Mathf.Rad2Deg * currentPlayerAngle);
    //    playerAngle = currentPlayerAngle; // Yeni a��y� g�ncelle

    //    // Oyuncunun �ember �zerinde kat etti�i mesafe
    //    float traveledDistance = GetDistanceBetweenPlayerAndCenter() * Mathf.Deg2Rad * deltaAngle;

    //    // Kameray� g�ncelleme
    //    UpdateCamera(traveledDistance);
    //}

    //void UpdateCamera(float traveledDistance)
    //{
    //    // Kameran�n a��sal konumunu oyuncunun kat etti�i mesafe ile g�ncelle
    //    float cameraAngleDelta = traveledDistance / GetDistanceBetweenPlayerAndCenter();
    //    float cameraAngle = playerAngle + cameraAngleDelta;

    //    // Kameran�n �ember �zerindeki yeni pozisyonunu hesapla
    //    Vector3 cameraOffset = new Vector3(Mathf.Cos(cameraAngle), 0, Mathf.Sin(cameraAngle)) * GetDistanceBetweenPlayerAndCenter();
    //    transform.position = Center.transform.position + cameraOffset;

    //    // Kameray� oyuncunun arkas�na bakacak �ekilde ayarla
    //    Vector3 lookAtTarget = Player.transform.position + Vector3.up * 1.5f; // Biraz yukar� bakmas� i�in offset ekliyoruz
    //    transform.LookAt(lookAtTarget);
    //}
}
