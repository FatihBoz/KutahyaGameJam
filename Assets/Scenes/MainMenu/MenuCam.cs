using UnityEngine;

public class MenuCam : MonoBehaviour
{
    
    [SerializeField]
    private Transform center;
    private Vector3 centerPos;

[SerializeField]
    private float downSpeed=1f;
    [SerializeField]
    private float rotateSpeed=1f;
    private void Start() {
        centerPos=center.position;
        centerPos.y=transform.position.y;
    }
    private void Update() {
        centerPos.y=transform.position.y;
        transform.RotateAround(centerPos,Vector3.up,Time.deltaTime*rotateSpeed);
        Vector3 transformPos=transform.position;
        transformPos.y-=downSpeed*Time.deltaTime;
        transform.position=transformPos;
    }

}
