using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Pusher : MovingTrap
{
    [SerializeField] private float pushForce;
    [SerializeField] private float rayDistance;
    [SerializeField] private LayerMask layerMask;
    private bool canPush;


    protected override void Start()
    {
        base.Start();

    }

    protected override void Update()
    {
        Ray ray = new(transform.position, Vector3.left);
        if (Physics.Raycast(ray, out _, rayDistance, layerMask))
        {
            StopAllCoroutines();
            return;
        }

        base.Update();
    }


    protected override IEnumerator ForwardMove()
    {
        canPush = true;
        idle = false;

        SetAnimation(true);

        yield return new WaitForSeconds(waitingTimeAfterPush + actionDuration);
        canPush = false;
        StartCoroutine(ReturnToDefault());
    }



    private void OnTriggerStay(Collider other)
    {
        if (!canPush)
        {
            return;
        }

        if (other.CompareTag("Player"))
        {
            Player.Instance.PlayerDied();
            canPush = false;
        }


    }

    //private void OnCollisionStay(Collision collision)
    //{
    //    if (!canPush)
    //    {
    //        return;
    //    }

    //    Vector3 dir = (transform.position - startPos).normalized;
    //    Vector3 force = dir * pushForce;

    //    collision.rigidbody.linearVelocity = Vector3.zero;
    //    collision.rigidbody.AddForce(force,ForceMode.Impulse);
    //}


}
