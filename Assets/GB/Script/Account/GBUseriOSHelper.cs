#if UNITY_IPHONE
using UnityEngine;
using System.Collections;
using System.Runtime.InteropServices;

public class GBUseriOSHelper : IUserHelper {

	[DllImport ("__Internal")]
	public static extern void ReqeustFriends(string callbackObjectName);
	
	[DllImport ("__Internal")]
	public static extern void RequestAddFriend(int userKey, string callbackObjectName);
	
	[DllImport ("__Internal")]
	public static extern void RequestUpdateFriendStatus(int userKey, int status, string callbackObjectName);
	
	[DllImport ("__Internal")]
	public static extern void RequestSearchUsers(string nickName, string callbackObjectName);
	
	public void RequestFriends(GBRequest callbackObject) {
		ReqeustFriends(callbackObject.GetCallbackGameObjectName());
	}
	
	public void AddFriend(int userKey, GBRequest callbackObject) {
		RequestAddFriend(userKey, callbackObject.GetCallbackGameObjectName());
	}
	
	public void UpdateFriendStatus(int userKey, GBUser.FriendStatus status, GBRequest callbackObject) {
		RequestUpdateFriendStatus(userKey, (int)status, callbackObject.GetCallbackGameObjectName());
	}
	
	public void RequestSearchUsers(string nickName, GBRequest callbackObject) {
		RequestSearchUsers(nickName, callbackObject.GetCallbackGameObjectName());
	}
}

#endif