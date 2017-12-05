using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Advertisements;
using System.Collections;

public class WaitForUnityAdsOn : MonoBehaviour {

	private Text writeOnScreen;
	public float limetTimeOut;

	void OnEnable(){

		writeOnScreen = GetComponentInChildren<Text> ();
		StartCoroutine ("AnimText");
		StartCoroutine ("CheckVideoStatus");

	}

	IEnumerator AnimText(){

		int i = 1;

		while (true) {

			switch (i) {

			case 1:
				i = 2;
				writeOnScreen.text = string.Format(GeneralLanguageSetup.setupLanguage.loading,".  ");
				break;
			
			case 2:
				i = 3;
				writeOnScreen.text = string.Format(GeneralLanguageSetup.setupLanguage.loading,".. ");
				break;

			case 3:
				i = 1;
				writeOnScreen.text = string.Format(GeneralLanguageSetup.setupLanguage.loading,"...");
				break;
			}

			yield return new WaitForSeconds (1f);
		}
	}

	IEnumerator CheckVideoStatus(){

		if (Advertisement.isShowing) {// Close WaitForUnityAds if the video is alredy being shown.

			gameObject.SetActive (false);

		} else {// Otherwise check if the video is ready and show

			if (string.IsNullOrEmpty (UnityAdsButton.zoneId)) UnityAdsButton.zoneId = null;
			float time = Time.time;

			while (!Advertisement.IsReady (UnityAdsButton.zoneId)) {
			
				if (!Advertisement.isInitialized) {
					Advertisement.Initialize (Advertisement.gameId);
					yield return new WaitForSeconds (limetTimeOut - 2f);
				}
		
				//Debug.Log (Advertisement.IsReady (UnityAdsButton.zoneId));

				if (Time.time > time + limetTimeOut) {
					StopCoroutine ("AnimText");
					UnityAdsButton.ShowAdPlacement (); // call video "A menssage of video failed will be shown"
					break;
				}

				yield return null;
		
			}

			UnityAdsButton.ShowAdPlacement (); // Call the video
			gameObject.SetActive (false);
		
		}
	}
}
