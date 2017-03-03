using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using GB;
using GB.Account;
using GB.Billing;
using GB.PlayGameService;

public interface IGBNativePlugin {

	/* Initialize SDK */
	void ConfigureSDKWithGameInfo(string clientSecretKey, int gameCode, GBSettings.Market markeet, GBSettings.LogLevel logLevel);

	/* Session */
	void Login(AuthType authType, GBRequest callbackObject);
//	void Login(AuthType authType, string snsAccessToken, GBRequest callbackObject);
	void ConnectChannel(AuthType authType, GBRequest callbackObject);
//	void LinkServiceWithAuthType(AuthType authType, string snsAccessToken, GBRequest callbackObject);
	void Logout(GBRequest callbackObject);
	void Unregister(GBRequest callbackObject);

	bool IsOpened();
	bool HasAccount();
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
    
	void BuyItem(string sku, int price, GBRequest callbackObject);
    
	void BuyItem(string sku, int price, string itemInfo, GBRequest callbackObject);    
    
    void BuyItem(string sku, int price, string itemInfo, string toUserKey, GBRequest callbackObject);
    
	void RestoreItems(GBRequest callbackObject);
	
	void GetPromotionItem(string userKey, GBRequest callbackObject);
	
	void SetPromotionItems(List<string> skus);
	/* app */
	// void ShowGBMain();

	/* Push */
	void SendPushMessage(string userKey, string title, string message);
	
	/* Utility */
	string GetMCC();
	string GetCurrentLanguage();
	string GetDeviceId();

	void ShowToast(string message);
	void ShowAlert();
	
	/* Play Game */
	/* Android : Google Play Game Service */
	/* iOS : Game Center */
	
	void SignIn(GBRequest callbackObject);
	void SignOut(GBRequest callbackObject);
	bool IsAuthenticated();
	void ShowLeaderboardUI(string ldId, GBRequest callbackObject);
	void ShowAchievementsUI(GBRequest callbackObject);
	/*
	void UnlockAchievement(string achId, double step, GBRequest callbackObject);
	void IncrementAchievement(string achId, int step, GBRequest callbackObject);
	*/
	void ReportProgress(string achId, double step, GBRequest callbackObject);
	
	void SubmitScore(long score, string leaderBoardId, GBRequest callbackObject);
	void FetchQuestById(string eventId, GBRequest callbackObject);

	/* Event */
	void IncrementEvent(string eventId, uint stepsToIncrement);
	
	/* Quest */
	void ShowAllQuestsUI(GBRequest callbackObject);
	void ClaimMilestone(IQuestMilestone milestone, GBRequest callbackObject);

	/* Analytics */
	// void LogEvent (EventType eventType, string eventName, double valueToSum, int paramNum, string[] paramKey, string[] paramValue);
	// void LogPurchase (EventType eventType, double purchaseAmount, string currency, int paramNum, string[] paramKey, string[] paramValue);
}

