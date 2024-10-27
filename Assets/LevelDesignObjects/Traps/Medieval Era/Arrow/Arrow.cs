using UnityEngine;

public class Arrow : MonoBehaviour
{
    [SerializeField] private float moveSpeed;
    [SerializeField] private float selfDestroyTime;
    [SerializeField] private float timeBetweenAttacks;

    private float elapsedTime;
    private bool canMove;
    private Vector3 startPos;

    private void Start()
    {
        elapsedTime = timeBetweenAttacks;
        startPos = transform.position;
    }

    private void Update()
    {

        Vector3 forwardDirection = Quaternion.Euler(0, transform.eulerAngles.y, 0) * transform.forward;
        transform.position -= moveSpeed * Time.deltaTime * forwardDirection;

        transform.rotation = Quaternion.Euler(90, transform.eulerAngles.y, transform.eulerAngles.z);

        elapsedTime += Time.deltaTime;

        if (elapsedTime >= selfDestroyTime)
        {
            Invoke(nameof(SetInactive), selfDestroyTime);
            elapsedTime = 0;
        }
    }

    void SetInactive()
    {
        canMove = false;
        transform.position = startPos;
        ArrowThrower.Instance.MakeActiveWithDelay(this.gameObject, timeBetweenAttacks);
        this.gameObject.SetActive(false);
    }



    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Player.Instance.PlayerDied();
        }
    }
}
