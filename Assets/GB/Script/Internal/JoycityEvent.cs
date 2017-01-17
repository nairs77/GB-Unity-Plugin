﻿using UnityEngine;
using System.Collections;


namespace GB {
	
	/**
	 * @brief Specify GBEvent
	 * @author chujinnoon@GB.com
	 */
	public enum GBEvent {	
		LOGIN,
		LOGOUT,
		Unregister,
		
		PROFILE,
		PROFILE_FAILED,	
		
		FRIENDS,
		FRIENDS_FAILED,
		ADD_FRIEND,
		ADD_FRIEND_FAILED,
		UPDATE_FRIEND_STATUS,
		UPDATE_FRIEND_STATUS_FAILED,
		SEARCH_USERS,
		SEARCH_USERS_FAILED,
		INVITED_USERS_COUNT,
		INVITED_USERS_COUNT_FAILED,		
				
		MAIN,
		EMAIL,
		SHOW_DIALOG,
		SHOW_ERROR_DIALOG,
		MAIN_CLOSED
	}
	
	public enum GBEventType { 
		
		AUTHORIZATION, 
		PROFILE,  
		FRIENDS,
		APPLICATION 
	}
}
