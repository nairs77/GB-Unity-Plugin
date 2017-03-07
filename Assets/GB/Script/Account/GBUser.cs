using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using SimpleJSON;
using GB;

public class GBUser
{
	// Profile
	public GBSession currentSession { get; set; }

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

	// Profile
	public void UpdateProfileInfo(JSONNode result) {
		this.currentSession = new GBSession(result["state"]);	
	}
}
