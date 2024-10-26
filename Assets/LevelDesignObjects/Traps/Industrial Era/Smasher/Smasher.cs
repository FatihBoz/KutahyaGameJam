using System.Collections;
using UnityEngine;

public class Smasher : MovingTrap
{
    [SerializeField] private float rayDistance;
    [SerializeField] private LayerMask layer;
    private bool canSmash;


    protected override void Update()
    {
        Ray ray = new(transform.position, Vector3.down);

        if (Physics.Raycast(ray, out RaycastHit hit, rayDistance, layer))
        {
            StopAllCoroutines();
            return;
        }

        base.Update();
    }

    private void OnDrawGizmos()
    {
        // Ray'in ba�lang�� noktas�n� belirle
        Vector3 startPosition = transform.position;
        // Ray'in y�n�n� belirle
        Vector3 direction = Vector3.down * rayDistance;

        // Ray'i �iz
        Gizmos.color = Color.red; // �izgi rengi
        Gizmos.DrawLine(startPosition, startPosition + direction); // A�a�� do�ru �izer

    }

    protected override IEnumerator ForwardMove(Vector3 targetPos)
    {
        canSmash = true;

        float timeElapsed = 0f;
        while (timeElapsed < actionDuration)
        {
            transform.position = Vector3.Lerp(startPos, targetPos, timeElapsed / actionDuration);
            timeElapsed += Time.deltaTime;

            yield return null;
        }

        canSmash = false;

        yield return new WaitForSeconds(waitingTimeAfterPush);
        StartCoroutine(ReturnToDefault(startPos));
    }


    private void OnCollisionEnter(Collision collision)
    {
        if (!canSmash)
        {
            return;
        }

        if (collision.gameObject.CompareTag("Player"))
        {
            Player.Instance.PlayerDied();
            //kan efekti belki
        }
    }

}
