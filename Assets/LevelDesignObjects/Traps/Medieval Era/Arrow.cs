using UnityEngine;

public class Arrow : MonoBehaviour
{
    [SerializeField] private float moveSpeed;
    Vector3 moveDir;

    // Update is called once per frame
    void Update()
    {
        if (moveDir == Vector3.zero)
            return;

        transform.position += moveSpeed * Time.deltaTime * moveDir;
    }

    public void SetMoveDirection(Vector3 dir)
    {
        moveDir = dir;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Destroy(collision.gameObject);
        }
    }
}
