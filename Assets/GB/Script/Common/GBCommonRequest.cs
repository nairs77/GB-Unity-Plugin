using UnityEngine;
using System;
using System.Collections;
using SimpleJSON;

namespace GB {

	public class GBCommonRequest : GBRequest {
		
		public static void RequestGlobalServerInfo(string branchURL, Action<bool, string> callback)
		{
			JLog.verbose("Call - RequestGlobalServerInfo");

			GameObject gameObject = new GameObject("RequestGlobalServerInfo" + DateTime.Now.Ticks);
			GBCommonRequest request = gameObject.AddComponent<GBCommonRequest>();
			
			Action<bool,string> wrapperCallback = (success, result) => {
				JLog.verbose("Wrapper callback called");
				
				JSONNode root = JSON.Parse(result);
				var response = root[API_RESPONSE_RESULT_KEY];
							
				if (success) {
					// TODO : Change API
					//GBConfig.SetServerList(result);
					GBSettings.SetGBServers(result);					
					callback(success, result);	
				} else {
					callback(success, new GBException(response[API_RESPONSE_ERROR_KEY].ToString()).getErrorMessage());
				}
			};
			
			request.RequestGlobalServerInfoWithCallback(branchURL, wrapperCallback);
		}

		public static void SetActiveMarket(GBSettings.Market market, Action<bool, string> callback) {
			GameObject gameObject = new GameObject("GetRuntimePermission" + DateTime.Now.Ticks);
			GBCommonRequest request = gameObject.AddComponent<GBCommonRequest>();

			Action<bool,string> wrapperCallback = (success, result) => {

				callback(success, "");
			};

			request.SetActiveMarketWithCallback(market, wrapperCallback);			
		}

		public static void GetRuntimePermission(string permission, bool isNecessary, Action<bool, string> callback)
		{
			GameObject gameObject = new GameObject("GetRuntimePermission" + DateTime.Now.Ticks);
			GBCommonRequest request = gameObject.AddComponent<GBCommonRequest>();

			Action<bool,string> wrapperCallback = (success, result) => {

				JSONNode root = JSON.Parse(result);
				var response = root[API_RESPONSE_RESULT_KEY];

				if (success) {
					callback(success, response[API_RESPONSE_DATA_KEY].ToString());	
				} else {
					callback(success, response[API_RESPONSE_DATA_KEY].ToString());
				}
			};

			request.GetRuntimePermissionWithCallback(permission, isNecessary, wrapperCallback);
		}

		private void RequestGlobalServerInfoWithCallback(string branchURL, Action<bool, string> callback) {
			GBRequest callbackObject = createRequestCallbackObject(callback);
			GBManager.Instance.PluginManager.GetGlobalServerInfo(branchURL, callbackObject);
		}

		private void GetRuntimePermissionWithCallback(string permission, bool isNecessary, Action<bool, string> callback) {
			GBRequest callbackObject = createRequestCallbackObject(callback);

			GBManager.Instance.PluginManager.GetRuntimePermission (permission, isNecessary, callbackObject);
		}

		private void SetActiveMarketWithCallback(GBSettings.Market market, Action<bool, string> callback) {
			GBRequest callbackObject = createRequestCallbackObject(callback);
			GBManager.Instance.PluginManager.SetActiveMarket(market, callbackObject);
		}		
	}
}
