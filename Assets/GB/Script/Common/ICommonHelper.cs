using UnityEngine;
using System.Collections;

namespace GB {

	public interface ICommonHelper {
		void SetActiveMarket(GBSettings.Market market, GBRequest callbackObject);
		void ConfigureSDKWithGameInfo(string clientSecretKey, int gameCode, GBSettings.Market market, int logLevel);
		void ConfigureSDKWithGameInfo(string clientSecretKey, int gameCode, string platformInfo, int logLevel);
		void RequestGlobalServerInfo(string branchURL, GBRequest callbackObject);
		void ShowToast(string message);
		void ShowAlert();
		string GetMCC();
		string GetCurrentLanguage();
		string GetDeviceId();
		string GetDeviceModel();
		void SendPushMessage(string userKey, string title, string message);
		void GetRuntimePermission (string permission, bool isNecessary, GBRequest callbackObject);
		bool CheckRuntimePermission (string permission);
	}

}


