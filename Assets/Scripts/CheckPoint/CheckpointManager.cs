using UnityEngine;

public class CheckpointManager : MonoBehaviour
{
    public GameObject playerPrefab;
    public static CheckpointManager Instance; 
    private Vector3 checkpointLoc;
    private bool hasCheckpoint = false;
    
    private void Awake() {
        Instance=this;
    }
    private void Start()
    {
        checkpointLoc = transform.position;
        Player.Instance.OnPlayerDied += SpawnPlayer;
    }
    public void SetCheckpoint(Vector3 position)
    {
        checkpointLoc=position;
        Debug.Log("Checkpoint "+checkpointLoc+" locaited");
    }
    public Vector3 GetCheckpointLocation()
    {
        return checkpointLoc;
    }
    private void OnDisable() {
        Player.Instance.OnPlayerDied-=SpawnPlayer;
    }
    private void SpawnPlayer()
    {
        Debug.Log("spawnlandi");
        Player.Instance.OnPlayerDied -= SpawnPlayer;
        GameObject player = Instantiate(playerPrefab);
        player.transform.position = checkpointLoc;
        Player.Instance.OnPlayerDied += SpawnPlayer;

    }


}   
