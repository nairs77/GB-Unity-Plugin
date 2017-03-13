using UnityEngine;
using System.Collections;

namespace GB {

	public interface ICommonHelper {
		void SetActiveMarket(GBSettings.Market market, GBRequest callbackObject);
		void ConfigureSDKWithGameInfo(string clientSecretKey, int gameCode, GBSettings.Market market, int logLevel);
		void ConfigureSDKWithGameInfo(string clientSecretKey, int gameCode, string platformInfo, int logLevel);
		string GetMCC();
		string GetCurrentLanguage();
		string GetDeviceId();
		string GetDeviceModel();
	}

}


