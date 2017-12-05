using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using System.Collections;

public class ModalPanelControl : MonoBehaviour {
	
	public GameObject modalPanelObject;
	public Button [] button;
	public AudioSource source;

	private GameSetupLanguages gameSetupL;

	private static ModalPanelControl modalPanel;

	public static ModalPanelControl Instance(){

		if (!modalPanel) {

			modalPanel = FindObjectOfType (typeof(ModalPanelControl)) as ModalPanelControl;
			if (!modalPanel) // Make sure that Modalpanel existes.
				Debug.Log ("There needs to be one active ModalPanelControl Script on a GameObject in your scene");
		}

		return modalPanel;
	}

	void Awake(){

		gameSetupL = GameSetupLanguages.Instance ();
	}

	//================================================================================
	/// <summary>
	/// Opens the Modal panel.
	/// </summary>
	/// <param name="Events">Events. Methods to deal with each button</param>
	//================================================================================
	public void OpenPanel(params UnityAction [] Events){
		
		Sprite yellowButSprite = Resources.Load<Sprite> ("Sprites/Buttons/orange button");

		if (QuestHandler.questHandler.isActiveComplet) {// change the menu button to yellow if quest is completed
			Image imgBt = button[0].GetComponent<Image> ();
			Text menuText = button[0].GetComponentInChildren<Text> ();
			imgBt.sprite = yellowButSprite;
			menuText.text = gameSetupL.questComplete;
		}
		modalPanelObject.SetActive (true);

		button[0].onClick.RemoveAllListeners ();
		button[0].onClick.AddListener (Events[0]);
		button[0].onClick.AddListener (Sound);
		button[0].onClick.AddListener (ClosePanel);

		button[1].onClick.RemoveAllListeners ();
		button[1].onClick.AddListener (Events[1]);
		button[1].onClick.AddListener (Sound);
		button[1].onClick.AddListener (ClosePanel);

	}

	void ClosePanel(){

		modalPanelObject.SetActive (false);
	}

	void Sound(){

		source.Play ();
	}
}
