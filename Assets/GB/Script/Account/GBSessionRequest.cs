using System;
using UnityEngine;
using System.Collections;
using SimpleJSON;
using GB;

namespace GB.Account
{
	public class GBSessionRequest : GBRequest {

		/*
		Session Format -
		{
			status : 0, 1 (SUCCESS, FAIL)
			state : 0, 1, 2, ... {@link SessionState (ACCESS, OPEN, TOKEN_REISSUED, CLOSE, ACCESS_FAILED)
			error : {
				code = error code	{@link GBAuthError.cs}
				msg = error message
			}
		}
		*/

		public static readonly string TAG = "[GBSessionRequest]";
		static readonly string API_SESSION_EVENT_KEY = "state";

		public static Action<SessionState, GBException> sessionStateCallback;

		static Action<bool,string> wrapperCallback = (success, result) => {
			
			print("[Session Callback : " + result);

			JSONNode root = JSON.Parse(result);
			var response = root[API_RESPONSE_RESULT_KEY];
			
			GBUser.Instance.UpdateProfileInfo(response);			
			GBSession newSession = GBUser.Instance.currentSession;

			//SessionState state = (SessionState)System.Enum.Parse(typeof(SessionState), response[API_SESSION_EVENT_KEY]);

			// GBLog.verbose(TAG + "Session Request callback!!! - " + state.ToString());
			// if (state.Equals(SessionState.ACCESS_FAILED)) {
			// 	sessionStateCallback(state, new GBException(response[API_RESPONSE_ERROR_KEY].ToString()));
			// } else {
			// 	sessionStateCallback(state, null);
			// }				
		};
		
		public static void RequestLogin(Action<SessionState, GBException> callback) {
			GameObject gameObject = new GameObject("RequestLogin" + DateTime.Now.Ticks);
			GBSessionRequest accountRequest = gameObject.AddComponent<GBSessionRequest>();

			sessionStateCallback = callback;

			accountRequest.RequestLoginWithCallback(wrapperCallback);						
		}
		public static void RequestLogin(AuthType authType, Action<SessionState, GBException> callback) {
						
			GameObject gameObject = new GameObject("RequestLogin" + DateTime.Now.Ticks);
			GBSessionRequest accountRequest = gameObject.AddComponent<GBSessionRequest>();

			sessionStateCallback = callback;
/*			
			Action<bool,string> wrapperCallback = (success, result) => {
				
				JSONNode root = JSON.Parse(result);
				var response = root[API_RESPONSE_RESULT_KEY];
				
				SessionState state = (SessionState)System.Enum.Parse(typeof(SessionState), response[API_SESSION_EVENT_KEY]);

				GBLog.verbose(TAG + "Request Login callback!!! - " + state.ToString());
				if (state.Equals(SessionState.ACCESS_FAILED)) {
					callback(state, new GBException(response[API_RESPONSE_ERROR_KEY].ToString()));
				} else {
					callback(state, null);
				}				
			};
*/			
			accountRequest.RequestLoginWithCallback(authType, wrapperCallback);
		}

		// public static void RequestLogin(AuthType authType, string snsAccessToken, Action<SessionState, GBException> callback) {

		// 	GameObject gameObject = new GameObject("RequestLogin" + DateTime.Now.Ticks);
		// 	GBSessionRequest accountRequest = gameObject.AddComponent<GBSessionRequest>();

		// 	sessionStateCallback = callback;	
		// 	accountRequest.RequestLoginWithCallback(authType, snsAccessToken, wrapperCallback);
		// }

		public static void RequestConnectChannel(AuthType authType, Action<SessionState, GBException> callback) {
			GameObject gameObject = new GameObject("RequestLinkAccount" + DateTime.Now.Ticks);
			GBSessionRequest accountRequest = gameObject.AddComponent<GBSessionRequest>();		
			sessionStateCallback = callback;
			
			accountRequest.RequestConnectChannelWithCallback(authType, wrapperCallback);
		}

		// public static void RequestLinkService(AuthType authType, string snsAccessToken, Action<SessionState, GBException> callback) {
		// 	GameObject gameObject = new GameObject("RequestLinkAccount" + DateTime.Now.Ticks);
		// 	GBSessionRequest accountRequest = gameObject.AddComponent<GBSessionRequest>();		
		// 	sessionStateCallback = callback;
			
		// 	accountRequest.RequestLinkServiceWithCallback(authType, snsAccessToken, wrapperCallback);			
		// }

		// public static void RequestLoginByUI(Action<SessionState, GBException> callback) {
		// 	GameObject gameObject = new GameObject("RequestLoginByUI" + DateTime.Now.Ticks);
		// 	GBSessionRequest accountRequest = gameObject.AddComponent<GBSessionRequest>();
			
		// 	sessionStateCallback = callback;			

		// 	accountRequest.RequestLoginByUIWithCallback(wrapperCallback);
		// }

		// public static void RequestLoginByUI(LoginUIType loginUIType, Action<SessionState, GBException> callback){
		// 	GameObject gameObject = new GameObject ("RequestLoginByUI" + DateTime.Now.Ticks);
		// 	GBSessionRequest accountRequest = gameObject.AddComponent<GBSessionRequest> ();

		// 	sessionStateCallback = callback;

		// 	accountRequest.RequestLoginByUIWithCallback (loginUIType, wrapperCallback);
		// }

		public static void RequestLogout(Action<SessionState, GBException> callback) {
			GameObject gameObject = new GameObject("RequestLogout" + DateTime.Now.Ticks);
			GBSessionRequest accountRequest = gameObject.AddComponent<GBSessionRequest>();
			
			sessionStateCallback = callback;

			accountRequest.RequestLogoutWithCallback(wrapperCallback);
		}

		/*

		Profile  Format -
		{
			status : 0, 1 (SUCCESS, FAIL)

			data : {
				userinfo : {
					...
				}
				devices : {
					...
				}
				services : {
					...
				}
				games : {
					...
				}
			}

			error : {
				code = error code
				msg = error message
			}
		}
		*/
/*		
		public static void RequestProfile(Action<bool, GBException> callback) {

			GameObject gameObject = new GameObject("ProfileCallbackTemp" + DateTime.Now.Ticks);
			GBSessionRequest accountRequest = gameObject.AddComponent<GBSessionRequest>();
			
			Action<bool,string> profileCallback = (success, result) => {

				JSONNode root = JSON.Parse(result);
				var response = root[API_RESPONSE_RESULT_KEY];
								
				GBLog.verbose(TAG + "Request Profile callback!!! - " + response.ToString());

				if (success) {
					var profile_result = response[API_RESPONSE_DATA_KEY];

					GBUser.Instance.UpdateProfileInfo(profile_result);

					callback(true, null);
				} else {
					callback(success, new GBException(response[API_RESPONSE_ERROR_KEY].ToString()));
				}
			};
			
			accountRequest.RequestProfileWithCallback(profileCallback);
		}

		public static void RequestClickWrap(Action<bool, GBException> callback) {
			GameObject gameObject = new GameObject("ClickWrapCallbackTemp" + DateTime.Now.Ticks);
			GBSessionRequest accountRequest = gameObject.AddComponent<GBSessionRequest>();
			
			Action<bool,string> clickWrapperCallback = (success, jsonResult) => {
				
				GBLog.verbose(TAG + "Request Click Wrap callback!!!");
				
				GBLog.verbose(TAG + "result = " + jsonResult);

				// Always true
				callback(true, null);
			};
			
			accountRequest.RequestClickWrapWithCallback(clickWrapperCallback);
		}

		public static void RequestMergeAccount(string userkey, Action<bool, GBException> callback) {
			GameObject gameObject = new GameObject ("RequestMergeAccountTemp" + DateTime.Now.Ticks);
			GBSessionRequest accountRequest = gameObject.AddComponent<GBSessionRequest> ();

			Action<bool,string> mergeAccountCallback = (success, result) => {

				JSONNode root = JSON.Parse(result);
				var response = root[API_RESPONSE_RESULT_KEY];

				GBLog.verbose (TAG + "Request Merge Account callback!!!");
				GBLog.verbose(TAG + "Request Merge Account callback!!! - " + response.ToString());

				if (success) {
					callback(true, null);
				} else {
					callback(success, new GBException(response[API_RESPONSE_ERROR_KEY].ToString()));
				}
			};

			accountRequest.RequestMergeAccountWithCallback(userkey, mergeAccountCallback);

		}

		public static void RequestVerifyAccount(AuthType authType, Action<bool, string> callback) {
			GameObject gameObject = new GameObject ("RequestVerifyAccountTemp" + DateTime.Now.Ticks);
			GBSessionRequest accountRequest = gameObject.AddComponent<GBSessionRequest> ();
			
			Action<bool, string> verifyAccountCallback = (success, result) => {
				
				JSONNode root = JSON.Parse(result);
				var response = root[API_RESPONSE_RESULT_KEY];	
				callback(success, response.ToString());
				
				//				if (success) {
				//					JSONNode root = JSON.Parse(result);
				//					var response = root[API_RESPONSE_RESULT_KEY];
				//					var verify_result = response[API_RESPONSE_DATA_KEY];
				//					callback(true, verify_result.ToString(), null);
				//				} else {
				//					JSONNode root = JSON.Parse(error);
				//					var response =  root[API_RESPONSE_RESULT_KEY];
				//					callback(success, null, new GBException(response[API_RESPONSE_ERROR_KEY].ToString()));
				//				}
			};
			
			accountRequest.RequestVerifyAccountWithCallback(authType, verifyAccountCallback);
			
		}

		public static void ReauestCheckExistAccount (AuthType authType, string email, string userId, Action<bool, GBException> callback){
			GameObject gameObject = new GameObject ("ReauestCheckExistAccount" + DateTime.Now.Ticks);
			GBSessionRequest accountRequest = gameObject.AddComponent<GBSessionRequest> ();

			Action<bool,string> checkCallback = (success, result) => {
				JSONNode root = JSON.Parse(result);
				var response = root[API_RESPONSE_RESULT_KEY];
				if (success) {

					var result_exists = response[API_RESPONSE_DATA_KEY];
					if(result_exists["exists"].AsInt == 1)
						callback(true, null);
					else
						callback(false, null);

				} else {
					callback(success, new GBException(response[API_RESPONSE_ERROR_KEY].ToString()));
				}

			};

			accountRequest.ReauestCheckExistAccountWithCallback (authType, email, userId, checkCallback);
		}
*/
		private void RequestLoginWithCallback(Action<bool, string> callback) {		
			GBRequest callbackObject = createRequestCallbackObject(callback);
			GBManager.Instance.PluginManager.Login(callbackObject);
		}
		private void RequestLoginWithCallback(AuthType authType, Action<bool, string> callback) {		
			GBRequest callbackObject = createRequestCallbackObject(callback);
			GBManager.Instance.PluginManager.Login(authType, callbackObject);
		}

		// private void RequestLoginWithCallback(AuthType authType, string snsAccessToken, Action<bool, string> callback) {		
		// 	GBRequest callbackObject = createRequestCallbackObject(callback);
		// 	GBManager.Instance.PluginManager.Login(authType, snsAccessToken, callbackObject);
		// }

		private void RequestConnectChannelWithCallback(AuthType authType, Action<bool, string> callback) {
			GBRequest callbackObject = createRequestCallbackObject(callback);
			GBManager.Instance.PluginManager.ConnectChannel(authType, callbackObject);
		}

		// private void RequestLinkServiceWithCallback(AuthType authType, string snsAccessToken, Action<bool, string> callback) {
		// 	GBRequest callbackObject = createRequestCallbackObject(callback);
		// 	GBManager.Instance.PluginManager.LinkServiceWithAuthType(authType, snsAccessToken, callbackObject);
		// }
		
		// private void RequestLoginByUIWithCallback(Action<bool, string> callback) {
		// 	GBRequest callbackObject = createRequestCallbackObject(callback);
		// 	GBManager.Instance.PluginManager.LoginByUI(callbackObject);			
		// }

		// private void RequestLoginByUIWithCallback(LoginUIType loginUIType, Action<bool, string> callback){
		// 	GBRequest callbackObject = createRequestCallbackObject (callback);
		// 	GBManager.Instance.PluginManager.LoginByUI (loginUIType, callbackObject);
		// }

		private void RequestLogoutWithCallback(Action<bool, string> callback) {
			GBRequest callbackObject = createRequestCallbackObject(callback);
			GBManager.Instance.PluginManager.Logout(callbackObject);
		}
/*
		private void RequestProfileWithCallback(Action<bool, string> callback) {
			GBRequest callbackObject = createRequestCallbackObject(callback);
			GBManager.Instance.PluginManager.RequestProfile(callbackObject);
		}

		private void RequestClickWrapWithCallback(Action<bool, string> callback) {
			GBRequest callbackObject = createRequestCallbackObject(callback);
			GBManager.Instance.PluginManager.ShowClickWrap(callbackObject);
		}
*/
		// private void RequestMergeAccountWithCallback(string userkey, Action<bool, string> callback) {
		// 	GBRequest callbackObject = createRequestCallbackObject(callback);
		// 	GBManager.Instance.PluginManager.RequestMergeAccount(userkey, callbackObject);
		// }

		// private void RequestVerifyAccountWithCallback(AuthType authType, Action<bool, string> callback) {
		// 	GBRequest callbackObject = createRequestCallbackObject(callback);
		// 	GBManager.Instance.PluginManager.RequestVerifyAccount(authType, callbackObject);
		// }

		// private void ReauestCheckExistAccountWithCallback(AuthType authType, string email, string userId, Action<bool, string> callback){
		// 	GBRequest callbackObject = createRequestCallbackObject (callback);
		// 	GBManager.Instance.PluginManager.CheckExistAccount (authType, email, userId, callbackObject);
		// }
	}
}
