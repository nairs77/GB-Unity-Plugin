using System;
using SimpleJSON;
using GB;
using GB.Account;

public class GBSessionManager {

	public static bool isOpened() {
		return GBManager.Instance.PluginManager.IsOpened();
	}

	public static bool isReady() {
		return GBManager.Instance.PluginManager.IsReady();
	}

	public static bool isAllowedEULA() {
		return GBManager.Instance.PluginManager.IsAllowedEULA();
	}

	public static bool isConnectedChannel() {
		return GBManager.Instance.PluginManager.IsConnectedChannel();
	}

	public static void Login(Action<SessionState, GBException> callback) {
		GBSessionRequest.RequestLogin(callback);
	}

	public static void LoginWithAuthType(AuthType authType, Action<SessionState, GBException> callback) {
		GBSessionRequest.RequestLogin(authType, callback);
	}
/*
	public static void LoginByUI(Action<SessionState, GBException> callback) {
		GBSessionRequest.RequestLoginByUI (callback);
	}

	public static void LoginByUI(LoginUIType loginUIType, Action<SessionState, GBException> callback){
		GBSessionRequest.RequestLoginByUI (loginUIType, callback);
	}
*/

	public static void ConnectChannel(AuthType authType, Action<SessionState, GBException> callback) {
		GBSessionRequest.RequestConnectChannel(authType, callback);
	}
	public static void Logout(Action<SessionState, GBException> callback) {
		GBSessionRequest.RequestLogout(callback);
	}

/*
	public static void GetProfile(Action<bool, GBException> callback) {
		GBSessionRequest.RequestProfile(callback);
	}
	public static void LinkServiceWithAuthType(AuthType authType, string snsAccessToken, Action<SessionState, GBException> callback) {
		GBSessionRequest.RequestLinkService(authType, snsAccessToken, callback);
	}
	
	public static void ShowGBMain() {
		GBManager.Instance.PluginManager.ShowGBMain();
	}

	public static void ShowClickWrap(Action<bool, GBException> callback) {
		GBSessionRequest.RequestClickWrap(callback);
	}

	public static void ShowEULA() {
		GBManager.Instance.PluginManager.ShowEULA();
	}

	public static void SetAllowedEULA(bool isAllowedEULA) {
		GBManager.Instance.PluginManager.SetAllowedEULA (isAllowedEULA);
	}

	public static void ShowViewByType(GBProfileViewType type) {
		GBManager.Instance.PluginManager.ShowViewByType (type);
	}
	
	public static bool isAlreadyLogin() {
		return GBManager.Instance.PluginManager.IsAlreadyLogin();
	}
	
	public static void HideGBStart() {
		GBManager.Instance.PluginManager.HideGBStart();
	}

	public static void RequestMergeAccount(string userkey, Action<bool, GBException> callback) {
		GBSessionRequest.RequestMergeAccount(userkey, callback);
	}

	public static void RequestVerifyAccount(AuthType authType, Action<bool, string> callback) {
		GBSessionRequest.RequestVerifyAccount(authType, callback);
	}

	public static void CheckExistAccount(AuthType authType, string email, string userId, Action<bool, GBException> callback){
		GBSessionRequest.ReauestCheckExistAccount (authType, email, userId, callback);
	}
*/	
}
