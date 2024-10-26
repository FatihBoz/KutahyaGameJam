using UnityEngine;

public class Rock : MonoBehaviour
{
    private Rigidbody rb;
    public float timeSpeed=2f;
    private float time;
    public float moveSpeed;
    public float rollForce;
    private Vector3 throwDirection;
    private Vector3 startPos;
    void Start()
    {
        startPos=transform.position;
        time=Time.time;
        throwDirection=Vector3.forward;
        rb=GetComponent<Rigidbody>();    
        rb.AddForce(moveSpeed*transform.forward);
        rb.AddTorque(Vector3.right*rollForce);
    }
    void Update()
    {
        if (Time.time>=time+timeSpeed)
        {
            rb.linearVelocity=Vector3.zero;
            rb.angularVelocity = Vector3.zero;

            transform.position=startPos;
            rb.AddForce(moveSpeed*throwDirection);
            rb.AddTorque(Vector3.right*rollForce);
            time=Time.time;
        }
    }
    public void SetThrowDirection(Vector3 throwDirection)
    {
        this.throwDirection=throwDirection;
    }
    /// <summary>
    /// OnCollisionEnter is called when this collider/rigidbody has begun
    /// touching another rigidbody/collider.
    /// </summary>
    /// <param name="other">The Collision data associated with this collision.</param>
    void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            other.gameObject.GetComponent<Rigidbody>().AddForce(transform.forward*30000);
            Player.Instance.PlayerDied(2f);
        }
    }
}
