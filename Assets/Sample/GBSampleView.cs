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

	private bool requestGlobalServer = false;
	private bool isLogin = false;

	private EmailTestType emailTestType = EmailTestType.NONE;

	private string strEmail = "Enter email";
	private string strEmailPassword = "Enter email password";
	private string strEmailJoin = "Enter email";
	private string strEmailJoinPassword_1 = "Enter email password";
	private string strEmailJoinPassword_2 = "Enter email password(confirm)";
	private string strEmailFind = "Enter email";


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
		#if UNITY_ANDROID
		GBSettings.SetActiveMarket(GBSettings.Market.GOOGLE);
		GBManager.ConfigureSDKWithGlobalInfo (GBSettings.AppKey, GBSettings.GameCode, GBSettings.Market.GOOGLE, GBSettings.LogLevel.DEBUG);
		GBPermissionManager.SetPermissionCallback (permissionCallback);

		List<string> promotionItems = new List<string>();
		promotionItems.Add("GB_coin_2000");
		GBInAppManager.SetPromotionItems(promotionItems);

		#elif UNITY_IPHONE
		GBSettings.SetActiveMarket(GBSettings.Market.APPLE_STORE);
		GBManager.ConfigureSDKWithGlobalInfo (GBSettings.AppKey, GBSettings.GameCode, GBSettings.Market.APPLE_STORE, GBSettings.LogLevel.DEBUG);
		#endif

		GBManager.Instance.onHandleNativeEvent = new GBManager.DelegateNativeEvents(onHandleNativeEvent);

		JLog.verbose("App Setup !!!");
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
				
				emailTestType = EmailTestType.NONE;
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

		if (!requestGlobalServer) {

			if (GUI.Button (new Rect (0, Screen.height/2, scrollContentsWidth, BUTTON_HEIGHT), "Request Global Server", buttonStyle)) {			
				// GBManager.GetGlobalServerInfo ("https://GB-qa1.GBplay.com/gbranch/branch/getzone", (bool success, string stringValue) => {
				// 	//QA : https://GB-cn-qa.GBplay.com/gbranch/branch/getzone
				// 	//PrintLog("stringValue=" + stringValue);
				// 	JLog.verbose ("result = " + stringValue);

				// 	JLog.verbose ("MCC =" + GBManager.GetMCC () + "Device Id = " + GBManager.GetDeviceId () + "Language = " + GBManager.GetCurrentLanguage ());

				// 	GBSessionManager.SetAllowedEULA(true);
				// 	bool isAlreadyLogin = GBSessionManager.isAlreadyLogin ();
				// 	PrintLog ("isAlreadyLogin() = " + isAlreadyLogin.ToString ());

				// 	GBManager.SetGameLanguage (LanguageType.GAME_LANG_EN);
				// 	requestGlobalServer = true;
				// });
			}	
		} 
		else {

			if (emailTestType == EmailTestType.LOGINNJOIN) {
				emailLoginNJoin ();
			} 
			else if (emailTestType == EmailTestType.OTHER) {
				emailOtherFunc ();
			}
			else {

				int firstPosX = 0;
				if (!isLogin) {
					if(GUI.Button(new Rect(0, posY, scrollContentsWidth, BUTTON_HEIGHT), "Google Login", buttonStyle)) {

						GBSessionManager.Login(AuthType.GOOGLE_PLUS, sessionCallback);
					}

					if(GUI.Button(new Rect(0, posY += BUTTON_HEIGHT, scrollContentsWidth, BUTTON_HEIGHT), "Email(Join) Login", buttonStyle)) {
						strEmail = "Enter email";
						strEmailPassword = "Enter email password";
						strEmailJoin = "Enter email";
						strEmailJoinPassword_1 = "Enter email password";
						strEmailJoinPassword_2 = "Enter email password(confirm)";
						emailTestType = EmailTestType.LOGINNJOIN;
					}

					if(GUI.Button(new Rect(0, posY += BUTTON_HEIGHT, scrollContentsWidth, BUTTON_HEIGHT), "New UI Login", buttonStyle)) {

						// GBSessionManager.LoginByUI(LoginUIType.LOGIN_UI, sessionCallback);		
					}

					firstPosX = posY += BUTTON_HEIGHT;
				}

				// if(GUI.Button(new Rect(0, firstPosX, scrollContentsWidth, BUTTON_HEIGHT), "EULA", buttonStyle)) {
				// 	GBSessionManager.ShowViewByType(GBProfileViewType.GBProfileEULA);
				// }

				if(GUI.Button(new Rect(0, posY += BUTTON_HEIGHT, scrollContentsWidth / 2, BUTTON_HEIGHT), "RequestPermission", buttonStyle)) {

					//			string[] permissions = {"WRITE_EXTERNAL_STORAGE", "READ_PHONE_STATE"};
					GBPermissionManager.RequestPermission ("WRITE_EXTERNAL_STORAGE", permissionCallback);

				}

				if(GUI.Button(new Rect(scrollContentsWidth / 2, posY, scrollContentsWidth / 2, BUTTON_HEIGHT), "ShowPermissionSnack", buttonStyle)) {

					GBPermissionManager.ShowPermissionSnack("WRITE_EXTERNAL_STORAGE", permissionCallback);

				}

				if (isLogin) {
					
					if(GUI.Button(new Rect(0, posY += BUTTON_HEIGHT, scrollContentsWidth, BUTTON_HEIGHT), "Email Func", buttonStyle)) {
						strEmail = "Enter email";
						strEmailPassword = "Enter Current email password";
						strEmailJoin = "Enter email";
						strEmailJoinPassword_1 = "Enter New email password";
						strEmailJoinPassword_2 = "Enter New email password(confirm)";
						emailTestType = EmailTestType.OTHER;
					}

					// if(GUI.Button(new Rect(0, posY += BUTTON_HEIGHT, scrollContentsWidth, BUTTON_HEIGHT), "Email Link", buttonStyle)) {

					// 	String email = "endow50@naver.com";
					// 	String password = "GB83";
					// 	GBSessionManager.LinkServiceWithAuthType (AuthType.EMAIL, email, password, sessionCallback);
					// }

					if(GUI.Button(new Rect(0, posY += BUTTON_HEIGHT, scrollContentsWidth / 2, BUTTON_HEIGHT), "Logout", buttonStyle)) {

						// 로그아웃 API 호출
						//Authorization.Instance.Logout();
						//PrintLog("Logout()");
						GBSessionManager.Logout(sessionCallback);
					}

					if(GUI.Button(new Rect(scrollContentsWidth / 2, posY, scrollContentsWidth / 2 , BUTTON_HEIGHT), "Unregister", buttonStyle)) {

						// 게임 탈퇴 API 호출
						//Authorization.Instance.Unregister();

						GBSessionManager.Unregister(sessionCallback);
					}

/*
					if(GUI.Button(new Rect(0, posY += BUTTON_HEIGHT, scrollContentsWidth, BUTTON_HEIGHT), "Profile UI", buttonStyle)) {

						// 메인 UI호출
						//GBSessionManager.ShowGBMain();
						GBSessionManager.ShowViewByType(GBProfileViewType.GBProfileUserInfo);
						PrintLog("Main()");
					}

					if(GUI.Button(new Rect(scrollContentsWidth / 2, y, scrollContentsWidth / 2, BUTTON_HEIGHT), "Manage Account", buttonStyle)) {
						GBSessionManager.ShowViewByType(GBProfileViewType.GBProfileManageAccount);
					}		
*/
					// Billing
					if(GUI.Button(new Rect(0, posY += BUTTON_HEIGHT, scrollContentsWidth / 2, BUTTON_HEIGHT), "Start Billing Service", buttonStyle)) {

						LocalUser localUser = GBUser.Instance.LocalUser;

						if (localUser == null) {
							return;
						}

						string userKey = localUser.userKey.ToString();

						JLog.verbose ("user Key = " + userKey);

						GBInAppManager.StartSetup(userKey, (bool success, GBException exception) => {
							if (success) {
								JLog.verbose ("Success Start Billing Service");
							} else {
								JLog.verbose ("error = " + exception.getErrorMessage());
							}
						});
					}

					if(GUI.Button(new Rect(scrollContentsWidth / 2, posY, scrollContentsWidth / 2, BUTTON_HEIGHT), "Query Inventory", buttonStyle)) {
						List<string> skus = new List<string>();

						#if UNITY_ANDROID
						skus.Add("GB_coin_1000");
						skus.Add("GB_coin_2000");
						#elif UNITY_IPHONE
						//skus.Add("Sample01");
						skus.Add("GB_product_id_7695245");
						skus.Add("GB_product_id_3234233");
						#endif

						//  GBInAppManager.QueryInventory(skus, (List<string> validateIdentifiers, GBException exception) => {
						//  	if (exception == null) {
						//  		JLog.verbose("Success Query Inventory");

						//  		foreach(string id in validateIdentifiers)
						//  			JLog.verbose("validate id =" + id);

						//  	} else {
						//  		JLog.verbose ("error = " + exception.getErrorMessage());
						//  	}				
						//  });

						GBInAppManager.QueryInventory(skus, (GBInventory inv, GBException exception) => { 
							if (exception == null) {
								JLog.verbose("Success Query Inventory"); 

								if (inv == null) {
									JLog.verbose("inventory is empty !!!");
									return;
								}

								List<GBInAppItem> itemList = inv.getInventory();

								foreach(GBInAppItem item in itemList) {
									//JLog.verbose("validate id =" + item.productId);
									JLog.verbose("item = " + item.ToString());
								}
							} else {
								JLog.verbose("error = " + exception.getErrorMessage());
							}
						});			
					}

					if(GUI.Button(new Rect(0, posY += BUTTON_HEIGHT, scrollContentsWidth / 2, BUTTON_HEIGHT), "Buy Item(inapp)", buttonStyle)) {
						string sku = null;
						#if !UNITY_EDITOR && UNITY_ANDROID
						sku = "GB_coin_2000";
						#elif UNITY_IPHONE
						//skus.Add("Sample01");
						sku = "GB_product_id_7695245";
						#endif
/*
						GBInAppManager.BuyItem(sku, 1, "30 Ruby", "{\"developer_payload\" : \"hello test\"}", false, (string paymentKey, GBException exception) => {
							if (exception == null) {
								JLog.verbose("paymentKey = " + paymentKey);
								PrintLog("BuyItem paymentKey::" + paymentKey);
							} else {
								JLog.verbose ("error = " + exception.getErrorMessage());	
								PrintLog("BuyItem exception::" + exception.getErrorCode() + " MSG:::" + exception.getErrorMessage());
							}
						});
*/						
					}

					if(GUI.Button(new Rect(scrollContentsWidth/2, posY, scrollContentsWidth / 2, BUTTON_HEIGHT), "Buy Item(subs)", buttonStyle)) {

						string sku = null;
						#if !UNITY_EDITOR && UNITY_ANDROID
						sku = "GB_sub_test";
						#elif UNITY_IPHONE
						//skus.Add("Sample01");
						sku = "GB_product_id_3234233";
						#endif
/*
						GBInAppManager.BuyItem(sku, 1, "30 Ruby", "{\"developer_payload\" : \"hello test\"}", true, (string paymentKey, GBException exception) => {
							if (exception == null) {
								JLog.verbose("paymentKey = " + paymentKey);
								PrintLog("BuyItem paymentKey::" + paymentKey);
							} else {
								JLog.verbose ("error = " + exception.getErrorMessage());	
								PrintLog("BuyItem exception::" + exception.getErrorCode() + " MSG:::" + exception.getErrorMessage());
							}
						});
*/						
					}

					if(GUI.Button(new Rect(0, posY += BUTTON_HEIGHT, scrollContentsWidth, BUTTON_HEIGHT), "Restore Item", buttonStyle)) {
						GBInAppManager.RestoreItems((List<string> paymentKeys, GBException exception) => {
							if (paymentKeys != null && paymentKeys.Count > 0) {
								foreach (string key in paymentKeys)
									JLog.verbose("restore payment key = " + key);
							} else {
								JLog.verbose("NOT RESTORE_ITEM");
							}
						});
					}
					/*
		if(GUI.Button(new Rect(0, y += BUTTON_HEIGHT, scrollContentsWidth / 2, BUTTON_HEIGHT), "Send Push to Me", buttonStyle)) {
			// Only iPhone Test			
			GBPushManager.SendMessageToUser(GBUser.Instance.LocalUser.userKey.ToString(), "Title : Test", "Send Message to Me", (string result, GBException exception) => {
				if (exception == null) {
					JLog.verbose("Success - Send me Push Message");
				} else {
					JLog.verbose("Failed - Failed Push Message");
				}
			});
		}

		if(GUI.Button(new Rect(scrollContentsWidth/2, y, scrollContentsWidth / 2, BUTTON_HEIGHT), "Get Current Language", buttonStyle)) {
			//GBManager.ShowToast(GBManager.GetCurrentLanguage());
			JLog.verbose("Language = " + GBManager.GetCurrentLanguage());
			JLog.verbose("Device = " + GBManager.GetDeviceId());
			JLog.verbose("device name = " + GBManager.GetDeviceModel());
		}

		string title = "Push " + (isPushEnable ? "On" : "Off");
		if(GUI.Button(new Rect(0, y += BUTTON_HEIGHT, scrollContentsWidth, BUTTON_HEIGHT), title, buttonStyle)) {
			
			GBPushManager.PushOnOff(!isPushEnable, (string result, GBException exception) => {
				if (exception == null) {
					JLog.verbose("Success Push Enable");
					isPushEnable = !isPushEnable;
				} else {
					JLog.verbose("Failed Push Enable");
				}
			});
		}		
*/			
				}
			}

		}
			
		GUI.Label(new Rect(0, posY += BUTTON_HEIGHT, scrollContentsWidth, labalHeight), sdkLog, labelStyle);		
		GUI.EndScrollView();
	}


	void emailLoginNJoin(){

		GUIStyle boxStyle = new GUIStyle(GUI.skin.box);
		boxStyle.fontSize = 50;

		posY = 50;
		int groupPosX = 50;
		GUI.BeginGroup (new Rect(groupPosX , posY, scrollContentsWidth - 100 , BUTTON_HEIGHT * 4));

		int boxY = 0;
		GUI.Box(new Rect(0 , boxY, scrollContentsWidth - 100 , BUTTON_HEIGHT * 4), "Email Login", boxStyle);

		strEmail = GUI.TextField (new Rect (groupPosX, boxY += BUTTON_HEIGHT / 2, scrollContentsWidth - 200, BUTTON_HEIGHT), strEmail, textStyle);
		strEmailPassword = GUI.TextField (new Rect (groupPosX, boxY += BUTTON_HEIGHT, scrollContentsWidth - 200, BUTTON_HEIGHT), strEmailPassword, textStyle);
		if(GUI.Button(new Rect(groupPosX , boxY += BUTTON_HEIGHT, scrollContentsWidth - 200, BUTTON_HEIGHT), "Login", buttonStyle)) {

			if (strEmail.Contains ("email") || strEmailPassword.Contains ("email"))
				return;
/*			
			GBSessionManager.Login(AuthType.EMAIL, strEmail, strEmailPassword, sessionCallback);
*/			
		}

		GUI.EndGroup ();

		posY += BUTTON_HEIGHT * 4 + 50;
		boxY = 0;

		GUI.BeginGroup (new Rect(groupPosX , posY, scrollContentsWidth - 100 , BUTTON_HEIGHT * 5));
		GUI.Box(new Rect(0 , boxY, scrollContentsWidth - 100 , BUTTON_HEIGHT * 5), "Email Join", boxStyle);
		strEmailJoin = GUI.TextField (new Rect (groupPosX, boxY += BUTTON_HEIGHT / 2, scrollContentsWidth - 200, BUTTON_HEIGHT), strEmailJoin, textStyle);
		strEmailJoinPassword_1 = GUI.TextField (new Rect (groupPosX, boxY += BUTTON_HEIGHT, scrollContentsWidth - 200, BUTTON_HEIGHT), strEmailJoinPassword_1, textStyle);
		strEmailJoinPassword_2 = GUI.TextField (new Rect (groupPosX, boxY += BUTTON_HEIGHT, scrollContentsWidth - 200, BUTTON_HEIGHT), strEmailJoinPassword_2, textStyle);
		if(GUI.Button(new Rect(groupPosX , boxY += BUTTON_HEIGHT, scrollContentsWidth - 200, BUTTON_HEIGHT), "Join", buttonStyle)) {

			if (strEmailJoin.Contains ("email") || strEmailJoinPassword_1.Contains ("email") || strEmailJoinPassword_2.Contains ("email"))
				return;

			if (!strEmailJoinPassword_1.Equals (strEmailJoinPassword_2)) {
				PrintLog ("password not equal");
				return;
			}
/*
			GBSessionManager.Login (AuthType.JOIN, strEmailJoin, strEmailJoinPassword_2, sessionCallback);
*/			
		}

		GUI.EndGroup ();

		posY += BUTTON_HEIGHT * 5 + 50;

		GUI.BeginGroup (new Rect(groupPosX , posY, scrollContentsWidth - 100 , BUTTON_HEIGHT * 3));

		boxY = 0;
		GUI.Box(new Rect(0 , boxY, scrollContentsWidth - 100 , BUTTON_HEIGHT * 3), "Email Password Find", boxStyle);

		strEmailFind = GUI.TextField (new Rect (groupPosX, boxY += BUTTON_HEIGHT / 2, scrollContentsWidth - 200, BUTTON_HEIGHT), strEmailFind, textStyle);

		if(GUI.Button(new Rect(groupPosX , boxY += BUTTON_HEIGHT, scrollContentsWidth - 200, BUTTON_HEIGHT), "Find", buttonStyle)) {

			if (strEmailFind.Contains ("email"))
				return;
/*
			GBSessionManager.ManagementEmailAccount (EmailOperationType.EMAIL_FIND_PASSWD, strEmailFind, null, (bool isSuccess, GBException exception) => {

				if(isSuccess){
					PrintLog ("EMAIL_FIND_PASSWD is " + isSuccess);
				}
				else{
					if(exception != null)
						PrintLog ("EMAIL_FIND_PASSWD Error code = " + exception.getErrorCode () + "," + "message = " + exception.getErrorMessage ());
				}
			});
*/			
		}

		GUI.EndGroup ();

		posY += BUTTON_HEIGHT * 3 + 50;

		if(GUI.Button(new Rect(50, posY, scrollContentsWidth - 100, BUTTON_HEIGHT), "Cancel", buttonStyle)) {
			emailTestType = EmailTestType.NONE;
		}

	}

	void emailOtherFunc(){

		GUIStyle boxStyle = new GUIStyle(GUI.skin.box);
		boxStyle.fontSize = 50;

		posY = 50;
		int groupPosX = 50;
		GUI.BeginGroup (new Rect(groupPosX , posY, scrollContentsWidth - 100 , BUTTON_HEIGHT * 2));

		int boxY = 0;
		GUI.Box(new Rect(0 , boxY, scrollContentsWidth - 100 , BUTTON_HEIGHT * 2), "Send Cert Email", boxStyle);

		if(GUI.Button(new Rect(groupPosX , boxY += BUTTON_HEIGHT / 2, scrollContentsWidth - 200, BUTTON_HEIGHT), "Send", buttonStyle)) {
/*			
			GBSessionManager.ManagementEmailAccount (EmailOperationType.EMAIL_CERT_SEND, null, null, (bool isSuccess, GBException exception) => {
				if(isSuccess){
					PrintLog ("EMAIL_CERT_SEND is " + isSuccess);
				}
				else{
					if(exception != null)
						PrintLog ("EMAIL_CERT_SEND Error code = " + exception.getErrorCode () + "," + "message = " + exception.getErrorMessage ());
				}
			});
*/
		}

		GUI.EndGroup ();

		posY += BUTTON_HEIGHT * 2 + 50;

		GUI.BeginGroup (new Rect(groupPosX , posY, scrollContentsWidth - 100 , BUTTON_HEIGHT * 3));

		boxY = 0;
		GUI.Box(new Rect(0 , boxY, scrollContentsWidth - 100 , BUTTON_HEIGHT * 3), "Email Change", boxStyle);

		strEmail = GUI.TextField (new Rect (groupPosX, boxY += BUTTON_HEIGHT / 2, scrollContentsWidth - 200, BUTTON_HEIGHT), strEmail, textStyle);

		if(GUI.Button(new Rect(groupPosX , boxY += BUTTON_HEIGHT, scrollContentsWidth - 200, BUTTON_HEIGHT), "Change", buttonStyle)) {

			if (strEmail.Contains ("email"))
				return;
/*
			GBSessionManager.ManagementEmailAccount (EmailOperationType.EMAIL_CHANGE, strEmail, null, (bool isSuccess, GBException exception) => {
				if(isSuccess){
					PrintLog ("EMAIL_CHANGE is " + isSuccess);
				}
				else{
					if(exception != null)
						PrintLog ("EMAIL_CHANGE Error code = " + exception.getErrorCode () + "," + "message = " + exception.getErrorMessage ());
				}
			});
*/			
		}
			
		GUI.EndGroup ();

		posY += BUTTON_HEIGHT * 3 + 50;

		boxY = 0;
		GUI.BeginGroup (new Rect(groupPosX , posY, scrollContentsWidth - 100 , BUTTON_HEIGHT * 5));
		GUI.Box(new Rect(0 , boxY, scrollContentsWidth - 100 , BUTTON_HEIGHT * 5), "Email Password Change", boxStyle);
		strEmailPassword = GUI.TextField (new Rect (groupPosX, boxY += BUTTON_HEIGHT / 2, scrollContentsWidth - 200, BUTTON_HEIGHT), strEmailPassword, textStyle);
		strEmailJoinPassword_1 = GUI.TextField (new Rect (groupPosX, boxY += BUTTON_HEIGHT, scrollContentsWidth - 200, BUTTON_HEIGHT), strEmailJoinPassword_1, textStyle);
		strEmailJoinPassword_2 = GUI.TextField (new Rect (groupPosX, boxY += BUTTON_HEIGHT, scrollContentsWidth - 200, BUTTON_HEIGHT), strEmailJoinPassword_2, textStyle);
		if(GUI.Button(new Rect(groupPosX , boxY += BUTTON_HEIGHT, scrollContentsWidth - 200, BUTTON_HEIGHT), "Password Change", buttonStyle)) {

			if (strEmailPassword.Contains ("email") || strEmailJoinPassword_1.Contains ("email") || strEmailJoinPassword_2.Contains ("email"))
				return;

			if (!strEmailJoinPassword_1.Equals (strEmailJoinPassword_2)) {
				PrintLog ("password not equal");
				return;
			}
/*
			GBSessionManager.ManagementEmailAccount (EmailOperationType.EMAIL_CHECK_PASSWORD, null, strEmailPassword, (bool isSuccess, GBException exception) => {

				if(isSuccess){
					PrintLog ("EMAIL_CHECK_PASSWORD is " + isSuccess);

					GBSessionManager.ManagementEmailAccount (EmailOperationType.EMAIL_CHANGE_PASSWORD, null, strEmailJoinPassword_2, (bool isSuccess2, GBException exception2) => {
						if(isSuccess2){
							PrintLog ("EMAIL_CHANGE_PASSWORD is " + isSuccess);
						}
						else{
							if(exception2 != null)
								PrintLog ("EMAIL_CHANGE_PASSWORD Error code = " + exception2.getErrorCode () + "," + "message = " + exception2.getErrorMessage ());
						}
					});
				}
				else{
					if(exception != null)
						PrintLog ("EMAIL_CHECK_PASSWORD Error code = " + exception.getErrorCode () + "," + "message = " + exception.getErrorMessage ());
				}
			});
*/			
		}

		GUI.EndGroup ();

		posY += BUTTON_HEIGHT * 5 + 50;

		if(GUI.Button(new Rect(50, posY, scrollContentsWidth - 100, BUTTON_HEIGHT), "Cancel", buttonStyle)) {
			emailTestType = EmailTestType.NONE;
		}

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
