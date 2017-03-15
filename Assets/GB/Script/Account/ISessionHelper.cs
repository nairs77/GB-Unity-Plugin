using UnityEngine;
using System;
using System.Collections;
using GB;
//public delegate void SessionHandler(bool success, string eventObject);

public interface ISessionHelper {

	bool IsOpened();
	bool IsReady();
	bool IsAllowedEULA();
	bool IsConnectedChannel();

	void Login(GBRequest callbackObject);	
	void LoginWithAuthType(AuthType authType, GBRequest callbackObject);
//	void Login(AuthType authType, string snsAccessToken, GBRequest callbackObject);
	// void LoginByUI(GBRequest callbackObject);
	// void LoginByUI(LoginUIType loginUIType, GBRequest callbackObject);
	void ConnectChannel(AuthType authType, GBRequest callbackObject);
	// void LinkServiceWithAuthType(AuthType authType, string snsAccessToken,  GBRequest callbackObject);
	void Logout(GBRequest callbackObject);
}

