using UnityEngine;
using UnityEngine.SceneManagement;
public class MainMenus : MonoBehaviour
{
   void Play()
   {
        SceneManager.LoadScene(1);
   }

   
   void Quit()
   {
    Application.Quit();
   }
}
