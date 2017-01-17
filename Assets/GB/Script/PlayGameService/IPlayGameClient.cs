using UnityEngine;
using System;
using GB.PlayGameService;

public interface IPlayGameClient {
	void SignIn(GBRequest callbackObject);
	
	void SignOut(GBRequest callbackObject);

	bool IsAuthenticated();	
	
	void ShowLeaderboardUI(string leaderboardId, GBRequest callbackObject);
	
	/* Achievement */
	void ShowAchievementsUI(GBRequest callbackObject);
/*
	void UnlockAchievement(string achId, GBRequest callbackObject);
		
	void IncrementAchievement(string resID, int step, GBRequest callbackObject);
*/
	void ReportProgress(string resID, double step, GBRequest callbackObject);
	
	/* Leaderboard */
	void SubmitScore(long score, string leaderBoardId, GBRequest callbackObject);
	
	/* Quest */
	void FetchQuestById(string questId, GBRequest callbackObject);
	
	void ShowAllQuestsUI(GBRequest callbackObject);
	void ClaimMilestone(IQuestMilestone milestone, GBRequest callbackObject);

	/* Event */
	void IncrementEvent(string eventId, uint step);
}
