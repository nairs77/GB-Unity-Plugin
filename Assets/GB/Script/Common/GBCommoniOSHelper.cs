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
		public static extern string GetMobileCountryCode();

		[DllImport (IOS_ATTR_INTERNAL)]
		public static extern string GetLanguage();

		[DllImport (IOS_ATTR_INTERNAL)]
		public static extern string GetDeviceUniqueId();

		[DllImport (IOS_ATTR_INTERNAL)]
		public static extern string GetDeviceModelName();

		public void SetActiveMarket(GBSettings.Market market, GBRequest funcCall) {
			funcCall.asyncCallSucceeded("");	
		}

		public void ConfigureSDKWithGameInfo(string clientSecretKey, int gameCode, GBSettings.Market market, int logLevel) {
			ConfigureSDKWithGameInfo(clientSecretKey, gameCode, 0, (int)logLevel);
		}

		public void ConfigureSDKWithGameInfo(string clientSecretKey, int gameCode, string platformType, int logLevel) {
			ConfigureSDKWithGameInfo(clientSecretKey, gameCode, 0, (int)logLevel);
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
	}
}
#endif

