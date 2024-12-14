using System.Collections;
using UnityEngine;

public class SpearTrap : MovingTrap
{
    [SerializeField] private float timeToTrigger = 1.0f;
    private bool canDamage;

    protected override void Update() { }

    protected override IEnumerator ReturnToDefault()
    {
        yield return base.ReturnToDefault();
        canDamage = false;
    }


    private IEnumerator DelayedActivation()
    {
        yield return new WaitForSeconds(timeToTrigger);
        ActivateTrap();
    }


    private void ActivateTrap()
    {
        StartCoroutine(ForwardMove());
        canDamage = true;
    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && idle)
        {
            StartCoroutine(DelayedActivation());
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (canDamage && other.CompareTag("Player"))
        {
            Player.Instance.PlayerDied();
        }
    }
}
