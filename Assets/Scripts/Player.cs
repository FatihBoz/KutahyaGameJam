using System;
using UnityEngine;

public class Player : MonoBehaviour
{
    public static Player Instance;
    public Action<float> OnPlayerDied;

    private void Awake()
    {
        Instance = this;
    }


    public void PlayerDied(float delay = 0)
    {
        OnPlayerDied?.Invoke(delay);
        gameObject.SetActive(false);
    }
}
