namespace GB.Unity.Editor 
{
    using System;
    using System.Collections;
    using System.IO;
    using UnityEditor;
    using UnityEngine;

    public class GBAndroidSettingWindow : EditorWindow 
    {
        private Vector2 scroll;

        private string mClientSecretKey;
        private string mGameCode;
        private string mConfigData = string.Empty;        
        
        [MenuItem("GB/SDK Configuration", false, 1)]
        public static void OnSDKConfig() {
            EditorWindow window = EditorWindow.GetWindow(typeof(GBAndroidSettingWindow), true, GBConstantStrings.Setup.Title);
            window.minSize = new Vector2(500, 400);
        }

        public void OnGUI()
        {
            // GUI.skin.label.wordWrap = true;
            GUILayout.BeginVertical();
            // GUIStyle link = new GUIStyle(GUI.skin.label);
            GUILayout.Space(15);

            if (string.IsNullOrEmpty(mConfigData))
                mConfigData = GBUtils.ReadTemplateFile("Assets/GB/Editor/AndroidSetup.txt");
            
            GUILayout.Label(GBConstantStrings.Setup.Header);

            GUILayout.Label("GB (Required)", EditorStyles.boldLabel);

            mClientSecretKey = EditorGUILayout.TextField("Client Secret Key :", mClientSecretKey, GUILayout.Width(480));
            mGameCode = EditorGUILayout.TextField("Game Code :", mGameCode, GUILayout.Width(480));            

            GUILayout.Space(5);
            GUILayout.Label("Markets(China) - Information", EditorStyles.boldLabel);
            GUILayout.Label("if you will prepare to build China.., you fill the information below.");
            GUILayout.Space(5);

            scroll = GUILayout.BeginScrollView(scroll);
            mConfigData = EditorGUILayout.TextArea(mConfigData, GUILayout.Width(475), GUILayout.Height(Screen.height));
            GUILayout.EndScrollView();
            GUILayout.Space(10);

            GUILayout.BeginHorizontal();
            GUILayout.FlexibleSpace();
            if (GUILayout.Button("Save", GUILayout.Width(100)))
            {
                GBSettingsImpl.Instance.SaveConfiguration(mClientSecretKey, mGameCode, mConfigData);
                Close();
            }
            if (GUILayout.Button("Cancel", GUILayout.Width(100)))
            {
                //GBSettings.SetConfigData(mClientSecretKey, mGameCode, mConfigData);
                Close();
            }

            GUILayout.FlexibleSpace();
            GUILayout.EndHorizontal();
            GUILayout.Space(20);
            GUILayout.EndVertical();
        }        
    }
}