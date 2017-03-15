using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using GB;
using GB.Account;
using GB.Billing;

public interface IGBNativePlugin {

	/* Initialize SDK */
	void ConfigureSDKWithGameInfo(string clientSecretKey, int gameCode, GBSettings.Market markeet, GBSettings.LogLevel logLevel);

	/* Session */
	void Login(AuthType authType, GBRequest callbackObject);

	void Login(GBRequest callbackObject);

//	void Login(AuthType authType, string snsAccessToken, GBRequest callbackObject);
	void ConnectChannel(AuthType authType, GBRequest callbackObject);
//	void LinkServiceWithAuthType(AuthType authType, string snsAccessToken, GBRequest callbackObject);
	void Logout(GBRequest callbackObject);

	bool IsOpened();
	bool IsReady();
	bool IsConnectedChannel();

	//string getAccessToken();

	/* Profile */
	// void RequestProfile(GBRequest callbackObject);

	/* Friends */
	// void RequestFriends(GBRequest callbackObject);
	// void AddFriend(int userKey, GBRequest callbackObject);
	// void UpdateFriendsStatus(int userKey, GBUser.FriendStatus status, GBRequest callbackObject);
	// void RequestSearchInFriends(string searchText, GBRequest callbackObject);
	//void RequestInvitedUserCount(GBRequest callbackObject);

	/* Billing */
	void StartSetup(string userKey, GBRequest callbackObject);
	void QueryInventory(List<string> skus, GBRequest callbackObject);
	void QueryInventoryItemInfo(List<string> skus, GBRequest callbackObject);
    
	void BuyItem(string userKey, string sku, int price, GBRequest callbackObject);
    
	void BuyItem(string userKey, string sku, int price, string itemInfo, GBRequest callbackObject);    
        
	void RestoreItems(GBRequest callbackObject);
	
	void GetPromotionItem(string userKey, GBRequest callbackObject);
	
	void SetPromotionItems(List<string> skus);
	/* app */
	// void ShowGBMain();


	/* Utility */
	string GetMCC();

	string GetCurrentLanguage();

	string GetDeviceId();
}

