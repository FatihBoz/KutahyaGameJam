using System.Collections;
using UnityEngine;

public class Spike : MovingTrap
{
    bool isActive;
    private void Update()
    {
        if (isActive || isReturning) return;

        elapsedTimeAfterAction += Time.deltaTime;

        if (elapsedTimeAfterAction >= timeBetweenActions)
        {
            Vector3 targetPos = new(startPos.x, startPos.y + displacementAmount, startPos.z);
            StartCoroutine(ForwardMove(targetPos));

            elapsedTimeAfterAction = 0;
            return;
        }
    }

    protected override IEnumerator ForwardMove(Vector3 targetPos)
    {
        isActive = true;
        StartCoroutine( base.ForwardMove(targetPos));
        

        yield return null;
    }

    protected override IEnumerator ReturnToDefault(Vector3 targetPos)
    {
        isActive = false;
        return base.ReturnToDefault(targetPos);
    }



    void OnTriggerStay(Collider other)
    {
        if (!isActive) return;

        if (other.CompareTag("Player"))
        {
            Destroy(other.gameObject);
            //Kan efekti belki
        }
    }

}
