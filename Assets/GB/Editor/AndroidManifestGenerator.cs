namespace GB.Unity.Editor {
    using UnityEngine;
    using System.IO;
    using System.Xml;
        
    public static class AndroidManifestGenerator {
        
        public static void GenerateManifest() {
            var outputFile = Path.Combine(Application.dataPath, GBConstantStrings.Path.AndroidManifestFile);
            
            if (File.Exists(outputFile)) {
                if (GBUtils.Confirm("GenerateManifest", "AndroidManifest alreadyy exists!!! Replace it?")) {
                    string rename = Path.Combine(Application.dataPath, "Plugin/Android/AndroidManifest" + "-" + "template.xml");
                    File.Move(outputFile, rename);
                } else {
                    GBUtils.Alert("GenerateManifest", "Pass");
                    return;
                }
            } else {
                CreateManifest();
            }
            
            //UpdateManifest();
        }
        
        private static void CreateManifest() {
            string destFilename = GBUtils.SlashesToPlatformSeparator(GBConstantStrings.Path.AndroidManifestFile);
            string templateBody = GBUtils.ReadTemplateFile("Assets/GB/Editor/AndroidManifest-template.txt");
            
            // package
            templateBody = templateBody.Replace("__APP_BUNDLE_ID__", GBSettings.PackageNames);
            templateBody = templateBody.Replace("__APP_VERSION_NAME__", GBSettings.AppVersion);
            
        }
        private static XmlNode FindChildNode(XmlNode parent, string name) {
            XmlNode curr = parent.FirstChild;
            
            while (curr != null) {
                if (curr.Name.Equals(name)) {
                    return curr;
                }

                curr = curr.NextSibling;
            }
            return null;
        }

        private static XmlNode FindChildNode(XmlNode parent, string name, string attr, string attrValue) {
            XmlNode curr = FindChildNode(parent, name);
            
            while (curr != null) { 
                if (curr.Name.Equals(name)) {
                    if (curr.Attributes[attr] != null) {
                        if (curr.Attributes[attr].Value == attrValue) {
                            return curr;
                        }
                    }
                }
                curr = curr.NextSibling;
            }

            return null;
        }
    }
}