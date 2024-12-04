using System.Collections.Generic;
using Unity.Cinemachine;
using UnityEngine;
using UnityEngine.Splines;

public class SplineChanger : MonoBehaviour
{
    public List<SplineContainer> gameObjectList;

    int level;

    private void Start()
    {
        level = CheckpointManager.Instance.currentLevel;
        Debug.Log("Current Level is  : " +  level);
    }

    void Update()
    {
        if(level != CheckpointManager.Instance.currentLevel)
        {
            level = CheckpointManager.Instance.currentLevel;
            ChangeSpline(gameObjectList[CheckpointManager.Instance.currentLevel]);
            Debug.Log("New Level is  : " + level);

        }

        /*
        if(CheckpointManager.Instance.currentLevel != null)
        {

        }
        */
    }

    void ChangeSpline(SplineContainer SplineContainer)
    {
        GetComponent<CinemachineSplineDolly>().Spline = SplineContainer;
    }
}
