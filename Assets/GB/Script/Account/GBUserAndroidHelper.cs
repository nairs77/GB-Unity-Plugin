#if UNITY_ANDROID
using UnityEngine;
using System.Collections;

namespace GB.Account {
	
	public class GBUserAndroidHelper : GBAndroidHelper, IUserHelper {

		private static readonly string NATIVE_FRIEND_CLASS_PACKAGE = "com.GB.platform.unity.FriendsPlugin";
		
		private static AndroidJavaClass _androidUserHelper;
		private static AndroidJavaClass AndroidUserHelper {
			get {
				if (_androidUserHelper == null) {
					_androidUserHelper = new AndroidJavaClass(NATIVE_FRIEND_CLASS_PACKAGE);
				}
				return _androidUserHelper;
			}
		}
		
		public void RequestFriends(GBRequest callbackObject) {			
			UnityActivity.Call ("runOnUiThread", new AndroidJavaRunnable (() => {
				AndroidUserHelper.CallStatic ("RequestFriends", callbackObject.GetCallbackGameObjectName ());
			}));	
		}
		
		public void AddFriend(int userKey, GBRequest callbackObject) {
			UnityActivity.Call ("runOnUiThread", new AndroidJavaRunnable (() => {
				AndroidUserHelper.CallStatic ("ReqeustAddFriend", userKey, callbackObject.GetCallbackGameObjectName ());
			}));
		}
		
		public void UpdateFriendStatus(int userKey, GBUser.FriendStatus status, GBRequest callbackObject) {
			UnityActivity.Call ("runOnUiThread", new AndroidJavaRunnable (() => {
				AndroidUserHelper.CallStatic ("RequestUpdateFriendStatus", userKey, (int)status, callbackObject.GetCallbackGameObjectName ());
			}));
		}
		
		public void RequestSearchUsers(string nickname, GBRequest callbackObject) {
			UnityActivity.Call("runOnUiThread", new AndroidJavaRunnable(() => {
				AndroidUserHelper.CallStatic ("RequestSearchUsers", nickname, callbackObject.GetCallbackGameObjectName());
			}));
		}
		
	}
}
#endif
