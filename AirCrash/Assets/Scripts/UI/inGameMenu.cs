using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class inGameMenu : MonoBehaviour
{
    [SerializeField]
    private GameObject panel;
    [SerializeField]
    private GameObject pausePopup;

    private Player player;

    private void Awake()
    {
        player = FindObjectOfType<Player>();
        Time.timeScale = 1;
    }

    public void OnPause()
    {
        Debug.Log("Pause");

        player.isPaused = true;
        Time.timeScale = 0;

        panel.SetActive(true);
        pausePopup.SetActive(true);
    }

    public void OnHome()
    {
        Debug.Log("Home");

        SceneManager.LoadScene("Menu");
    }

    public void OnRestart()
    {
        Debug.Log("Restart");

        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void OnResume()
    {
        Debug.Log("Resume");

        panel.SetActive(false);
        pausePopup.SetActive(false);

        player.isPaused = false;
        Time.timeScale = 1;
    }
}
