using System.Collections;
using UnityEngine;

public class Smasher : MovingTrap
{
    private bool canSmash;


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
        if (!canSmash)
        {
            return;
        }

        if (collision.gameObject.CompareTag("Player"))
        {
            Destroy(collision.gameObject);
            //kan efekti belki
        }
    }

}
