using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using GB;

namespace GB.Billing {

	public interface IIabHelper {

		void StartSetup(string userKey, GBRequest callbackObject);
		void QueryInventory(List<string> skus, GBRequest callbackObject);
		void QueryInventoryItemInfo(List<string> skus, GBRequest callbackObject);
		void BuyItem(string userKey, string sku, int price, GBRequest callbackObject);		
		void BuyItem(string userKey, string sku, int price, string itemInfo, GBRequest callbackObject);
        
		void RestoreItems(GBRequest callbackObject);
		void GetPromotionItem(string userKey, GBRequest callbackObject);
		void SetPromotionItems(List<string> skus);		
	}
}
