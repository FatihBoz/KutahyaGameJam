using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.FullSerializer;
using UnityEngine;


public class Pusher : MovingTrap
{
    [SerializeField] private float pushForce;

    private bool canPush;


    private void Start()
    {
        startPos = transform.position;
    }

    protected override void Update()
    {
        if (!canPush)
        {
            elapsedTimeAfterFirstMove += Time.deltaTime;

            if (elapsedTimeAfterFirstMove >= timeBetweenForwardBackwardMoves)
            {
                Vector3 targetPos = new(startPos.x - displacementAmount, startPos.y, startPos.z);

                StartCoroutine(ForwardMove(targetPos));

                elapsedTimeAfterFirstMove = 0;
                return;
            }

        }

    }



    protected override IEnumerator ForwardMove(Vector3 targetPos)
    {
        canPush = true;
        StartCoroutine(base.ForwardMove(targetPos));

        canPush = false;

        yield return null;
    }


    private void OnTriggerStay(Collider other)
    {
        if (!canPush)
        {
            return;
        }

        Vector3 dir = (transform.position - startPos).normalized;
        Vector3 force = dir * pushForce;

        if (other.TryGetComponent<Rigidbody>(out var rb))
        {
            rb.linearVelocity = Vector3.zero;
            rb.AddForce(force, ForceMode.Impulse);
        }

        
    }


}
