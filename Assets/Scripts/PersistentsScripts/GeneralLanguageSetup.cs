using UnityEngine;
using System.Collections;
using System.Collections.Generic;


public class GeneralLanguageSetup : MonoBehaviour {

	public static GeneralLanguageSetup setupLanguage;

	public TextAsset dictonary;

	public string usingLanguage;
	public string systemLanguage;
	public int currentLanguage;

	[HideInInspector]
	public string resume,loading,buy,levelUp,description,hint,clickToplay,nextLevel,level,cooldown,activeTime,bowDescp,rapidFireDescp,
	explosionDescp,soapTimeDescp,comingSoon,yourScore,bestScore,extraTime,accuracy,playAgain,fireArrow,jump,
	popBubbles,popBlueBubbles,popGreenBubbles,popRedBubbles,finishGames,upgradeStore,freeButterflies,extraSecs,scorePoints,earnCoins,
	useExplosion,reachCombo,endQuest,challengeMsg,challengeTitle,scoreChallenge,captionShare,descriptionShare;

	private List<Dictionary<string,string>> languages = new List<Dictionary<string,string>>();

	void Awake () {

		#region Make the object persistent
		if (setupLanguage == null) {

			DontDestroyOnLoad (gameObject);
			setupLanguage = this;

			// Set initial language
			systemLanguage = Application.systemLanguage.ToString(); // Gets the language from the user's system

			//Read the xml file using the dll MyXmlReader
			MyXmlReader xmlreader = gameObject.AddComponent<MyXmlReader> ();
			languages = xmlreader.ReadXml (dictonary);

			// Set initial language
			SetUserLanguage ();

		} else if (setupLanguage != this) {

			Destroy (gameObject);
		}
		#endregion

	}


	void SetUserLanguage() {

		if (PlayerPrefs.HasKey ("UserLanguage")) {

			currentLanguage = PlayerPrefs.GetInt ("UserLanguage");

		} else {

			// This method check if the user language is within the language xml file..if is we set the user language if not 
			// we set the defout language.
			int i = 0 ; 
			currentLanguage = 0; //set to default (English).

			foreach (Dictionary<string,string> dic  in languages ){

				if (dic.ContainsValue (systemLanguage)) {

					currentLanguage = i;
				}

				i++;
			}

		}

		SetAllLanguages ();

	}

	public void ChangeLanguages(){

		SetAllLanguages ();

		// Save Player preference when language is changed
		PlayerPrefs.SetInt("UserLanguage",currentLanguage);
		PlayerPrefs.Save ();

	}

	void SetAllLanguages (){

		SetLanguage ("Name", out usingLanguage); // The names beteween "" must to have the same name that in the xml file
		SetLanguage("resume", out resume);
		SetLanguage ("loading", out loading);
		SetLanguage ("buy", out buy);
		SetLanguage ("comingSoon",out comingSoon);
		SetLanguage ("levelUp", out levelUp);
		SetLanguage ("description", out description);
		SetLanguage ("hint", out hint);
		SetLanguage ("clickToplay", out clickToplay);
		SetLanguage ("fireArrow", out fireArrow);
		SetLanguage ("jump", out jump);
		SetLanguage ("playAgain", out playAgain);
		SetLanguage ("yourScore", out yourScore);
		SetLanguage ("bestScore", out bestScore);
		SetLanguage ("extraTime", out extraTime);
		SetLanguage ("accuracy", out accuracy);
		SetLanguage ("nextLevel", out nextLevel);
		SetLanguage ("level", out level);
		SetLanguage ("bowDescp", out bowDescp);
		SetLanguage ("cooldown", out cooldown);
		SetLanguage ("activeTime", out activeTime);
		SetLanguage ("rapidFireDescp", out rapidFireDescp);
		SetLanguage ("explosionDescp", out explosionDescp);
		SetLanguage ("soapTimeDescp", out soapTimeDescp);
		SetLanguage ("popBubbles", out popBubbles);
		SetLanguage ("popBlueBubbles", out popBlueBubbles);
		SetLanguage ("popGreenBubbles", out popGreenBubbles);
		SetLanguage ("popRedBubbles", out popRedBubbles);
		SetLanguage ("finishGames", out finishGames);
		SetLanguage ("upgradeStore", out upgradeStore);
		SetLanguage ("reachCombo", out reachCombo);
		SetLanguage ("endQuest", out endQuest);
		SetLanguage ("freeButterflies", out freeButterflies);
		SetLanguage ("scorePoints", out scorePoints);
		SetLanguage ("extraSecs", out extraSecs);
		SetLanguage ("earnCoins", out earnCoins);
		SetLanguage ("useExplosion", out useExplosion);
		SetLanguage ("challengeMsg", out challengeMsg);
		SetLanguage ("challengeTitle", out challengeTitle);
		SetLanguage ("scoreChallenge", out scoreChallenge);
		SetLanguage ("captionShare", out captionShare);
		SetLanguage ("descriptionShare", out descriptionShare);

	}

	void SetLanguage(string name, out string value){

		languages [currentLanguage].TryGetValue (name, out value);

	}

	//================================================================
	/// <summary>
	/// Translate the specified label. It must be within the file GeneralSetLanguages.xml 
	/// </summary>
	/// <param name="label">Label.</param>
	//================================================================
	public string Translate(string label){

		string labelOut;
		SetLanguage (label, out labelOut);
		return labelOut;
	}
		
}
