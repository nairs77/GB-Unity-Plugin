using UnityEngine;
using System.Collections;
using GoogleMobileAds.Api;
using System;

public class GBAdManager : MonoBehaviour
{
    private static GBAdManager _instance = null;

    public static GBAdManager Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = FindObjectOfType<GBAdManager>();
                if (_instance == null)
                {
                    GameObject obj = new GameObject();
                    obj.name = "GBAdManager";
                    _instance = obj.AddComponent<GBAdManager>();
                    DontDestroyOnLoad(obj);
                }
            }
            return _instance;
        }
    }

    private string mUnitAdId;
    private RewardBasedVideoAd rewardbasedVideo = RewardBasedVideoAd.Instance; 

    public delegate void onRewardVideoAdComplete();
    public onRewardVideoAdComplete OnRewardComplete = null;
    bool isInit = false;

    bool isRewarded = false;
    
    Coroutine indicate = null;

    IEnumerator ShowIndicate()
    {
        //PopupManager.ShowWaitLockPacket();

        yield return new WaitForSeconds(2.0f * 60.0f);      // 3분간 응답이 없으면 자동 꺼줌.        

        //PopupManager.HideWaitLockPacket();
    }
    
    void HideIndicate()
    {
        //PopupManager.HideWaitLockPacket();
        if(indicate != null)
        {
            StopCoroutine(indicate);
            indicate = null;
        }
    }

    public void Init(string adUnitId, onRewardVideoAdComplete callback) {
        if (isInit == false) {
            mUnitAdId = adUnitId;

            // Ad event fired when the rewarded video ad
            // has been received.
            rewardbasedVideo.OnAdLoaded += HandleRewardBasedVideoLoaded;
            // has failed to load.
            rewardbasedVideo.OnAdFailedToLoad += HandleRewardBasedVideoFailedToLoad;
            // is opened.
            rewardbasedVideo.OnAdOpening += HandleRewardBasedVideoOpened;
            // has started playing.
            rewardbasedVideo.OnAdStarted += HandleRewardBasedVideoStarted;
            // has rewarded the user.
            rewardbasedVideo.OnAdRewarded += HandleRewardBasedVideoRewarded;
            // is closed.
            rewardbasedVideo.OnAdClosed += HandleRewardBasedVideoClosed;
            // is leaving the application.
            rewardbasedVideo.OnAdLeavingApplication += HandleRewardBasedVideoLeftApplication;
            OnRewardComplete = callback;

            AdRequest request = new AdRequest.Builder().Build();
            rewardbasedVideo.LoadAd(request, adUnitId);                    
            isInit = true;
        }
    }

    private void RequestAd() {
        AdRequest request = new AdRequest.Builder().Build();
        rewardbasedVideo.LoadAd(request, mUnitAdId);        
    }
/* 
    public void Init(string adUnitId)
    {
        if(isInit == false)
        {
            mUnitAdId = adUnitId;

            // Ad event fired when the rewarded video ad
            // has been received.
            rewardbasedVideo.OnAdLoaded += HandleRewardBasedVideoLoaded;
            // has failed to load.
            rewardbasedVideo.OnAdFailedToLoad += HandleRewardBasedVideoFailedToLoad;
            // is opened.
            rewardbasedVideo.OnAdOpening += HandleRewardBasedVideoOpened;
            // has started playing.
            rewardbasedVideo.OnAdStarted += HandleRewardBasedVideoStarted;
            // has rewarded the user.
            rewardbasedVideo.OnAdRewarded += HandleRewardBasedVideoRewarded;
            // is closed.
            rewardbasedVideo.OnAdClosed += HandleRewardBasedVideoClosed;
            // is leaving the application.
            rewardbasedVideo.OnAdLeavingApplication += HandleRewardBasedVideoLeftApplication;
            isInit = true;
        }
    }

    private void RequestAd(string adUnitId)
    {
        //GBLog.verbose("[AdManager::RequestAd] adUnitId : " + adUnitId);
        print("[AdManager::RequestAd] adUnitId : " + adUnitId);
        
        AdRequest request = new AdRequest.Builder().Build();
        rewardbasedVideo.LoadAd(request, adUnitId);        
    }

    public void LoadAd(onRewardVideoAdComplete callback)
    {        
        if (isInit == true)
        {
            GBLog.verbose("[AdManager::LoadAd] Call!!!");
            OnRewardComplete = callback;
            RequestAd(mUnitAdId);
        }
    }
*/
    public void ShowAd()
    {              
        if(indicate == null)
        {
            indicate = StartCoroutine(ShowIndicate());
        }        

        if (rewardbasedVideo.IsLoaded())
        {
            rewardbasedVideo.Show();            
        }
        else
        {
            GBLog.verbose("Not Load...So Load Ad!");            
            //LoadAd(_adType, OnReward_Ad_complete);                         
        }

        // if (OnRewardComplete != null)
        // {
        //     OnRewardComplete(_adType);
        //     HideIndicate();
        // }            
    }
    
    void WaitRewardAd()
    {
        HideIndicate();

        // if(isRewarded == true)
        // {
        //     // 보상이 Close 콜백 이후 뒤늦게 들어 옴.            
        //     if (OnRewardComplete != null)
        //         OnRewardComplete(_adType);

        //     isRewarded = false;
        // }
        // else
        // {
        //     //PopupManager.ShowToast(StringManager.Instance().GetString("Advertising_Change_Toast"));            
        // }
        // _adType = ADTYPE.Max;
    }

    void HandleRewardBasedVideoLoaded(object sender, EventArgs args)
    {
        GBLog.verbose("[HandleRewardBasedVideoLoaded]");

        HideIndicate(); 
    }

    void HandleRewardBasedVideoFailedToLoad(object sender, AdFailedToLoadEventArgs args)
    {
        isRewarded = false;

        HideIndicate();
     
        GBLog.verbose("[HandleRewardBasedVideoFailedToLoad] : " + args.Message);

        RequestAd();
    }

    void HandleRewardBasedVideoOpened(object sender, EventArgs args)
    {
        isRewarded = false;     

        //NetworkProcess.Instance().Send_CG_DELAYED_PINGPONG();        

        GBLog.verbose("[HandleRewardBasedVideoOpened]");       
    }

    void HandleRewardBasedVideoStarted(object sender, EventArgs args)
    {
        isRewarded = false;

        GBLog.verbose("[HandleRewardBasedVideoStarted]");
    }

    void HandleRewardBasedVideoRewarded(object sender, Reward args)
    {
        isRewarded = true;

        GBLog.verbose("[HandleRewardBasedVideoRewarded] : Reward Type : " + args.Type + " Amount : " + args.Amount);        
    }
    
    void HandleRewardBasedVideoClosed(object sender, EventArgs args)
    {
        GBLog.verbose("[HandleRewardBasedVideoClosed]");
        
        //NetworkProcess.Instance().Send_CG_DELAYED_PINGPONG();        

        if(isRewarded == true)
        {
            HideIndicate();

            if(OnRewardComplete != null)
                OnRewardComplete();

            RequestAd();
            isRewarded = false;
        }
        else
        {
            Invoke("WaitRewardAd", 3.0f);
        }        
    }
    void HandleRewardBasedVideoLeftApplication(object sender, EventArgs args)
    {
        if (OnRewardComplete != null)
             OnRewardComplete();

        isRewarded = false;

        HideIndicate();

        GBLog.verbose("[HandleRewardBasedVideoLeftApplication]");
    }

/*
    public void OnReward_Ad_complete(ADTYPE type)
    {
        GBLog.verbose("[AdManager::OnShow_ad_complete] type : " + type);
        switch(type)
        {
            case ADTYPE.CHARACTER:
                //NetworkProcess.Instance().Send_CG_USE_ADVERTISING(ADVERTISING_TYPE.ADVERTISING_TYPE_CHARACTER);    
                break;
            case ADTYPE.DAILY:
                //NetworkProcess.Instance().Send_CG_USE_ADVERTISING(ADVERTISING_TYPE.ADVERTISING_TYPE_DAILY);    
                break;
            case ADTYPE.GOODS:
                //NetworkProcess.Instance().Send_CG_USE_ADVERTISING(ADVERTISING_TYPE.ADVERTISING_TYPE_GOODS);    
                break;
            case ADTYPE.JOY:
                //NetworkProcess.Instance().Send_CG_FREE_CHARGE();
                break;
            case ADTYPE.SKILLCARD:
                //NetworkProcess.Instance().Send_CG_USE_ADVERTISING(ADVERTISING_TYPE.ADVERTISING_TYPE_SKILLCARD);    
                break;                
            case ADTYPE.TARGET:
                //NetworkProcess.Instance().Send_CG_USE_ADVERTISING(ADVERTISING_TYPE.ADVERTISING_TYPE_TARGET);
                //GameGlobal.Instance().Show_Ad_Time = TimeManager.GetCurrntUTCTime().ToString();
                //GameGlobal.Instance().Limit_Show_Target_AD_Count += 1;
                break;
        }       
    }    
*/    
}
