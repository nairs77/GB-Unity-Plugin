using System;
using UnityEngine;
using System.Collections.Generic;

/**
 * @brief Manage GB SDK
 * @author gebros.nairs77@gmail.com
 */

namespace GB {
	public class GBManager : MonoBehaviour{
		
		private static readonly string TAG = "GBManager";
							
		public delegate void DelegateNativeEvents(string eventName);
									
		private static GBManager _instance;
		private static GBPluginManager _pluginManager;
		public DelegateNativeEvents onHandleNativeEvent;		
		
		/////////////////////////////////////////////////////////////////////////////////////////////////////
		#region Singleton Implementation
		/////////////////////////////////////////////////////////////////////////////////////////////////////
		// Utilizing singleton pattern (Not thread safe!  That should be ok).
		// http://msdn.microsoft.com/en-us/library/ff650316.aspx
		
		/**
	 * @brief Get GBManager Instance
	 * @return GBManager
	 */
		public static GBManager Instance
		{		
			get
			{
				if(_instance == null ) 
				{				
					_instance = GameObject.FindObjectOfType(typeof(GBManager)) as GBManager;				
					if (!_instance) 
					{			
						GameObject container = new GameObject();
						container.name = TAG;					
						_instance = container.AddComponent(typeof(GBManager)) as GBManager;
						DontDestroyOnLoad(_instance);

						_pluginManager = new GBPluginManager();
					}					
					GBLog.verbose (TAG + " Has been created.");
					if (_pluginManager == null)
						_pluginManager = new GBPluginManager();
				}			
				return _instance;
			}
		}
		
		#endregion
		
		public GBPluginManager PluginManager
		{
			get {
				return _pluginManager;
			}
		}

		public void HandleNativeEvent(string result) {
			onHandleNativeEvent(result);			
		}

		public void Destroy() {
			_instance = null;
			_pluginManager = null;
			GBLog.verbose ("Destroy ---");
		}
/*
		public static void SetActiveMarket(GBSettings.Market market, Action<bool, string> callback) {
			GBSettings.SetActiveMarket(market);
			GBCommonRequest.SetActiveMarket(market, callback);
		}
*/
        public static void ConfigureSDKWithGameInfo(string clientSecretKey, int gameCode, GBSettings.LogLevel logLevel) {
#if !UNITY_EDITOR && UNITY_IPHONE			
			GBManager.Instance.PluginManager.ConfigureSDKWithGameInfo(clientSecretKey, gameCode, GBSettings.Market.APPLE, logLevel);
#elif !UNITY_EDITOR && UNITY_ANDROID
			GBManager.Instance.PluginManager.ConfigureSDKWithGameInfo(clientSecretKey, gameCode, GBSettings.Market.GOOGLE, logLevel);
#endif
		}

        /**
		* @brief Configuration GB SDK
		* @param clientSecret String
		* @param market Market
		* @param gameCode int
		* @param logLevel LogLevel
		*/
        public static void ConfigureSDKWithGameInfo(string clientSecretKey, int gameCode, GBSettings.Market market, GBSettings.LogLevel logLevel) {
			GBManager.Instance.PluginManager.ConfigureSDKWithGameInfo(clientSecretKey, gameCode, market, logLevel);
		}

		/**
		 *
		 */
		// public static void ConfigureSDKWithGameInfo(GBSettings.Market releaseMarket) {
		// 	GBManager.Instance.PluginManager.ConfigureSDKWithGameInfo(GBSettings.AppKey, GBSettings.GameCode, GBSettings.GetPlatformInfo(), GBSettings.LogLevel.DEBUG);
		// }
		//  public static void ConfigureSDKWithGameInfo(string clientSecretKey, int gameCode, string platformInfo, GBSettings.LogLevel logLevel) {
		//  	GBManager.Instance.PluginManager.ConfigureSDKWithGameInfo(clientSecretKey, gameCode, platformInfo, logLevel);
		//  }
/*		
		public static void GetGlobalServerInfo(string branchURL, Action<bool, string> callback) {
			GBCommonRequest.RequestGlobalServerInfo(branchURL, callback);
		}
*/		
		public static void SetPromotionItems(List<string> skus) {
			GBManager.Instance.PluginManager.SetPromotionItems(skus);
		}

		public static string GetMCC() {
			return GBManager.Instance.PluginManager.GetMCC();
		}

		public static string GetCurrentLanguage() {
			return GBManager.Instance.PluginManager.GetCurrentLanguage();
		}

		public static string GetDeviceId() {
			return GBManager.Instance.PluginManager.GetDeviceId();
		}

		public static string GetDeviceModel() {
			return GBManager.Instance.PluginManager.GetDeviceModel();
		}
/*
		public static void SetGameLanguage(LanguageType languageType) {
			GBManager.Instance.PluginManager.SetGameLanguage(languageType);	
		}		
*/		
	}
}
