using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using System.Collections;

public class ModalPanelAskForWatchVideo : MonoBehaviour {

	public GameObject modalPanelObject;
	public Button watch;
	public Button exit;
	public AudioSource source;

	private static ModalPanelAskForWatchVideo modalPanel;

	public static ModalPanelAskForWatchVideo Instance(){

		if (!modalPanel) {

			modalPanel = FindObjectOfType (typeof(ModalPanelAskForWatchVideo)) as ModalPanelAskForWatchVideo;
			if (!modalPanel) // Make sure that Modalpanel existes.
				Debug.Log ("There needs to be one active ModalPanelAskForWatchVideo Script on a GameObject in your scene");
		}

		return modalPanel;
	}

	//================================================================================
	/// <summary>
	/// Opens the Modal panel.
	/// </summary>
	/// <param name="Events">Events. Methods to deal with each button</param>
	//================================================================================
	public void OpenPanel(UnityAction Event1, UnityAction Event2 ){

		modalPanelObject.SetActive (true);

		watch.onClick.RemoveAllListeners ();
		watch.onClick.AddListener (Event1);
		watch.onClick.AddListener (Sound);
	    watch.onClick.AddListener (ClosePanel);

		exit.onClick.RemoveAllListeners ();
		exit.onClick.AddListener (Event2);
		exit.onClick.AddListener (Sound);
		exit.onClick.AddListener (ClosePanel);

	}

	void ClosePanel(){

		modalPanelObject.SetActive (false);
	}

	void Sound(){

		source.Play ();
	}
}

