using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Video;

public class VideoController : MonoBehaviour
{
    public VideoPlayer videoPlayer;
    public string nextSceneName;

    void Start()
    {
        // Video bitti�inde 'OnVideoEnd' metodunu �a��r
        videoPlayer.loopPointReached += OnVideoEnd;
    }

    // Video bitti�inde �al��acak fonksiyon
    void OnVideoEnd(VideoPlayer vp)
    {
        // Belirtilen sahneye ge�i� yap
        SceneManager.LoadScene(nextSceneName);
    }
}
