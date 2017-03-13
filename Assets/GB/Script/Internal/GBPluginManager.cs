using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using GB;
using GB.Account;
using GB.Billing;

public class GBPluginManager : IGBNativePlugin
{
	private static ICommonHelper _commonHelper = null;
	private static ISessionHelper _sessionHelper = null;
	private static IIabHelper _inAppHelper = null;

	private static ICommonHelper CommonHelper {
		get {
			if (_commonHelper == null) {
#if UNITY_ANDROID
				_commonHelper = new GBCommonAndroidHelper();
#elif UNITY_IPHONE
				_commonHelper = new GBCommoniOSHelper(); 
#endif
			}

			return _commonHelper;
		}
	}
	private static IIabHelper InAppHelper {
		get {
			if (_inAppHelper == null) {
#if UNITY_ANDROID
				_inAppHelper = new GBInAppAndroidHelper();
#elif UNITY_IPHONE
				_inAppHelper = new GBInAppiOSHelper();
#endif
			}

			return _inAppHelper;
		}
	}

	private static ISessionHelper SessionHelper {
		get {
			if (_sessionHelper == null) {
#if UNITY_ANDROID
				_sessionHelper = new GBSessionAndroidHelper();
#elif UNITY_IPHONE
				_sessionHelper = new GBSessioniOSHelper();
#endif
			}

			return _sessionHelper;
		}
	}

	public GBPluginManager() {}

	public void SetActiveMarket(GBSettings.Market market, GBRequest callbackObject) {
		CommonHelper.SetActiveMarket(market, callbackObject);
	}

	/* Initialize SDK */
	public void ConfigureSDKWithGameInfo(string clientSecretKey, int gameCode, GBSettings.Market market, GBSettings.LogLevel logLevel) {
		CommonHelper.ConfigureSDKWithGameInfo(clientSecretKey, gameCode, market, (int)logLevel);
	}

	public void ConfigureSDKWithGameInfo(string clientSecretKey, int gameCode, string platformInfo, GBSettings.LogLevel logLevel) {
		CommonHelper.ConfigureSDKWithGameInfo(clientSecretKey, gameCode, platformInfo, (int)logLevel);
	}

	/* Session */
	public void Login(GBRequest callbackObject) {
		SessionHelper.Login(callbackObject);
	}	
	public void Login(AuthType authType, GBRequest callbackObject) {
		SessionHelper.LoginWithAuthType(authType, callbackObject);
	}

	// public void Login(AuthType authType, string snsAccessToken, GBRequest callbackObject) {
	// 	SessionHelper.Login(authType, snsAccessToken, callbackObject);
	// }

	// public void LoginByUI(GBRequest callbackObject) {
	// 	SessionHelper.LoginByUI(callbackObject);
	// }

	// public void LoginByUI(LoginUIType loginUIType, GBRequest callbackObject){
	// 	SessionHelper.LoginByUI (loginUIType, callbackObject);
	// }

	// public void CheckExistAccount(AuthType authType, string email, string userId, GBRequest callbackObject){
	// 	SessionHelper.CheckExistAccount (authType, email, userId, callbackObject);
	// }

	public void ConnectChannel(AuthType authType, GBRequest callbackObject) {
	//public void LinkServiceWithAuthType(AuthType authType, GBRequest callbackObject) {
		SessionHelper.ConnectChannel(authType, callbackObject);
	}

	// public void LinkServiceWithAuthType(AuthType authType, string snsAccessToken, GBRequest callbackObject) {
	// 	SessionHelper.LinkServiceWithAuthType(authType, snsAccessToken, callbackObject);
	// }

	public void Logout(GBRequest callbackObject) {
		SessionHelper.Logout(callbackObject);
	}

	public bool IsOpened() {
		return SessionHelper.IsOpened();
	}
	public bool IsReady() {
		return SessionHelper.IsReady();
	}

	public bool IsAllowedEULA() {
		return SessionHelper.IsAllowedEULA();
	}

	// public bool IsConnectedChannel() {
	// 	return SessionHelper.IsConnectedChannel();
	// }

	public string GetActiveSession() {
		return SessionHelper.GetActiveSession();
	}
/*
	public string GetAccessToken() {
		return SessionHelper.GetAccessToken();
	}

	public string GetRefreshToken() {
		return SessionHelper.GetRefreshToken();
	}
*/
	/* Profile */
/*	
	public void RequestProfile(GBRequest callbackObject) {
		SessionHelper.RequestProfile(callbackObject);
	}
*/	
	
	/* Billing */
	public void StartSetup(string userKey, GBRequest callbackObject) {
		InAppHelper.StartSetup (userKey, callbackObject);
	}
	public void QueryInventory(List<string> skus, GBRequest callbackObject) {
		InAppHelper.QueryInventory (skus, callbackObject);
	}
	public void QueryInventoryItemInfo(List<string> skus, GBRequest callbackObject) {
		InAppHelper.QueryInventoryItemInfo (skus, callbackObject);
	}
		
	public void BuyItem(string sku, int price, GBRequest callbackObject) {
		InAppHelper.BuyItem (sku, price, callbackObject);
	}
	
	public void BuyItem(string sku, int price, string itemInfo, GBRequest callbackObject) { 
		InAppHelper.BuyItem (sku, price, itemInfo, "", callbackObject);
	}
    
    public void BuyItem(string sku, int price, string itemInfo, string toUserKey, GBRequest callbackObject) {
  		InAppHelper.BuyItem (sku, price, itemInfo, toUserKey, callbackObject);
    }
	public void RestoreItems(GBRequest callbackObject) {
		InAppHelper.RestoreItems(callbackObject);
	}
	
	public void GetPromotionItem(string userKey, GBRequest callbackObject) {
		InAppHelper.GetPromotionItem(userKey, callbackObject);
	}	
	
	public void SetPromotionItems(List<string> skus) {
		InAppHelper.SetPromotionItems(skus);
	}
	/* Notice */
	//void RequestUrgentNotice(GBRequest callbackObject);
	
	/* app */
/*	
	public void ShowGBMain() {
		SessionHelper.ShowGBMain ();
	}

	public void ShowClickWrap(GBRequest callbackObject) {
		SessionHelper.ShowClickWrap(callbackObject);
	}

	public void HideGBStart() {
		SessionHelper.HideGBStart ();
	}

	public void ShowEULA() {
		SessionHelper.ShowEULA ();
	}

	// public void ShowViewByType(GBProfileViewType type) {
	// 	SessionHelper.ShowViewByType (type);
	// }

	public void SetAllowedEULA(bool isAllowedEULA) {
		SessionHelper.SetAllowedEULA (isAllowedEULA);
	}

	public bool IsAlreadyLogin() {
		return SessionHelper.IsAlreadyLogin();
	}
	
	public void SetGameLanguage(LanguageType languageType) {
		SessionHelper.SetGameLanguage(languageType);
	}

	public void RequestMergeAccount(string userkey, GBRequest callbackObject) {
		SessionHelper.RequestMergeAccount(userkey, callbackObject);
	}

	public void RequestVerifyAccount(AuthType authType, GBRequest callbackObject) {
		SessionHelper.RequestVerifyAccount(authType, callbackObject);
	}
*/
	/* Utility */
	public string GetMCC() {
		return CommonHelper.GetMCC();
	}

	public string GetCurrentLanguage() {
		return CommonHelper.GetCurrentLanguage();
	}

	public string GetDeviceId() {
		return CommonHelper.GetDeviceId ();
	}

	public string GetDeviceModel() {
		return CommonHelper.GetDeviceModel();
	}
}
