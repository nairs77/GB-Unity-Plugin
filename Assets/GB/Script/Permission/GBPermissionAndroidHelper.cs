#if UNITY_ANDROID

namespace GB
{
	using System;
	using System.Collections.Generic;
	using UnityEngine;
	using GB.Callback;
	using SimpleJSON;

	internal class GBPermissionAndroidHelper : GBAndroidHelper, IPermissionHelper
	{

		private static readonly string NATIVE_CLASS_PACKAGE = "com.joycity.platform.unity.PermissionPlugin";

		private static AndroidJavaClass _androidPermissionHelper;
		private static AndroidJavaClass AndroidPermissionHelper {
			get {
				if (_androidPermissionHelper == null) {
					_androidPermissionHelper = new AndroidJavaClass(NATIVE_CLASS_PACKAGE);
				}
				return _androidPermissionHelper;
			}
		}


		public void SetPermissionCallback (GBDelegate<GBPermissionResult> callback){
			
			String callbackId = GBCallbackManager.Instance.addGBDelegate (callback);

			GBMethodArguments methodArg = new GBMethodArguments ();
			methodArg.AddString (BaseResult.API_RESPONSE_CALLBACKID_KEY, callbackId);

			AndroidPermissionHelper.CallStatic("SetPermissionCallback", methodArg.ToJsonString());
		}

		public void SetAutoOpenPermissionView (bool enabled){

			AndroidPermissionHelper.CallStatic ("SetAutoOpenPermissionView", enabled);
		}

		public bool IsAutoOpenPermissionView (){

			return AndroidPermissionHelper.CallStatic<bool> ("IsAutoOpenPermissionView");
		}


		public bool IsPermissionGranted (string permission){

			return IsPermissionGranted (new string[]{ permission });
		}

		public bool IsPermissionGranted (string[] permissions){

			GBMethodArguments methodArg = new GBMethodArguments ();
			methodArg.AddList ("permissions", permissions);

			return AndroidPermissionHelper.CallStatic<bool> ("IsPermissionGranted", methodArg.ToJsonString());
		}

		public bool ShouldShowRequestPermissionRationale (string permission){
			return AndroidPermissionHelper.CallStatic<bool> ("ShouldShowRequestPermissionRationale", permission);
		}

		public void RequestPermission (string permission, GBDelegate<GBPermissionResult> callback){

			RequestPermission (new string[]{ permission }, callback);
		}

		public void RequestPermission (string[] permissions, GBDelegate<GBPermissionResult> callback){

			string callbackId = GBCallbackManager.Instance.addGBDelegate (callback);

			GBMethodArguments methodArg = new GBMethodArguments ();
			methodArg.AddString (BaseResult.API_RESPONSE_CALLBACKID_KEY, callbackId);
			methodArg.AddList ("permissions", permissions);

			AndroidPermissionHelper.CallStatic ("RequestPermission", methodArg.ToJsonString ());
		}
			
		public void ShowDetailPermissionView(bool isSnack, string permission, GBDelegate<GBPermissionResult> callback){

			ShowDetailPermissionView(isSnack, new string[]{ permission }, callback);
		}

		public void ShowDetailPermissionView(bool isSnack, string[] permissions, GBDelegate<GBPermissionResult> callback){
			
			string callbackId = GBCallbackManager.Instance.addGBDelegate (callback);

			GBMethodArguments methodArg = new GBMethodArguments ();
			methodArg.AddString (BaseResult.API_RESPONSE_CALLBACKID_KEY, callbackId);
			methodArg.AddList ("permissions", permissions);
			methodArg.AddPrimative ("isSnack", isSnack);

			AndroidPermissionHelper.CallStatic ("ShowDetailPermissionView", methodArg.ToJsonString ());	
		}

		public void ShowPermissionSnack (string permission, GBDelegate<GBPermissionResult> callback){

			ShowPermissionSnack (permission, null, null, callback);
		}

		public void ShowPermissionSnack (string permission, long lDuration, GBDelegate<GBPermissionResult> callback){

			ShowPermissionSnack (permission, lDuration, null, callback);
		}

		public void ShowPermissionSnack (string permission, SnackbarDuration duration, GBDelegate<GBPermissionResult> callback){

			ShowPermissionSnack (permission, null, duration, callback);
		}

		private void ShowPermissionSnack(string permission, long? lDuration, SnackbarDuration? duration, GBDelegate<GBPermissionResult> callback){

			UnityActivity.Call("runOnUiThread", new AndroidJavaRunnable(() => {
				
				string callbackId = GBCallbackManager.Instance.addGBDelegate (callback);

				GBMethodArguments methodArg = new GBMethodArguments ();
				methodArg.AddString (BaseResult.API_RESPONSE_CALLBACKID_KEY, callbackId);
				methodArg.AddString ("permission", permission);
				methodArg.AddNullablePrimitive<SnackbarDuration> ("duration", duration);
				methodArg.AddNullablePrimitive<long> ("l_duration", lDuration);

				AndroidPermissionHelper.CallStatic ("ShowPermissionSnack", methodArg.ToJsonString ());
			}));
		}

	}
}
#endif
