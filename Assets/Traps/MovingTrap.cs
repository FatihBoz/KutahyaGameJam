using System.Collections;
using UnityEngine;

public class MovingTrap : MonoBehaviour
{
    [SerializeField] protected float timeBetweenForwardBackwardMoves;
    [SerializeField] protected float forwardMoveDuration;
    [SerializeField] protected float displacementAmount;
    [SerializeField] protected float waitingTimeAfterForwardMove;


    protected float elapsedTimeAfterFirstMove = 0f;
    protected Vector3 startPos;

    private void Start()
    {
        startPos = transform.position;
    }

    protected virtual IEnumerator ForwardMove(Vector3 targetPos)
    {
        float timeElapsed = 0f;


        while (timeElapsed < forwardMoveDuration)
        {
            transform.position = Vector3.Lerp(startPos, targetPos, timeElapsed / forwardMoveDuration);
            timeElapsed += Time.deltaTime;

            yield return null;
        }

        StartCoroutine(ReturnToDefault(startPos));
    }


    protected virtual IEnumerator ReturnToDefault(Vector3 targetPos)
    {
        yield return new WaitForSeconds(waitingTimeAfterForwardMove);

        float timeElapsed = 0f;
        float returnDuration = forwardMoveDuration * 6;

        while (timeElapsed < returnDuration)
        {
            transform.position = Vector3.Lerp(transform.position, targetPos, timeElapsed / returnDuration);
            timeElapsed += Time.deltaTime;
            yield return null;
        }

    }


}
