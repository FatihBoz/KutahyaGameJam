using System.Collections;
using UnityEngine;

public class CheckpointManager : MonoBehaviour
{



    public GameObject playerPrefab;
    public static CheckpointManager Instance; 
    private Vector3 checkpointLoc;
    
	[SerializeField]
	public GameObject[] traps;

    [SerializeField]
    private int maxLevel=4;
    private int currentLevel;

    [SerializeField]
    private GameObject boundary;
    
    private void Awake() {
        Instance=this;
    }
    private void Start()
    {
        currentLevel=0;
        checkpointLoc = transform.position;
        Player.Instance.OnPlayerDied += SpawnPlayer;
    }
    public void SetCheckpoint(Vector3 position,int checkpointLevel)
    {
	foreach(var item in traps)
	{
	item.SetActive(false);
	}
	if(traps.Length>checkpointLevel)
	{
	traps[checkpointLevel].SetActive(true);
	}
        currentLevel=checkpointLevel;
        if (currentLevel==maxLevel)
        {
            Destroy(boundary);
        }
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
        playerPrefab.transform.position = checkpointLoc;
        playerPrefab.SetActive(true);
        Player.Instance.OnPlayerDied += SpawnPlayer;
    }

}   
