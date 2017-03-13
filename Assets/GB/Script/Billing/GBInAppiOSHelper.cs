#if UNITY_IPHONE
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;

namespace GB.Billing {

	public class GBInAppiOSHelper : IIabHelper {

		public const string IOS_ATTR_INTERNAL = "__Internal";
		
		[DllImport (IOS_ATTR_INTERNAL)]
		public static extern void StartStoreService(string useKey, string callbackObjectName);

		[DllImport (IOS_ATTR_INTERNAL)]
		public static extern void RequestProducts(string skus, string callbackObjectName);

		[DllImport (IOS_ATTR_INTERNAL)]
		public static extern void RequestProductsInfo(string skus, string callbackObjectName);
		
		[DllImport (IOS_ATTR_INTERNAL)]
		public static extern void BuyItem(string sku, int price, string callbackObjectName);

		[DllImport (IOS_ATTR_INTERNAL)]
		public static extern void ReStoreItems(string callbackObjectName);

		public void StartSetup(string userKey, GBRequest callbackObject) {
			StartStoreService(userKey, callbackObject.GetCallbackGameObjectName());
		}

		public void QueryInventory(List<string> skus, GBRequest callbackObject) {
			string skuArray = string.Join(",", skus.ToArray());
			RequestProducts(skuArray, callbackObject.GetCallbackGameObjectName());
		}
		
		public void QueryInventoryItemInfo(List<string> skus, GBRequest callbackObject) {
			string skuArray = string.Join(",", skus.ToArray());
			RequestProductsInfo(skuArray, callbackObject.GetCallbackGameObjectName());
		}

		public void BuyItem(string sku, int price, GBRequest callbackObject) {
			BuyItem(sku, 0, callbackObject.GetCallbackGameObjectName());
		}
		
		public void BuyItem(string sku, int price, string itemInfo, GBRequest callbackObject) {
			BuyItem(sku, 0, callbackObject.GetCallbackGameObjectName());
		}
        
        public void BuyItem(string sku, int price, string itemInfo, string toUserkey, GBRequest callbackObject) {
            BuyItem(sku, 0, callbackObject.GetCallbackGameObjectName());
        }		

		public void RestoreItems(GBRequest callbackObject) {
			ReStoreItems(callbackObject.GetCallbackGameObjectName());
		}
				
		public void SetPromotionItems(List<string> skus) {
			return;
		}
		public void GetPromotionItem(string userKey, GBRequest callbackObject) {
			return;
		}
	}
}
#endif
