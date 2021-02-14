using GoogleMobileAds.Api;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class AdsManager : MonoBehaviour
{
    #region Singleton
    private static AdsManager instance;
    public static AdsManager Instance
    {
        get
        {
            if (instance != null)
            {
                return instance;
            }
            else
            {
                return null;
            }
        }
    }
    #endregion

    private BannerView bannerView;
    public InterstitialAd interstitial;
    private RewardedAd rewardedAd;

    //private string adId = "ca-app-pub-2811790606724562~1770905146";

    private string TESTBannerID = "ca-app-pub-3940256099942544/6300978111";
    private string TESTInterstitialID = "ca-app-pub-3940256099942544/1033173712";
    private string TESTRewardedID = "ca-app-pub-3940256099942544/1033173713";

    //private string myPhoneID = "bfbed49a-fcce-408f-ab2a-f2b49167d2fc";

    [HideInInspector]
    public inGameMenu inGameMenu;

    private void Awake()
    {
        instance = this;

        DontDestroyOnLoad(gameObject);
    }


    //TODO: target ads for children

    private void Start()
    {

        // Initialize the Google Mobile Ads SDK.
        MobileAds.Initialize(initStatus => { });

    }

    /// <summary>
    /// Banner
    /// </summary>
    public void RequestAndShowBanner()
    {
        //TODO: Change to adId;
        string adUnitId = TESTBannerID;

        // Clean up banner before reusing
        if (bannerView != null)
        {
            bannerView.Destroy();
        }

        // Create a banner at the top of the screen.
        bannerView = new BannerView(adUnitId, AdSize.GetLandscapeAnchoredAdaptiveBannerAdSizeWithWidth(Screen.width), AdPosition.Top);

        // Create an empty ad request.
        AdRequest request = new AdRequest.Builder().Build();

        // Load the banner with the request.
        bannerView.LoadAd(request);
    }

    #region INTERSTITIAL
    /// <summary>
    /// Load Interstitial
    /// </summary>
    public void RequestInterstitial()
    {
        //TODO: Change to adId;
        string adUnitId = TESTInterstitialID;

        // Clean up banner before reusing
        if (interstitial != null)
        {
            interstitial.Destroy();
        }

        // Initialize an InterstitialAd.
        interstitial = new InterstitialAd(adUnitId);

        // Create an empty ad request.
        AdRequest request = new AdRequest.Builder().Build();
        // Load the interstitial with the request.
        interstitial.LoadAd(request);
    }

    /// <summary>
    /// Show interstitial
    /// </summary>
    public void ShowIntestitial()
    {
        if (interstitial.IsLoaded())
        {
            interstitial.Show();
        }
    }
    #endregion

    #region REWARDED AD

    public void RequesteRewardedAd()
    {
        //TODO: Change to adId;
        string adUnitId = TESTRewardedID;

        rewardedAd = new RewardedAd(adUnitId);

        // Create an empty ad request.
        AdRequest request = new AdRequest.Builder().Build();
        // Load the rewarded ad with the request.
        rewardedAd.LoadAd(request);
    }

    public void UserChoseToWatchAd()
    {
        if (rewardedAd.IsLoaded())
        {
            rewardedAd.Show();
        }
    }

    public void HandleRewardedAdLoaded(object sender, EventArgs args)
    {
        MonoBehaviour.print("HandleRewardedAdLoaded event received");
    }

    public void HandleRewardedAdFailedToLoad(object sender, AdErrorEventArgs args)
    {
        MonoBehaviour.print(
            "HandleRewardedAdFailedToLoad event received with message: "
                             + args.Message);
    }

    public void HandleRewardedAdOpening(object sender, EventArgs args)
    {
        MonoBehaviour.print("HandleRewardedAdOpening event received");
    }

    public void HandleRewardedAdFailedToShow(object sender, AdErrorEventArgs args)
    {
        MonoBehaviour.print(
            "HandleRewardedAdFailedToShow event received with message: "
                             + args.Message);
    }

    public void HandleRewardedAdClosed(object sender, EventArgs args)
    {
        MonoBehaviour.print("HandleRewardedAdClosed event received");
    }

    public void HandleUserEarnedReward(object sender, Reward args)
    {
        string type = args.Type;
        double amount = args.Amount;
        MonoBehaviour.print(
            "HandleRewardedAdRewarded event received for "
                        + amount.ToString() + " " + type);
    }

    #endregion

    #region EVENTS
    public void HandleOnAdLoaded(object sender, EventArgs args)
    {
        MonoBehaviour.print("HandleAdLoaded event received");
    }

    public void HandleOnAdFailedToLoad(object sender, AdFailedToLoadEventArgs args)
    {
        MonoBehaviour.print("HandleFailedToReceiveAd event received with message: " + args.Message);
    }

    public void HandleOnAdOpened(object sender, EventArgs args)
    {
        MonoBehaviour.print("HandleAdOpened event received");
    }

    public void HandleOnAdClosed(object sender, EventArgs args)
    {
        MonoBehaviour.print("HandleAdClosed event received");
    }

    public void HandleOnAdLeavingApplication(object sender, EventArgs args)
    {
        MonoBehaviour.print("HandleAdLeavingApplication event received");
    }

    #endregion

    /// <summary>
    /// First ad and banner in the main menu
    /// </summary>
    public void RequestAndShowFirstInterstitialAndBanner()
    {
        RequestInterstitial();

        interstitial.OnAdClosed += CallBanner;

        ShowIntestitial();
    }

    private void CallBanner(object sender, EventArgs e)
    {
        RequestAndShowBanner();
    }

    /// <summary>
    /// Game over Ad
    /// </summary>
    public void RequestAndShowInterstitialOnGameOver()
    {
        RequestInterstitial();
        interstitial.OnAdClosed += inGameMenu.RestartGame;
        ShowIntestitial();
    }

    public void RequestedAndShowRewardedAd()
    {
        RequesteRewardedAd();
        rewardedAd.OnUserEarnedReward += LevelManager.Instance.RespawnPlayer;
        UserChoseToWatchAd();
    }

}
