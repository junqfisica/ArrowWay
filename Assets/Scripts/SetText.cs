using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class SetText : MonoBehaviour {

	public string translate;

	private Text text;

	void Awake(){

		text = GetComponent<Text> ();
	}


	void OnEnable(){

		text.text = GeneralLanguageSetup.setupLanguage.Translate (translate);

	}
		
}
