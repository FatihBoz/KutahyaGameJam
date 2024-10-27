using UnityEngine;

public class CheckpointTrigger : MonoBehaviour
{
    [SerializeField]
    private int checkpointLevel=0;
    /// <summary>
    /// OnTriggerEnter is called when the Collider other enters the trigger.
    /// </summary>
    /// <param name="other">The other Collider involved in this collision.</param>
    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            CheckpointManager.Instance.SetCheckpoint(transform.position,checkpointLevel);
        }
    }
}
