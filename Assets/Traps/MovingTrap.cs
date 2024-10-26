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
    protected bool isReturning;
    protected float elapsedTimeAfterAction = 0f;

    private void Start()
    {
        startPos = transform.position;
    }

    protected virtual IEnumerator ForwardMove(Vector3 targetPos)
    {
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

        isReturning = true;
        float timeElapsed = 0f;
        float returnDuration = actionDuration * returnTimeMultiplier;

        while (timeElapsed < returnDuration)
        {
            transform.position = Vector3.Lerp(transform.position, targetPos, timeElapsed / returnDuration);
            timeElapsed += Time.deltaTime;
            yield return null;
        }
        isReturning = false;

    }


}
