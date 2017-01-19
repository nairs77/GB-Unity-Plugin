using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine; 
using SimpleJSON;

namespace GB
{
	public class GBNetworkManager : MonoBehaviour
	{
		private static readonly string TAG = "[GBNetworkManager]";
		private static GBNetworkManager _instance;
		 
		private static string serverResponse;
		private delegate string ResponseDelegate(string url, string buffer);
		private Action<string, GBException> serverCallback;
				
		public static GBNetworkManager Instance
		{
					
			get {
				if (_instance == null ) {	
					GameObject container = new GameObject();
					container.name = TAG;					
					_instance = container.AddComponent(typeof(GBNetworkManager)) as GBNetworkManager;			

					GBLog.verbose (TAG + " Has been created.");
				}	
						
				return _instance;
			}
		}
		
		public void SendPostMessage(string url, string buffer, Action<string, GBException> callback) {
			
			//  GBNetworkManager.Instance.serverCallback = callback;
			
			//  ResponseDelegate delObject = new ResponseDelegate(new AsyncConnect().DoPostConnect);
			//  delObject.BeginInvoke(url, buffer, new AsyncCallback(onServerResponse), null);
			StartCoroutine(DoPostMessage(url, buffer, callback));
			
		}

		private IEnumerator DoPostMessage(string url, string buffer, Action<string, GBException> callback) {
			Dictionary<string, string> headers = new Dictionary<string, string>();
			headers.Add("Content-Type", "application/x-www-form-urlencoded");
			
			WWW www = new WWW(url, Encoding.UTF8.GetBytes(buffer), headers);
			
			float sentTime = Time.time;
			bool isTimeOut = false;
			
			while (!www.isDone) {
				if (sentTime + 100 <= Time.time) {
					GBLog.verbose("Time Out :" + url);
					isTimeOut = true;
					break;
				}
				
				GBLog.verbose("Waiting... ");
				
				yield return null;
			}
			
			if (isTimeOut) {
				GBLog.verbose("Time out break!!!");
				yield break;
			}
			
			serverResponse = www.text;
			
			GBLog.verbose("server = " + serverResponse);
			
			JSONNode root = JSON.Parse(serverResponse);
			int status = root["status"].AsInt;
			GBException exception = null;
						
			if (www.error == null) {
				// Success
				//GBLog.verbose("result =" + www.text);
				if (status == 1) {
					string responseData = root["result"];//.ToString();
					
					GBLog.verbose("Response = " + responseData.ToString());
					
					if (responseData.Equals("null")) {				
						GBLog.verbose("Just Status ... Success");
						callback(null, null);
					} else {
						GBLog.verbose("have result data");
						callback(responseData, null);
					}
				} else {
					exception = new GBException(root["error"]);
					callback(null, exception);
				}								
				
			} else {
				// Failed
				GBLog.verbose("DoPostMessage failed!!! - " + www.error.ToString());
			}			
		} 
	}	 
}

