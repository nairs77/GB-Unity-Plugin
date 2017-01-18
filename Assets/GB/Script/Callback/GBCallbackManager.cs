
namespace GB.Callback
{

	using System;
	using System.Reflection;
	using System.Collections.Generic;
	using UnityEngine;
	using GB;

	internal class GBCallbackManager : MonoBehaviour
	{

		private static readonly string PLUGIN_NAME = "GBPluginObject";

		private static GBCallbackManager gbCallbackManager = null;
		public static GBCallbackManager Instance {
			get {
				if (gbCallbackManager == null) {
					gbCallbackManager = GameObject.FindObjectOfType(typeof(GBCallbackManager)) as GBCallbackManager;				
					if (gbCallbackManager == null) 
					{			
						GameObject container = new GameObject();
						container.name = PLUGIN_NAME;
						gbCallbackManager = container.AddComponent(typeof(GBCallbackManager)) as GBCallbackManager;
						DontDestroyOnLoad(gbCallbackManager);
					}					
				}
				return gbCallbackManager;
			}
		}
			
		private IDictionary<string, object> joypleDelegates = new Dictionary<string, object> ();
		private int nextAsyncId;

		public void Awake ()
		{
			UnityEngine.Object.DontDestroyOnLoad (this);
			this.name = "JoyplePluginObject";
		}


		public string addAction(Action<bool, string> action){
			if(action == null)
				return null;

			this.nextAsyncId++;
			this.joypleDelegates.Add (this.nextAsyncId.ToString (), action);
			return this.nextAsyncId.ToString ();
		}

		public void onActionResponse(string callbackId, bool success, string result){
			if (result == null || callbackId == null)
				return;
			
			object callback;
			if (this.joypleDelegates.TryGetValue (callbackId, out callback)) {
				Action<bool, string> joypleAction = callback as Action<bool, string>;
				joypleAction (success, result);
				this.joypleDelegates.Remove (callbackId);
			}
		}

		public string addJoypleDelegate<T> (JoypleDelegate<T> callback) where T : IResult
		{
			if (callback == null)
				return null;

			this.nextAsyncId++;
			this.joypleDelegates.Add (this.nextAsyncId.ToString (), callback);
			return this.nextAsyncId.ToString ();
		}

		public void onJoypleResponse (IResult result)
		{
			if (result == null || result.CallbackId == null)
				return;
			
			object callback;
			if (this.joypleDelegates.TryGetValue (result.CallbackId, out callback)) {
				ConveyCallback (callback, result);
				if(!result.IsKeepCallback)
					this.joypleDelegates.Remove (result.CallbackId);
			}
		}

		private void ConveyCallback (object callback, IResult result)
		{
			if (callback == null || result == null)
				return;

			if (TryConveyCallback<GBPermissionResult> (callback, result))
				return;
			
			throw new NotSupportedException ("Unexpected result type: " + callback.GetType ().FullName);
		}


		private bool TryConveyCallback<T> (object callback, IResult result) where T : IResult
		{
			JoypleDelegate<T> joypleDelegate = callback as JoypleDelegate<T>;
			if (joypleDelegate != null) {
				joypleDelegate ((T)((object)result));
				return true;
			}
			return false;
		}
			

		// AOS, IOS Callback 
		public void onPermissionComplete(string message){

			GBPermissionResult PResult = new GBPermissionResult (message);
			onJoypleResponse (PResult);
		}

	}
}

