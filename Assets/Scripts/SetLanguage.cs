using UnityEngine;
using System.Collections;

public class SetLanguage : MonoBehaviour {

	public enum Languages{English,Portuguese,Deutsch};
	public Languages language;


	public void ChangeLangue(){

		switch (language) {

		case Languages.English:
			GeneralLanguageSetup.setupLanguage.currentLanguage = 0;
			break;

		case Languages.Portuguese:
			GeneralLanguageSetup.setupLanguage.currentLanguage = 1;
			break;

		case Languages.Deutsch:
			GeneralLanguageSetup.setupLanguage.currentLanguage = 2;
			break;
		}

	}
}
