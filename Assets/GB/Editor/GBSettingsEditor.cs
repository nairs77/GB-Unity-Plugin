namespace GB.Unity.Editor 
{
    using UnityEngine;
    using UnityEditor;
    using System;
    using GB;
    
    [CustomEditor(typeof(GBSettingsImpl))]
    public class GBSettingsEditor : Editor 
    {
        public enum Market {
            GOOGLE = 1,
            APPLE = 2,
        };

        private static GBSettingsImpl instance;
        // Game Info
        private GUIContent appNameLabel = new GUIContent("App Name :", "For your own use and organization.\n");
        private GUIContent appBundleIdLabel = new GUIContent("Bundle Identifier :", "App Bundle Id.\n");
        private GUIContent appVersion = new GUIContent("App Version :", "Required.");
        
        // GB
        private GUIContent appkeyLabel = new GUIContent("App Key (Client Secret Key) :", "Required!!!");
        private GUIContent appGameCodeLabel = new GUIContent("Game Code :", "Required");
        
        private GUIContent appKeyTitleLabel = new GUIContent("AppKey :", "Required");
        private GUIContent appIDTitleLabel = new GUIContent("AppId :", "Required");
        private GUIContent appSecretTitleLabel = new GUIContent("AppSecret :", "Required");

        private const string _generate_manifest = "Generating Manifest";
        private const string _update_manifest = "Updating Manifest";
        
        private bool showAppSettings = false;
        private bool showGBSettings = false;
        
        private bool _isAvailable = true;
        private bool showPlatformSettings = false;
        #if UNITY_5
        private bool showiOSSettings = (EditorUserBuildSettings.activeBuildTarget == BuildTarget.iOS);
        #else
        private bool showiOSSettings = (EditorUserBuildSettings.activeBuildTarget == BuildTarget.iPhone);
        #endif
        private bool showAndroidSettings = (EditorUserBuildSettings.activeBuildTarget == BuildTarget.Android);
        private bool showArcadeSettings = (EditorUserBuildSettings.activeBuildTarget == BuildTarget.StandaloneWindows64);
        //private bool showCanvasSettings = (EditorUserBuildSettings.activeBuildTarget == BuildTarget.WebGL) || (EditorUserBuildSettings.activeBuildTarget == BuildTarget.WebPlayer);

        private Market selectedMarket = Market.GOOGLE;
        private bool _isGlobalSetttings = false;        
      
        [MenuItem("GeBros/GB Settings", false, 100)]
        public static void Edit() 
        {
            Selection.activeObject = GBSettingsImpl.Instance;
        }

        public override void OnInspectorGUI() {
            instance = (GBSettingsImpl)target;            
            #region GB SDK Version
            EditorGUILayout.BeginHorizontal();
            EditorGUILayout.LabelField("GB SDK", EditorStyles.boldLabel);            
            EditorGUILayout.LabelField("v" + GBSettingsImpl.SDK_VERSION);
            // Change Log에 대한 Redmine link를 걸어둬라.
            
            EditorGUILayout.EndHorizontal();            
            #endregion
            
            EditorGUILayout.Separator();
            
            #region App Settings
            LoadAppSettings();
            #endregion

            EditorGUILayout.Separator();
                        
            #region GB Settings
            LoadGBSettings();
            #endregion

            EditorGUILayout.Separator();
                        
            #region Platform Settings
            LoadPlatformSettings();
            #endregion
            EditorGUILayout.Separator();   

            SaveSettings();
        }    
        
        private void LoadAppSettings() {
            this.showAppSettings = EditorGUILayout.Foldout(this.showAppSettings, "App Info :");
            
            if (this.showAppSettings) {
                EditorGUILayout.BeginHorizontal();
                EditorGUILayout.LabelField(this.appNameLabel);
                EditorGUILayout.LabelField(this.appBundleIdLabel);               
                EditorGUILayout.EndHorizontal();
                
                EditorGUILayout.BeginHorizontal();
                if (!string.IsNullOrEmpty(instance.AppName))
                    instance.AppName = PlayerSettings.productName;
                if (!string.IsNullOrEmpty(instance.PackageName))
                    instance.PackageName = PlayerSettings.bundleIdentifier;
                if (!string.IsNullOrEmpty(instance.AppVersion))
                    instance.AppVersion = PlayerSettings.bundleVersion;

                instance.AppName = EditorGUILayout.TextField(instance.AppName);
                instance.PackageName = EditorGUILayout.TextField(instance.PackageName);
                EditorGUILayout.EndHorizontal();
                EditorGUILayout.LabelField(this.appVersion);                
                instance.AppVersion = EditorGUILayout.TextField(instance.AppVersion);                                                                           
            }

            if (!this.showAppSettings && 
                (string.IsNullOrEmpty(instance.AppName) || string.IsNullOrEmpty(instance.PackageName))) { 
                EditorGUILayout.HelpBox("Empty App Name or Bundle ID", MessageType.Error);
                _isAvailable = false;
            }
        }
        
        private void LoadGBSettings() {
            this.showGBSettings = EditorGUILayout.Foldout(this.showGBSettings, "GB (required) :");
            
            if (this.showGBSettings) {
                EditorGUILayout.BeginHorizontal();
                EditorGUILayout.LabelField(this.appkeyLabel);
                EditorGUILayout.LabelField(this.appGameCodeLabel);
                EditorGUILayout.EndHorizontal();
                
                EditorGUILayout.BeginHorizontal();
                EditorGUILayout.LabelField(instance.ClientSecretKey);
                EditorGUILayout.LabelField(instance.GameCode.ToString());
                EditorGUILayout.EndHorizontal();
            }
            
            if (!this.showGBSettings && 
                (string.IsNullOrEmpty(instance.ClientSecretKey) || instance.GameCode == 0)) {
                EditorGUILayout.HelpBox("GB ", MessageType.Error);
                _isAvailable = false;
            }                        
        }
        
        private void LoadPlatformSettings() {
            AndroidSettings();
            EditorGUILayout.Space();                
            iOSSettings();
            if (_isGlobalSetttings)
                ThirdPartySettings();                                                       
        }
        
        private void ThirdPartySettings() {
            // - Facebook App Id
            showPlatformSettings = EditorGUILayout.Foldout(showPlatformSettings, "Platform :");
            // TODO : Utility IsNullOrEmpty..
            if (showPlatformSettings) {
/*                
                EditorGUILayout.BeginHorizontal();           
                bool _isFbEnable = (instance.FBAppId.Length > 0) ? true : false;
                _isFbEnable = EditorGUILayout.Toggle("Facebook ", _isFbEnable);
                instance.FBAppId = EditorGUILayout.TextField(instance.FBAppId);
                EditorGUILayout.EndHorizontal();
                
                // - Google Project Id
                EditorGUILayout.BeginHorizontal();           
                bool _isGoogleEnabled = (instance.GoogleAppId.Length > 0) ? true : false;
                _isGoogleEnabled = EditorGUILayout.Toggle("Google+ ", _isGoogleEnabled);
                instance.GoogleAppId = EditorGUILayout.TextField(instance.GoogleAppId);
                EditorGUILayout.EndHorizontal();            
                // _showCanvasSettings();
*/                
            }                                   
        }
        
        private void AndroidSettings() {          
            showAndroidSettings = EditorGUILayout.Foldout(showAndroidSettings, "Android :");           

            if (this.showAndroidSettings) {
                if (selectedMarket == Market.GOOGLE ||
                selectedMarket == Market.APPLE) {
                    _isGlobalSetttings = true;

                    if (selectedMarket == Market.APPLE)
                        _isAvailable = false;
                    else
                        _isAvailable = true;
                } else {
                    _isAvailable = true;
                    _isGlobalSetttings = false;
                }

                selectedMarket = (Market)instance.Market;
                if (_isGlobalSetttings) {
                    ShowGlobalSettings();
                } else {
                    ShowChinaSettings();
                }

                if (!_isAvailable) {
                    EditorGUILayout.HelpBox("Error", MessageType.Error);                    
                }                             

                instance.GetPlatformInfo(instance.Market);                 
            }
        }
        
        private void iOSSettings() {
            showiOSSettings = EditorGUILayout.Foldout(showiOSSettings, "iOS :");            
            EditorGUILayout.BeginHorizontal();
//            EditorGUILayout.LabelField(this._iosTitleLabel);
            EditorGUILayout.EndHorizontal();
        }
        
        // private void ShowCanvasSettings() {
        //     showCanvasSettings = EditorGUILayout.Foldout(showCanvasSettings, "Canvas :");
            
        //     if (showCanvasSettings) {
                
        //     }
        // }

        private void ShowGlobalSettings() {
            selectedMarket = (Market)EditorGUILayout.EnumPopup("Market :", selectedMarket);            
            // - Push Setting (GCM)
            EditorGUILayout.BeginHorizontal();
/*            
            bool isPushEnabled = !string.IsNullOrEmpty(instance.GCMSenderId);
            isPushEnabled = EditorGUILayout.Toggle("Push (GCM Sender Id):", isPushEnabled);
            instance.GCMSenderId = EditorGUILayout.TextField(instance.GCMSenderId);
*/            
            EditorGUILayout.EndHorizontal();
            
            instance.Market = (int)selectedMarket;
        }

        private void ShowChinaSettings() {
            selectedMarket = (Market)EditorGUILayout.EnumPopup("Market :", selectedMarket);

            EditorGUILayout.BeginVertical();
            EditorGUILayout.LabelField(this.appKeyTitleLabel);

            Debug.Log(instance.MarketAppKey);
            EditorGUILayout.TextField(instance.MarketAppKey);            
            EditorGUILayout.LabelField(this.appIDTitleLabel);            
            EditorGUILayout.TextField(instance.MarketAppId);
            EditorGUILayout.LabelField(this.appSecretTitleLabel);            
            EditorGUILayout.TextField(instance.MarketAppSecret);
            EditorGUILayout.EndVertical();

            instance.Market = (int)selectedMarket;                                                                                                                              
        }
        
        private void SaveSettings() {
            if (GUILayout.Button("Save Setting!!!")) {
                try {
                    instance.SaveSettings();
                } catch (System.Exception e) {
                    EditorUtility.DisplayDialog("Error... ", e.Message, "OK");
                }
            }            
        }
/*        
        private void GenerateManifest() {
            // 1. Has Manifest?
            
            // 2. Read GB Default Template
            // 3. Generate package name (App Bundle ID)
            // 4. Generate Version Code / Name
            // 4. Generate Application tag (Only MyCard)
            // 5. Generate GBUnityActivity (Default)
            // 
            // 6. Third Party Setting
            // 7. Android Target (Target version)
            
            var outputFile = Path.Combine(Application.dataPath, "Plugins/Android/AndroidManifest.xml");

            // only copy over a fresh copy of the AndroidManifest if one does not exist
            if (!File.Exists(outputFile)) {
                //UpdatingManifest();
                CreateManifest();
            }

            UpdateManifest();
                        
        }
*/        
    }
}

