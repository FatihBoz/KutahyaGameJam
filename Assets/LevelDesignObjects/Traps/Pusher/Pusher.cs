using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Pusher : MovingTrap
{
    [SerializeField] private float pushForce;
    private bool canPush;



    protected override void Update()
    {
        if (idle)
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



    private void OnTriggerStay(Collider other)
    {
        print(canPush);
        if (!canPush)
        {
            return;
        }

        Vector3 dir = (transform.position - startPos).normalized;
        Vector3 force = dir * pushForce;

        if (other.TryGetComponent<Rigidbody>(out var rb) && other.CompareTag("Player"))
        {
            rb.linearVelocity = Vector3.zero;
            rb.AddForce(force, ForceMode.VelocityChange);
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
