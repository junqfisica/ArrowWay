using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using Facebook.Unity;

public class FBscript : MonoBehaviour {

	public GameObject FBPanel;
	public GameObject MenuPanel;
	public GameObject ScorePanelObj;
	public GameObject ScoreViwePort;

	private int highscore;

	void Awake(){

		MenuPanel.SetActive (true);
		FBPanel.SetActive (false);
		FBManager.Instance.InitFB ();
	}


	public void FBLogin(){

		FBManager.Instance.FBLogin ();
		StartCoroutine ("WaitForFaceBookLogin");
	}

	IEnumerator WaitForFaceBookLogin(){

		while (!FBManager.Instance.isLoggedIn) {
			MenuPanel.SetActive (true);
			FBPanel.SetActive (false);
			yield return null;
		}

		FBManager.Instance.FBLoginWithPublishActions ();
		StartCoroutine ("WaitForPublishAction");
	}

	IEnumerator WaitForPublishAction(){

		while (!FBManager.Instance.havePublishActions) {
			MenuPanel.SetActive (true);
			FBPanel.SetActive (false);
			yield return null;
		}

		OpenScore ();
	}

	void OpenScore(){

		MenuPanel.SetActive (false);
		FBPanel.SetActive (true);
		SetScore ();
		QueryScore ();

	}

	public void Share(){

		FBManager.Instance.Share ();
	}

	public void Invite(){

		FBManager.Instance.Invite ();
	}

	public void ChallengeUsers(){

		FBManager.Instance.ShareWithUsers (highscore);
	}

	void QueryScore(){

		//============== clean the score view ==========================
		GameObject[] destObjs = GameObject.FindGameObjectsWithTag("ScoreContent");
		foreach(GameObject obj in destObjs){
			Destroy (obj);
		}
		//=============================================================

		FBManager.Instance.QueryScore ();
		StartCoroutine ("WaitForFaceBookScore");

	}

	IEnumerator WaitForFaceBookScore(){

		while (!FBManager.Instance.scoreListIsFinishToLoad) {
			yield return null;
		}

		foreach (Dictionary<string,object> dic in FBManager.Instance.scoreQueryList) {

			// Here you inclue the code to set the picture, name and score from users;
			GameObject scorePanel = Instantiate (ScorePanelObj) as GameObject;
			scorePanel.transform.SetParent (ScoreViwePort.transform,false);

			Text[] setText = scorePanel.transform.FindChild("Text Panel").GetComponentsInChildren<Text> ();
			RawImage userAvatar = scorePanel.GetComponentInChildren<RawImage> ();
			Text friendNameText = setText [0];
			Text friendScoreText = setText [1];

			friendNameText.text = dic["name"].ToString();
			friendScoreText.text = string.Format(GeneralLanguageSetup.setupLanguage.bestScore,dic ["score"]);
			userAvatar.texture = (Texture)dic["picture"];

		}

	}

	void SetScore(){

		string name = "HighScore"; // Same used in TimeOver.cs
		highscore = 0;
		if (PlayerPrefs.HasKey (name))
			highscore = (int)PlayerPrefs.GetFloat (name);

		FBManager.Instance.SetScore (highscore.ToString ());

	}

	public void QuitFBScore(){

		FBPanel.SetActive (false);
		MenuPanel.SetActive (true);
	}
}
