using UnityEngine;
using System;
using System.Collections;

namespace GB.PlayGameService {
	
	public class GBPlayGameService {
		
		public static void SignIn(Action<bool, GBException> callback) {
			GBGameClientRequest.RequestSignIn(callback);
		}
		
		public static void SignOut(Action<bool, GBException> callback) {
			GBGameClientRequest.RequestSignOut(callback);
		}
	
		public static bool IsSignIn() {
			return GBManager.Instance.PluginManager.IsAuthenticated();
		}
		
		/**
		 *  true if connected PGS, false if not  
		 */
		public static bool IsAuthenticated() {
			return GBManager.Instance.PluginManager.IsAuthenticated();
		}	
		
		/* Leader board */
		public static void ShowLeaderboardUI(string leaderboardId, Action<bool, GBException> callback) {
			#if UNITY_ANDROID //If not sign-in to Game Center, will move to Game Center App in iOS
			if (!IsAuthenticated()) {
				callback(false, null);
				return;
			}
			#endif

			GBGameClientRequest.RequestLeaderboardUI(leaderboardId, callback);
		}

		public static void ShowLeaderboardUI() {
			ShowLeaderboardUI(null, null);	
		}
			
		public static void ReportScore(long score, string leaderBoardId, Action<bool, GBException> callback) {
			if (!IsAuthenticated()) {
				callback(false, null);
				return;
			}
			GBGameClientRequest.RequestSubmitScore(score, leaderBoardId, callback);			
		}		
		
		/* Achievement */
		public static void ShowAchievementsUI(Action<bool, GBException> callback) {
			#if UNITY_ANDROID //If not sign-in to Game Center, will move to Game Center App in iOS
			if (!IsAuthenticated()) {
				callback(false, null);
				return;
			}			
			#endif
			GBGameClientRequest.RequestAchievementsUI(callback);
		}

		/*			
		public static void IncrementAchievement(string achID, int steps, Action<bool, GBException> callback) {
			if (!IsAuthenticated()) callback(false, null);
			
			GBGameClientRequest.RequestIncAchievement(achID, steps, callback);
		}

		public static void UnlockAchievement(string achId, Action<bool, GBException> callback) {
			if (!IsAuthenticated()) callback(false, null);
			
			GBGameClientRequest.RequestUnlockAchievement(achId, 100.0f, callback);						
		}
		*/
			
		public static void ReportProgress(string achId, double step, Action<bool, GBException> callback) {
			if (!IsAuthenticated()) {
				callback(false, null);
				return;	
			}
			GBGameClientRequest.RequestReportProgress(achId, step, callback);			
		}
		
		/* Event */
		
		public static void IncrementEvent(string eventId, uint stepToIncrement) {
			if (!IsAuthenticated()) return;
			GBManager.Instance.PluginManager.IncrementEvent(eventId, stepToIncrement);	
		}
		
		/* Quest */ 
		public static void Fetch(DataSource dataSource, string questId, Action<ResponseStatus, IQuest> callback) {
			if (!IsAuthenticated()) {
				callback(ResponseStatus.NotAuthorized, null);
				return;	
			} 
			GBGameClientRequest.RequestFetchQuestById(questId, callback);
		}
		
		public static void ShowAllQuestsUI(Action<QuestUiResult, IQuest, IQuestMilestone> callback) {
			if (!IsAuthenticated()) {
				callback(QuestUiResult.NotAuthorized, null, null);
				return;
			}
			
			GBGameClientRequest.RequestAllQuestsUI(callback);
		}
		
		public static void ClaimMilestone(IQuestMilestone milestone,
                            Action<QuestClaimMilestoneStatus, IQuest, IQuestMilestone> callback) {
			if (!IsAuthenticated()) {
				callback(QuestClaimMilestoneStatus.NotAuthorized, null, null);
				return;
			}
			
			GBGameClientRequest.RequestClaimMilestone(milestone, callback);
		}	
	}
}
