using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Advertisements;
using System.Collections;
using Facebook.Unity;

public class TimeOver : MonoBehaviour {

	public Text yourScoreText;
	public Text bestScoreText;
	public Text timeScoreText;
	public Text accuracyText;
	public GameObject waitForUnityAdsOnPanel;

	private bool wasAskedToWatch;

	private ModalPanelControl gameOver;
	private ModalPanelAskForWatchVideo watchVideo;

	void Awake(){

		gameOver = ModalPanelControl.Instance ();
		watchVideo = ModalPanelAskForWatchVideo.Instance ();
		UnityAdsButton.AdsOnComplet = false;
		wasAskedToWatch = false;
	}

	void Update(){

		if (GameLevelParameter.playTime <= 0) {

			GameControl.control.Pause ();

			if (UnityAdsButton.AdsOnComplet) // After watch the add call game over
				GameOver ();
			
			if (Advertisement.IsReady (UnityAdsButton.zoneId)) {// in the case the add is read it ask to user if he wants to watch to it

				if (!wasAskedToWatch){// more efficient 
					wasAskedToWatch = true;
					AskToWatchVideo ();
				}
				

			} else {// otherwise finish the game.

				GameOver ();
			}
		}
	}

	void RestartGame(){

		GameLevelParameter.RestartVar ();
		GameControl.control.UnPause (); //  initiate the game
		LevelManager.lm.LoadLevel ("Game");

	}

	void Menu(){

		GameLevelParameter.RestartVar ();
		GameControl.control.UnPause (); //  initiate the game
		Debug.Log ("Menu");
		LevelManager.lm.LoadLevel ("Menu");
	}
		

	int Accuracy(int total, int ontarget){

		int accuracy = 100;

		if (total != 0)
			accuracy = ontarget * 100 / total;

		return accuracy;
	}

	//=======================================================
	/// <summary>
	/// Checks the high score. If score > highscore, replace it. 
	/// </summary>
	/// <returns>The high score.</returns>
	/// <param name="score">Score.</param>
	//=======================================================
	float CheckHighScore(float score){

		string name = "HighScore";

		if (PlayerPrefs.HasKey (name)) {

			float highscore = PlayerPrefs.GetFloat (name);

			if (score > highscore) {
				PlayerPrefs.SetFloat (name, score);
				PlayerPrefs.Save ();
				if (FB.IsLoggedIn) {
					int fbScore = (int)score;
					FBManager.Instance.SetScore (fbScore.ToString ());
				}
			}
			
		} else {

			PlayerPrefs.SetFloat (name, score);
			PlayerPrefs.Save ();
		}

		return PlayerPrefs.GetFloat (name);
	}


	void Cine(){

		waitForUnityAdsOnPanel.SetActive (true);

	}

	void Exit(){

		GameOver ();

	}

	void AskToWatchVideo(){

		GameControl.control.Pause (); // pause the game.
		watchVideo.OpenPanel (Cine, Exit);

	}

	void GameOver(){

		GameControl.control.SavePlayerMoney (); // save the players money after the mach.
		GameControl.control.Pause (); // pause the game.
		float score = GameLevelParameter.playerScore;
		float highScore = CheckHighScore (score);
		int timeGain = GameLevelParameter.totalTimeGain;
		yourScoreText.text = string.Format (GeneralLanguageSetup.setupLanguage.yourScore, score);
		bestScoreText.text = string.Format (GeneralLanguageSetup.setupLanguage.bestScore, highScore);
		timeScoreText.text = string.Format (GeneralLanguageSetup.setupLanguage.extraTime, timeGain);
		accuracyText.text = string.Format (GeneralLanguageSetup.setupLanguage.accuracy, Accuracy (GameLevelParameter.totalShoots, GameLevelParameter.shootsOnTarget));
		QuestHandler.questHandler.finish3matchs++;
		QuestHandler.questHandler.extraSecs = timeGain;
		QuestHandler.questHandler.scorePoints = (int)score;
		QuestHandler.questHandler.CheckIfQuestIsComplet (false); // Check if active quest is complet.
		gameOver.OpenPanel (Menu, RestartGame);
		GameLevelParameter.playTime = 1; // To Avoid the Update keeping calling GameOver;

	}
		
}
