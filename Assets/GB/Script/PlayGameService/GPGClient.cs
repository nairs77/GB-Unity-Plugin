#if UNITY_ANDROID
using UnityEngine;
using System;
using GB;

namespace GB.PlayGameService {
	public class GPGClient : GBAndroidHelper, IPlayGameClient {
		
		private static readonly string NATIVE_GPG_CLASS_PACKAGE = "com.joycity.platform.unity.GPGPlugin";

		private static readonly string GET_IS_LOGIN = "IsAuthenticated";
				
		private static AndroidJavaClass _androidGPGHelper;
		private static AndroidJavaClass AndroidGPGHelper {
			get {
				if (_androidGPGHelper == null) {
					_androidGPGHelper = new AndroidJavaClass(NATIVE_GPG_CLASS_PACKAGE);
				}
				return _androidGPGHelper;
			}
		}
		
		public void SignIn(GBRequest callbackObject) {
			UnityActivity.Call("runOnUiThread", new AndroidJavaRunnable(() => {
				AndroidGPGHelper.CallStatic("SignIn", callbackObject.GetCallbackGameObjectName());
		  	}));
		}
		
		public void SignOut(GBRequest callbackObject) {
			UnityActivity.Call("runOnUiThread", new AndroidJavaRunnable(() => {
				AndroidGPGHelper.CallStatic("SignOut", callbackObject.GetCallbackGameObjectName());
		  	}));
		}
		
		public bool IsAuthenticated() {
			return AndroidGPGHelper.CallStatic<bool> (GET_IS_LOGIN);			
		}
		public void ShowLeaderboardUI(string leaderboardId, GBRequest callbackObject) {
			UnityActivity.Call("runOnUiThread", new AndroidJavaRunnable(() => {
				AndroidGPGHelper.CallStatic("ShowLeaderboardUI", leaderboardId, callbackObject.GetCallbackGameObjectName());
		  	}));			
		}
		
		public void ShowAchievementsUI(GBRequest callbackObject) {
			UnityActivity.Call("runOnUiThread", new AndroidJavaRunnable(() => {
				AndroidGPGHelper.CallStatic("ShowAchievementsUI", callbackObject.GetCallbackGameObjectName());
		  	}));			
		}
		
		public void ReportProgress(string achId, double step, GBRequest callbackObject) {
			UnityActivity.Call("runOnUiThread", new AndroidJavaRunnable(() => {
				string strStep = Convert.ToString(step);
				AndroidGPGHelper.CallStatic("ReportProgress", achId, strStep, callbackObject.GetCallbackGameObjectName());
		  	}));						
		}
		/*
		public void IncrementAchievement(string resID, int step, GBRequest callbackObject) {
			UnityActivity.Call("runOnUiThread", new AndroidJavaRunnable(() => {
				AndroidGPGHelper.CallStatic("IncrementAchievement", resID, step, callbackObject.GetCallbackGameObjectName());
		  	}));			
		}		
		*/
		public void FetchQuestById(string questId, GBRequest callbackObject) {
			UnityActivity.Call("runOnUiThread", new AndroidJavaRunnable(() => {
				AndroidGPGHelper.CallStatic("FetchQuestById", questId, callbackObject.GetCallbackGameObjectName());
			}));
		}
		
		public void ShowAllQuestsUI(GBRequest callbackObject) {
			UnityActivity.Call("runOnUiThread", new AndroidJavaRunnable(() => {
				AndroidGPGHelper.CallStatic("ShowAllQuestsUI", callbackObject.GetCallbackGameObjectName());
			}));			
		}
		
		public void SubmitScore(long score, string leaderBoardId, GBRequest callbackObject) {
			UnityActivity.Call("runOnUiThread", new AndroidJavaRunnable(() => {
				AndroidGPGHelper.CallStatic("SubmitScore", score, leaderBoardId, callbackObject.GetCallbackGameObjectName());
			}));			
		}
		
		public void IncrementEvent(string eventId, uint stepsToIncrement) {
			AndroidGPGHelper.CallStatic("IncrementEvent", eventId, (int)stepsToIncrement);
		}		
		//  public void IncrementAchievement(string achID, GBRequest callbackObject) {
		//  	UnityActivity.Call("runOnUiThread", new AndroidJavaRunnable(() => {
		//  		AndroidGPGHelper.CallStatic("FetchEvent", achID, callbackObject.GetCallbackGameObjectName());
		//  	}));
		//  }
		public void ClaimMilestone(IQuestMilestone milestone, GBRequest callbackObject) {
			UnityActivity.Call("runOnUiThread", new AndroidJavaRunnable(() => {
				AndroidGPGHelper.CallStatic("ClaimMilestone", milestone.QuestId, milestone.Id, callbackObject.GetCallbackGameObjectName());
			}));						
		}		
	}
}
#endif
