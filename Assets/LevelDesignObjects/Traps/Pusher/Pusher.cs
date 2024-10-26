using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Pusher : MovingTrap
{
    [SerializeField] private float pushForce;
    private bool canPush;



    private void Update()
    {
        if (!canPush && !isReturning)
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
