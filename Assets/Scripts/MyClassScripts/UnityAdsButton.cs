using UnityEngine;
using UnityEngine.Advertisements;
using System.Collections;

//[RequireComponent(typeof(Button))]
public static class UnityAdsButton{

	public static string zoneId = "rewardedVideo";
	public static bool AdsOnIsrunnig{ get; set;}
	public static bool AdsOnComplet{ get; set;}
	public static int coinsGot{ get; set;}

	private static ModalPanelGetYourCoinControl modalPanelGetYourCoinControl;
	//private Button _button;
	/*
	void Start(){

		_button = GetComponent<Button>();

		if (_button) _button.onClick.AddListener (delegate() { ShowAdPlacement(); });
	}

	void Update(){

		if (_button) {

			if (string.IsNullOrEmpty (zoneId)) zoneId = null;
			_button.interactable = Advertisement.IsReady (zoneId);
		}
	}*/

	public static void ShowAdPlacement (){

		AdsOnIsrunnig = true;
		AdsOnComplet = false;
		modalPanelGetYourCoinControl = ModalPanelGetYourCoinControl.Instance ();

		if (string.IsNullOrEmpty (zoneId)) zoneId = null;

		ShowOptions options = new ShowOptions();
		options.resultCallback = HandleShowResult;

		Advertisement.Show (zoneId, options);

	}

	private static void HandleShowResult (ShowResult result){

		switch (result) {

		case ShowResult.Finished:
			Debug.Log ("Video completed. Offer a reward to the player.");
			coinsGot = Random.Range (100, 601);
			modalPanelGetYourCoinControl.OpenPanel (ModalPanelGetYourCoinControl.Status.Success, GetCoin);
			break;

		case ShowResult.Skipped:
			Debug.LogWarning ("Video was skipped.");
			modalPanelGetYourCoinControl.OpenPanel(ModalPanelGetYourCoinControl.Status.Skipped,VideoSkipped);
			break;

		case ShowResult.Failed:
			Debug.LogError ("Video failed to show.");
			modalPanelGetYourCoinControl.OpenPanel(ModalPanelGetYourCoinControl.Status.Fail,VideoFail);
			break;
		}
	}

	static void GetCoin(){

		GameControl.control.playerMoney += coinsGot;
		Debug.Log ("You Gain "+ coinsGot +" Coin");
	}

	static void VideoFail(){

		coinsGot = 0;
		Debug.Log ("Fail to run the video");
	}

	static void VideoSkipped(){

		coinsGot = 0;
		Debug.Log ("Video Has been skipped");
	}
}
