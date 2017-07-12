using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using GB;
using GB.Account;
using GB.Billing;
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

		GBManager.ConfigureSDKWithGameInfo("", 1, GBSettings.LogLevel.DEBUG);
		
#if (UNITY_ANDROID && !NO_GPGS)
		// Google Play Games Initialize
		PlayGamesClientConfiguration config = new PlayGamesClientConfiguration.Builder()
        .Build();

		PlayGamesPlatform.InitializeInstance(config);
		// recommended for debugging:
		PlayGamesPlatform.DebugLogEnabled = true;
		// Activate the Google Play Games platform
		PlayGamesPlatform.Activate();		
#endif

		string adMobId = string.Empty;
#if UNITY_ANDROID
		adMobId = "ca-app-pub-5698820917568735/7934468808";
#elif UNITY_IPHONE
		adMobId = "ca-app-pub-5698820917568735/3504269208";
#endif	
		// GBAdManager.Instance.Init(adMobId);
		// GBAdManager.Instance.LoadAd(onRewardVideoAdCompleted);
		GBAdManager.Instance.Init(adMobId, onRewardAdCompleted);
	}

	void sessionCallback(SessionState state, GBException exception){

		if (state.Equals(SessionState.ACCESS_FAILED)) {
			PrintLog("session Error code = " + exception.getErrorCode() + "," + "message = " + exception.getErrorMessage());					
		} else {
			if (state.Equals (SessionState.OPEN)) {				
				isLogin = true;

				// Sign In Play Game Service
#if (!UNITY_EDITOR && UNITY_ANDROID && !NO_GPGS)				
				// Social.localUser.Authenticate((bool success) => {
					
				// });
#endif
				string userKey = GBUser.Instance.getActiveSession().getUserKey();
				GBInAppManager.StartSetup(userKey, (bool success, GBException e) => {

				});
			
				PrintLog("Session Success = " + GBUser.Instance.currentSession.userKey);
				
			} 
			else if (state.Equals (SessionState.CLOSED)) {
				PrintLog ("Session Closed!!! - LogOut");
				isLogin = false;
			}
			else {
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
#if (!UNITY_EDITOR && UNITY_ANDROID)
		skus.Add("sample_coin_10");
#elif (!UNITY_EDITOR && UNITY_IPHONE)
		skus.Add("sample_coin_100");
#endif			
		if(GUI.Button(new Rect(MARGIN, posY, BUTTON_WIDTH, BUTTON_HEIGHT), "Login", buttonStyle)) {
#if (!UNITY_EDITOR && UNITY_ANDROID)				
				GBSessionManager.LoginWithAuthType(AuthType.GOOGLE, sessionCallback);
#elif (!UNITY_EDITOR && UNITY_IPHONE)
				GBSessionManager.LoginWithAuthType(AuthType.GUEST, sessionCallback);
#endif			
			// Already have a last session?
// 			if (GBSessionManager.isReady()) {
// 				GBSessionManager.Login(sessionCallback);			
// 			} else {
// 				// Default AuthType.GOOGLE
// #if (!UNITY_EDITOR && UNITY_ANDROID)				
// 				GBSessionManager.LoginWithAuthType(AuthType.GOOGLE, sessionCallback);
// #elif (!UNITY_EDITOR && UNITY_IPHONE)
// 				GBSessionManager.LoginWithAuthType(AuthType.GUEST, sessionCallback);
// #endif
// 			}
		}

		if (GUI.Button(new Rect(Screen.width / 2 + 40, posY, BUTTON_WIDTH, BUTTON_HEIGHT), "Connect Link", buttonStyle)) {
			// Check Session Open or not
			bool isOpened = GBSessionManager.isOpened();

			if (isOpened && !GBSessionManager.isConnectedChannel()) {
				GBSessionManager.ConnectChannel(AuthType.FACEBOOK, (SessionState state, GBException exception) => {
					
				});
			} else {
				// Button Disable
			}
		}

		if(GUI.Button(new Rect(0, posY += BUTTON_HEIGHT, scrollContentsWidth, BUTTON_HEIGHT), "Logout", buttonStyle)) {
			GBSessionManager.Logout(sessionCallback);
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

					GBInAppManager.BuyItem(skus[0], 0, (string paymentKey, GBException ex) => {
						if (exception == null) {
							GBLog.verbose("paymentKey = " + paymentKey);
							PrintLog("BuyItem paymentKey::" + paymentKey);
						} else {
							GBLog.verbose ("error = " + exception.getErrorMessage());	
							PrintLog("BuyItem exception::" + exception.getErrorCode() + " MSG:::" + exception.getErrorMessage());
						}				
					});					
				} else {
					GBLog.verbose("error = " + exception.getErrorMessage());
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
			if (GBAdManager.Instance.isEnableAds())
				GBAdManager.Instance.ShowAd();
		}				
					
		GUI.Label(new Rect(0, posY += BUTTON_HEIGHT, scrollContentsWidth, labalHeight), sdkLog, labelStyle);		
		GUI.EndScrollView();
	}
	
	void onRewardAdCompleted() {
		PrintLog("Ad Completed!!!");
	}
	// void onHandleNativeEvent(string result) {
	// 	PrintLog("onHandleNativeEvent result = " + result);
	// 	JSONNode root = JSONNode.Parse(result);
	// 	var response = root["result"];

	// 	string eventKey = response["eventKey"];
	// 	PrintLog("onHandleNativeEvent event name = " + eventKey);
	// 	if (eventKey.Equals("onMarketAccountStateChange")) {
	// 		// TO-DO : JSON PARSING
	// 		PrintLog("onHandleNativeEvent [event name] = " + eventKey);
	// 	} 
	// }
	
	void PrintLog(string text) {
		
		sdkLog = text + "\n" + sdkLog;
		sdkLogCount++;
	}

	#region RewardBasedVideo callback handlers

	#endregion
}
