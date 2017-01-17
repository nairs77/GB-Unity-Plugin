using UnityEngine;
using System.Collections;

public interface IUserHelper{

	void RequestFriends(GBRequest callbackObject);
	void AddFriend(int userKey, GBRequest callbackObject);
	void UpdateFriendStatus(int userKey, GBUser.FriendStatus status, GBRequest callbackObject);
	void RequestSearchUsers(string nickname, GBRequest callbackObject);
}

