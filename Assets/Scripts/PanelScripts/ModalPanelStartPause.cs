using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using System.Collections;

public class ModalPanelStartPause : MonoBehaviour {


	[HideInInspector]
	public enum Status{Start,Pause};

	public GameObject modalPanelObject;
	public Text headText;
	public Text questText;
	public Text timeText;
	public Slider progressBar;
	public Button unPauseBut;

	private GameSetupLanguages gameSetupL;

	private static ModalPanelStartPause modalPanel;

	public static ModalPanelStartPause Instance(){

		if (!modalPanel) {

			modalPanel = FindObjectOfType (typeof(ModalPanelStartPause)) as ModalPanelStartPause;
			if (!modalPanel) // Make sure that Modalpanel existes.
				Debug.Log ("There needs to be one active ModalPanelStartPause Script on a GameObject in your scene");
		}

		return modalPanel;
	}

	void Awake(){

		gameSetupL = GameSetupLanguages.Instance ();
	}

	public void OpenPanel(Status myStatus, UnityAction Event){

		modalPanelObject.SetActive (true);
		questText.text = QuestHandler.questHandler.activeQuest.description;
		timeText.text = "";

		QuestHandler.questHandler.CheckIfQuestIsComplet (true);

		if (QuestHandler.questHandler.isActiveComplet) {
			progressBar.value = 1f;
		} else {
			progressBar.value = QuestHandler.questHandler.questProgress;
		}


		switch (myStatus) {

		case Status.Start:
			unPauseBut.gameObject.SetActive (false);
			headText.text = gameSetupL.quest;
			StartCountDown (5);
			break;

		case Status.Pause:
			unPauseBut.gameObject.SetActive (true);
			headText.text = gameSetupL.pause;
			break;

		}

		unPauseBut.onClick.RemoveAllListeners ();
		unPauseBut.onClick.AddListener (Event);

	}

	void ClosePanel(){

		modalPanelObject.SetActive (false);
	}

	public void StartCountDown(int wait){
		
		unPauseBut.gameObject.SetActive (false);
		StartCoroutine ("WaitForClosePanel", wait);
	}

	IEnumerator WaitForClosePanel(int wait){

		int time = wait;
		while (time > 0) {
			timeText.text = time.ToString ();
			yield return StartCoroutine (CoroutineUtil.WaitForRealSeconds (1f));
			time--;

		}

		GameControl.control.UnPause ();
		ClosePanel ();
		yield return null;
	}
}
