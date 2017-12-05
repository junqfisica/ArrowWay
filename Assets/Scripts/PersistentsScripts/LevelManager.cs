using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;


public class LevelManager : MonoBehaviour {

	public AsyncOperation async{ get; set;}

	/// <summary>
	/// Gets the name of the active level.
	/// </summary>
	/// <value>The get level.</value>
	public string GetLevel{get{ 

			return SceneManager.GetActiveScene ().name;

		}
	}

	public static LevelManager lm;

	void Awake () {

		if (lm == null) {

			DontDestroyOnLoad (gameObject);
			lm = this;

		} else if (lm != this) {

			Destroy (gameObject);
		}
			
			
	}

	void Start(){

		if (SceneManager.GetActiveScene ().name == "_Start")
			StartCoroutine ("GoToMenuAfterXSecs",5f);

	}

	void Update(){

		if (Input.GetKeyDown (KeyCode.Escape))
			OnBackButtonClick ();
			
	}

	void OnApplicationPause(bool pause){

		Debug.Log ("Game Pause: " + pause);
		//if (!pause)
			//Screen.orientation = ScreenOrientation.Landscape;

	}

	void OnBackButtonClick(){

		switch (GetLevel) {

		default:
			QuitGame ();
			break;

		case "Menu":

			MenuControl menuControl = GameObject.Find ("Menu Control").GetComponent<MenuControl> ();
			FBscript fbScript = GameObject.Find ("FBholder").GetComponent<FBscript> (); // FB maneger

			if (menuControl.storePanel.activeSelf) {// close store

				menuControl.QuitStore ();

			} else if (menuControl.settingPanel.activeSelf) {// close settings

				menuControl.QuitSettings ();

			} else if (menuControl.questPanel.activeSelf) {// close quest

				menuControl.QuitQuest ();

			}else if (fbScript.FBPanel.activeSelf){//close FB Panel

				fbScript.QuitFBScore ();

			} else { // Quit Game

				QuitGame ();
			}

			break;

		case "Game":

			GameObject skillDespObj = GameObject.Find ("Modal Panel Skill Description");
			if (skillDespObj) {
				skillDespObj.SetActive (false);
				Time.timeScale = 1f;

			} else {

				QuitGame ();
			}

			break;

		}

	}

	public void LoadLevel (string name) {

		SceneManager.LoadScene (name);
	
	}
		

	public void LoadLevelAsy(string name){

		SceneManager.LoadSceneAsync ("TransitionScreen");
		async = SceneManager.LoadSceneAsync (name);
		async.allowSceneActivation = false;
	
	}

	public void QuitGame (){

		Application.Quit();

		#if UNITY_EDITOR 
		// set the play mode to stop
		UnityEditor.EditorApplication.isPlaying = false;
		#endif
	}

	void OnApplicationQuit(){ // Internal function that is call when the application is quite.

		#if UNITY_EDITOR
		//GameControl.control.playerMoney = 10000;
		#endif

		GameControl.control.SavePlayerMoney (); // Save the money before leave the application.
		QuestHandler.questHandler.SaveIsActiveComplet(); // Save the status of quests.
		QuestHandler.questHandler.CheckIfQuestIsComplet(false); // Check the progrees of the quest and save it.
		FBManager.Instance.LogOutFB();
		Debug.Log ("Game was quitted");
	}

	IEnumerator GoToMenuAfterXSecs(float sec){

		yield return new WaitForSeconds (sec);
		LoadLevel ("Menu");
	}
}
