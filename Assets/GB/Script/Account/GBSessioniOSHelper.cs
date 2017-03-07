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
		public static extern bool isReady();
		
		[DllImport ("__Internal")]
		public static extern bool isConnectedChannel();

		[DllImport ("__Internal")]
		public static extern void setAllowedEULA(bool isAllowed);
		
		[DllImport ("__Internal")]
		public static extern void Login(string callbackObjectName);

		[DllImport ("__Internal")]
		public static extern void Login(int authType, string callbackObjectName);

		[DllImport ("__Internal")]
		public static extern void ConnectChannel(int authType, string callbackObjectName);

		[DllImport ("__Internal")]
		public static extern void Logout(string callbackObjectName);

		[DllImport ("__Internal")]
		public static extern void ShowClickWrap(string callbackObjectName);

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

		public bool IsReady() {
			return isReady(); 
		}

		public bool IsConnectedChannel() {
			return isConnectedChannel();
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

		public void Login(GBRequest callbackObject) {
			Login(callbackObject.GetCallbackGameObjectName());
		}

		public void Login(AuthType authType, GBRequest callbackObject) {
			Login(authType.TypeValue, callbackObject.GetCallbackGameObjectName());
		}
/*
		public void LoginByUI(GBRequest callbackObject) {
			LoginByNativeUI(callbackObject.GetCallbackGameObjectName());
		}

		public void LoginByUI(LoginUIType loginUIType, GBRequest callbackObject) {
			LoginByNativeUIWithViewType((int)loginUIType, callbackObject.GetCallbackGameObjectName());
		}
*/
		public void ConnectChannel(AuthType authType, GBRequest callbackObject) {
			ConnectChannel(authType.TypeValue, callbackObject.GetCallbackGameObjectName());
		}
/*
		public void LinkServiceWithAuthType(AuthType authType, string snsAccessToken, GBRequest callbackObject) {
			LinkNativeServiceWithAuthTypeWithToken(authType.TypeValue, snsAccessToken, callbackObject.GetCallbackGameObjectName());
		}
*/
		public void Logout(GBRequest callbackObject) {
			Logout(callbackObject.GetCallbackGameObjectName());
		}

		public void ShowEULA() {
			ShowNativeEULA();
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
