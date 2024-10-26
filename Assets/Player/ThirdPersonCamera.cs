using UnityEngine;

public class ThirdPersonCamera : MonoBehaviour
{
    public GameObject Center;    // Merkez objesi
    public GameObject Player;    // Oyuncu objesi
    public float angularSpeed = 1f;  // Açýsal hýz

    private float playerAngle;       // Oyuncunun açýsal pozisyonu (radyan cinsinden)

    private Vector3 oldPos;

    void Start()
    {
        // Oyuncunun baþlangýç açýsal pozisyonunu belirle
       // playerAngle = Mathf.Atan2(Player.transform.position.z - Center.transform.position.z, Player.transform.position.x - Center.transform.position.x);
    }

    float GetDistanceBetweenPlayerAndCenter()
    {
        // Dinamik yarýçap güncellemesi
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

    //    // Ýki vektör arasýndaki açýyý bulma
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
    //    // Oyuncunun açýsýný dinamik olarak güncelle
    //    float currentPlayerAngle = Mathf.Atan2(Player.transform.position.z - Center.transform.position.z, Player.transform.position.x - Center.transform.position.x);

    //    // Açýsal fark
    //    float deltaAngle = Mathf.DeltaAngle(Mathf.Rad2Deg * playerAngle, Mathf.Rad2Deg * currentPlayerAngle);
    //    playerAngle = currentPlayerAngle; // Yeni açýyý güncelle

    //    // Oyuncunun çember üzerinde kat ettiði mesafe
    //    float traveledDistance = GetDistanceBetweenPlayerAndCenter() * Mathf.Deg2Rad * deltaAngle;

    //    // Kamerayý güncelleme
    //    UpdateCamera(traveledDistance);
    //}

    //void UpdateCamera(float traveledDistance)
    //{
    //    // Kameranýn açýsal konumunu oyuncunun kat ettiði mesafe ile güncelle
    //    float cameraAngleDelta = traveledDistance / GetDistanceBetweenPlayerAndCenter();
    //    float cameraAngle = playerAngle + cameraAngleDelta;

    //    // Kameranýn çember üzerindeki yeni pozisyonunu hesapla
    //    Vector3 cameraOffset = new Vector3(Mathf.Cos(cameraAngle), 0, Mathf.Sin(cameraAngle)) * GetDistanceBetweenPlayerAndCenter();
    //    transform.position = Center.transform.position + cameraOffset;

    //    // Kamerayý oyuncunun arkasýna bakacak þekilde ayarla
    //    Vector3 lookAtTarget = Player.transform.position + Vector3.up * 1.5f; // Biraz yukarý bakmasý için offset ekliyoruz
    //    transform.LookAt(lookAtTarget);
    //}
}
