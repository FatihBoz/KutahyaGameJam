using System.Collections;
using UnityEngine;

public class MovingTrap : MonoBehaviour
{
    [SerializeField] protected float timeBetweenActions;
    [SerializeField] protected float actionDuration;
    [SerializeField] protected float displacementAmount;
    [SerializeField] protected float waitingTimeAfterPush;
    [SerializeField] protected float returnTimeMultiplier;


    protected float elapsedTimeAfterFirstMove = 0f;
    protected Vector3 startPos;
    protected bool idle;
    protected float elapsedTimeAfterAction = 0f;

    private void Start()
    {
        startPos = transform.position;
        idle = true;
    }

    protected virtual void Update()
    {
        if (idle)
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

    protected virtual IEnumerator ForwardMove(Vector3 targetPos)
    {

        idle = false;
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


    protected virtual IEnumerator ReturnToDefault(Vector3 targetPos)
    {
        float timeElapsed = 0f;
        float returnDuration = actionDuration * returnTimeMultiplier;

        while (timeElapsed < returnDuration)
        {
            transform.position = Vector3.Lerp(transform.position, targetPos, timeElapsed / returnDuration);
            timeElapsed += Time.deltaTime;
            yield return null;
        }

        idle = true;
    }


}
