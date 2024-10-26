using UnityEngine;

public class Spike : MovingTrap
{


    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Destroy(other.gameObject);
            //kan efekti belki
        }
    }
}