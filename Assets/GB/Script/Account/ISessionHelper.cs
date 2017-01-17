using UnityEngine;
using System;
using System.Collections;

//public delegate void SessionHandler(bool success, string eventObject);

public interface ISessionHelper {

	bool IsOpened();
	bool HasToken();
	bool IsAllowedEULA();
	// string GetAccessToken();
	// string GetRefreshToken();
	void Login(AuthType authType, GBRequest callbackObject);
//	void Login(AuthType authType, string snsAccessToken, GBRequest callbackObject);
	// void LoginByUI(GBRequest callbackObject);
	// void LoginByUI(LoginUIType loginUIType, GBRequest callbackObject);
	void LinkServiceWithAuthType(AuthType authType, GBRequest callbackObject);
	// void LinkServiceWithAuthType(AuthType authType, string snsAccessToken,  GBRequest callbackObject);
	void Logout(GBRequest callbackObject);
	void Unregister(GBRequest callbackObject);
}

