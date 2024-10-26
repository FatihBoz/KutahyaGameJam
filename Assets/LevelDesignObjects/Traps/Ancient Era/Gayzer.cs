using UnityEngine;

public class Gayzer : MonoBehaviour
{
    [SerializeField] private GameObject waterDropPrefab;
    [SerializeField] private Transform pointToShoot;

    [Header("Float Values")]
    [SerializeField] private float pushForce;
    [SerializeField] private float timeBetweenAttacks;
    [SerializeField] private float prefabSelfDestroyTime;

    private float elapsedTimeAfterAttack = 0f;

    


    private void Update()
    {
        elapsedTimeAfterAttack += Time.deltaTime;

        while (elapsedTimeAfterAttack >= timeBetweenAttacks)
        {
            elapsedTimeAfterAttack = 0;

            GameObject obj = Instantiate(waterDropPrefab, pointToShoot.position, waterDropPrefab.transform.rotation);

            if (obj.TryGetComponent<Rigidbody>(out var rb))
            {
                rb.linearVelocity = pushForce * Vector3.up;
                Destroy(obj, prefabSelfDestroyTime);
            }
        }
    }

}
