using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using GoogleMobileAds.Api;

public class adManager : MonoBehaviour
{
    // string appId = "ca-app-pub-1419825942572265~7034611204";
    // string interstitialID = "ca-app-pub-3940256099942544/1033173712";
    string interstitialID = "ca-app-pub-1419825942572265/7836734599";
    InterstitialAd interstitial;
    public bool adClosed;
    // List<string> deviceIds = new List<string>();
    // Start is called before the first frame update
    void Start()
    {
        // Initialize the Google Mobile Ads SDK.
        MobileAds.Initialize(initStatus => { });
        
        // deviceIds.Add("070DC918DA7653E44A77BAA0543DFE12");

        // RequestConfiguration requestConfiguration = new RequestConfiguration.Builder().SetTestDeviceIds(deviceIds).build();

        // MobileAds.SetRequestConfiguration(requestConfiguration);

        RequestInterstitial();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void RequestInterstitial()
    {
        // Initialize an InterstitialAd.
        interstitial = new InterstitialAd(interstitialID);
        // hanle ad closing
        interstitial.OnAdClosed += HandleOnAdClosed;
        // Called when an ad request failed to load.
        interstitial.OnAdFailedToLoad += HandleOnAdFailedToLoad;
        
        // Create an empty ad request.
        AdRequest request = new AdRequest.Builder().AddTestDevice("070DC918DA7653E44A77BAA0543DFE12").Build();
        // Load the interstitial with the request.
        interstitial.LoadAd(request);
    }

    public void displayInterstitial()
    {
        if (interstitial.IsLoaded()) {
            interstitial.Show();
        }
    }

    public void HandleOnAdClosed(object sender, EventArgs args)
    {
        // MonoBehaviour.print("HandleAdClosed event received");
        adClosed = true;
    }

    public void HandleOnAdFailedToLoad(object sender, AdFailedToLoadEventArgs args)
    {
        // MonoBehaviour.print("HandleFailedToReceiveAd event received with message: " + args.Message);
        adClosed = true;
    }

    public void destroyInterStitial()
    {
        interstitial.Destroy();
    }

    
}
