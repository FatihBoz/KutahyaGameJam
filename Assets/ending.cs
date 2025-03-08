using UnityEngine;
using UnityEngine.SceneManagement;

public class ending : MonoBehaviour
{
    public GameObject kamera;
    private void Start()
    {
        kamera.SetActive(false);
    }
    private void Update()
    {
        if (Input.GetKey(KeyCode.M))
        {
            kamera.SetActive(true);
        }
        if(FinalPuzzle.gameFinished)
        {
            print("e bitmiþ bu");
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
            SceneManager.LoadScene("MainMenu");
        }
    }
}
