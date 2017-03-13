using UnityEngine;
using System;
using System.Collections;
using SimpleJSON;

namespace GB {

	public class GBCommonRequest : GBRequest {
		
		public static void SetActiveMarket(GBSettings.Market market, Action<bool, string> callback) {
			GameObject gameObject = new GameObject("GetRuntimePermission" + DateTime.Now.Ticks);
			GBCommonRequest request = gameObject.AddComponent<GBCommonRequest>();

			Action<bool,string> wrapperCallback = (success, result) => {

				callback(success, "");
			};

			request.SetActiveMarketWithCallback(market, wrapperCallback);			
		}

		private void SetActiveMarketWithCallback(GBSettings.Market market, Action<bool, string> callback) {
			GBRequest callbackObject = createRequestCallbackObject(callback);
			GBManager.Instance.PluginManager.SetActiveMarket(market, callbackObject);
		}		
	}
}
