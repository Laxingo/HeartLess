using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class SceneManager : MonoBehaviour
{
    [SerializeField] GameObject mainCamera;
    [SerializeField] GameObject mainMenu;
    [SerializeField] GameObject jukebox;
    [SerializeField] GameObject pauseMenu;

    public void StartGame()
    {
        mainCamera.SetActive(true);
        mainMenu.SetActive(false);
        jukebox.SetActive(false);   
    }

    public void ExitGame()
    {
        Application.Quit(); 
        Debug.Log("Exit");
    }
    public void Resume()
    {
        Time.timeScale = 1f;
        pauseMenu.SetActive(false);
    }

    public void MainMenu()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene("MainMenu");
    } 
    public void Credits()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene("Creditos");
    }

    public void QuitGame()
    {
        Application.Quit();
        Debug.Log("Exit");
    }
}

