using UnityEngine;

public class Spike : MovingTrap
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Player.Instance.PlayerDied();
            //kan efekti belki
        }
    }
}
