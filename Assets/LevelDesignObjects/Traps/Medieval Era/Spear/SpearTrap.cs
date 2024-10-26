using System.Collections;
using UnityEngine;

public class SpearTrap : MovingTrap
{
    [SerializeField] private float timeToTrigger = 1.0f;
    [SerializeField] private float resetDelay = 1.0f; // Geri dönüþ süresi

    private bool canDamage;
    private Coroutine activationCoroutine;

    protected override void Update()
    {
        return;
    }

    protected override IEnumerator ReturnToDefault(Vector3 targetPos)
    {
        yield return base.ReturnToDefault(targetPos);
        canDamage = false; // Tuzaðýn geri döndüðünde hasar verememesi için kapatýyoruz
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

    private void ResetTrap()
    {
        if (activationCoroutine != null)
        {
            //StopCoroutine(activationCoroutine);
            activationCoroutine = null;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && activationCoroutine == null && idle)
        {
            activationCoroutine = StartCoroutine(DelayedActivation());
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            ResetTrap(); // karakter çýktýðýnda tuzaðý sýfýrla
            canDamage = false;
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (canDamage && other.CompareTag("Player"))
        {
            Player.Instance.PlayerDied();
            ResetTrap(); // Karakter yok edildiðinde tuzaðý sýfýrla
        }
    }
}
