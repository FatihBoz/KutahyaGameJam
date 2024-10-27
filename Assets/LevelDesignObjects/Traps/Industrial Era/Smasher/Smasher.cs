using System;
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
        // Ray'in baþlangýç noktasýný belirle
        Vector3 startPosition = transform.position;
        // Ray'in yönünü belirle
        Vector3 direction = Vector3.down * rayDistance;

        // Ray'i çiz
        Gizmos.color = Color.red; // Çizgi rengi
        Gizmos.DrawLine(startPosition, startPosition + direction); // Aþaðý doðru çizer

    }

    protected override IEnumerator ForwardMove(Vector3 targetPos)
    {
        float timeElapsed = 0f;
        while (timeElapsed < actionDuration)
        {
            transform.position = Vector3.Lerp(startPos, targetPos, timeElapsed / actionDuration);
            timeElapsed += Time.deltaTime;

            yield return null;
        }
        yield return new WaitForSeconds(waitingTimeAfterPush);
        StartCoroutine(ReturnToDefault(startPos));
    }

    protected override IEnumerator ReturnToDefault(Vector3 targetPos)
    {
        canSmash = true;
        yield return base.ReturnToDefault(targetPos); // `yield return` kullanarak IEnumerator tamamlanana kadar bekle
        canSmash = false;
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
