using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using GB;
using GB.Account;
using GB.Billing;
using GB.PlayGameService;

using SimpleJSON;

public class GBSampleView : MonoBehaviour {

	public enum EmailTestType{
		NONE,
		LOGINNJOIN,
		OTHER
	}

	private static readonly int BUTTON_HEIGHT = Screen.height / 15;
	private static readonly int MENU_COUNT = 10;	
	private static readonly int BUTTON_FONT_SIZE = 40;
	private static readonly int LABEL_FONT_SIZE = 30;
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

	void SetUp() {
		/**P
		 * SDK 사용을 위한 GBManager 클래스를 초기화 합니다.
		 * Initialize GB SDK
		 * */		

		// GBManager.SetActiveMarket(GBSettings.Market.BAIDU, (bool success, string stringValue) => {
		// 	PrintLog("Active Market =" + GBSettings.GetMarketToString());
		// });	
/*		
		#if UNITY_ANDROID
		GBSettings.SetActiveMarket(GBSettings.Market.GOOGLE);
		GBManager.ConfigureSDKWithGameInfo (GBSettings.AppKey, GBSettings.GameCode, GBSettings.Market.GOOGLE, GBSettings.LogLevel.DEBUG);
		GBPermissionManager.SetPermissionCallback (permissionCallback);

		List<string> promotionItems = new List<string>();
		promotionItems.Add("GB_coin_2000");
		GBInAppManager.SetPromotionItems(promotionItems);

		#elif UNITY_IPHONE
		GBSettings.SetActiveMarket(GBSettings.Market.APPLE_STORE);
		GBManager.ConfigureSDKWithGameInfo (GBSettings.AppKey, GBSettings.GameCode, GBSettings.Market.APPLE_STORE, GBSettings.LogLevel.DEBUG);
		#endif
*/
		GBManager.ConfigureSDKWithGameInfo("", 10, GBSettings.LogLevel.DEBUG);
		GBManager.Instance.onHandleNativeEvent = new GBManager.DelegateNativeEvents(onHandleNativeEvent);

		GBLog.verbose("App Setup !!!");
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

		if(GUI.Button(new Rect(0, posY, scrollContentsWidth, BUTTON_HEIGHT), "Login With Type", buttonStyle)) {

			GBSessionManager.Login(AuthType.GOOGLE, sessionCallback);
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

}
