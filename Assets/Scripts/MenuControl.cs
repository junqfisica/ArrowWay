using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using UnityEngine.Advertisements;
using System.Collections;

public class MenuControl : MonoBehaviour {

	public AudioSource clickButtonAudio;
	public Text coinPanelText;
	public Button start;
	public Button quest;
	public Button cinema;
	public Button settings;
	public Button store;
	public Button storeExitButton;
	public GameObject storePanel;
	public GameObject settingPanel;
	public GameObject questPanel;
	public GameObject menuPanel;
	public GameObject waitForUnityAdsOnPanel;

	private Sprite orangeButtonSprite;
	private Sprite greenButtonSprite;

	void Awake(){

		orangeButtonSprite = Resources.Load<Sprite> ("Sprites/Buttons/orange button");
		greenButtonSprite = Resources.Load<Sprite> ("Sprites/Buttons/green button");

	}

	void Start(){
		
		coinPanelText.text = GameControl.control.playerMoney.ToString();
	}

	void OnGUI(){


		if (coinPanelText.text != GameControl.control.playerMoney.ToString ()) {
			int moneyHad = int.Parse(coinPanelText.text);
			int gainMoney = GameControl.control.playerMoney - moneyHad;
			if (gainMoney > 0)
				QuestHandler.questHandler.earnCoins += gainMoney;

			//Update the Panel for the coins.
			Animator anim = coinPanelText.gameObject.GetComponentInParent<Animator> ();
			anim.SetTrigger ("gotCoin");
			coinPanelText.text = GameControl.control.playerMoney.ToString();
		}

		if (QuestHandler.questHandler.isActiveComplet && quest.image.sprite.name == "green button") {
			quest.image.sprite = orangeButtonSprite;

		} else if (!QuestHandler.questHandler.isActiveComplet && quest.image.sprite.name == "orange button"){
			quest.image.sprite = greenButtonSprite;
		}

		start.onClick.RemoveAllListeners ();
		start.onClick.AddListener (StartButton);
		start.onClick.AddListener (PlayClickAudio);

		quest.onClick.RemoveAllListeners ();
		quest.onClick.AddListener (QuestButton);
		quest.onClick.AddListener (PlayClickAudio);


		cinema.onClick.RemoveAllListeners ();
		if (string.IsNullOrEmpty (UnityAdsButton.zoneId)) UnityAdsButton.zoneId = null;
		cinema.onClick.AddListener (CineButton);
		cinema.onClick.AddListener (PlayClickAudio);

		store.onClick.RemoveAllListeners ();
		store.onClick.AddListener (StoreButton);
		store.onClick.AddListener (PlayClickAudio);

		storeExitButton.onClick.RemoveAllListeners ();
		storeExitButton.onClick.AddListener (QuitStore);
		storeExitButton.onClick.AddListener (PlayClickAudio);

		settings.onClick.RemoveAllListeners ();
		settings.onClick.AddListener (SettingsButton);
		settings.onClick.AddListener (PlayClickAudio);
	}

	void StartButton(){

		LevelManager.lm.LoadLevelAsy("Game");
	}

	void QuestButton(){

		questPanel.SetActive (true);
		menuPanel.SetActive (false);
		Animator anim = questPanel.GetComponent<Animator> ();
		anim.enabled = questPanel.activeSelf;
		Debug.Log ("Quest pressed");
	}

	void StoreButton(){

		storePanel.SetActive (true);
		menuPanel.SetActive (false);
		Animator anim = storePanel.GetComponent<Animator> ();
		anim.enabled = storePanel.activeSelf;
		Debug.Log ("Store pressed");
	}

	public void QuitStore(){

		storePanel.SetActive (false);
		Animator anim = storePanel.GetComponent<Animator> ();
		anim.enabled = storePanel.activeSelf;
		menuPanel.SetActive (true);
	}

	void CineButton(){

		waitForUnityAdsOnPanel.SetActive (true); //This Panel will handle with UnityAdsButton.ShowAdPlacement ();
		Debug.Log ("Cine pressed");
	}

	void SettingsButton(){

		settingPanel.SetActive (true);
		menuPanel.SetActive (false);
		Animator anim = settingPanel.GetComponent<Animator> ();
		anim.enabled = settingPanel.activeSelf;
		Debug.Log ("Settings pressed");
	}

	public void QuitSettings(){

		settingPanel.SetActive (false);
		Animator anim = settingPanel.GetComponent<Animator> ();
		anim.enabled = settingPanel.activeSelf;
		menuPanel.SetActive (true);
	}

	public void QuitQuest(){

		questPanel.SetActive (false);
		Animator anim = questPanel.GetComponent<Animator> ();
		anim.enabled = questPanel.activeSelf;
		menuPanel.SetActive (true);
	}

	void PlayClickAudio(){

		clickButtonAudio.Play ();
	}

	#region Deal with Settings

	public void ChangeMuscisVol(Slider slider){

		GameControl.control.musicVol = ConvertValueTodB(slider.value);
		GameControl.control.SetMusicVol (GameControl.control.musicVol);
		PlayerPrefs.SetFloat ("musicVol", GameControl.control.musicVol);
		PlayerPrefs.Save ();
	}

	public void ChangeEffectsVol(Slider slider){

		GameControl.control.effectsVol = ConvertValueTodB (slider.value);
		GameControl.control.SetEffectsVol (GameControl.control.effectsVol);
		PlayerPrefs.SetFloat ("effectsVol", GameControl.control.effectsVol);
		PlayerPrefs.Save ();

	}

	public void SoundOnOff(Toggle toggle){

		if (toggle.isOn) {
			
			GameControl.control.masterVol = 0f;
			GameControl.control.SetMasterSoundVol (GameControl.control.masterVol);

		} else {
			
			GameControl.control.masterVol = -80f;
			GameControl.control.SetMasterSoundVol (GameControl.control.masterVol);

		}

		PlayerPrefs.SetFloat ("masterVol", GameControl.control.masterVol);
		PlayerPrefs.Save ();
			

	}


	float ConvertValueTodB(float x){

		// This function convert the x values (from 0 to 1) to dB values from -80 to 0 dB.
		// Using this convertion gives a more smooth valume change at the seeting rather than a liner 
		// conversion.

		float y = -800f * Mathf.Pow (10f, -x) + 80f;
		y /= 9f;

		return y;
	}

	#endregion

}
