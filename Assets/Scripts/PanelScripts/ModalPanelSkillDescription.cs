using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using System.Collections;

public class ModalPanelSkillDescription : MonoBehaviour {

	public GameObject modalPanelObject;
	public Button exit;
	public Text headText;
	public Text description;
	public AudioSource source;

	private static ModalPanelSkillDescription modalPanel;

	public static ModalPanelSkillDescription Instance(){

		if (!modalPanel) {

			modalPanel = FindObjectOfType (typeof(ModalPanelSkillDescription)) as ModalPanelSkillDescription;
			if (!modalPanel) // Make sure that Modalpanel existes.
				Debug.Log ("There needs to be one active ModalPanelSkillDescription Script on a GameObject in your scene");
		}

		return modalPanel;
	}

	//================================================================================
	/// <summary>
	/// Opens the Modal panel.
	/// </summary>
	/// <param name="Events">Events. Methods to deal with each button</param>
	//================================================================================
	public void OpenPanel(UnityAction Event1){

		modalPanelObject.SetActive (true);

		exit.onClick.RemoveAllListeners ();
		exit.onClick.AddListener (Event1);
		exit.onClick.AddListener (Sound);
		exit.onClick.AddListener (ClosePanel);

	}

	public void SetHeadText(string str){

		headText.text = str;
	}

	public void SetDescription(string str){

		description.text = str;
	}

	void ClosePanel(){

		modalPanelObject.SetActive (false);
	}

	void Sound(){

		source.Play ();
	}
}
