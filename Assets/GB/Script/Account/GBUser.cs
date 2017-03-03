using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using SimpleJSON;
using GB;

public class GBUser
{
	// Profile
	public GBSession LocalUser { get; set; }
	public List<Device> Devices = new List<Device>();
	public List<Game> Games = new List<Game>();
	public List<Service> Services = new List<Service>();

	// Friends
	public List<Friend> GameFriends = new List<Friend>();
	public List<Friend> GBFriends = new List<Friend>();
	public List<Friend> RecommendedUsers = new List<Friend>();

	private static GBUser _instance;

	/**
 	 * @brief Get Profile instance
 	 */

	public static GBUser Instance
	{
		get
		{
			if (_instance == null) {
				_instance = new GBUser();
			}
			return _instance;
		}
	}

	public enum FriendStatus {
		FRIEND,
		DELETE,
		BLOCK
	}

	static public void GetFriends(Action<bool, GBException> callback) {
		GBUserRequest.RequestFriends(callback);
		//		GBManager.Instance.NativePlugin.Call(param);
	}

	static public void AddFriend(int userKey, Action<bool, GBException> callback) {
		GBUserRequest.RequestAddFriend(userKey, callback);
	}

	static public void UpdateFriendStatus(int userKey, FriendStatus status, Action<bool, GBException> callback) {
		GBUserRequest.RequestUpdateFriendStatus(userKey, status, callback);
	}

	static public void GetSearchUsers(string nickname, Action<bool, GBException> callback) {
		GBUserRequest.RequestSearchUsers(nickname, callback);
	}

	public bool HasGameFriends() {
		if(GameFriends.Count > 0)
			return true;
		return false;
	}
	
	public bool HasGBFriends() {
		if(GBFriends.Count > 0)
			return true;
		return false;
	}
	
	public bool HasRecommendedUsers() {
		if(RecommendedUsers.Count > 0)
			return true;
		return false;
	}

	// Profile
	public void UpdateProfileInfo(JSONNode result) {
		this.LocalUser = new LocalUser(result["user_info"]);	
		this.Devices = AsListDevices(result["devices"]);
		this.Services = AsListServices(result["services"]);
		this.Games = AsListGames(result["games"]);		
	}
	
	private List<Device> AsListDevices(JSONNode root) {		
		List<Device> devices = new List<Device>();
		if(root == null || root.Count == 0)
			return devices;
		
		JSONArray array = root.AsArray;
		foreach(JSONNode node in array) {			
			devices.Add(new Device(node));
		}		
		return devices;
	}	
	
	private List<Service> AsListServices(JSONNode root) {
		List<Service> services = new List<Service>();
		if(root == null || root.Count == 0)
			return services;
		
		JSONArray array = root.AsArray;
		foreach(JSONNode node in array) {			
			services.Add(new Service(node));
		}		
		return services;
	}
	
	private List<Game> AsListGames(JSONNode root) {		
		List<Game> games = new List<Game>();
		if(root == null || root.Count == 0)
			return games;
		
		JSONArray array = root.AsArray;
		foreach(JSONNode node in array) {
			games.Add(new Game(node));
		}		
		return games;
	}


	// Friends
	public void UpdateFriends(JSONNode result) {
		this.GameFriends = AsListFriends(result["friends_game"]);
		this.RecommendedUsers = AsListFriends(result["friends_recommend"]);
		this.GBFriends = AsListFriends(result["friends_list"]);
	}

	private List<Friend> AsListFriends(JSONNode root) {
		List<Friend> friends = new List<Friend>();
		if (root == null | root.Count == 0)
			return friends;

		JSONArray array = root.AsArray;

		foreach (JSONNode node in array) {
			friends.Add(new Friend(node));
		}

		return friends;
		
	}
}
