#if UNITY_IPHONE
using UnityEngine;
using System.Collections;
using System.Runtime.InteropServices;

namespace GB.PlayGameService
{
	public class GameCenterClient : IPlayGameClient {


		[DllImport ("__Internal")]
		public static extern void signIn(string callbackObjectName);

		[DllImport ("__Internal")]
		public static extern bool isAuthenticated();

		[DllImport ("__Internal")]
		public static extern void showLeaderboardUI(string leaderboardId, string callbackObjectName);

		[DllImport ("__Internal")]
		public static extern void showAchievementsUI(string callbackObjectName);

		[DllImport ("__Internal")]
		public static extern void reportProgress(string resId, double step, string callbackObjectName);

		[DllImport ("__Internal")]
		public static extern void submitScore(int score, string leaderboardId, string callbackObjectName);


		
		public void SignIn(GBRequest callbackObject) {
			signIn(callbackObject.GetCallbackGameObjectName());
		}
		
		public void SignOut(GBRequest callbackObject) {
			
		}
	
		public bool IsAuthenticated() {
			return isAuthenticated();
		}	
		
		public void ShowLeaderboardUI(string leaderboardId, GBRequest callbackObject) {
			showLeaderboardUI(leaderboardId, callbackObject.GetCallbackGameObjectName());
		}
		
		/* Achievement */
		public void ShowAchievementsUI(GBRequest callbackObject) {
			showAchievementsUI(callbackObject.GetCallbackGameObjectName());
		}
	/*
		void UnlockAchievement(string achId, GBRequest callbackObject);
			
		void IncrementAchievement(string resID, int step, GBRequest callbackObject);
	*/
		public void ReportProgress(string resID, double step, GBRequest callbackObject) {
			reportProgress(resID, step, callbackObject.GetCallbackGameObjectName());
		}
		
		/* Leaderboard */
		public void SubmitScore(long score, string leaderBoardId, GBRequest callbackObject) {
			int iScore = (int)score;
			submitScore(iScore, leaderBoardId, callbackObject.GetCallbackGameObjectName());
		}
		//
		/* Quest */
		public void FetchQuestById(string questId, GBRequest callbackObject) {
			
		}
		
		public void ShowAllQuestsUI(GBRequest callbackObject) {
			
		}
	//	void ClaimMilestone();
	
		/* Event */
		public void IncrementEvent(string eventId, uint step) {
			
		}
		
		public void ClaimMilestone(IQuestMilestone milestone, GBRequest callbackObject) {
			
		}
	}
}
#endif
