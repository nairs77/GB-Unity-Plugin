namespace GB {

    using UnityEngine;
    using System.IO;
    using System.Collections.Generic;
    using System.Linq;
    using System.Xml.Linq;
    using SimpleJSON;    

    #if UNITY_EDITOR
    using UnityEditor;
    #endif
        
    #if UNITY_EDITOR
    [InitializeOnLoad]
    #endif
        
    public class GBSettingsImpl : ScriptableObject {
               
        public const string SDK_VERSION = "0.9.1";
                      
        private const string GBProjectAssetName = "GBSettings";
        private const string GBResourcePath = "GB/Resources";
        private const string GBAssetExt = ".asset";
        
        public string ClientSecretKey;
        public int GameCode;

        public string AppName;
        public string PackageName;
        public string AppVersion;

        public string FBAppId;
        public string GoogleAppId;

        public string MarketAppKey;
        public string MarketAppId;
        public string MarketAppSecret;   

        public string MarketCpId;
        public string MarketBuoSecret;
        public string MarketPayId;
        public string MarketPayRsaPrivate;
        public string MarketPayRsaPublic;

        public int Market;
        public string GcmSenderId;      
        public string ConfigData;
              
        private static GBSettingsImpl gInstance;
       
        public static GBSettingsImpl Instance {
            get {                
                if (gInstance == null) {
                    gInstance = Resources.Load(GBProjectAssetName) as GBSettingsImpl;
                    
                    if (gInstance == null) {
                        // If not found, autocreate the asset object.
                        gInstance = ScriptableObject.CreateInstance<GBSettingsImpl>();
                        #if UNITY_EDITOR
                        string resourcePath = Path.Combine(Application.dataPath, GBResourcePath);
                        if (!Directory.Exists(resourcePath)) {
                            AssetDatabase.CreateFolder("Assets/GB", "Resources");
                        }
                        
                        string fullPath = Path.Combine(Path.Combine("Assets", GBResourcePath), GBProjectAssetName + GBAssetExt);
                        AssetDatabase.CreateAsset(gInstance, fullPath);
                        #endif
                    }
                }                
                return gInstance;
            }
        }
/*
        public string AppName {
            get {
                return PlayerSettings.productName;                              
            }
        }
        
        public string AppBundleID {
            get {
                return PlayerSettings.bundleIdentifier;
            }
        }
        
        public string AppVersion {
            get {
                return PlayerSettings.bundleVersion;
            }
        }              
*/
  
        public void SaveConfiguration(string clientSecretKey, string gameCode, string configData) {
            this.ClientSecretKey = clientSecretKey;
            int code;
            int.TryParse(gameCode, out code);
            this.GameCode = code;
            this.ConfigData = configData;

            SaveSettings();
        }

        public void SaveSettings() {
            #if UNITY_EDITOR
            //Instance.saveSettings();
            EditorUtility.SetDirty(Instance);
            #endif
        }
                        
        public string GetPlatformInfo(int marketCode) {
            return GetInfoFromConfig(marketCode);
        }
        //- Private Methods        
        
        private string GetInfoFromConfig(int marketCode) {
            if (string.IsNullOrEmpty(ConfigData)) {
                return "";
            }

            XDocument document = XDocument.Parse(ConfigData);           
            GBSettings.Market selectedMarket = (GBSettings.Market)marketCode;

            Debug.Log(selectedMarket.ToString());

            #region Fetch Market Info
            if (selectedMarket == GBSettings.Market.HUAWEI) {
                var selectMarket = from r in document.Descendants(selectedMarket.ToString())
                    select new
                {
                    appKey = r.Element("appKey").Value,
                    appId = r.Element("appId").Value,
                    appSecret = r.Element("appSecret").Value,
                    cpId = r.Element("cpId").Value,
                    buoSecret = r.Element("buoSecret").Value,
                    payId = r.Element("payId").Value,
                    payRsaPrivate = r.Element("payRsaPrivate").Value,
                    payRsaPublic = r.Element("payRsaPublic").Value,
                };

                JSONNode info = new JSONClass();
                foreach (var r in selectMarket) {
                    Debug.Log("loop !!!");
                    MarketAppKey = r.appKey;
                    MarketAppId = r.appId;
                    MarketAppSecret = r.appSecret;
                    MarketCpId = r.cpId;
                    MarketBuoSecret = r.buoSecret;
                    MarketPayId = r.payId;
                    MarketPayRsaPrivate = r.payRsaPrivate;
                    MarketPayRsaPublic = r.payRsaPublic;

                    info["appKey"] = r.appKey;
                    info["appId"] = r.appId;
                    info["appSecret"] = r.appSecret;
                    info["platformType"].AsInt = marketCode;
                    info["cpId"] = r.cpId;
                    info["buoSecret"] = r.buoSecret;
                    info["payId"] = r.payId;
                    info["payRsaPrivate"] = r.payRsaPrivate;
                    info["payRsaPublic"] = r.payRsaPublic;                 
                }
                return info.ToString();  
            } else {
                var selectMarket = from r in document.Descendants(selectedMarket.ToString())
                    select new
                {
                    appKey = r.Element("appKey").Value,
                    appId = r.Element("appId").Value,
                    appSecret = r.Element("appSecret").Value,
                };

                JSONNode info = new JSONClass();
                foreach (var r in selectMarket) {
                    Debug.Log("loop !!!");
                    MarketAppKey = r.appKey;
                    MarketAppId = r.appId;
                    MarketAppSecret = r.appSecret;

                    info["appKey"] = r.appKey;
                    info["appId"] = r.appId;
                    info["appSecret"] = r.appSecret;
                    info["platformType"].AsInt = marketCode;                 
                }
                return info.ToString();  
            }            
            #endregion

            

                                            
        }        
    }    
    
}