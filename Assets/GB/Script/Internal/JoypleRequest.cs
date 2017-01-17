using System;
using UnityEngine;
using System.Collections;
using System.Reflection;
using GB;
using SimpleJSON;

public class GBRequest : MonoBehaviour {

	public GBRequest() {}
	
	protected Action<bool, string> functionCallback;
	//protected Action<bool, string, string> functionCallbacks; // If you need more arguments, do modify FunctionCallbacks methed.
	protected string callbackGameObjectName;
	//protected GameObject callGameObject;

	protected static readonly string API_RESPONSE_RESULT_KEY = "result";
	protected static readonly string API_RESPONSE_DATA_KEY = "data";
	protected static readonly string API_RESPONSE_ERROR_KEY = "error";
		
	public GBRequest createRequestCallbackObject(Action<bool, string> callback) {
		functionCallback = callback;
		string gameObjectName = "GBRequestFunctionCall."+DateTime.Now.Ticks;
		callbackGameObjectName = gameObjectName;
		
		// This allows us to track unique calls to async native code
		
		#if !UNITY_EDITOR
		GameObject gameObject = new GameObject(gameObjectName);
		DontDestroyOnLoad(gameObject);
		
		GBRequest createdCallBackObject = gameObject.AddComponent<GBRequest>();
		createdCallBackObject.functionCallback = callback;
		createdCallBackObject.callbackGameObjectName = callbackGameObjectName;
		
		return createdCallBackObject;
		#else
		asyncCallFailed("GBRequest are not supported in the Unity editor");
		return null;
		#endif
	}
	/** 
	 *  FunctionCallbacks methed
		public GBRequest createRequestCallbackObject(Action<bool, string, string> callback) {
			functionCallbacks = callback;
			string gameObjectName = "GBRequestFunctionCall."+DateTime.Now.Ticks;
			callbackGameObjectName = gameObjectName;

			// This allows us to track unique calls to async native code

			#if !UNITY_EDITOR
			GameObject gameObject = new GameObject(gameObjectName);
			DontDestroyOnLoad(gameObject);

			GBRequest createdCallBackObject = gameObject.AddComponent<GBRequest>();
			createdCallBackObject.functionCallbacks = callback;
			createdCallBackObject.callbackGameObjectName = callbackGameObjectName;

			return createdCallBackObject;
			#else
			asyncCallFailed("GBRequest are not supported in the Unity editor");
			return null;
			#endif
		}
	**/
	public void asyncCallSucceeded(string paramString)
	{
		JLog.verbose (paramString);
		if(functionCallback != null) {
			functionCallback(true, paramString);
		} 
	/** 
	 *  FunctionCallbacks methed
	 
		if(functionCallbacks != null) {
			functionCallbacks (true, paramString, null); //If you want use this code, you must check to null parsing crash.
		}
	**/

		GameObject.Destroy(this.gameObject);
	}
	
	public void asyncCallFailed(string errorString)
	{
		if(functionCallback != null) {
			functionCallback(false, errorString);
		} 
	/** 
	 *  FunctionCallbacks methed
	 
		if(functionCallbacks != null) {
			functionCallbacks (false, null, errorString); //If you want use this code, you must check to null parsing crash.
		}
	**/
		/**
		 *	User Cancelled !!!
		 **/
		 
		if (!isUserCancelled(errorString)) {
			GameObject.Destroy(this.gameObject);
		} 
	}
	
	public string GetCallbackGameObjectName()
	{
		return callbackGameObjectName;
	}
	
	/**
	 *  true, if pressed 'X' (GB Login UI)
	 **/
	
	bool isUserCancelled(string errorResult) {
		bool alive_object = false;
				
		if (errorResult == null || errorResult.Length == 0)
			return alive_object;
				
		JSONNode root = JSON.Parse(errorResult);
		var response = root[API_RESPONSE_RESULT_KEY];
		var error_response = response[API_RESPONSE_ERROR_KEY];

		int error_code = error_response["error_code"].AsInt;
				
#if !UNITY_EDITOR && UNITY_ANDROID		
		if (error_code == -501)
			alive_object = true;
#elif !UNITY_EITOR && UNITY_IPHONE
		if (error_code == -501 || error_code == -3202)
			alive_object = true;		
#endif
		JLog.verbose("error code = " + error_code);
				
		return alive_object;
	}
}
