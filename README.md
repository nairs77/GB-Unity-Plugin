# GeBros plugin for Unity
_Copyright (c) 2016 GeBros Inc. All rights reserved._

The GeBros plugin for Unity&reg; is a plugin that allows game developers to integrate with
the GeBros API from a game written in Unity&reg;

## Overview

The plugin provides support for the
following features of the GeBros API:<br/>

* SNS sign in (Facebook, Google)
    * (Android) Google Identity platform, Facebook
    * (iOS) Guest, Facebook
* In App Purchase 
* The Google Play Games API
* Google AdMob
    * Google
    * UnityAds
    * Vungle
    * AppLovin

System requirements:

* Unity&reg; 5 or above.
* To deploy on Android:
    * Android SDK
    * Android v4.0 or higher
    * Google Play Services library, version 8.4 or above
* To deploy on iOS:
    * XCode 6 or above

## Installation
To download the plugin, download it as a ZIP file and unpack it. Then, it consists of two folders as below:

    Assets  
    Framework

simply open your game project in Unity (open project that file into
your project's assets), as you would any other Unity Project. 

**Note:**  For more information Frmework folder, See iOS Setup page

## Android Setup

Next, set up the path to your Android SDK installation in Unity. This is located in the
preferences menu, under the **External Tools** section.

To configure your Unity game to run on Android, first
open the Android SDK manager and verify that you have downloaded the following
packages.  Depending on if you are using the SDK manager from Android Studio,
or using the standalone SDK manager, the name of the components may be different.
- Google Play Services
- Android Support Library
- Local Maven repository for Support Libraries (Also known as Android Support Repository)
- Google Repository
- Android 6.0 (API 23) (this does not affect the min SDK version).

Next, configure your game's package name. To do this, click **File > Build Settings**,
select the **Android** platform and click **Player Settings** to show Unity's
Player Settings window. In that window, look for the **Bundle Identifier** setting
under **Other Settings**. Enter your package name there (for example
_com.example.my.awesome.game_).

## iOS Setup

Next, open the iOS build settings dialog. To do so, click File > Build Settings, select the iOS platform, and click Player Settings. Find the Bundle Identifier setting and enter your bundle identifier there.

### Not Supported PGS
Next, Find Scripting Define Symbols setting and enter your macro in Player Settings

    NO_GPGS

### Build Project

When building for iOS in Unity, a post build step is executed that runs python script and then adds the required libraries and frameworks directly to the XCode project.

    Assets/GB/Editor/GBPostprocessScript.cs

```csharp
public static void OnPostprocessBuild(..., ...) {
  ....

  string frameworkPath = /* Unity Project Path */ + "Framework/";

		Process buildProcess = new Process();
		buildProcess.StartInfo.FileName = "python";
		buildProcess.StartInfo.Arguments = string.Format(/* Unity Project Path */ + "Assets/GB/Editor/post_process.py \"{0}\" \"{1}\"", pathToBuildProject, frameworkPath);
		buildProcess.StartInfo.UseShellExecute = false;
		buildProcess.StartInfo.RedirectStandardOutput = false;
		
		buildProcess.Start();
		buildProcess.WaitForExit();

    ....
}
```

    Assets/GB/Editor/post_process.py


```python
...
facebookAppID = /* Facebook App ID */
...
plist["AppLovinSdkKey"] = /* AppLovin Sdk Key */
```

## Configuration & Initialization GeBros API

In order to require access to user Social authentication, handle  payment (In App Purchase) and show Google Admob or require access to a player Google Play Game Service.

```csharp
    using GB;
    using GooglePlayGames;

		GBManager.ConfigureSDKWithGameInfo("", 1, GBSettings.LogLevel.DEBUG);
		
#if (UNITY_ANDROID && !NO_GPGS)
		// Google Play Games Initialize
		PlayGamesClientConfiguration config = new PlayGamesClientConfiguration.Builder()
        .Build();

		PlayGamesPlatform.InitializeInstance(config);
		// recommended for debugging:
		PlayGamesPlatform.DebugLogEnabled = true;
		// Activate the Google Play Games platform
		PlayGamesPlatform.Activate();		
#endif

		string adMobId = string.Empty;
#if UNITY_ANDROID
		adMobId = /* AdMob Ad Id */;
#elif UNITY_IPHONE
		adMobId = /* AdMob Ad Id */;
#endif	

		GBAdManager.Instance.Init(adMobId, onRewardAdCompleted);
```

## Login in

To sign in, call **GBSessionManager.Login**, which is part of the
standard Unity social platform interface.

```csharp
using GB.Account;
    ...
    // authenticate user:
    // Already have a last session?
  if (GBSessionManager.isReady()) {
    GBSessionManager.Login(sessionCallback);			
	} 
  else {
		// Default AuthType.GOOGLE
#if (!UNITY_EDITOR && UNITY_ANDROID)				
		GBSessionManager.LoginWithAuthType(AuthType.GOOGLE, sessionCallback);
#elif (!UNITY_EDITOR && UNITY_IPHONE)
		GBSessionManager.LoginWithAuthType(AuthType.GUEST, sessionCallback);
#endif
	}
```
Authentication will show the required consent dialogs. If the user has already
signed into the game in the past, this process will be silent and the user will
not have to interact with any dialogs. (Only Android)

## Connect Channel

## IAP (In App Purchase)
Both iOS and Android support IAP. 

### Query / Request Products
    GBInAppManager.QueryInventory(skus, (GBInventory inv, GBException exception) => {

    });  

### Payment
before **BuyItem** called it, must call **QueryInventory**

    GBInAppManager.BuyItem(/* product id */, 0, (string paymentKey, GBException ex) => {

    });
### Restore

    GBInAppManager.RestoreItems((List<string>paymentKeys, GBException exception) => {
      
    });

## Reward Video Admob Unity
Once the plugin is initialized, you can start loading ads. To do so, you invoke the method Show of GBAdManager. The first ad can also be loaded automatically by switching on the field **onRewardAdCompleted** callback.

```csharp
  GBAdManager.Instance.Init(adMobId, onRewardAdCompleted);
  ...
  GBAdManager.Instance.ShowAd();
```
## Sample
