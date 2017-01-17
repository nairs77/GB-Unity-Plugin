namespace GB.Unity.Editor
{
    using UnityEngine;
    using System.Reflection;

    public static class GBConstantStrings {
        public const string Error = "Error";
        public const string Ok = "OK";

        public class Setup {
            //public const string AndroidSetupFile = "AndroidSetup.txt";
            public const string Title = "GB Configurations";
            public const string Header = "To configure GB or Market information in this project, \n" +
                                        "fill the information below text box and click on the Save Button";
        }

        public class Path {

            public const string AndroidPluginPath = "Plugins/Android";
            public const string iOSPluginPath = "Plugins/iOS";
            public const string AndroidManifestFile = "Plugins/Android/AndroidManifest.xml";
            
        }
        public class GameInfo {
            public const string Title = "";
            public const string AppTitle = "";
        }         
        
        public class GB {
            public const string Title = "GB";
            public const string AppKeyTitle = "AppKey"; 
        }
        
        public class Platform {
            public const string Title = "Platform";
            
            
            public class AndroidSetup {
                
            }
            
            public class iOSSetup {
                
            }
        }
        
        public class Version {
            public const string Title = "SDK Version";
        }
    }    
}

