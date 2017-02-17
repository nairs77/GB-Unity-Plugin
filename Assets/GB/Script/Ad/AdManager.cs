using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using GoogleMobileAds.Api;
using System;

public class AdManager : MonoBehaviour
{
    private static AdManager _instance = null;

    public static AdManager Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = FindObjectOfType<AdManager>();
                if (_instance == null)
                {
                    GameObject obj = new GameObject();
                    obj.name = "AdManager";
                    _instance = obj.AddComponent<AdManager>();
                    DontDestroyOnLoad(obj);
                }
            }
            return _instance;
        }
    }

    private Dictionary<int, string> adUnitIds = new Dictionary<int, string>();
#if USE_AD
    private RewardBasedVideoAd rewardbasedVideo = RewardBasedVideoAd.Instance; 
#endif
    public enum ADTYPE
    {
        JOY,
        SKILLCARD,
        CHARACTER,
        DAILY,
        GOODS,
        TARGET,

       // VUNGLE,
       // UNITY_AD,
       // APPLOVIN,
        
        Max,
    }

    public delegate void onRewardVideoAdComplete(ADTYPE type);
    public onRewardVideoAdComplete OnRewardComplete = null;
    bool isInit = false;
    ADTYPE _adType = ADTYPE.Max;
    bool isRewarded = false;
    
    Coroutine indicate = null;

    IEnumerator ShowIndicate()
    {
        PopupManager.ShowWaitLockPacket();

        yield return new WaitForSeconds(2.0f * 60.0f);      // 3분간 응답이 없으면 자동 꺼줌.        

        PopupManager.HideWaitLockPacket();
    }
    
    void HideIndicate()
    {
        PopupManager.HideWaitLockPacket();
        if(indicate != null)
        {
            StopCoroutine(indicate);
            indicate = null;
        }
    }

    public void Init()
    {
        if(isInit == false)
        {
            GodLib.MLog.Log("Setting Ad"); 
#if UNITY_EDITOR
            adUnitIds.Add((int)ADTYPE.JOY, "unused");
            adUnitIds.Add((int)ADTYPE.SKILLCARD, "unused");
            adUnitIds.Add((int)ADTYPE.CHARACTER, "unused");
            adUnitIds.Add((int)ADTYPE.DAILY, "unused");
            adUnitIds.Add((int)ADTYPE.GOODS, "unused");
            adUnitIds.Add((int)ADTYPE.TARGET, "unused");
            
#elif UNITY_ANDROID && USE_AD // 중국 빌드가 아닌 경우에만 등록.           
            AppLovin.InitializeSdk();

            adUnitIds.Add((int)ADTYPE.JOY, "ca-app-pub-6040866524746900/2524870470");
            adUnitIds.Add((int)ADTYPE.SKILLCARD, "ca-app-pub-6040866524746900/4141204472");
            adUnitIds.Add((int)ADTYPE.CHARACTER, "ca-app-pub-6040866524746900/1187738074");
            adUnitIds.Add((int)ADTYPE.DAILY, "ca-app-pub-6040866524746900/8711004872");
            adUnitIds.Add((int)ADTYPE.GOODS, "ca-app-pub-6040866524746900/8571404070");
            adUnitIds.Add((int)ADTYPE.TARGET, "ca-app-pub-6040866524746900/8711004872");    // 타켓 광고는 데일리와 같은 키 사용.

          //  adUnitIds.Add((int)ADTYPE.VUNGLE, "ca-app-pub-6040866524746900/3616703670");
          //  adUnitIds.Add((int)ADTYPE.UNITY_AD, "ca-app-pub-6040866524746900/5093436871");
          //  adUnitIds.Add((int)ADTYPE.APPLOVIN, "ca-app-pub-6040866524746900/9354534874");
#elif UNITY_IPHONE
            AppLovin.SetSdkKey("RMOJTDsRIt8RHfWb2xznOgJie0ul6LaXh-rFBhhTZrwrURQWNNHm3aSibLRhh1DO_3eBmKfJ9Z9e0Z9NyR701a");
            AppLovin.InitializeSdk();

            adUnitIds.Add((int)ADTYPE.JOY, "ca-app-pub-6040866524746900/5478336870");
            adUnitIds.Add((int)ADTYPE.SKILLCARD, "ca-app-pub-6040866524746900/5617937671");
            adUnitIds.Add((int)ADTYPE.CHARACTER, "ca-app-pub-6040866524746900/2664471274");
            adUnitIds.Add((int)ADTYPE.DAILY, "ca-app-pub-6040866524746900/7234271673");
            adUnitIds.Add((int)ADTYPE.GOODS, "ca-app-pub-6040866524746900/7094670871");
            adUnitIds.Add((int)ADTYPE.TARGET, "ca-app-pub-6040866524746900/7234271673");        // 타켓 광고는 데일리와 같은 키 사용.
#endif

#if USE_AD

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
#endif
            isInit = true;
        }
    }

    private void RequestAd(string adUnitId)
    {
#if USE_AD
        GodLib.MLog.Log("[AdManager::RequestAd] adUnitId : " + adUnitId);
        AdRequest request = new AdRequest.Builder().Build();
        rewardbasedVideo.LoadAd(request, adUnitId);        
#endif
    }

    public void LoadAd(ADTYPE type, onRewardVideoAdComplete callback)
    {        
        if (isInit == true)
        {
            GodLib.MLog.Log("[AdManager::LoadAd] Call!!!");
            _adType = type;            
            OnRewardComplete = callback;
#if USE_AD
            RequestAd(adUnitIds[(int)type]);
#endif
        }
    }

    public void ShowAd(ADTYPE type)
    {        
        if (type == ADTYPE.Max)
        {
            GodLib.MLog.Log("ShowAd Unkonwn type : " + type );
            return;
        }           

        _adType = type;
        
        if(indicate == null)
        {
            indicate = StartCoroutine(ShowIndicate());
        }        

#if USE_AD
        if (rewardbasedVideo.IsLoaded())
        {
            rewardbasedVideo.Show();            
        }
        else
        {
            GodLib.MLog.Log("Not Load...So Load Ad!");            
            LoadAd(_adType, OnReward_Ad_complete);                         
        }
#else
        if (OnRewardComplete != null)
        {
            OnRewardComplete(_adType);
            HideIndicate();
        }            
#endif
    }
    
    void WaitRewardAd()
    {
        HideIndicate();

        if(isRewarded == true)
        {
            // 보상이 Close 콜백 이후 뒤늦게 들어 옴.            
            if (OnRewardComplete != null)
                OnRewardComplete(_adType);

            isRewarded = false;
        }
        else
        {
            PopupManager.ShowToast(StringManager.Instance().GetString("Advertising_Change_Toast"));            
        }
        _adType = ADTYPE.Max;
    }

    void HandleRewardBasedVideoLoaded(object sender, EventArgs args)
    {
        GodLib.MLog.Log("[HandleRewardBasedVideoLoaded]");

        HideIndicate(); 

        if(_adType != ADTYPE.Max)
            ShowAd(_adType);        
    }

#if USE_AD
    void HandleRewardBasedVideoFailedToLoad(object sender, AdFailedToLoadEventArgs args)
    {
        isRewarded = false;
        _adType = ADTYPE.Max;

        HideIndicate();
        PopupManager.ShowToast(StringManager.Instance().GetString("Advertising_fail"));
     
        GodLib.MLog.Log("[HandleRewardBasedVideoFailedToLoad] : " + args.Message);
    }
#endif
    void HandleRewardBasedVideoOpened(object sender, EventArgs args)
    {
        isRewarded = false;     

        NetworkProcess.Instance().Send_CG_DELAYED_PINGPONG();        

        GodLib.MLog.Log("[HandleRewardBasedVideoOpened]");       
    }

    void HandleRewardBasedVideoStarted(object sender, EventArgs args)
    {
        isRewarded = false;

        GodLib.MLog.Log("[HandleRewardBasedVideoStarted]");
    }

#if USE_AD
    void HandleRewardBasedVideoRewarded(object sender, Reward args)
    {
        isRewarded = true;

        GodLib.MLog.Log("[HandleRewardBasedVideoRewarded] : Reward Type : " + args.Type + " Amount : " + args.Amount);        
    }
#endif
    void HandleRewardBasedVideoClosed(object sender, EventArgs args)
    {
        GodLib.MLog.Log("[HandleRewardBasedVideoClosed]");
        
        NetworkProcess.Instance().Send_CG_DELAYED_PINGPONG();        

        if(isRewarded == true)
        {
            HideIndicate();

            if(OnRewardComplete != null)
                OnRewardComplete(_adType);

            isRewarded = false;
            _adType = ADTYPE.Max;
        }
        else
        {
            Invoke("WaitRewardAd", 3.0f);
        }        
    }
    void HandleRewardBasedVideoLeftApplication(object sender, EventArgs args)
    {
        if (OnRewardComplete != null)
            OnRewardComplete(_adType);

        isRewarded = false;
        _adType = ADTYPE.Max;
        NetworkProcess.Instance().Send_CG_DELAYED_PINGPONG();

        HideIndicate();

        GodLib.MLog.Log("[HandleRewardBasedVideoLeftApplication]");
    }

    public void OnReward_Ad_complete(ADTYPE type)
    {
        GodLib.MLog.Log("[AdManager::OnShow_ad_complete] type : " + type);
        switch(type)
        {
            case ADTYPE.CHARACTER:
                NetworkProcess.Instance().Send_CG_USE_ADVERTISING(ADVERTISING_TYPE.ADVERTISING_TYPE_CHARACTER);    
                break;
            case ADTYPE.DAILY:
                NetworkProcess.Instance().Send_CG_USE_ADVERTISING(ADVERTISING_TYPE.ADVERTISING_TYPE_DAILY);    
                break;
            case ADTYPE.GOODS:
                NetworkProcess.Instance().Send_CG_USE_ADVERTISING(ADVERTISING_TYPE.ADVERTISING_TYPE_GOODS);    
                break;
            case ADTYPE.JOY:
                NetworkProcess.Instance().Send_CG_FREE_CHARGE();
                break;
            case ADTYPE.SKILLCARD:
                NetworkProcess.Instance().Send_CG_USE_ADVERTISING(ADVERTISING_TYPE.ADVERTISING_TYPE_SKILLCARD);    
                break;                
            case ADTYPE.TARGET:
                NetworkProcess.Instance().Send_CG_USE_ADVERTISING(ADVERTISING_TYPE.ADVERTISING_TYPE_TARGET);
                GameGlobal.Instance().Show_Ad_Time = TimeManager.GetCurrntUTCTime().ToString();
                GameGlobal.Instance().Limit_Show_Target_AD_Count += 1;
                break;
        }       
    }    
}
