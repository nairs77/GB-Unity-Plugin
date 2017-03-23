﻿#if UNITY_ANDROID
using UnityEngine;
using System.Collections.Generic;
using SimpleJSON;

namespace GB.Billing {

	public class GBInAppAndroidHelper : GBAndroidHelper, IIabHelper {
		
		private static readonly string ANDROID_BILLING_PLUGIN_CLASS_PACKAGE = "com.gebros.platform.unity.BillingPlugin";
		private static readonly string IN_APP_START_BILLING = "StartSetup";
		private static readonly string IN_APP_QUERY_INVENTORY = "QueryInventory";
		private static readonly string IN_APP_QUERY_INVENTORY_ITEM_INFO = "QueryInventoryItemInfo";
		private static readonly string IN_APP_BUY_ITEM = "BuyItem";
		private static readonly string IN_APP_RESTORE_ITEM = "RestoreItems";
		private static readonly string IN_APP_GET_PROMOTION_ITEM = "GetPromotionItem";
		private static readonly string IN_APP_SET_PROMOTION_ITEM = "SetPromotionItems";

		private static AndroidJavaClass _inAppHelper;
		private static AndroidJavaClass InAppHelper {
			get {
				if (_inAppHelper == null) {
					_inAppHelper = new AndroidJavaClass(ANDROID_BILLING_PLUGIN_CLASS_PACKAGE);
				}
				return _inAppHelper;
			}
		}
		
		public GBInAppAndroidHelper () {}

		public void StartSetup(string userKey, GBRequest funcCallback) {
			UnityActivity.Call ("runOnUiThread", new AndroidJavaRunnable (() => {
				InAppHelper.CallStatic (IN_APP_START_BILLING, userKey, funcCallback.GetCallbackGameObjectName ());
			}));
		}

		public void QueryInventory(List<string> skus, GBRequest funcCallback) {
			string skuArray = string.Join(",", skus.ToArray());

			UnityActivity.Call ("runOnUiThread", new AndroidJavaRunnable (() => {
				InAppHelper.CallStatic (IN_APP_QUERY_INVENTORY, skuArray, funcCallback.GetCallbackGameObjectName ());
			}));
		}
		
		public void QueryInventoryItemInfo(List<string> skus, GBRequest funcCallback) {
			string skuArray = string.Join(",", skus.ToArray());

			UnityActivity.Call ("runOnUiThread", new AndroidJavaRunnable (() => {
				InAppHelper.CallStatic (IN_APP_QUERY_INVENTORY_ITEM_INFO, skuArray, funcCallback.GetCallbackGameObjectName ());
			}));
		}
		
		public void BuyItem(string sku, int price, GBRequest funcCallback) {
			UnityActivity.Call ("runOnUiThread", new AndroidJavaRunnable (() => {
				InAppHelper.CallStatic (IN_APP_BUY_ITEM, sku, price, funcCallback.GetCallbackGameObjectName ());
			}));
		}
		
		public void BuyItem(string sku, int price, string itemInfo, GBRequest funcCallback) {
			UnityActivity.Call ("runOnUiThread", new AndroidJavaRunnable (() => {
				//InAppHelper.CallStatic (IN_APP_BUY_ITEM, sku, price, itemInfo, funcCallback.GetCallbackGameObjectName ());
                string param = _makeInAppJSONParam(sku, price, itemInfo, "");                
                InAppHelper.CallStatic(IN_APP_BUY_ITEM, param, funcCallback.GetCallbackGameObjectName());
			}));
		}

		public void RestoreItems(GBRequest funcCallback) {
			UnityActivity.Call ("runOnUiThread", new AndroidJavaRunnable (() => {
				InAppHelper.CallStatic (IN_APP_RESTORE_ITEM, funcCallback.GetCallbackGameObjectName ());
			}));						
		}
		
		public void GetPromotionItem(string userKey, GBRequest funcCallback) {
			UnityActivity.Call ("runOnUiThread", new AndroidJavaRunnable (() => {
				InAppHelper.CallStatic (IN_APP_GET_PROMOTION_ITEM, userKey, funcCallback.GetCallbackGameObjectName ());
			}));									
		}
		
		public void SetPromotionItems(List<string> skus) {
			string skuArray = string.Join(",", skus.ToArray());
			InAppHelper.CallStatic(IN_APP_SET_PROMOTION_ITEM, skuArray);
		}
        
        private string _makeInAppJSONParam(string sku, int price, string itemInfo, string toUserkey) {
            JSONNode root = new JSONClass();
            JSONNode item = new JSONClass();

			// TODO : 중국이랑 Global이랑 key / value가 다름... 나중에 합쳐야지... 생각 못했음... ㅠ.ㅠ
			// GBForUnity내에서 데이터를 parsing 할 때 key / value가 다름.
			if (GBSettings.GetActiveMarket() == GBSettings.Market.GOOGLE ||
				GBSettings.GetActiveMarket() == GBSettings.Market.MYCARD ||
				GBSettings.GetActiveMarket() == GBSettings.Market.ONESTORE) {
				item["sku"] = sku;
				item["price"].AsInt = price;
				item["info"] = itemInfo;
			} else {
				item["product_id"] = sku;
				item["product_price"].AsInt = price;
				item["product_name"] = itemInfo;
			}
			root.Add("item", item);
			root["to"] = toUserkey;
            
           return root.ToString();
        }
	}
}
#endif
