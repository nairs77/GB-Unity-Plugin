using UnityEngine;
using SimpleJSON;
using System.Collections;

/**
 * @brief Specify LogLevel
 * @author nairs77@GB.com
 */

	/**
	 * @var TEST_MODE 
	 * @var DEBUG 디버그 모드
	 * @var DEBUG 릴리지 모드
	 */
/* 
public enum LogLevel {
	
	TEST_MODE,
	DEBUG,
	RELEASE
}

public enum Market {
	GOOGLE,
	ONESTORE,
	NAVER,
	GOOGLE_BETA,
	MYCARD,	
	APP_STORE,

}

public enum GBServerType {
	ACCOUNT_SERVER,
	CONTENTS_SERVER,
	BILLING_SERVER,
	PUSH_SERVER,
	SYS_SERVER,
}

public enum PaymentType {
	STORE,
	AGENCY,
}

public interface IStoreInfo { 
	PaymentType getPaymentType();
	bool mustConsumable();
	Market getMarket();
} 


public class GBConfig
{
	public static readonly string TAG = "[GBConfig]";
	
	private static GBConfig _instance;
	public string AccountServer { get; private set; }
	public string ConentServer { get; private set; }
	public string BillingServer { get; private set; }
	public string SysServer { get; private set; }
	public string PushServer { get; private set; }
	public int GameCode {get; private set;}

	public string ClientSecretKey {get; private set;}

	public IStoreInfo Store {get; private set;}
	
	public LogLevel LogLevel{ get; private set;}
	
	public static GBConfig Instance
	{	
		get {
			if (_instance == null ) {	
				_instance = new GBConfig();
				JLog.verbose (TAG + " Has been created.");
			}	
					
			return _instance;
		}
	}	
	public static void SetServerList(string server_list) {
		GBConfig.Instance.SetGBServers(server_list);
	}

	public static string GetServerURL(GBServerType serverType) {
		return GBConfig.Instance.GetServerURLByType(serverType);
	}
	
	public static int GetGameCode() {
		return GBConfig.Instance.GameCode;
	}

	public static void SetClientSecretKey(string key) {
		GBConfig.Instance.ClientSecretKey = key;
	}

	public static string GetClientSecretKey() {
		return GBConfig.Instance.ClientSecretKey;
	}

	public static void SetGameCode(int gameCode) {
		GBConfig.Instance.GameCode = gameCode;
	}
	
	public static void SetLogLevel(LogLevel level) {
		GBConfig.Instance.LogLevel = level; 
	}
	
		public static void SetStore(Market market) {
		if (market == Market.GOOGLE || market == Market.GOOGLE_BETA) {
			GBConfig.Instance.Store = new GoogleStore(market);	
		} else if (market == Market.ONESTORE) {
			GBConfig.Instance.Store = new OneStore(market);
		} else if (market == Market.MYCARD) {
			GBConfig.Instance.Store = new MyCardStore(market);
		} else if (market == Market.APP_STORE) {
			GBConfig.Instance.Store = new AppleStore(market);
		} else {
            #if UNITY_ANDROID
			GBConfig.Instance.Store = new GoogleStore(market);
            #elif UNITY_IPHONE
			GBConfig.Instance.Store = new AppleStore(market);
            #else
            Debug.Log("Error Store");
            #endif                               
        }		        
	}
	
	public static IStoreInfo getStroe() {
		return GBConfig.Instance.Store;
	}
	
	public static LogLevel GetLogLevel() {
		return GBConfig.Instance.LogLevel;
	}
	
	private void SetGBServers(string serverlist) {
		JSONNode root = JSON.Parse(serverlist);
			
		AccountServer = root["account"];
		ConentServer = root["content"];
		BillingServer = root["bill"];
		SysServer = root["sys"];
		PushServer = root["push"];		
	}
	
	private string GetServerURLByType(GBServerType serverType) {
		switch (serverType) {
			case GBServerType.ACCOUNT_SERVER:
				return AccountServer;
			case GBServerType.CONTENTS_SERVER:
				return ConentServer;
			case GBServerType.BILLING_SERVER:
				return BillingServer;
			case GBServerType.PUSH_SERVER:
				return PushServer;
			case GBServerType.SYS_SERVER:
				return SysServer;
			
			default:
				return "";
		}
	}
}

public class GoogleStore : IStoreInfo {
	
	private Market marketType;
	
	public GoogleStore(Market market) {
		marketType = market;	
	}
	public PaymentType getPaymentType() {
		return PaymentType.STORE;
	}
	public bool mustConsumable() {
		return true;
	}
	public Market getMarket() {
		return marketType;
	}	
}

public class OneStore : IStoreInfo {
	
	private Market marketType;
	
	public OneStore(Market market) {
		marketType = market;	
	}
	public PaymentType getPaymentType() {
		return PaymentType.STORE;
	}
	public bool mustConsumable() {
		return true;
	}
	public Market getMarket() {
		return marketType;
	}	
}

public class MyCardStore : IStoreInfo {
	
	private Market marketType;
	
	public MyCardStore(Market market) {
		marketType = market;	
	}
	public PaymentType getPaymentType() {
		return PaymentType.AGENCY;
	}
	public bool mustConsumable() {
		return false;
	}
	public Market getMarket() {
		return marketType;
	}	
}

public class AppleStore : IStoreInfo {
	private Market marketType;
	
	public AppleStore(Market market) {
		marketType = market;	
	}
	public PaymentType getPaymentType() {
		return PaymentType.STORE;
	}
	public bool mustConsumable() {
		return false;
	}
	public Market getMarket() {
		return marketType;
	}	
}
*/