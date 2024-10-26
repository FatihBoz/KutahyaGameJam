using UnityEngine;

public class CheckpointManager : MonoBehaviour
{
    public static CheckpointManager Instance; 
    private Vector3 checkpointLoc;
    private bool hasCheckpoint = false;
    
    private void Awake() {
        Instance=this;
    }
    private void Start()
    {
        checkpointLoc = transform.position;
    }
    public void SetCheckpoint(Vector3 position)
    {
        checkpointLoc=position;
    }

    public Vector3 GetCheckpointLocation()
    {
        return checkpointLoc;
    }

}   
