using UnityEngine;

public class Arrow : MonoBehaviour
{
    [SerializeField] private float moveSpeed;
    [SerializeField] private float selfDestroyTime;

    private void Start()
    {
        Destroy(this.gameObject,selfDestroyTime);
    }

    // Update is called once per frame
    void Update()
    {
        transform.position -= moveSpeed * Time.deltaTime * Vector3.right;
    }


    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Player.Instance.PlayerDied();
        }
    }
}
