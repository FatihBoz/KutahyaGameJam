using UnityEngine;
using UnityEngine.SceneManagement;

public class ending : MonoBehaviour
{
    public GameObject kamera;

    private void Update()
    {
        if (Input.GetKey(KeyCode.M))
        {
            kamera.SetActive(true);
        }
        if(FinalPuzzle.gameFinished)
        {
            SceneManager.LoadScene("MainMenu");
        }
    }
}
