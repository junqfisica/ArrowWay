using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;


public class MenuSetupLanguages : MonoBehaviour {

	public TextAsset dictonary;

	public Text questText;
	public Text storeText;
	public Text settingsText;
	public Text playText;
	public Text storeHead;
	public Text settingsHead;
	public Text questhead;
	public Text musciVolText;
	public Text effectsText;
	public Text soundText;
	public Text languageText;
	public Text category1Store;
	public Text category2Store;
	public Text category3Store;
	public Text category4Store;
	public Text loginFbBt;
	public Text inviteFbBt;
	public Text shareFbBt;
	public Text challengeFbBut;
	public Text ratingFbHead;

	private int currentLanguage;

	private string textQuest,textStore,textSettings,textPlay,textBow,textSkill,textAvatar,textScene,
	idiom,musicVol,effectsVol,sound,rating,invite,share,challenge,topTeen;


	private List<Dictionary<string,string>> languages = new List<Dictionary<string,string>>();

	private static MenuSetupLanguages menuSetupLanguage;

	public static MenuSetupLanguages Instance(){

		if (!menuSetupLanguage) {

			menuSetupLanguage = FindObjectOfType (typeof(MenuSetupLanguages)) as MenuSetupLanguages;
			if (!menuSetupLanguage) // Make sure that Modalpanel existes.
				Debug.Log ("There needs to be one active MenuSetupLanguages Script on a GameObject in your scene");
		}

		return menuSetupLanguage;

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
			QuestHandler.questHandler.ChangeLanguageOnQuest ();
		}
	}

	public void ChangeLanguages(){

		this.currentLanguage = GeneralLanguageSetup.setupLanguage.currentLanguage;
		SetLanguage ("questBut", out textQuest);   // The names beteween "" must to have the same name that in the xml file
		SetLanguage ("storeBut", out textStore);
		SetLanguage ("settingsBut", out textSettings);
		SetLanguage ("playBut", out textPlay);
		SetLanguage("bow",out textBow);
		SetLanguage ("skill", out textSkill);
		SetLanguage("avatar",out textAvatar);
		SetLanguage ("scene", out textScene);
		SetLanguage("idiom",out idiom);
		SetLanguage ("musicVol", out musicVol);
		SetLanguage ("effectsVol", out effectsVol);
		SetLanguage ("sound", out sound);
		SetLanguage ("rating", out rating);
		SetLanguage ("invite", out invite);
		SetLanguage ("share", out share);
		SetLanguage ("challenge", out challenge);
		SetLanguage ("topTeen", out topTeen);

		SetText ();
	}

	public void SetLanguage(string name, out string value){

		languages [currentLanguage].TryGetValue (name, out value);

	}

	void SetText () {

		storeText.text = textStore;
		questText.text = textQuest;
		settingsText.text = textSettings;
		playText.text = textPlay;
		storeHead.text = textStore;
		settingsHead.text = textSettings;
		questhead.text = textQuest;
		languageText.text = idiom;
		musciVolText.text = musicVol;
		effectsText.text = effectsVol;
		soundText.text = sound;
		category1Store.text = textBow;
		category2Store.text = textSkill;
		category3Store.text = textAvatar;
		category4Store.text = textScene;
		loginFbBt.text = rating;
		inviteFbBt.text = invite;
		shareFbBt.text = share;
		challengeFbBut.text = challenge;
		ratingFbHead.text = string.Format (topTeen, 10);

	}
		
}
