using UnityEngine.Events;
using UnityEngine;
using GoogleMobileAds.Api;
using GoogleMobileAds.Common;
using UnityEngine.UI;
using System;
using System.Collections.Generic;

public class AdManager : MonoBehaviour
{
    private int rewardAmount;
    private RewardedAd rewardedAd;
    public SystemController system;
    private string adUnitIdTest = "ca-app-pub-3940256099942544/5224354917";
    public void Start()
    {
        // Initialize the Google Mobile Ads SDK.
        MobileAds.Initialize(initStatus => { });

        rewardedAd = new RewardedAd(adUnitIdTest);

        // Create an empty ad request.
        AdRequest request = new AdRequest.Builder().Build();
        // Load the rewarded ad with the request.
        rewardedAd.LoadAd(request);
        rewardedAd.OnUserEarnedReward += HandleUserEarnedReward;
        rewardedAd.OnAdClosed += HandleRewardedAdClosed;

        //UserChoseToWatchAd();
    }

    public void PlayRewardedAd(int reward)
    {
        SetRewardAmount(reward);
        if (rewardedAd.IsLoaded()) {
            rewardedAd.Show();
        }
    }

    public void HandleRewardedAdClosed(object sender, EventArgs args)
    {
        print("HandleRewardedAdClosed event received" + args);
        // Create an empty ad request.
        AdRequest request = new AdRequest.Builder().Build();
        // Load the rewarded ad with the request.
        this.rewardedAd.LoadAd(request);

    }

    public void HandleUserEarnedReward(object sender, Reward args)
    {
        string type = args.Type;
        double amount = args.Amount;
        MonoBehaviour.print(
            "HandleRewardedAdRewarded event received for "
                + amount.ToString() + " " + type);
        print("real reward = " + rewardAmount);
        system.SetAdButtonActive(false);
    }

    private void SetRewardAmount(int reward)
    {
        rewardAmount = reward * 2;
    }
}
