using System.Collections;
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
    private void SpawnPlayer(float delay)
    {
       StartCoroutine(spawn(delay));
    }
    private IEnumerator spawn(float delay)
    {
        Player.Instance.OnPlayerDied -= SpawnPlayer;
        yield return new WaitForSeconds(delay);
         Debug.Log("spawnlandi");
        GameObject player = Instantiate(playerPrefab);
        player.transform.position = checkpointLoc;
        Player.Instance.OnPlayerDied += SpawnPlayer;
    }

}   
