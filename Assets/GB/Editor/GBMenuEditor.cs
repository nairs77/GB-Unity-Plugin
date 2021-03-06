namespace GB.Unity.Editor {
    using UnityEngine;
    using UnityEditor;
    
    public class GBMenuEditor {
        [MenuItem ("GeBros/Documents/Start Guide...", false, 200)]
        public static void ShowDocumentsStartGuide() {
            Application.OpenURL("127.0.0.1");
        }
        
        [MenuItem ("GeBros/Documents/API Guide...", false, 201)]
        public static void ShowDocumentsAPIGuide() {
            
        }

        [MenuItem ("GeBros/Download", false, 300)]
        public static void goDownloadSite() {
            
        }   
             
        [MenuItem ("GeBros/About", false, 400)]
        public static void About() {
            // string msg = GPGSStrings.AboutText + PluginVersion.VersionString + " (" +
            //              string.Format("0x{0:X8}", GooglePlayGames.PluginVersion.VersionInt) + ")";
            // EditorUtility.DisplayDialog(GPGSStrings.AboutTitle, msg,
            //     GPGSStrings.Ok);
            GBUtils.Alert(GBConstantStrings.Version.Title, GBSettingsImpl.SDK_VERSION);            
        }
    }
}