#if UNITY_ANDROID
using UnityEngine;
using System.Collections;
using GB;

namespace GB.Account {

	public class GBSessionAndroidHelper : GBAndroidHelper, ISessionHelper {
		private static readonly string NATIVE_SESSION_CLASS_PACKAGE = "com.gebros.platform.unity.AuthorizationPlugin";

		private static readonly string GET_IS_READY = "isReady";
		private static readonly string GET_IS_OPENED = "isOpened";
		private static readonly string GET_ALLOWED_EULA = "isAllowedEULA";

		private static readonly string GET_IS_CONNECT_CHANNEL = "isConnectedChannel";
		private static readonly string GET_ACTIVE_SESSION = "getActiveSession";
		private static readonly string SET_ALLOWED_EULA = "setAllowedEULA";
		private static readonly string GET_IS_ALREAD_LOGIN = "isAlreadyLogin";
		private static readonly string GB_GAME_LANGUAGE = "setGameLanguage";			

		private static AndroidJavaClass _androidSessionHelper;
		private static AndroidJavaClass AndroidSessionHelper {
			get {
				if (_androidSessionHelper == null) {
					_androidSessionHelper = new AndroidJavaClass(NATIVE_SESSION_CLASS_PACKAGE);
				}
				return _androidSessionHelper;
			}
		}

		public bool IsOpened() {
			return AndroidSessionHelper.CallStatic<bool>(GET_IS_OPENED);
		}
		
		public bool IsAllowedEULA() {
			return AndroidSessionHelper.CallStatic<bool>(GET_ALLOWED_EULA);
		}
		
		// public bool IsConnectedChannel() {
		// 	return AndroidSessionHelper.CallStatic<bool>(GET_IS_CONNECT_CHANNEL);
		// }

		public string GetActiveSession() {
			return AndroidSessionHelper.CallStatic<string>(GET_ACTIVE_SESSION);
		}
		public void SetAllowedEULA(bool isAllowed) { 
			AndroidSessionHelper.CallStatic(SET_ALLOWED_EULA, isAllowed);
		}

		public bool IsAlreadyLogin() {
			return AndroidSessionHelper.CallStatic<bool>(GET_IS_ALREAD_LOGIN);	
		}
		
		public bool IsReady() {
			return AndroidSessionHelper.CallStatic<bool>(GET_IS_READY);
		}

		public void Login(AuthType authType, GBRequest callbackObject) {			

		  UnityActivity.Call("runOnUiThread", new AndroidJavaRunnable(() => {
		  		AndroidSessionHelper.CallStatic("LoginWithType", authType.TypeValue, callbackObject.GetCallbackGameObjectName());				
		  }));

		}

		public void Login(GBRequest callbackObject) {						
			UnityActivity.Call("runOnUiThread", new AndroidJavaRunnable(() => {
				AndroidSessionHelper.CallStatic("Login", callbackObject.GetCallbackGameObjectName());				
			}));
		}
			
		public void ConnectChannel(AuthType authType, GBRequest callbackObject) {
			UnityActivity.Call("runOnUiThread", new AndroidJavaRunnable (() => {
				AndroidSessionHelper.CallStatic("LinkServiceWithAuthType", authType.TypeValue, callbackObject.GetCallbackGameObjectName());
			}));				
		}
/*
		public void LinkServiceWithAuthType(AuthType authType, string snsAccessToken, GBRequest callbackObject) {
			UnityActivity.Call("runOnUiThread", new AndroidJavaRunnable (() => {
				AndroidSessionHelper.CallStatic("LinkServiceWithAuthType", authType.TypeValue, snsAccessToken, callbackObject.GetCallbackGameObjectName());
			}));				
		}
*/		
		public void Logout(GBRequest callbackObject) {
			UnityActivity.Call ("runOnUiThread", new AndroidJavaRunnable (() => {
				AndroidSessionHelper.CallStatic ("Logout", callbackObject.GetCallbackGameObjectName ());
			}));
		}
/*
		public void RequestProfile(GBRequest callbackObject) {
			UnityActivity.Call("runOnUiThread", new AndroidJavaRunnable(() => {
				AndroidSessionHelper.CallStatic ("RequestProfile", callbackObject.GetCallbackGameObjectName());
			}));
		}

		public void ShowGBMain() {
			UnityActivity.Call("runOnUiThread", new AndroidJavaRunnable(() => {
				AndroidSessionHelper.CallStatic("ShowGBMain");
			}));
		}

		public void ShowClickWrap(GBRequest callbackObject) {
			UnityActivity.Call("runOnUiThread", new AndroidJavaRunnable(() => {
				AndroidSessionHelper.CallStatic("ShowClickWrap", callbackObject.GetCallbackGameObjectName());
			}));
		}
*/
		public void HideGBStart() {
			UnityActivity.Call ("runOnUiThread", new AndroidJavaRunnable (() => {
				AndroidSessionHelper.CallStatic("HideGBStart");
			}));
			
		}

		public void ShowEULA() {
			UnityActivity.Call("runOnUiThread", new AndroidJavaRunnable(() => {
				AndroidSessionHelper.CallStatic("ShowEULA");
			}));
		}
			
		public void SetGameLanguage(LanguageType type) {
			AndroidSessionHelper.CallStatic(GB_GAME_LANGUAGE, (int)type);
			//  using (AndroidJavaClass jc = new AndroidJavaClass(ANDROID_UTILITY_PLUGIN_CLASS_PACKAGE)) {
			//  	jc.CallStatic(GB_GAME_LANGUAGE);
			//  }
		}
	}
}
#endif
