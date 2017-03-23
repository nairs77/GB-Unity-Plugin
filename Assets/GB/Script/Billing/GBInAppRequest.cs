using System;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using SimpleJSON;
using GB;

public class GBInAppRequest : GBRequest {

	public static readonly string TAG = "[GBInAppRequest]";

	public GBInAppRequest () {}

	public static void RequestStartSetup(string userKey, Action<bool, GBException> callback) {

		GameObject gameObject = new GameObject("RequestStartSetup" + DateTime.Now.Ticks);
		GBInAppRequest inAppRequest = gameObject.AddComponent<GBInAppRequest>();
		
		Action<bool,string> wrapperCallback = (success, result) => {
			GBLog.verbose(TAG + "Callback Start Billing Service");

			if (success) {							
				callback(success, null);
			} else {
				JSONNode root = JSON.Parse(result);				
				var response = root[API_RESPONSE_RESULT_KEY];
				callback(success, new GBException(response[API_RESPONSE_ERROR_KEY].ToString()));
			}
		};
		
		inAppRequest.RequestStartSetupWithCallback(userKey, wrapperCallback);
	}

	public static void RequestQueryInventory(List<string> skus, Action<List<string>, GBException> callback) {

		GameObject gameObject = new GameObject("RequestQueryInventory" + DateTime.Now.Ticks);
		GBInAppRequest inAppRequest = gameObject.AddComponent<GBInAppRequest>();
		
		Action<bool, string> wrapperCallback = (success, result) => {
			GBLog.verbose(TAG + "Callback Query Inventory ");

			JSONNode root = JSON.Parse(result);
			var response = root[API_RESPONSE_RESULT_KEY];
			
			if (success) {				
				string queryItems = response[API_RESPONSE_DATA_KEY]["valid"];
								
				List<string> validateIdentifiers = new List<string>(queryItems.Split(','));
				callback(validateIdentifiers, null);
			} else {
				callback(null, new GBException(response[API_RESPONSE_ERROR_KEY].ToString()));
			}			
		};
		
		inAppRequest.RequestQueryInventoryWithCallback(skus, wrapperCallback);
	}

	public static void RequestQueryInventory(List<string> skus, Action<GBInventory, GBException> callback) {

		GameObject gameObject = new GameObject("RequestQueryInventoryInfo" + DateTime.Now.Ticks);
		GBInAppRequest inAppRequest = gameObject.AddComponent<GBInAppRequest>();
		
		Action<bool, string> wrapperCallback = (success, result) => {
			GBLog.verbose(TAG + "Callback Query Item Info ");
			
			JSONNode root = JSON.Parse(result);
			var response = root[API_RESPONSE_RESULT_KEY];
							
			if (success) {
				var data = response[API_RESPONSE_DATA_KEY];				
				callback(new GBInventory(data), null);
			} else {
				callback(null, new GBException(response[API_RESPONSE_ERROR_KEY].ToString()));
			}
		};
		
		inAppRequest.RequestQueryInventoryItemWithCallback(skus, wrapperCallback);
	}
	
	public static void RequestBuyItem(string sku, int price, Action<string, GBException> callback) {
		GameObject oldBuyItemObject = GameObject.Find("RequestBuyItem");
		if (oldBuyItemObject) {
			GBLog.verbose(TAG + "have old object");
			GameObject.Destroy(oldBuyItemObject);
		}
		
		GameObject gameObject = new GameObject("RequestBuyItem");
		GBInAppRequest inAppRequest = gameObject.AddComponent<GBInAppRequest>();						
		
		Action<bool,string> wrapperCallback = (success, result) => {
			
			Debug.Log("result = " + result);

			JSONNode root = JSON.Parse(result);
			var response = root[API_RESPONSE_RESULT_KEY];
			var data = response[API_RESPONSE_DATA_KEY];
			
			GBLog.verbose("result = " + result);
			
			if (success) {
				string paymentKey = data["payment_key"];
				GBLog.verbose(@"payment key = " + paymentKey);				
				callback(paymentKey, null);
			} else {
				callback(null, new GBException(response[API_RESPONSE_ERROR_KEY].ToString()));
			}
		};
		
		inAppRequest.RequestBuyItemWithCallback(sku, price, wrapperCallback);
	}
	
	public static void RequestBuyItem(string sku, int price, string itemInfo, Action<string, GBException> callback) {
		GameObject oldBuyItemObject = GameObject.Find("RequestBuyItem");
		if (oldBuyItemObject) {
			GBLog.verbose(TAG + "have old object");
			GameObject.Destroy(oldBuyItemObject);
		}
		
		GameObject gameObject = new GameObject("RequestBuyItem");
		GBInAppRequest inAppRequest = gameObject.AddComponent<GBInAppRequest>();						
		
		Action<bool,string> wrapperCallback = (success, result) => {
			
			JSONNode root = JSON.Parse(result);
			var response = root[API_RESPONSE_RESULT_KEY];
			var data = response[API_RESPONSE_DATA_KEY];
			
			GBLog.verbose("result = " + result);
			
			if (success) {
				string paymentKey = data["payment_key"];
				//GBLog.verbose(@"payment key = " + paymentKey);				
				callback(paymentKey, null);
			} else {
				callback(null, new GBException(response[API_RESPONSE_ERROR_KEY].ToString()));
			}
		};
		
		inAppRequest.RequestBuyItemWithCallback(sku, price, itemInfo, wrapperCallback);
	}	

	public static void RequestRestoreItems(Action<List<string>, GBException> callback) {
		GameObject gameObject = new GameObject("RequestRestoreItems" + DateTime.Now.Ticks);
		GBInAppRequest inAppRequest = gameObject.AddComponent<GBInAppRequest>();
		
		Action<bool,string> wrapperCallback = (success, result) => {

			GBLog.verbose(TAG + "Callback Restore Items...!!!");
			GBLog.verbose(TAG + "result = " + result);
			JSONNode root = JSON.Parse(result);
			var response = root[API_RESPONSE_RESULT_KEY];
			var data = response[API_RESPONSE_DATA_KEY];
					
			if (success) {
				string restoreItems = data["restore_keys"];
				
				List<string> restoreKeys = new List<string>(restoreItems.Split(','));
				callback(restoreKeys, null);
			} else {
				callback(null, new GBException(response[API_RESPONSE_ERROR_KEY].ToString()));
			}			
		};
		
		inAppRequest.RequestRestoreItemsWithCallback(wrapperCallback);
	}
	
	public static void RequestPromotionItem(string userKey, Action<string, GBException> callback) {
		GameObject gameObject = new GameObject("RequestPromotionItem" + DateTime.Now.Ticks);
		GBInAppRequest inAppRequest = gameObject.AddComponent<GBInAppRequest>();
		
		Action<bool,string> wrapperCallback = (success, result) => {

			GBLog.verbose(TAG + "Callback Restore Items...!!!");
			JSONNode root = JSON.Parse(result);
			var response = root[API_RESPONSE_RESULT_KEY];
			var data = response[API_RESPONSE_DATA_KEY];
			
			GBLog.verbose("result = " + result);
			
			if (success) {
				string paymentKey = data["payment_key"];
				GBLog.verbose(@"payment key = " + paymentKey);				
				callback(paymentKey, null);
			} else {
				callback(null, new GBException(response[API_RESPONSE_ERROR_KEY].ToString()));
			}						
		};
		
		inAppRequest.RequestPromotionItemWithCallback(userKey, wrapperCallback);		
	}
	private void RequestStartSetupWithCallback(string userKey, Action<bool, string> callback) {
		GBRequest callbackObject = createRequestCallbackObject(callback);
		GBManager.Instance.PluginManager.StartSetup(userKey, callbackObject);		
	}

	private void RequestQueryInventoryWithCallback(List<string> skus, Action<bool, string> callback) {
		GBRequest callbackObject = createRequestCallbackObject(callback);
		GBManager.Instance.PluginManager.QueryInventory(skus, callbackObject);				
	}
	
	private void RequestQueryInventoryItemWithCallback(List<string> skus, Action<bool, string> callback) {
		GBRequest callbackObject = createRequestCallbackObject(callback);
		GBManager.Instance.PluginManager.QueryInventoryItemInfo(skus, callbackObject);				
	}	

	private void RequestBuyItemWithCallback(string sku, int price, Action<bool, string> callback) {
		GBRequest callbackObject = createRequestCallbackObject(callback);
		GBManager.Instance.PluginManager.BuyItem(sku, price, callbackObject);		
	}
	
	private void RequestBuyItemWithCallback(string sku, int price, string itemInfo, Action<bool, string> callback) {
		GBRequest callbackObject = createRequestCallbackObject(callback);
		GBManager.Instance.PluginManager.BuyItem(sku, price, itemInfo, callbackObject);		
	}	

	private void RequestRestoreItemsWithCallback(Action<bool, string> callback) {
		GBRequest callbackObject = createRequestCallbackObject(callback);
		GBManager.Instance.PluginManager.RestoreItems(callbackObject);				
/*		
		functionCallback = callback;
		string gameObjectName = "RequestRestoreItemsWithCallback."+DateTime.Now.Ticks;
		callbackGameObjectName = gameObjectName;
		
		// This allows us to track unique calls to async native code
		
		#if !UNITY_EDITOR
		GameObject gameObject = new GameObject(gameObjectName);
		DontDestroyOnLoad(gameObject);
		
		GBInAppRequest createdCallBackObject = gameObject.AddComponent<GBInAppRequest>();
		createdCallBackObject.functionCallback = callback;
		createdCallBackObject.callbackGameObjectName = callbackGameObjectName;
		
		GBManager.Instance.PluginManager.RestoreItems(createdCallBackObject);
		#else
		asyncCallFailed("GBNativeCal are not supported in the Unity editor");
		#endif	
*/		
	}
	
	private void RequestPromotionItemWithCallback(string userKey, Action<bool, string> callback) {
		GBRequest callbackObject = createRequestCallbackObject(callback);
		GBManager.Instance.PluginManager.GetPromotionItem(userKey, callbackObject);				
/*		
		functionCallback = callback;
		string gameObjectName = "RequestPromotionItemsWithCallback."+DateTime.Now.Ticks;
		callbackGameObjectName = gameObjectName;
		
		// This allows us to track unique calls to async native code
		
		#if !UNITY_EDITOR && UNITY_ANDROID
		GameObject gameObject = new GameObject(gameObjectName);
		DontDestroyOnLoad(gameObject);
		
		GBInAppRequest createdCallBackObject = gameObject.AddComponent<GBInAppRequest>();
		createdCallBackObject.functionCallback = callback;
		createdCallBackObject.callbackGameObjectName = callbackGameObjectName;
		
		GBManager.Instance.PluginManager.GetPromotionItem(userKey, createdCallBackObject);
		#else
		asyncCallFailed("Promotion item are not supported in the Unity editor / iPhone");
		#endif
*/		
	}	
    
    private void RequestBuyItemWithCallback(string sku, int price, string itemInfo, string toUserkey, Action<bool, string> callback) {
		GBRequest callbackObject = createRequestCallbackObject(callback);
		GBManager.Instance.PluginManager.BuyItem(sku, price, itemInfo, callbackObject);				
    }
}
