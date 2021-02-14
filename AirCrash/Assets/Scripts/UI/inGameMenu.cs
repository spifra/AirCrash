using GoogleMobileAds.Api;
using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class inGameMenu : MonoBehaviour
{
    [SerializeField]
    private GameObject panel;
    [SerializeField]
    private GameObject pausePopup;
    [SerializeField]
    private GameObject gameoverPopup;

    private bool isAlreadyRewarded = false;

    private void Awake()
    {
        AdsManager.Instance.inGameMenu = this;
    }

    /// <summary>
    /// When player tap on pause
    /// </summary>
    public void OnPause()
    {
        Debug.Log("Pause");

        LevelManager.Instance.PauseGame();

        panel.SetActive(true);
        pausePopup.SetActive(true);
    }

    /// <summary>
    /// When the player tap on Home
    /// </summary>

    public void OnHome()
    {
        Debug.Log("Home");

        SceneManager.LoadScene("Menu");
    }

    /// <summary>
    /// When the player tap on restart
    /// </summary>

    public void OnRestart()
    {
        isAlreadyRewarded = false;

        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    /// <summary>
    /// When the player close the popup
    /// </summary>
    public void OnResume()
    {
        Debug.Log("Resume");

        panel.SetActive(false);
        pausePopup.SetActive(false);

        LevelManager.Instance.ResumeGame();
    }

    /// <summary>
    /// Called when the player died
    /// </summary>
    public void OnGameOver()
    {
        LevelManager.Instance.PauseGame();

        panel.SetActive(true);
        gameoverPopup.SetActive(true);

        if (isAlreadyRewarded)
        {
            gameoverPopup.transform.Find("Continue").gameObject.SetActive(false);
        }

        gameoverPopup.transform.Find("DistanceText").GetComponent<TMP_Text>().text = "Score: " + LevelManager.Instance.score / 2;

    }

    /// <summary>
    /// When player tap to see the rewarded ad
    /// </summary>
    public void OnRewardedAd()
    {
        isAlreadyRewarded = true;
        panel.SetActive(false);
        gameoverPopup.SetActive(false);
        AdsManager.Instance.RequestedAndShowRewardedAd();
    }

    /// <summary>
    /// Called by AdsManager after ads on death
    /// </summary>

    internal void RestartGame(object sender, EventArgs e)
    {
        OnGameOver();
    }

}
