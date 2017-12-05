using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;


public class GenericLanguageSetup : MonoBehaviour {

	public TextAsset dictonary;

	public Text[] text;

	private Dictionary<string,string>.ValueCollection valueColl;
	private List<string> dicContent;

	private int currentLanguage;
	private List<Dictionary<string,string>> languages = new List<Dictionary<string,string>>();

	// Use this for initialization
	void Awake () {

		//Read the xml file using the dll MyXmlReader
		MyXmlReader xmlreader = gameObject.AddComponent<MyXmlReader> ();
		languages = xmlreader.ReadXml (dictonary);

	}

	void OnEnable(){

		ChangeLanguages ();
	}

	void ChangeLanguages(){

		this.currentLanguage = GeneralLanguageSetup.setupLanguage.currentLanguage;
		SetLanguage ();
		SetText ();
	}

	void SetLanguage(){
	
		valueColl = languages [currentLanguage].Values;
		dicContent = new List<string> ();
		foreach (string str in valueColl) {

			dicContent.Add (str);
		}
	}

	void SetText () {

		for (int i = 0; i < text.Length; i++) {

			text[i].text = dicContent[i];
		}

	}


}
