using System.Collections;
using UnityEngine;

public class SpearTrap : MovingTrap
{
    [SerializeField] private float timeToTrigger = 1.0f;

    private bool canDamage;
    private Coroutine activationCoroutine;

    protected override void Update()
    {
        return;
    }

    protected override IEnumerator ReturnToDefault(Vector3 targetPos)
    {
        yield return base.ReturnToDefault(targetPos);
        canDamage = false;
    }

    private void ActivateTrap()
    {
        
        Vector3 targetPos = new(transform.position.x, transform.position.y + displacementAmount, transform.position.z);
        StartCoroutine(ForwardMove(targetPos));
        canDamage = true;
    }

    private IEnumerator DelayedActivation()
    {
        yield return new WaitForSeconds(timeToTrigger);
        ActivateTrap();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && activationCoroutine == null)
        {
            activationCoroutine = StartCoroutine(DelayedActivation());
            print("enter");
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            print("exit");
            ResetTrap();
        }
    }

    private void ResetTrap()
    {
        canDamage = false;
        if (activationCoroutine != null)
        {
            StopCoroutine(activationCoroutine);
            activationCoroutine = null;
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (canDamage && other.CompareTag("Player"))
        {
            Player.Instance.PlayerDied();
            ResetTrap();
        }
    }
}
