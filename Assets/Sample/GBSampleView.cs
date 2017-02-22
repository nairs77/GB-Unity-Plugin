using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using GB;
using GB.Account;
using GB.Billing;
//using GB.PlayGameService;
using GoogleMobileAds.Api;
using GooglePlayGames;
using GooglePlayGames.BasicApi;
using SimpleJSON;

public class GBSampleView : MonoBehaviour {

	private static readonly int BUTTON_HEIGHT = Screen.height / 15;
	private static readonly int MENU_COUNT = 10;	
	private static readonly int BUTTON_FONT_SIZE = 40;
	private static readonly int LABEL_FONT_SIZE = 30;

	private static readonly int MARGIN = 20;

	private static readonly int BW_BUTTON_MARGIN = 40;

	private static readonly int BUTTON_WIDTH = Screen.width / 2 - (2*MARGIN) - BW_BUTTON_MARGIN;

	private Vector2 scrollPosition = Vector2.zero;

	//private bool isPushEnable = true;

	private string sdkLog = "";
	private int sdkLogCount = 1;

	private bool isLogin = false;

	int posY;
	int scrollContentsWidth;
	int labalHeight;
	int scrollContentsHeight;
	GUIStyle buttonStyle;
	GUIStyle labelStyle;
	GUIStyle textStyle;

    void Start() {
		SetUp();
    }

	/// <summary>
	/// Initialize GBSDK 
	/// </summary>
	void SetUp() {
/*
		GBPlatformConfiguration config = new GBPlatformConfiguration.Builder()
		.enableAdmob(UnitId)
		.enablePlayGameService()
		.Build();

		GBPlatform.InitializeWithConfig(config);
*/		
		//GBManager.ConfigureSDKWithGameInfo("", 1, GBSettings.LogLevel.DEBUG);

		// AdMob Initialize
		GBAdManager.Instance.Init("ca-app-pub-5698820917568735/9991786004");
		GBAdManager.Instance.LoadAd(null);
		// Google Play Games Initialize
		PlayGamesClientConfiguration config = new PlayGamesClientConfiguration.Builder()
        .Build();

		PlayGamesPlatform.InitializeInstance(config);
		// recommended for debugging:
		PlayGamesPlatform.DebugLogEnabled = true;
		// Activate the Google Play Games platform
		PlayGamesPlatform.Activate();		

	// 	mRewardBasedVideo = RewardBasedVideoAd.Instance;

	// 	mRewardBasedVideo.OnAdLoaded += HandleRewardBasedVideoLoaded;
    // // has failed to load.
	// 	mRewardBasedVideo.OnAdFailedToLoad += HandleRewardBasedVideoFailedToLoad;
	// 	// is opened.
	// 	mRewardBasedVideo.OnAdOpening += HandleRewardBasedVideoOpened;
	// 	// has started playing.
	// 	mRewardBasedVideo.OnAdStarted += HandleRewardBasedVideoStarted;
	// 	// has rewarded the user.
	// 	mRewardBasedVideo.OnAdRewarded += HandleRewardBasedVideoRewarded;
	// 	// is closed.
	// 	mRewardBasedVideo.OnAdClosed += HandleRewardBasedVideoClosed;
	// 	// is leaving the application.
	// 	mRewardBasedVideo.OnAdLeavingApplication += HandleRewardBasedVideoLeftApplication;		
/*
		mInterstitial = new InterstitialAd("ca-app-pub-5698820917568735/3329803608");

		mInterstitial.OnAdLoaded += HandleRewardBasedVideoLoaded;
    // has failed to load.
		mInterstitial.OnAdFailedToLoad += HandleRewardBasedVideoFailedToLoad;
		// is opened.
		mInterstitial.OnAdOpening += HandleRewardBasedVideoOpened;
		// has started playing.
		// mInterstitial.OnAd .OnAdStarted += HandleRewardBasedVideoStarted;
		// // has rewarded the user.
		// mInterstitial.OnAdRewarded += HandleRewardBasedVideoRewarded;
		// is closed.
		mInterstitial.OnAdClosed += HandleRewardBasedVideoClosed;
		// is leaving the application.
		mInterstitial.OnAdLeavingApplication += HandleRewardBasedVideoLeftApplication;
		AdRequest request = new AdRequest.Builder()
			.AddTestDevice("C85608B21885A69E8EA9FB9AD8683CEC")  // My test device.
			.Build();
		// Load the interstitial with the request.
		mInterstitial.LoadAd(request);		
*/							
	}


	void permissionCallback(GBPermissionResult result){

		GBPermissionResult.GBPermission permission = result.Permissions [0];
		PrintLog ("Permission name = " + permission.PermissionName);
		if (result.PermissionStatus == GBPermissionStatus.USER_DENIED) {
			if (permission.PermissionName.Equals ("GET_ACCOUNTS") && permission.Status == GBPermissionStatus.USER_DENIED) {
				GBPermissionManager.ShowDetailPermissionView (false, permission.PermissionName, permissionCallback);
			} 
			else if (permission.PermissionName.Equals ("READ_PHONE_STATE") && permission.Status == GBPermissionStatus.USER_DENIED) {
				GBPermissionManager.ShowDetailPermissionView (false, permission.PermissionName, permissionCallback);
			}
		} 
		else if (result.PermissionStatus == GBPermissionStatus.PERMISSION_VIEW_CLOSE) {
			if (permission.PermissionName.Equals ("GET_ACCOUNTS") && permission.Status == GBPermissionStatus.USER_DENIED) {
				GBSessionManager.Login(AuthType.GUEST, sessionCallback);				
			}
			else if (permission.PermissionName.Equals ("READ_PHONE_STATE") && permission.Status == GBPermissionStatus.USER_DENIED) {
				Application.Quit ();
			}
		}
		else if (result.PermissionStatus == GBPermissionStatus.SNACKBAR_DETAIL) {
			GBPermissionManager.ShowDetailPermissionView (true, permission.PermissionName, permissionCallback);
		}
	}

	void sessionCallback(SessionState state, GBException exception){

		if (state.Equals(SessionState.ACCESS_FAILED)) {
			PrintLog("session Error code = " + exception.getErrorCode() + "," + "message = " + exception.getErrorMessage());					
		} 
		else {
			if (state.Equals (SessionState.OPEN) || state.Equals (SessionState.TOKEN_REISSUED) || state.Equals( SessionState.JOIN )) {
				
				//emailTestType = EmailTestType.NONE;
				isLogin = true;
				// PrintLog ("Session Open!!! - Token = " + GBSessionManager.getAccessToken ());

				// GBSessionManager.GetProfile ((bool success, GBException profile_exception) => {
				// 	PrintLog ("User Key = " + GBUser.Instance.LocalUser.userKey.ToString ());

				// 	if (GBSettings.GetActiveMarket () == GBSettings.Market.UC) {							
				// 		//GBPlatformHelper.SubmitExtendedData("test", "tester", "25", "CHINA-SERVER", "CHINA-ZONE");
				// 		GBPlatformHelper.SubmitExtendedData ("test", "tester", "24");
				// 	}
				// });
			} 
			else if (state.Equals (SessionState.CLOSED)) {
				PrintLog ("Session Closed!!! - LogOut");
				isLogin = false;
			}
			else {
				// SessionState Join, REISSUED..
			}	
		}
	}
	
	void Update () {
		
		if(Application.platform == RuntimePlatform.Android) {
			if(Input.GetKey (KeyCode.Escape)) {
				Application.Quit ();
			}
		}
	}	
	
	void OnDestroy() {	
		GBManager.Instance.Destroy();
	}
	
	void OnGUI() {
		
		posY = 0;
		scrollContentsWidth = Screen.width;
		labalHeight = (LABEL_FONT_SIZE * 5 * sdkLogCount);
		scrollContentsHeight = BUTTON_HEIGHT * MENU_COUNT + labalHeight;
		scrollPosition = GUI.BeginScrollView(new Rect(0, 0, Screen.width, Screen.height), scrollPosition, new Rect(0, 0, scrollContentsWidth, scrollContentsHeight));

		
		buttonStyle = new GUIStyle(GUI.skin.button);
    	buttonStyle.fontSize = BUTTON_FONT_SIZE;

		labelStyle = new GUIStyle(GUI.skin.label);
    	labelStyle.fontSize = LABEL_FONT_SIZE;

		textStyle = new GUIStyle (GUI.skin.textField);
		textStyle.fontSize = 50;

		List<string> skus = new List<string>();

		#if UNITY_ANDROID
		skus.Add("GB_coin_1000");
		skus.Add("GB_coin_2000");
		#elif UNITY_IPHONE
		//skus.Add("Sample01");
		skus.Add("GB_product_id_7695245");
		skus.Add("GB_product_id_3234233");
		#endif

		if(GUI.Button(new Rect(MARGIN, posY, BUTTON_WIDTH, BUTTON_HEIGHT), "LoginWithAuthType", buttonStyle)) {

			GBSessionManager.Login(AuthType.GOOGLE, sessionCallback);
		}

		if (GUI.Button(new Rect(Screen.width / 2 + 40, posY, BUTTON_WIDTH, BUTTON_HEIGHT), "Connect Link", buttonStyle)) {
		}

		if(GUI.Button(new Rect(0, posY += BUTTON_HEIGHT, scrollContentsWidth, BUTTON_HEIGHT), "Query Inventory", buttonStyle)) {
			GBInAppManager.QueryInventory(skus, (List<string> validateIdentifiers, GBException exception) => {

			});
		}

		if (GUI.Button(new Rect(0, posY += BUTTON_HEIGHT, scrollContentsWidth, BUTTON_HEIGHT), "Query Inventory With Info", buttonStyle)) {
			GBInAppManager.QueryInventory(skus, (GBInventory inv, GBException exception) => { 
				if (exception == null) {
					GBLog.verbose("Success Query Inventory"); 

					if (inv == null) {
						GBLog.verbose("inventory is empty !!!");
						return;
					}

					List<GBInAppItem> itemList = inv.getInventory();

					foreach(GBInAppItem item in itemList) {
						//GBLog.verbose("validate id =" + item.productId);
						GBLog.verbose("item = " + item.ToString());
					}
				} else {
					GBLog.verbose("error = " + exception.getErrorMessage());
				}
			});			
		}

		if(GUI.Button(new Rect(0, posY += BUTTON_HEIGHT, scrollContentsWidth, BUTTON_HEIGHT), "BuyItem", buttonStyle)) {
			
			GBInAppManager.BuyItem("gb_coin_1000", 1000, null, (string paymentKey, GBException exception) => {
				if (exception == null) {
					GBLog.verbose("paymentKey = " + paymentKey);
					PrintLog("BuyItem paymentKey::" + paymentKey);
				} else {
					GBLog.verbose ("error = " + exception.getErrorMessage());	
					PrintLog("BuyItem exception::" + exception.getErrorCode() + " MSG:::" + exception.getErrorMessage());
				}				
			});
		}		

		if(GUI.Button(new Rect(0, posY += BUTTON_HEIGHT, scrollContentsWidth, BUTTON_HEIGHT), "Restore Item", buttonStyle)) {

			GBInAppManager.RestoreItems((List<string>paymentKeys, GBException exception) => {
				if (exception == null) {
					if (paymentKeys != null && paymentKeys.Count > 0) {
						foreach (string key in paymentKeys)
							GBLog.verbose("restore payment key = " + key);
					} else {
						GBLog.verbose("NOT RESTORE_ITEM");
					}					
				}
			});
		}		

		if(GUI.Button(new Rect(0, posY += BUTTON_HEIGHT, scrollContentsWidth, BUTTON_HEIGHT), "Show Ad (Reward Video)", buttonStyle)) {


			GBAdManager.Instance.ShowAd();			

			
			// if (mInterstitial.IsLoaded()) {
			// 	Debug.Log("IsLoaded()");
			// 	mInterstitial.Show();
			// }
		}				
					
		GUI.Label(new Rect(0, posY += BUTTON_HEIGHT, scrollContentsWidth, labalHeight), sdkLog, labelStyle);		
		GUI.EndScrollView();
	}
	
	void onHandleNativeEvent(string result) {
		PrintLog("onHandleNativeEvent result = " + result);
		JSONNode root = JSONNode.Parse(result);
		var response = root["result"];

		string eventKey = response["eventKey"];
		PrintLog("onHandleNativeEvent event name = " + eventKey);
		if (eventKey.Equals("onMarketAccountStateChange")) {
			// TO-DO : JSON PARSING
			PrintLog("onHandleNativeEvent [event name] = " + eventKey);
		} 
	}
	
	void PrintLog(string text) {
		
		sdkLog = text + "\n" + sdkLog;
		sdkLogCount++;
	}

	#region RewardBasedVideo callback handlers

	public void HandleRewardBasedVideoRewarded(object sender, Reward args)
	{
		string type = args.Type;
		double amount = args.Amount;
		print("User rewarded with: " + amount.ToString() + " " + type);
	}	

	public void HandleRewardBasedVideoLoaded(object sender, EventArgs args)
	{
		print("OnLoaded!!!!");

		//mRewardBasedVideo.Show();		
	}

	public void HandleRewardBasedVideoFailedToLoad(object sender, AdFailedToLoadEventArgs args)
	{
		print("OnFailedToLoad");

		print("Video Failed to load: " + args.Message);
	}

	public void HandleRewardBasedVideoOpened(object sender, EventArgs args)
	{

	}

	public void HandleRewardBasedVideoStarted(object sender, EventArgs args)
	{

	}

	public void HandleRewardBasedVideoClosed(object sender, EventArgs args)
	{
    	// Create an empty ad request.
		// AdRequest request = new AdRequest.Builder()
		// 	.AddTestDevice("C85608B21885A69E8EA9FB9AD8683CEC")  // My test device.
		// 	.Build();
		// // Load the interstitial with the request.
		// mInterstitial.LoadAd(request);					
	}

	public void HandleRewardBasedVideoLeftApplication(object sender, EventArgs args)
	{

	}
	#endregion
}
