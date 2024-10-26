using System;
using UnityEngine;

public class Player : MonoBehaviour
{
    public static Player Instance;
    public Action OnPlayerDied;

    private void Awake()
    {
        Instance = this;
    }


    public void PlayerDied(float delay = 0)
    {
        OnPlayerDied?.Invoke();
        Destroy(gameObject, delay);
    }
}
