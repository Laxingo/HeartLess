using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class CutsceneFinal : MonoBehaviour
{
    [SerializeField] GameObject mainCamera;
    [SerializeField] GameObject canvas;
    [SerializeField] GameObject player;
    [SerializeField] GameObject jukebox;
    // Start is called before the first frame update
    void Start()
    {
        var videoPlayer = this.GetComponent<UnityEngine.Video.VideoPlayer>();
        videoPlayer.renderMode = UnityEngine.Video.VideoRenderMode.CameraNearPlane;
        videoPlayer.loopPointReached += EndReached;
        player.SetActive(false);
        jukebox.SetActive(false);

    }
    void EndReached(UnityEngine.Video.VideoPlayer vp)
    {
       // mainCamera.SetActive(true);
        //canvas.SetActive(true);
        //gameObject.SetActive(false);
        UnityEngine.SceneManagement.SceneManager.LoadScene("Creditos");

    }
    // Update is called once per frame
    void Update()
    {
    }
}