using System.Collections;
using UnityEngine;

public class DoubleLever : Lever
{
    [SerializeField] private DoubleLever otherLever;
    [SerializeField] private float timeToCancel;
    [SerializeField] private float moveAmount;

    float elapsedTime = 0f;


    private void Start()
    {
        StartCoroutine(Rotate(objectToMove));
    }
    private void Update()
    {
        if (isPulled)
        {
            elapsedTime = Time.deltaTime;

            if (elapsedTime >= timeToCancel)
            {

            }
            else if (otherLever.isPulled && isPulled)
            {
                StartCoroutine(Rotate(objectToMove));

            }
        }
    }

    protected override IEnumerator Rotate(Transform t)
    {
        float elapsedTime = 0f;
        float duration = 1.5f;

        Vector3 startPos = objectToMove.position;
        Vector3 endPos = transform.up * moveAmount;

        while (elapsedTime < duration)
        {
            elapsedTime += Time.deltaTime;
            Vector3.Lerp(startPos, endPos, elapsedTime / duration);
            yield return null;
        }
        isPulled = !isPulled;
        animator.SetBool("Pull", isPulled);
    }

}
