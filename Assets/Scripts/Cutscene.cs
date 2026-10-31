using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class Cutscene : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
         
        var videoPlayer = this.GetComponent<UnityEngine.Video.VideoPlayer>();
        videoPlayer.renderMode = UnityEngine.Video.VideoRenderMode.CameraNearPlane;
        videoPlayer.loopPointReached += EndReached;

    }
    void EndReached(UnityEngine.Video.VideoPlayer vp)
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene("Jogo");
    }
    // Update is called once per frame
    void Update()
    {
    }
}
