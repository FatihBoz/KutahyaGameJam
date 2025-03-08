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
        if (Physics.Raycast(ray, out _, rayDistance, layer))
        {
            StopAllCoroutines();
            return;
        }


        base.Update();
    }

    private void OnDrawGizmos()
    {
        Vector3 startPosition = transform.position;
        Vector3 direction = Vector3.down * rayDistance;

        Gizmos.color = Color.red;
        Gizmos.DrawLine(startPosition, startPosition + direction);

    }


    protected override IEnumerator ForwardMove()
    {
        canSmash = true;
        idle = false;

        SetAnimation(true);
        yield return new WaitForSeconds(waitingTimeAfterPush + actionDuration);
        canSmash = false;

        StartCoroutine(ReturnToDefault());
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
        }
    }

}
