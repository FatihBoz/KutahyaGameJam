using UnityEngine;

public class AutoAnimatedTrap : MonoBehaviour
{
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Player.Instance.PlayerDied();
        }
    }
}
