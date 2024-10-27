using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Video;

public class VideoController : MonoBehaviour
{
    public VideoPlayer videoPlayer;
    public string nextSceneName;

    void Start()
    {
        // Video bittiðinde 'OnVideoEnd' metodunu çaðýr
        videoPlayer.loopPointReached += OnVideoEnd;
    }

    // Video bittiðinde çalýþacak fonksiyon
    void OnVideoEnd(VideoPlayer vp)
    {
        // Belirtilen sahneye geçiþ yap
        SceneManager.LoadScene(nextSceneName);
    }
}
