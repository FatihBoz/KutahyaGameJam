using System.Collections;
using UnityEngine;

public class SpearTrap : MonoBehaviour
{

    [SerializeField] private float timeUntilTrapTriggered;
    [SerializeField] private GameObject spears;
    [SerializeField] private float spearsMoveAmount;

    private Vector3 initialPos;
    private Vector3 targetPos;

    void ActivateTrap()
    {
        targetPos = new Vector3(transform.position.x,transform.position.y + spearsMoveAmount,transform.position.y);
        StartCoroutine(Move(initialPos,targetPos,1f,0f)); // spears to be appeared

        StartCoroutine(Move(targetPos, initialPos, 1f, 4f));
    }

    private IEnumerator Move(Vector3 startPos, Vector3 endPos, float duration,float delay)
    {
        yield return new WaitForSeconds(delay);

        float elapsedTime = 0f;

        while (elapsedTime < duration)
        {
            spears.transform.position = Vector3.Lerp(startPos, endPos, elapsedTime/duration);
            yield return null;
        }
    }

    private IEnumerator TriggerTrap(float timeToWait)
    {
        initialPos = transform.position;
        yield return new WaitForSeconds(timeToWait);
        ActivateTrap();

    }

    private void OnTriggerEnter(Collider other)
    {
        StartCoroutine(TriggerTrap(timeUntilTrapTriggered));
    }
}
