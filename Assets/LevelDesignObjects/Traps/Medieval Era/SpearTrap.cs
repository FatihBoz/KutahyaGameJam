using System.Collections;
using UnityEngine;

public class SpearTrap : MonoBehaviour
{

    [SerializeField] private float timeUntilTrapTriggered;
    [SerializeField] private GameObject spears;

    void ActivateTrap()
    {

    }



    private IEnumerator TriggerTrap(float timeToWait)
    {
        yield return new WaitForSeconds(timeToWait);

    }

    private void OnTriggerEnter(Collider other)
    {
        StartCoroutine(TriggerTrap(timeUntilTrapTriggered));
    }
}
