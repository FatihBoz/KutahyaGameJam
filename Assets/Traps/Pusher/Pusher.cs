using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Pusher : MovingTrap
{
    [SerializeField] private float pushForce;


    private bool canPush;



    private void Update()
    {
        if (!canPush)
        {
            elapsedTimeAfterAction += Time.deltaTime;

            if (elapsedTimeAfterAction >= timeBetweenActions)
            {
                Vector3 targetPos = new(startPos.x - displacementAmount, startPos.y, startPos.z);

                StartCoroutine(ForwardMove(targetPos));

                elapsedTimeAfterAction = 0;
                return;
            }

        }


        //if (canPush)
        //{
        //    if (waitingTimeBetweenPushAndPull >= waitingTimeAfterPush)
        //    {
        //        StartCoroutine(ReturnToDefault());
        //        waitingTimeBetweenPushAndPull = 0;
        //        return;
        //    }
        //    waitingTimeBetweenPushAndPull += Time.deltaTime;
        //}
    }



    protected override IEnumerator ForwardMove(Vector3 targetPos)
    {
        canPush = true;
        float timeElapsed = 0f;
 

        while (timeElapsed < actionDuration)
        {
            transform.position = Vector3.Lerp(startPos, targetPos, timeElapsed / actionDuration);
            timeElapsed += Time.deltaTime;

            yield return null;
        }

        canPush = false;

        StartCoroutine(ReturnToDefault(startPos));
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

    //    collision.rigidbody.AddForce(force, ForceMode.Impulse);
    //    //collision.rigidbody.linearVelocity = force;
    //}

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
