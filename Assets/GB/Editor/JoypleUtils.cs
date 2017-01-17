namespace GB.Unity.Editor {
    using UnityEngine;
    using UnityEditor;
    using System.IO;
    
    public static class GBUtils {
    
        public static string SlashesToPlatformSeparator(string path) {
            return path.Replace("/", System.IO.Path.DirectorySeparatorChar.ToString());
        }
        
        public static void GeneratingManifest() {
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
                
            }

//            UpdateManifest(outputFile);            
            
        }
        
        private static bool HasAndroidManifest() {
            
            return false;
        }
        
        public static void Alert(string title, string msg)
        {
            EditorUtility.DisplayDialog(title, msg, "OK");
        }
        
        public static void AlertError(string msg) {
            Alert("Error :", msg);
        }
        
        public static bool Confirm(string title, string msg) {
            return EditorUtility.DisplayDialog(title, msg, "Ok", "Cancel");
        }
        
        public static string ReadTemplateFile(string filePath) {
            filePath = SlashesToPlatformSeparator(filePath);
            return ReadFile(filePath);
        }
        
        private static string ReadFile(string filePath) {
            filePath = SlashesToPlatformSeparator(filePath);
            if (!System.IO.File.Exists(filePath)) {
                AlertError("Plugin error: file not found: " + filePath);
                return null;
            }

            System.IO.StreamReader sr = new System.IO.StreamReader(filePath);
            string body = sr.ReadToEnd();
            sr.Close();
            return body;
        }
    }
}


