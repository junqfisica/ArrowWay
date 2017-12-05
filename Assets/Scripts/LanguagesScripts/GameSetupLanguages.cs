using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;


public class GameSetupLanguages : MonoBehaviour {

	public TextAsset dictonary;

	public Text textScore;
	public Text textTimeOver;
	public Text textInfoAskForWatch;
	public Text textGameOver;
	public Text textHeadTut1;
	public Text textHeadTut2;
	[HideInInspector]
	public string secs,pause,quest,t2infoText1,t2infoText2,t2infoText3,t2infoText4,questComplete;

	private int currentLanguage;

	private string score,timeOver,infoAskForWatch,tutorialComplet; 
	 


	private List<Dictionary<string,string>> languages = new List<Dictionary<string,string>>();

	private static GameSetupLanguages gameSetupLanguage;

	public static GameSetupLanguages Instance(){

		if (!gameSetupLanguage) {

			gameSetupLanguage = FindObjectOfType (typeof(GameSetupLanguages)) as GameSetupLanguages;
			if (!gameSetupLanguage) // Make sure that Modalpanel existes.
				Debug.Log ("There needs to be one active MenuSetupLanguages Script on a GameObject in your scene");
		}

		return gameSetupLanguage;

	}
	// Use this for initialization
	void Awake () {

		//Read the xml file using the dll MyXmlReader
		MyXmlReader xmlreader = gameObject.AddComponent<MyXmlReader> ();
		languages = xmlreader.ReadXml (dictonary);
		ChangeLanguages ();

	}

	void Update(){

		if (this.currentLanguage != GeneralLanguageSetup.setupLanguage.currentLanguage) {
			ChangeLanguages ();
			GeneralLanguageSetup.setupLanguage.ChangeLanguages ();
		}
	}

	public void ChangeLanguages(){

		this.currentLanguage = GeneralLanguageSetup.setupLanguage.currentLanguage;

		SetLanguage ("score", out score);
		SetLanguage ("secs", out secs);
		SetLanguage ("timeOver", out timeOver);
		SetLanguage ("infoAskForWatch",out infoAskForWatch);
		SetLanguage ("pause", out pause);
		SetLanguage ("quest", out quest);
		SetLanguage ("questComplete", out questComplete);
		SetLanguage ("t2infoText1", out t2infoText1);
		SetLanguage ("t2infoText2", out t2infoText2);
		SetLanguage ("t2infoText3", out t2infoText3);
		SetLanguage ("t2infoText4", out t2infoText4);
		SetLanguage ("tutorialComplet", out tutorialComplet);

		SetText ();
	}

	public void SetLanguage(string name, out string value){

		languages [currentLanguage].TryGetValue (name, out value);

	}

	void SetText () {

		textScore.text = score+":";
		textTimeOver.text = timeOver;
		textInfoAskForWatch.text = infoAskForWatch;
		textGameOver.text = timeOver;
		textHeadTut1.text = tutorialComplet;
		textHeadTut2.text = tutorialComplet;
	}

}
