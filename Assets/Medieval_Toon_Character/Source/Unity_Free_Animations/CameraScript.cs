using UnityEngine;

public class CameraScript : MonoBehaviour

{
    [SerializeField]
    private Transform center;
    [SerializeField]
    private Vector3 offset;
    private float rad;
    private Vector3 dirToCenter;
    private Vector3 centerVec;
    void Start()
    {
        transform.position=Player.Instance.transform.position;
        
        centerVec=center.position;
        centerVec.y=transform.position.y;
        
        dirToCenter=centerVec-transform.position;
        rad=Vector3.Magnitude(dirToCenter);
    
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 realPos= Player.Instance.transform.position+offset;

        centerVec.y=realPos.y;
        Vector3 fakePos= realPos-centerVec;

        transform.position=centerVec+fakePos.normalized*rad;

        centerVec.y=transform.position.y;
        transform.LookAt(centerVec);
        transform.Rotate(new Vector3(0,90f,0));
    }
}
