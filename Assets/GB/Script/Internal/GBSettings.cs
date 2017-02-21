namespace GB {

    using UnityEngine;
    using SimpleJSON;

    public class GBSettings 
    {

        public enum ServerType 
        {
            DEV, 
            QA, 
            REVIEW, 
            LIVE
        };
        
        public enum LogLevel 
        {
            DEBUG = 1, 
            RELEASE = 2
        };

        public enum MarketType {
            STORE,
            AGENCY
        };

        public enum Market {
            APPLE = 0,
            GOOGLE = 1,
            ONESTORE = 3,
            LG_UPLUS = 4,
            CHINA360 = 11,
            BAIDU = 12,
            XIAOMI = 13,
            UC = 14,
            WANDOUJIA = 15,
            HUAWEI = 17,
            MYCARD = 21
        };

        private static GBSettingsImpl settingsImpl = GBSettingsImpl.Instance;
        private static int platformType;
        private static MarketType marketType;

        private static Market activeMarket;

        public static string AccountServer;
        public static string ConentsServer;
        public static string BillingServer;
        public static string SysServer;
        public static string PushServer;

        public static string AppName {
            get {
                return settingsImpl.AppName;
            }
        }
        
        public static string PackageNames {
            get {
                return settingsImpl.PackageName;
            }
        }
        
        public static string AppVersion {
            get {
                return settingsImpl.AppVersion;
            }
        }              
        public static string AppKey {
            get {
                return settingsImpl.ClientSecretKey;
            }
        }
        
        public static int GameCode {
            get {
                return settingsImpl.GameCode;
            }
        }

        public static string MarketAppKey {
            get {
                return settingsImpl.MarketAppKey;
            }
        }

        public static string MarketAppId {
            get {
                return settingsImpl.MarketAppId;
            }
        }        

        public static string MarketAppSecret {
            get {
                return settingsImpl.MarketAppSecret;
            }
        }

        public static string MarketCpId {
            get { 
                return settingsImpl.MarketCpId;
            }
        }

        public static string MarketBuoSecret {
            get { 
                return settingsImpl.MarketBuoSecret;
            }
        }

        public static string MarketPayId {
            get { 
                return settingsImpl.MarketPayId;
            }
        }

        public static string MarketPayRsaPrivate {
            get {
                return settingsImpl.MarketPayRsaPrivate;
            }
        }

        public static string MarketPayRsaPublic {
            get {
                return settingsImpl.MarketPayRsaPublic;
            }
        }

        public static string GetMarketToString() {
            return ((Market)platformType).ToString();
        }

        public static string GetPlatformInfo() {
            JSONNode info = new JSONClass();
            info["appKey"] = MarketAppKey;
            info["appId"] = MarketAppId;
            info["appSecret"] = MarketAppSecret;
            info["platformType"].AsInt = platformType;

            if (platformType == (int)GBSettings.Market.HUAWEI) {
                info["cpId"] = MarketCpId;                  
                info["buoSecret"] = MarketBuoSecret;
                info["payId"] = MarketPayId;
                info["payRsaPrivate"] = MarketPayRsaPrivate;
                info["payRsaPublic"] = MarketPayRsaPublic;
            }

            return info.ToString();
        }    

        public static MarketType GetMarketType() {
            return marketType;
        }

        public static void SetActiveMarket(GBSettings.Market market) {
            activeMarket = market;

            if (market == GBSettings.Market.GOOGLE ||
                market == GBSettings.Market.APPLE) {
                platformType = 0;
                marketType = MarketType.STORE;
            } else if (market == GBSettings.Market.ONESTORE) {
                platformType = (int)market;   
                marketType = MarketType.STORE;
            } else {
                platformType = (int)market;
                marketType = MarketType.AGENCY;
            }

            settingsImpl.GetPlatformInfo((int)market);            
        }

        public static Market GetActiveMarket() {
            return activeMarket;
        }

        public static void SetGBServers(string serverlist) {
            JSONNode root = JSON.Parse(serverlist);
            AccountServer = root["account"];
            ConentsServer = root["content"];
            BillingServer = root["bill"];
            SysServer = root["sys"];
            PushServer = root["push"];      
        }        
 
/*
        public static string FBAppId {
            get {
                return settingsImpl.FBAppId;
            }
            
            set {
                if (settingsImpl.FBAppId != value)
                    settingsImpl.FBAppId = value;
            }
        }
        
        public static string GoogleAppId {
            get {
                return settingsImpl.GoogleAppId;
            }
            
            set {
                if (settingsImpl.GoogleAppId != value)
                    settingsImpl.GoogleAppId = value;
            }
        }

        public static string GCMSenderId {
            get {
                return settingsImpl.GCMSenderId;
            }
            
            set {
                if (settingsImpl.GCMSenderId != value)
                    settingsImpl.GCMSenderId = value;
            }
        }
*/        
    }
}

