using NUnit.Framework;
using UnityEngine;
using System.Collections.Generic;


public class Optimization : MonoBehaviour
{
    public List<GameObject> floors;

    int currentLevel;

    public static Optimization Instance; // Singleton tasarýmý

    private void Awake()
    {
        Instance = this;
    }

    private void OnDestroy()
    {

       // CheckpointManager.OnLevelChanged -= LevelChanged;
    }
    private void Start()
    {
       // CheckpointManager.OnLevelChanged += LevelChanged;
    }

    public void UpdateFloors(int newLevel)
    {
        for(int i = 0; i < floors.Count; i++)
        {
            if (currentLevel == i)
            {
                floors[i].gameObject.SetActive(true);
            }
            else
            {
                floors[i].gameObject.SetActive(false);
            }
        }
    }

    private void LevelChanged(int currentLevel)
    {
        UpdateFloors(currentLevel);
    }
}
