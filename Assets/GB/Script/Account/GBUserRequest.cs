using UnityEngine;
using System;
using System.Collections;
using SimpleJSON;
using GB;

public class GBUserRequest : GBRequest {

	public static readonly string TAG = "[GBUserRequest]";
	
	public static void RequestFriends(Action<bool, GBException> callback) {

		GameObject gameObject = new GameObject("RequestFriends" + DateTime.Now.Ticks);
		GBUserRequest userRequest = gameObject.AddComponent<GBUserRequest>();
		
		Action<bool,string> wrapperCallback = (success, result) => {
			JLog.verbose(TAG + "Callback Get Friends");

			JSONNode root = JSON.Parse(result);
			var response = root[API_RESPONSE_RESULT_KEY];

			if (success) {				
				GBUser.Instance.UpdateFriends(response[API_RESPONSE_DATA_KEY]);
				
				callback(success, null);
			} else {
				callback(success, new GBException(response[API_RESPONSE_ERROR_KEY].ToString()));
			}
		};
		
		userRequest.RequestFriendsWithCallback(wrapperCallback);
	}

	public static void RequestAddFriend(int userKey, Action<bool, GBException> callback) {

		GameObject gameObject = new GameObject("RequestAddFriend" + DateTime.Now.Ticks);
		GBUserRequest userRequest = gameObject.AddComponent<GBUserRequest>();
		
		Action<bool,string> wrapperCallback = (success, result) => {
			JLog.verbose(TAG + "Callback Add Friend");

			JSONNode root = JSON.Parse(result);
			var response = root[API_RESPONSE_RESULT_KEY];

			if (success) {				
//				GBUser.Instance.UpdateFriends(response[API_RESPONSE_DATA_KEY]);
				
				callback(success, null);
			} else {
				callback(success, new GBException(response[API_RESPONSE_ERROR_KEY].ToString()));
			}
		};
		
		userRequest.RequestAddFriendWithCallback(userKey, wrapperCallback);
	}

	public static void RequestUpdateFriendStatus(int userKey, GBUser.FriendStatus status, Action<bool, GBException> callback) {
		GameObject gameObject = new GameObject("RequestUpdateFriendStatus" + DateTime.Now.Ticks);
		GBUserRequest userRequest = gameObject.AddComponent<GBUserRequest>();
		
		Action<bool,string> wrapperCallback = (success, result) => {
			JLog.verbose(TAG + "Callback Update Friend Status");
			
			JSONNode root = JSON.Parse(result);
			var response = root[API_RESPONSE_RESULT_KEY];

			if (success) {				
//				GBUser.Instance.UpdateFriends(response[API_RESPONSE_DATA_KEY]);
				
				callback(success, null);
			} else {
				callback(success, new GBException(response[API_RESPONSE_ERROR_KEY].ToString()));
			}
		};
		
		userRequest.RequestUpdateFriendStatusWithCallback(userKey, status, wrapperCallback);
	}

	public static void RequestSearchUsers(string searchText, Action<bool, GBException> callback) {
		GameObject gameObject = new GameObject("RequestUpdateFriendStatus" + DateTime.Now.Ticks);
		GBUserRequest userRequest = gameObject.AddComponent<GBUserRequest>();
		
		Action<bool,string> wrapperCallback = (success, result) => {
			JSONNode root = JSON.Parse(result);
			var response = root[API_RESPONSE_RESULT_KEY];

			if (success) {				
//				GBUser.Instance.UpdateFriends(response[API_RESPONSE_DATA_KEY]);
				
				callback(success, null);
			} else {
				callback(success, new GBException(response[API_RESPONSE_ERROR_KEY].ToString()));
			}
		};
		
		userRequest.RequestSearchUserWithCallback(searchText, wrapperCallback);
	}

	private void RequestFriendsWithCallback(Action<bool, string> callback) {
		GBRequest request = createRequestCallbackObject (callback);
		GBManager.Instance.PluginManager.RequestFriends (request);
	}

	private void RequestAddFriendWithCallback(int userKey, Action<bool, string> callback) {
		GBRequest request = createRequestCallbackObject(callback);
		
		GBManager.Instance.PluginManager.AddFriend(userKey, request);
	}

	private void RequestUpdateFriendStatusWithCallback(int userKey, GBUser.FriendStatus status, Action<bool, string> callback) {
		GBRequest request = createRequestCallbackObject (callback);
		GBManager.Instance.PluginManager.UpdateFriendsStatus(userKey, status, request);
	}

	private void RequestSearchUserWithCallback(string searchText, Action<bool, string> callback) {
		GBRequest request = createRequestCallbackObject(callback);
		GBManager.Instance.PluginManager.RequestSearchInFriends(searchText, request);
	}
}

