using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class TransitionScreen : MonoBehaviour {

	public TextAsset dictonary;
	public Button nextScreen;
	public Image loadingImag;
	public Text infoText;


	private AudioSource source;
	private float time;
	private float waitMinTime;

	private int currentLanguage;
	private List<Dictionary<string,string>> languages = new List<Dictionary<string,string>>();
	private Dictionary<string,string>.ValueCollection valueColl;
	private List<string> dicContent;

	void Awake(){

		//Read the xml file using the dll MyXmlReader
		MyXmlReader xmlreader = gameObject.AddComponent<MyXmlReader> ();
		languages = xmlreader.ReadXml (dictonary);

		int hint = Random.Range (0, 9);
		infoText.text = Hints (hint);

		time = Time.time;
		waitMinTime = 3f;
		nextScreen.gameObject.SetActive (false);
		loadingImag.gameObject.SetActive (true);
		loadingImag.fillAmount = 0f;
		source = nextScreen.gameObject.GetComponent<AudioSource> ();

	}

	void Start(){

		StartCoroutine ("LoadingNextScreen");

	}


	IEnumerator LoadingNextScreen(){

		while (Time.time < time + waitMinTime || LevelManager.lm.async.progress < 0.9f) {

			if (loadingImag.fillAmount >= 1f)
				loadingImag.fillAmount = 0f;
			
			loadingImag.fillAmount += 0.05f;
			yield return new WaitForSeconds (0.1f);
		}

		loadingImag.gameObject.SetActive (false);
		nextScreen.gameObject.SetActive (true);

	}


	public void ContinueToNextLevel(){

		source.Play ();
		LevelManager.lm.async.allowSceneActivation = true;
	}

	void ChangeLanguages(){

		this.currentLanguage = GeneralLanguageSetup.setupLanguage.currentLanguage;
		SetLanguage ();
	}

	void SetLanguage(){

		valueColl = languages [currentLanguage].Values;
		dicContent = new List<string> ();
		foreach (string str in valueColl) {

			dicContent.Add (str);
		}
	}


	string Hints(int hint){

		ChangeLanguages ();

		return dicContent [hint];

	}
}
