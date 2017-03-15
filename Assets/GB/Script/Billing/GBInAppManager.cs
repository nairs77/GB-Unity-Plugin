using System;
using System.Collections.Generic;

namespace GB.Billing {	
	public class GBInAppManager {

		// public static void StartSetup(string userKey, Action<bool, GBException> funcCallback) {
		// 	GBInAppRequest.RequestStartSetup(userKey, funcCallback);
		// }
		public static void QueryInventory(List<string> skus, Action<List<string>, GBException> funcCallback) {
			if (GBSettings.GetMarketType() == GBSettings.MarketType.AGENCY) {
				//GBManager.ShowToast("Not Supported API : Current Market - " + GBSettings.GetMarketToString());
				GBLog.verbose("Not Supported API : Current Market - " + GBSettings.GetMarketToString());
				funcCallback(null, null);

				return;
			}
			GBInAppRequest.RequestQueryInventory(skus, funcCallback);
		}
	
		public static void QueryInventory(List<string>skus, Action<GBInventory, GBException> funcCallback) {
			if (GBSettings.GetMarketType() == GBSettings.MarketType.AGENCY) {
				//GBManager.ShowToast("Not Supported API : Current Market - " + GBSettings.GetMarketToString());
				GBLog.verbose("Not Supported API : Current Market - " + GBSettings.GetMarketToString());
				funcCallback(null, null);

				return;
			}
				
			GBInAppRequest.RequestQueryInventory(skus, funcCallback);			
		}

        /**
         * @brief Buy item 
         * @param sku   The sku of the item to purchase.
         * @param price the price of the item to purchase. (required if N-Stroe, others 0)
         * @param itemInfo  indicates if it's a product type or a product name or product information.
         *                  required if MyCard, must set item name (product name) 
         */

		public static void BuyItem(string sku, int price, Action<string, GBException> funcCallback) {
			GBSession currentSession = GBUser.Instance.currentSession;
			GBInAppRequest.RequestBuyItem(currentSession.userKey, sku, price, funcCallback);
		}
		
        /**
         * @brief Buy item 
         * @param sku   The sku of the item to purchase.
         * @param price the price of the item to purchase. (required if N-Stroe, others 0)
         * @param itemInfo  indicates if it's a product type or a product name or product information.
         *                  required if MyCard, must set item name (product name) 
         */

		public static void BuyItem(string sku, int price, string itemInfo, Action<string, GBException> funcCallback) {
			GBSession currentSession = GBUser.Instance.currentSession;
			GBInAppRequest.RequestBuyItem(currentSession.userKey, sku, price, itemInfo, funcCallback);
		}
	
		public static void RestoreItems(Action<List<string>, GBException> funcCallback) {
			GBInAppRequest.RequestRestoreItems(funcCallback);
		}
		
		public static void SetPromotionItems(List<string> skus) {
			if (GBSettings.GetMarketType() == GBSettings.MarketType.AGENCY) {
				//GBManager.ShowToast("Not Supported API : Current Market - " + GBSettings.GetMarketToString());
				GBLog.verbose("Not Supported API : Current Market - " + GBSettings.GetMarketToString());
				return;
			}

			GBManager.Instance.PluginManager.SetPromotionItems(skus);
		}
                
		public static void GetPromotionItem(string userKey, Action<string, GBException> funCallback) {
			if (GBSettings.GetMarketType() == GBSettings.MarketType.AGENCY) {
				//GBManager.ShowToast("Not Supported API : Current Market - " + GBSettings.GetMarketToString());
				GBLog.verbose("Not Supported API : Current Market - " + GBSettings.GetMarketToString());
				funCallback(null, null);

				return;
			}

			GBInAppRequest.RequestPromotionItem(userKey, funCallback);
		}
        
	}
}
