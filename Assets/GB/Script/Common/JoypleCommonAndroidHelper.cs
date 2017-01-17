#if UNITY_ANDROID
using UnityEngine;
using System.Collections;

namespace GB {
	public class GBCommonAndroidHelper : GBAndroidHelper, ICommonHelper {
		private static readonly string ANDROID_PLUGIN_CLASS_PACKAGE = "com.GB.platform.unity.GBUnityPlugin";
		private static readonly string ANDROID_UTILITY_PLUGIN_CLASS_PACKAGE = "com.GB.platform.unity.ApplicationPlugin";
		private static readonly string SET_ACTIVE_MARKET = "setActiveMarket";
		private static readonly string GB_SET_CONFIGURE = "configureWithGameInfo";
		private static readonly string GB_GLOBAL_SERVER_INFO = "requestGlobalServerInfo";
		private static readonly string GB_GET_MCC = "getMCC";
		private static readonly string GB_GET_LANGUAGE = "getCurrentLanguage";
		private static readonly string GB_GET_DEVICE_ID = "getDeviceId";
		private static readonly string GB_GET_DEVICE_MODEL = "getDeviceModel";
		private static readonly string GB_SHOW_TOAST = "ShowToast";
		private static readonly string GB_SHOW_ALERT = "ShowAlert";
		private static readonly string GB_GET_RUNTIME_PERMISSION = "GetRuntimePermission";
		private static readonly string GB_CHECK_RUNTIME_PERMISSION = "CheckRuntimePermission";
		
		public GBCommonAndroidHelper () {}

		public void SetActiveMarket(GBSettings.Market market, GBRequest funcCall) {
			UnityActivity.Call("runOnUiThread", new AndroidJavaRunnable(() => {
				using (AndroidJavaClass jc = new AndroidJavaClass(ANDROID_PLUGIN_CLASS_PACKAGE)) {
					jc.CallStatic(SET_ACTIVE_MARKET, GBSettings.GetPlatformInfo(), funcCall.GetCallbackGameObjectName());
				}
			}));
		}

		public void ConfigureSDKWithGameInfo(string clientSecretKey, int gameCode, GBSettings.Market market, int logLevel) {
			using (AndroidJavaClass jc = new AndroidJavaClass(ANDROID_PLUGIN_CLASS_PACKAGE) ) {
				jc.CallStatic(GB_SET_CONFIGURE, clientSecretKey, gameCode, market, logLevel);
			}	
		}

		public void ConfigureSDKWithGameInfo(string clientSecretKey, int gameCode, string platformInfo, int logLevel) {
			UnityActivity.Call("runOnUiThread", new AndroidJavaRunnable(() => {			
				using (AndroidJavaClass jc = new AndroidJavaClass(ANDROID_PLUGIN_CLASS_PACKAGE) ) {
					jc.CallStatic(GB_SET_CONFIGURE, clientSecretKey, gameCode, platformInfo, logLevel);
				}
			}));	
		}		
		
		public void RequestGlobalServerInfo(string branchURL, GBRequest funcCall) {
			UnityActivity.Call("runOnUiThread", new AndroidJavaRunnable(() => {
				using (AndroidJavaClass jc = new AndroidJavaClass(ANDROID_PLUGIN_CLASS_PACKAGE)) {
					jc.CallStatic(GB_GLOBAL_SERVER_INFO, branchURL, GBSettings.GameCode, funcCall.GetCallbackGameObjectName());
				}
			}));
		}

		public string GetMCC() {
			using (AndroidJavaClass jc = new AndroidJavaClass(ANDROID_UTILITY_PLUGIN_CLASS_PACKAGE)) {
				return jc.CallStatic<string>(GB_GET_MCC);
			}
		}
		
		public string GetCurrentLanguage() {
			using (AndroidJavaClass jc = new AndroidJavaClass(ANDROID_UTILITY_PLUGIN_CLASS_PACKAGE)) {
				return jc.CallStatic<string>(GB_GET_LANGUAGE);
			}
		}

		public string GetDeviceId() {
			using (AndroidJavaClass jc = new AndroidJavaClass(ANDROID_UTILITY_PLUGIN_CLASS_PACKAGE)) {
				return jc.CallStatic<string>(GB_GET_DEVICE_ID);
			}
		}

		public string GetDeviceModel() {
			using (AndroidJavaClass jc = new AndroidJavaClass(ANDROID_UTILITY_PLUGIN_CLASS_PACKAGE)) {
				return jc.CallStatic<string>(GB_GET_DEVICE_MODEL);
			}
		}

		public void ShowToast(string message) {
			UnityActivity.Call("runOnUiThread", new AndroidJavaRunnable(() => {
				using (AndroidJavaClass jc = new AndroidJavaClass(ANDROID_UTILITY_PLUGIN_CLASS_PACKAGE)) {
					jc.CallStatic(GB_SHOW_TOAST, message);
				}
			}));
		}

		public void ShowAlert() {
			UnityActivity.Call("runOnUiThread", new AndroidJavaRunnable(() => {
				using (AndroidJavaClass jc = new AndroidJavaClass(ANDROID_UTILITY_PLUGIN_CLASS_PACKAGE)) {
					jc.CallStatic(GB_SHOW_ALERT);
				}
			}));
		}

		public void GetRuntimePermission(string permission, bool isNecessary, GBRequest funcCall) {
			UnityActivity.Call("runOnUiThread", new AndroidJavaRunnable(() => {
				using (AndroidJavaClass jc = new AndroidJavaClass (ANDROID_PLUGIN_CLASS_PACKAGE)) {
					jc.CallStatic (GB_GET_RUNTIME_PERMISSION, permission, isNecessary, funcCall.GetCallbackGameObjectName());
				}
			}));
		}

		public bool CheckRuntimePermission(string permission) {
			using (AndroidJavaClass jc = new AndroidJavaClass(ANDROID_PLUGIN_CLASS_PACKAGE)) {
				return jc.CallStatic<bool>(GB_CHECK_RUNTIME_PERMISSION, permission);
			}
		}
		
		public void SendPushMessage(string userKey, string title, string message) {
			/* Dummy */
		}

		public void Call(GBEventParam param) {		
//			using (AndroidJavaClass jc = new AndroidJavaClass(ANDROID_PLUGIN_CLASS_PACKAGE) ) {
//				jc.CallStatic(GB_ON_UNITY_EVENT, param.ToString());
//			}
			
		}
	}
}

#endif
