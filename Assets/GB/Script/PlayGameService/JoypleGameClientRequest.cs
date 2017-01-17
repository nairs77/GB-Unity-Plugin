using UnityEngine;
using System;
using SimpleJSON;

namespace GB.PlayGameService {

	public class GBGameClientRequest : GBRequest {

		public static readonly string TAG = "[GBGameClientRequest]";
		public static Action<bool, GBException> gameStatusCallback;

		static Action<bool,string> gameWrapperCallback = (success, result) => {
					
			JLog.verbose(TAG + "Game Status callback!!! - ");
			
			gameStatusCallback(success, null);
		};
		 		
		public static void RequestSignIn(Action<bool, GBException> callback) {
			GameObject gameObject = new GameObject("RequestSignIn" + DateTime.Now.Ticks);
			GBGameClientRequest request = gameObject.AddComponent<GBGameClientRequest>();
			gameStatusCallback = callback;
			
			request.RequestSignInWithCallback(gameWrapperCallback);
		}
		
		public static void RequestSignOut(Action<bool, GBException> callback) {
			GameObject gameObject = new GameObject("RequestSignOut" + DateTime.Now.Ticks);
			GBGameClientRequest request = gameObject.AddComponent<GBGameClientRequest>();
			gameStatusCallback = callback;
			
			request.RequestSignOutWithCallback(gameWrapperCallback);									
		}
		
		/* Leaderboard */
		public static void RequestLeaderboardUI(string leaderboardId, Action<bool, GBException> callback) {
			GameObject gameObject = new GameObject("RequestLeaderboardUI" + DateTime.Now.Ticks);
			GBGameClientRequest request = gameObject.AddComponent<GBGameClientRequest>();
			gameStatusCallback = callback;
			
			request.RequestLeaderboardUIWithCallback(leaderboardId, gameWrapperCallback);									
		}
		
		public static void RequestSubmitScore(long score, string leaderBoardId, Action<bool, GBException> callback) {
			GameObject gameObject = new GameObject("RequestSubmitScore" + DateTime.Now.Ticks);
			GBGameClientRequest request = gameObject.AddComponent<GBGameClientRequest>();			
			gameStatusCallback = callback;
			
			request.RequestSubmitScoreWithCallback(score, leaderBoardId, gameWrapperCallback);					
		}		
		
		/* Achievement */
		public static void RequestAchievementsUI(Action<bool, GBException> callback) {
			GameObject gameObject = new GameObject("RequestAchievementsUI" + DateTime.Now.Ticks);
			GBGameClientRequest request = gameObject.AddComponent<GBGameClientRequest>();			
			gameStatusCallback = callback;
			
			request.RequestAchievementsUIWithCallback(gameWrapperCallback);									
		}
		
		public static void RequestReportProgress(string achID, double step, Action<bool, GBException> callback) {
			GameObject gameObject = new GameObject("RequestReportProgress" + DateTime.Now.Ticks);
			GBGameClientRequest request = gameObject.AddComponent<GBGameClientRequest>();			
			gameStatusCallback = callback;
			
			request.RequestReportProgressWithCallback(achID, step, gameWrapperCallback);		
		}

		/* Quest */		
		public static void RequestAllQuestsUI(Action<QuestUiResult, IQuest, IQuestMilestone> callback) {
			GameObject gameObject = new GameObject("RequestAllQuestsUI" + DateTime.Now.Ticks);
			GBGameClientRequest request = gameObject.AddComponent<GBGameClientRequest>();
						
			Action<bool,string> questWrapperCallback = (success, result) => {
					
				JLog.verbose(TAG + "Show All Quests UI callback!!! - ");
				
				JSONNode root = JSON.Parse(result);
				var response = root[API_RESPONSE_RESULT_KEY];
				
				if (success) {
					IQuest quest = new NativeQuest(response[API_RESPONSE_DATA_KEY]);
					
					if (quest.State == QuestState.Completed) {
						callback(QuestUiResult.UserRequestsMilestoneClaiming, null, quest.Milestone);
					} else {
						callback(QuestUiResult.UserRequestsMilestoneClaiming, quest, null);
					}
				} else {
					callback(QuestUiResult.InternalError, null, null);
				}	
			};
			
			request.RequestAllQuestsUIWithCallback(questWrapperCallback);			
		}
		
		public static void RequestFetchQuestById(string questId, Action<ResponseStatus, IQuest> callback) {
			GameObject gameObject = new GameObject("RequestFetchQuestById" + DateTime.Now.Ticks);
			GBGameClientRequest request = gameObject.AddComponent<GBGameClientRequest>();			
			
			Action<bool,string> fetchWrapperCallback = (success, result) => {
				JLog.verbose(TAG + "Callback Fetch Quest (By Id)");
	
				JSONNode root = JSON.Parse(result);
				var response = root[API_RESPONSE_RESULT_KEY];
				
				Debug.Log("result = " + result);
				Debug.Log("response =" + response[API_RESPONSE_DATA_KEY]);
				if (success) {								
					callback(ResponseStatus.Success, new NativeQuest(response[API_RESPONSE_DATA_KEY]));
				} else {
					callback(ResponseStatus.InternalError, null);
				}	
				
			};
			
			request.RequestFetchQuestByIdWithCallback(questId, fetchWrapperCallback);
		}
		
		public static void RequestClaimMilestone(IQuestMilestone milestone, Action<QuestClaimMilestoneStatus, IQuest, IQuestMilestone> callback) {
			GameObject gameObject = new GameObject("RequestClaimMilestone" + DateTime.Now.Ticks);
			GBGameClientRequest request = gameObject.AddComponent<GBGameClientRequest>();			
			
			Action<bool,string> milestoneWrapperCallback = (success, result) => {
				JLog.verbose(TAG + "Callback RequestClaimMilestone!!!");
	
				JSONNode root = JSON.Parse(result);
				var response = root[API_RESPONSE_RESULT_KEY];
				
				if (success) {												
					IQuest quest = new NativeQuest(response[API_RESPONSE_DATA_KEY]);
					callback(QuestClaimMilestoneStatus.Success, quest, quest.Milestone);
					
				} else {
					QuestClaimMilestoneStatus status = (QuestClaimMilestoneStatus)System.Enum.Parse(typeof(QuestClaimMilestoneStatus), response["status"]);					
					callback(status, null, null);
				}	
				
			};
			
			request.RequestClaimMilestoneWithCallback(milestone, milestoneWrapperCallback);			
		}
		

		private void RequestSignInWithCallback(Action<bool, string> callback) {
			GBRequest callbackObject = createRequestCallbackObject(callback);
			GBManager.Instance.PluginManager.SignIn(callbackObject);
		}
		
		private void RequestSignOutWithCallback(Action<bool, string> callback) {
			GBRequest callbackObject = createRequestCallbackObject(callback);
			GBManager.Instance.PluginManager.SignOut(callbackObject);
		}		
		
		private void RequestLeaderboardUIWithCallback(string leaderboardId, Action<bool, string> callback) {
			GBRequest callbackObject = createRequestCallbackObject(callback);
			GBManager.Instance.PluginManager.ShowLeaderboardUI(leaderboardId, callbackObject);
		}
		
		private void RequestAchievementsUIWithCallback(Action<bool, string> callback) {
			GBRequest callbackObject = createRequestCallbackObject(callback);
			GBManager.Instance.PluginManager.ShowAchievementsUI(callbackObject);
		}
				
		private void RequestReportProgressWithCallback(string achId, double step, Action<bool, string> callback) {
			GBRequest callbackObject = createRequestCallbackObject(callback);
			GBManager.Instance.PluginManager.ReportProgress(achId, step, callbackObject);
		}
		/*
		private void RequestIncAchievementWithCallback(string achId, int steps, Action<bool, string> callback) {
			GBRequest callbackObject = createRequestCallbackObject(callback);
			GBManager.Instance.PluginManager.IncrementAchievement(achId, steps, callbackObject);
		}
		*/
		private void RequestSubmitScoreWithCallback(long score, string leaderBoardId, Action<bool, string> callback) {
			GBRequest callbackObject = createRequestCallbackObject(callback);
			GBManager.Instance.PluginManager.SubmitScore(score, leaderBoardId, callbackObject);
		}		
		
		private void RequestFetchQuestByIdWithCallback(string questId, Action<bool, string> callback) {
			GBRequest callbackObject = createRequestCallbackObject(callback);
			GBManager.Instance.PluginManager.FetchQuestById(questId, callbackObject);			
		}
		
		private void RequestAllQuestsUIWithCallback(Action<bool, string> callback) {
			GBRequest callbackObject = createRequestCallbackObject(callback);
			GBManager.Instance.PluginManager.ShowAllQuestsUI(callbackObject);			
		}
		
		private void RequestClaimMilestoneWithCallback(IQuestMilestone milestone, Action<bool, string> callback) {
			GBRequest callbackObject = createRequestCallbackObject(callback);
			GBManager.Instance.PluginManager.ClaimMilestone(milestone, callbackObject);						
		}		
	}	
}
