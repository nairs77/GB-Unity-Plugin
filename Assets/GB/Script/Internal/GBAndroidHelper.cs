#if UNITY_ANDROID
using UnityEngine;
using System.Collections;

public class GBAndroidHelper {

	private static readonly string NATIVE_UNITYPLAYER_CLASS_PACKAGE = "com.unity3d.player.UnityPlayer";
	private static AndroidJavaObject _unityActivity = null;
	private static AndroidJavaClass _unityPlayer = null;
	
	public static AndroidJavaObject UnityActivity {
		get {
			if (_unityPlayer == null) {
				_unityPlayer = new AndroidJavaClass(NATIVE_UNITYPLAYER_CLASS_PACKAGE);
			}

			if (_unityActivity == null) {
				_unityActivity = _unityPlayer.GetStatic<AndroidJavaObject>("currentActivity");
			}

			return _unityActivity;
		}
	}
}
#endif