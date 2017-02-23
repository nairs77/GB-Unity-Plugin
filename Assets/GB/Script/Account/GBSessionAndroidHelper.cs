#if UNITY_ANDROID
using UnityEngine;
using System.Collections;
using GB;

namespace GB.Account {

	public class GBSessionAndroidHelper : GBAndroidHelper, ISessionHelper {
		private static readonly string NATIVE_SESSION_CLASS_PACKAGE = "com.gebros.platform.unity.AuthorizationPlugin";

		private static readonly string GET_HAS_TOKEN = "hasToken";
		private static readonly string GET_IS_OPENED = "isOpened";
		private static readonly string GET_ACCESS_TOKEN = "getAccessToken";
		private static readonly string GET_REFRESH_TOKEN = "getRefreshToken";
		private static readonly string GET_ALLOWED_EULA = "isAllowedEULA";
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
		
		public void SetAllowedEULA(bool isAllowed) { 
			AndroidSessionHelper.CallStatic(SET_ALLOWED_EULA, isAllowed);
		}

		public bool IsAlreadyLogin() {
			return AndroidSessionHelper.CallStatic<bool>(GET_IS_ALREAD_LOGIN);	
		}
		
		public bool HasToken() {
			return AndroidSessionHelper.CallStatic<bool>(GET_HAS_TOKEN);
		}

		public string GetAccessToken() {
			return AndroidSessionHelper.CallStatic<string> (GET_ACCESS_TOKEN);
		}

		public string GetRefreshToken() {
			return AndroidSessionHelper.CallStatic<string> (GET_REFRESH_TOKEN);
		}

		public void Login(AuthType authType, GBRequest callbackObject) {			

		  UnityActivity.Call("runOnUiThread", new AndroidJavaRunnable(() => {
			//if (authType.Equals(AuthType.NONE)) {
		  		AndroidSessionHelper.CallStatic("LoginWithType", authType.TypeValue, callbackObject.GetCallbackGameObjectName());				
			// if (authType == AuthType.NONE) {				
		  	// 	AndroidSessionHelper.CallStatic("Login", callbackObject.GetCallbackGameObjectName());
		  	// } else {
		  	// 	AndroidSessionHelper.CallStatic("LoginWithType", authType.TypeValue, callbackObject.GetCallbackGameObjectName());
		  	// }				
		  }));

		}

		public void Login(AuthType authType, string snsAccessToken, GBRequest callbackObject) {			
			
			UnityActivity.Call("runOnUiThread", new AndroidJavaRunnable(() => {
			//if (authType.Equals(AuthType.NONE)) {
			
			AndroidSessionHelper.CallStatic("LoginWithType", authType.TypeValue, snsAccessToken, callbackObject.GetCallbackGameObjectName());				
			// if (authType == AuthType.NONE) {
			// 	AndroidSessionHelper.CallStatic("Login", callbackObject.GetCallbackGameObjectName());
			// } else {
			// 	AndroidSessionHelper.CallStatic("LoginWithType", authType.TypeValue, snsAccessToken, callbackObject.GetCallbackGameObjectName());
			// }				
		}));
			
		}
/*		
		public void LoginByUI(GBRequest callbackObject) {
			UnityActivity.Call("runOnUiThread", new AndroidJavaRunnable (() => {
				AndroidSessionHelper.CallStatic("LoginByUI", callbackObject.GetCallbackGameObjectName());
			}));
		}

		public void LoginByUI(LoginUIType loginUIType, GBRequest callbackObject){
			UnityActivity.Call ("runOnUiThread", new AndroidJavaRunnable (() => {
				AndroidSessionHelper.CallStatic("LoginByUI", (int)loginUIType, callbackObject.GetCallbackGameObjectName());
			}));
		}
*/
		public void LinkServiceWithAuthType(AuthType authType, GBRequest callbackObject) {
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

		public void Unregister(GBRequest callbackObject) {
			UnityActivity.Call("runOnUiThread", new AndroidJavaRunnable(() => {
				AndroidSessionHelper.CallStatic("Unregister", callbackObject.GetCallbackGameObjectName());
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
