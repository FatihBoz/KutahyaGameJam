using System.Collections;
using UnityEngine;

public class Smasher : MovingTrap
{
    private bool canSmash;


    private void Update()
    {
        if (!canSmash && !isReturning)
        {
            elapsedTimeAfterAction += Time.deltaTime;

            if (elapsedTimeAfterAction >= timeBetweenActions)
            {
                Vector3 targetPos = new(startPos.x, startPos.y - displacementAmount, startPos.z);

                StartCoroutine(ForwardMove(targetPos));

                elapsedTimeAfterAction = 0;
                return;
            }
        }
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

        StartCoroutine(ReturnToDefault(startPos));
    }


    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Destroy(collision.gameObject);
            //kan efekti belki
        }
    }

}
