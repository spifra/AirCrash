using GoogleMobileAds.Api;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class inGameMenu : MonoBehaviour
{
    [Tooltip("Panel on pause menu")]
    [SerializeField]
    private GameObject firstPanel;
    [Tooltip("Panel before ad")]
    [SerializeField]
    private GameObject secondPanel;
    [SerializeField]
    private GameObject pausePopup;
    [SerializeField]
    private GameObject gameoverPopup;

    private bool isAlreadyRewarded = false;

    private void Awake()
    {
        AdsManager.instance.inGameMenu = this;
        AdsManager.instance.bannerView.Destroy();
    }

    /// <summary>
    /// When player tap on pause
    /// </summary>
    public void OnPause()
    {
        Debug.Log("Pause");

        LevelManager.Instance.PauseGame();

        firstPanel.SetActive(true);
        pausePopup.SetActive(true);
    }

    /// <summary>
    /// When the player tap on Home
    /// </summary>

    public void OnHome()
    {
        secondPanel.SetActive(true);
        AdsManager.instance.RequestAndShowInterstitialOnHome();
    }


    /// <summary>
    /// When the player tap on restart
    /// </summary>

    public void OnRestart()
    {
        isAlreadyRewarded = false;
        secondPanel.SetActive(true);
        AdsManager.instance.RequestAndShowInterstitialOnRestart();
    }

    /// <summary>
    /// When the player close the popup
    /// </summary>
    public void OnResume()
    {
        Debug.Log("Resume");

        firstPanel.SetActive(false);
        pausePopup.SetActive(false);

        LevelManager.Instance.ResumeGame();
    }

    /// <summary>
    /// Called when the player died
    /// </summary>
    public void OnGameOver()
    {
        if (firstPanel != null && gameoverPopup != null)
        {
            firstPanel.SetActive(true);
            gameoverPopup.SetActive(true);
        }
        LevelManager.Instance.PauseGame();

        if (isAlreadyRewarded)
        {
            gameoverPopup.transform.Find("Continue").gameObject.SetActive(false);
        }

        gameoverPopup.transform.Find("DistanceText").GetComponent<Text>().text = "Score: " + LevelManager.Instance.score / 2;

    }

    /// <summary>
    /// When player tap to see the rewarded ad
    /// </summary>
    public void OnRewardedAd()
    {
        isAlreadyRewarded = true;
        firstPanel.SetActive(false);
        gameoverPopup.SetActive(false);
        secondPanel.SetActive(true);
        AdsManager.instance.RequestedAndShowRewardedAd();
    }

    public void SetPanel(bool isVisible)
    {
        if (secondPanel != null)
            secondPanel.SetActive(isVisible);
    }
}
