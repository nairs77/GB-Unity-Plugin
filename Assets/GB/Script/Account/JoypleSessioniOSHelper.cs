#if UNITY_IPHONE
using UnityEngine;
using System.Collections;
using System.Runtime.InteropServices;

namespace GB.Account {

	public class GBSessioniOSHelper : ISessionHelper
	{

		[DllImport ("__Internal")]
		public static extern bool isOpened();
		
		[DllImport ("__Internal")]
		public static extern bool isAllowedEULA();
		
		[DllImport ("__Internal")]
		public static extern bool hasAccount();
		
		[DllImport ("__Internal")]
		public static extern string getAccessToken();
		
		[DllImport ("__Internal")]
		public static extern string getRefreshToken();	

		[DllImport ("__Internal")]
		public static extern void setAllowedEULA(bool isAllowed);
		
		[DllImport ("__Internal")]
		public static extern void Login(int authType, string callbackObjectName);

		[DllImport ("__Internal")]
		public static extern void LoginWithToken(int authType, string snsAccessToken, string callbackObjectName);

		[DllImport ("__Internal")]
		public static extern void LoginByNativeUI(string callbackObjectName);

		[DllImport ("__Internal")]
		public static extern void LoginByNativeUIWithViewType(int loginUIType ,string callbackObjectName);

		[DllImport ("__Internal")]
		public static extern void LinkNativeServiceWithAuthType(int authType, string callbackObjectName);

		[DllImport ("__Internal")]
		public static extern void LinkNativeServiceWithAuthTypeWithToken(int authType, string snsAccessToken, string callbackObjectName);

		[DllImport ("__Internal")]
		public static extern void Logout(string callbackObjectName);

		[DllImport ("__Internal")]
		public static extern void Unregister(string callbackObjectName);

		[DllImport ("__Internal")]
		public static extern void RequestProfile(string callbackObjectName);

		[DllImport ("__Internal")]
		public static extern void ShowMain();

		[DllImport ("__Internal")]
//		public static extern void ShowClickWrap(string callbackObjectName);

		[DllImport ("__Internal")]
		public static extern void ShowNativeEULA();
		
		[DllImport ("__Internal")]
		public static extern void ShowNativeViewByType(int viewType);

		[DllImport ("__Internal")]
		public static extern bool isAlreadyLogin();
		
		[DllImport ("__Internal")]
		public static extern void setGameLanguage(int languageType);

		[DllImport ("__Internal")]
		public static extern void requestMergeAccount (string userkey, string callbackObjectName);

		[DllImport ("__Internal")]
		public static extern void requestVerifyAccount(int authType, string callbackObjectName);

		[DllImport ("__Internal")]
		public static extern void checkExistAccount(int authType, string email, string userId, string callbackObjectName);

		
		public bool IsOpened() {
			return isOpened();
		}
		
		public bool IsAllowedEULA() {
			return isAllowedEULA();
		}

		public bool HasToken() {
			return hasAccount(); 
		}

		public string GetAccessToken() {
			return getAccessToken();
		}
		
		public string GetRefreshToken() {
			return getRefreshToken(); 
		}
		
		public void SetAllowedEULA(bool isAllowed) {
			setAllowedEULA(isAllowed);
		}
		
		public bool IsAlreadyLogin() {
			return isAlreadyLogin();
		}
		
		public void SetGameLanguage(LanguageType languageType) {
			setGameLanguage((int)languageType);
		}

		public void Login(AuthType authType, GBRequest callbackObject) {
			Login(authType.TypeValue, callbackObject.GetCallbackGameObjectName());
		}

		public void Login(AuthType authType, string snsAccessToken, GBRequest callbackObject) {
			LoginWithToken(authType.TypeValue, snsAccessToken, callbackObject.GetCallbackGameObjectName());
		}

		public void LoginWithType(AuthType authType, GBRequest callbackObject) {
			Login (authType.TypeValue, callbackObject.GetCallbackGameObjectName());
		}
/*
		public void LoginByUI(GBRequest callbackObject) {
			LoginByNativeUI(callbackObject.GetCallbackGameObjectName());
		}

		public void LoginByUI(LoginUIType loginUIType, GBRequest callbackObject) {
			LoginByNativeUIWithViewType((int)loginUIType, callbackObject.GetCallbackGameObjectName());
		}
*/
		public void LinkServiceWithAuthType(AuthType authType, GBRequest callbackObject) {
			LinkNativeServiceWithAuthType(authType.TypeValue, callbackObject.GetCallbackGameObjectName());
		}
/*
		public void LinkServiceWithAuthType(AuthType authType, string snsAccessToken, GBRequest callbackObject) {
			LinkNativeServiceWithAuthTypeWithToken(authType.TypeValue, snsAccessToken, callbackObject.GetCallbackGameObjectName());
		}
*/
		public void Logout(GBRequest callbackObject) {
			Logout(callbackObject.GetCallbackGameObjectName());
		}

		public void Unregister(GBRequest callbackObject) {
			Unregister(callbackObject.GetCallbackGameObjectName());
		}
/*
		public void RequestProfile(GBRequest callbackObject) {
			RequestProfile(callbackObject.GetCallbackGameObjectName());
		}

		public void ShowGBMain() {
			ShowMain();
		}

		public void ShowClickWrap(GBRequest callbackObject) {
			ShowClickWrap(callbackObject.GetCallbackGameObjectName());
		}
*/
		public void ShowEULA() {
			ShowNativeEULA();
		}
		
		public void ShowViewByType(GBProfileViewType type) {
			ShowNativeViewByType((int)type);
		}
		
		public void HideGBStart() {
			
		}

		public void RequestMergeAccount(string userkey ,GBRequest callbackObject) {
			requestMergeAccount(userkey, callbackObject.GetCallbackGameObjectName());
		}

		public void RequestVerifyAccount(AuthType authType ,GBRequest callbackObject) {
			requestVerifyAccount(authType.TypeValue, callbackObject.GetCallbackGameObjectName());
		}

		public void CheckExistAccount(AuthType authType, string email, string userId, GBRequest callbackObject) {
			checkExistAccount(authType.TypeValue, email, userId, callbackObject.GetCallbackGameObjectName());
		}

	}
}
#endif
