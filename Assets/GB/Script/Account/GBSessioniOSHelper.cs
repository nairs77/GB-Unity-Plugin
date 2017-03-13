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

		// [DllImport ("__Internal")]
		// public static extern bool setAllowedEULA();

		[DllImport ("__Internal")]
		public static extern string getActiveSession();
	
		[DllImport ("__Internal")]
		public static extern void Login(string callbackObjectName);

		[DllImport ("__Internal")]
		public static extern void LoginWithAuthType(int authType, string callbackObjectName);

		[DllImport ("__Internal")]
		public static extern void ConnectChannel(int authType, string callbackObjectName);

		[DllImport ("__Internal")]
		public static extern void Logout(string callbackObjectName);

		
		public bool IsOpened() {
			return isOpened();
		}
		
		public bool IsAllowedEULA() {
			return isAllowedEULA();
		}

		public bool IsReady() {
			return isReady(); 
		}
	
		// public void SetAllowedEULA(bool isAllowed) {
		// 	setAllowedEULA(isAllowed);
		// }
		
		public string GetActiveSession() {
			return getActiveSession();
		}
		
		// public void SetGameLanguage(LanguageType languageType) {
		// 	setGameLanguage((int)languageType);
		// }

		public void Login(GBRequest callbackObject) {
			Login(callbackObject.GetCallbackGameObjectName());
		}

		public void LoginWithAuthType(AuthType authType, GBRequest callbackObject) {
			LoginWithAuthType(authType.TypeValue, callbackObject.GetCallbackGameObjectName());
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
	}
}
#endif
