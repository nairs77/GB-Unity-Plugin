#if UNITY_IPHONE
using UnityEngine;
using System.Collections;
using System.Runtime.InteropServices;

namespace GB {

	public class GBCommoniOSHelper : ICommonHelper {

		public const string IOS_ATTR_INTERNAL = "__Internal";
		
		[DllImport (IOS_ATTR_INTERNAL)]
		public static extern void ConfigureSDKWithGameInfo(string clientScret, int gameCode, int market, int logLevel);
		
		[DllImport (IOS_ATTR_INTERNAL)]
		public static extern void RequestGlobalServerInfo(string branchURL, string gameObjectName);

		[DllImport (IOS_ATTR_INTERNAL)]
		public static extern string GetMobileCountryCode();

		[DllImport (IOS_ATTR_INTERNAL)]
		public static extern string GetLanguage();

		[DllImport (IOS_ATTR_INTERNAL)]
		public static extern string GetDeviceUniqueId();

		[DllImport (IOS_ATTR_INTERNAL)]
		public static extern string GetDeviceModelName();

		[DllImport (IOS_ATTR_INTERNAL)]
		public static extern void ShowToastMessage(string message);

		[DllImport (IOS_ATTR_INTERNAL)]
		public static extern void ShowAlertMessage();

		[DllImport (IOS_ATTR_INTERNAL)]
		public static extern void RequestPushMessage(string useKey, string title, string message);

		public void SetActiveMarket(GBSettings.Market market, GBRequest funcCall) {
			funcCall.asyncCallSucceeded("");	
		}

		public void ConfigureSDKWithGameInfo(string clientSecretKey, int gameCode, GBSettings.Market market, int logLevel) {
			ConfigureSDKWithGameInfo(clientSecretKey, gameCode, 0, (int)logLevel);
		}

		public void ConfigureSDKWithGameInfo(string clientSecretKey, int gameCode, string platformType, int logLevel) {
			ConfigureSDKWithGameInfo(clientSecretKey, gameCode, 0, (int)logLevel);
		}		 
		

		/**
		 *  Global인 경우에 Configure 이후에 Server Info를 호출하나, 중국 버전에서는 Server Info 호출 이후에 Configure 하도록 로직 수정
		 *  하지만 기존 Global과 혼용할 수 있도록 하기 위해서 수정 (GBForUnity에서 Configure 관련 중복 호출이 안되도록 flag 처리)
		 */
		public void RequestGlobalServerInfo(string branchURL, GBRequest funcCall) {
			ConfigureSDKWithGameInfo(GBSettings.AppKey, GBSettings.GameCode, 0, (int)GBSettings.LogLevel.RELEASE);			
			RequestGlobalServerInfo (branchURL, funcCall.GetCallbackGameObjectName ());
		}

		public string GetMCC() {
			return GetMobileCountryCode();
		}

		public string GetCurrentLanguage() {
			return GetLanguage();
		}

		public string GetDeviceId() {
			return GetDeviceUniqueId();
		}

		public string GetDeviceModel() {
			return GetDeviceModelName();
		}

		public void ShowToast(string message) {
			ShowToastMessage(message);
		}

		public void ShowAlert() {
			ShowAlertMessage();
		}
		
		public void SendPushMessage(string userKey, string title, string message) {
			RequestPushMessage(userKey, title, message);
		}

		public void GetRuntimePermission (string permission, bool isNecessary, GBRequest funcCall) {
			/* Dummy */
		}

		public bool CheckRuntimePermission(string permission) {
			// Alway True
			return true;
		}
	}
}
#endif

