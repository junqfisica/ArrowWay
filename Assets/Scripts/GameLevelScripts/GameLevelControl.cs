using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using MyClass;

public class GameLevelControl : MonoBehaviour {

	public GameObject tutorial1;
	public GameObject tutorial2;
	public GameObject enemies;
	public GameObject bonusObj;
	public GameObject comboObj;
	public AudioSource explodeAudio;
	public AudioSource getCoinsAudio;
	public Transform spawn1stPoint;
	public Transform spawn2ndPoint;
	public Transform spawn3thPoint;
	public Transform spawBonus;
	public Text scoreText;
	public Text timeText;
	public Text plusTimeText;
	public Text coins;
	public Animator CoinsWereAdd;

	private float updateTime;
	private float updateCombo;
	private Text comboTex;
	private Animator comboAni;

	private GameSetupLanguages gameSetupL;
	private ModalPanelStartPause modalPanelStartPause;

	private static GameLevelControl gamelc;

	public static GameLevelControl Instance(){

		if (!gamelc) {

			gamelc = FindObjectOfType (typeof(GameLevelControl)) as GameLevelControl;
			if (!gamelc) // Make sure that Modalpanel existes.
				Debug.Log ("There needs to be one active GameLevelControl Script on a GameObject in your scene");
		}

		return gamelc;
	}

	void Awake(){

		GameLevelParameter.RestartVar ();
		coins.text = GameControl.control.playerMoney.ToString ();
		gameSetupL = GameSetupLanguages.Instance ();
		comboTex = comboObj.GetComponentInChildren<Text> ();
		comboAni = comboObj.GetComponent<Animator> ();
		comboObj.SetActive (false);
		tutorial1.SetActive (false); // Deactivate tutotial
		tutorial2.SetActive (false);  // Deactivate tutotial
	}

	void Start(){

		updateTime = Time.time;
		updateCombo = Time.time;

		if (!PlayerPrefs.HasKey ("Fire&Jump")) {
			tutorial1.SetActive (true);
		
		} else if(!PlayerPrefs.HasKey ("SkillTutorial")) {
			tutorial2.SetActive (true);

		} else {
			
			StartGame ();
		}

	}

	void StartGame(){

		modalPanelStartPause = ModalPanelStartPause.Instance ();
		GameControl.control.Pause ();
		modalPanelStartPause.OpenPanel (ModalPanelStartPause.Status.Start, null);
		StartCoroutine ("SpawEnemies");
		StartCoroutine ("SpawBonus");
	}

	public void PauseGame(){

		GameControl.control.Pause ();
		modalPanelStartPause.OpenPanel (ModalPanelStartPause.Status.Pause, () => {modalPanelStartPause.StartCountDown(3);});
	}

	void OnGUI(){

		Combo (); // check the combo.

		if (coins.text != GameControl.control.playerMoney.ToString()) {
			int moneyHad = int.Parse(coins.text);
			int gainMoney = GameControl.control.playerMoney - moneyHad;
			if (gainMoney > 0)
				QuestHandler.questHandler.earnCoins += gainMoney;

			//Update the Panel for the coins.
			coins.text = GameControl.control.playerMoney.ToString();
			CoinsWereAdd.SetTrigger ("gotCoin");
		}

	}

	void Update(){


		if (Time.time > updateTime + 1f) {

			updateTime = Time.time;
			GameLevelParameter.playTime--;
			GameLevelParameter.playTime = Mathf.Clamp (GameLevelParameter.playTime, 0, 999999);
			AddTime ();
			UpdateTextTime ();
		}
	}

	IEnumerator SpawEnemies(){

		while (true) {

			yield return new WaitForSeconds (GameControl.control.waitSpawnTime);
			Instantiate (enemies, spawn1stPoint.position, spawn1stPoint.rotation);
			GameObject bubbleClone = (GameObject) Instantiate (enemies, spawn3thPoint.position, spawn3thPoint.rotation);
			Rigidbody2D rbBc = bubbleClone.GetComponent<Rigidbody2D> ();
			rbBc.velocity = new Vector2 (rbBc.velocity.x, -1f * rbBc.velocity.y); // change the bubble velocity's direction

			yield return new WaitForSeconds (GameControl.control.waitSpawnTime);
			Instantiate (enemies, spawn2ndPoint.position, spawn2ndPoint.rotation);

		}

	}

	IEnumerator SpawBonus(){

		while (true) {

			int selected = Random.Range (0, 5);
			yield return new WaitForSeconds (6f);

			int pick = Random.Range (0, 5);
			if (selected == pick) // 20% of a chance of selected = pick
				Instantiate (bonusObj, spawBonus.position, spawBonus.rotation);

		}
	}

	void AddTime(){

		if (GameLevelParameter.plusTimeTemp > 0) {

			GameLevelParameter.shootsOnTarget++;
			GameLevelParameter.playTime += GameLevelParameter.plusTimeTemp;          //Add the time
			GameLevelParameter.totalTimeGain += GameLevelParameter.plusTimeTemp;
			plusTimeText.text = "+" + GameLevelParameter.plusTimeTemp.ToString ();   // Set the gain time to the canvas
			plusTimeText.gameObject.SetActive (true);             // Activate the Canvas object to show the time;
			GameLevelParameter.plusTimeTemp = 0;                                    // Set the temporary plustime var to zero
		}

	}
	//===================================================
	/// <summary>
	/// Check combo. The interval time that the combo can be execute is 
	/// controlled by the combo animation. 
	/// </summary>
	//===================================================
	void Combo(){

		if (GameLevelParameter.temporaryCombo > 2) {
			
			comboObj.SetActive (true);
			GameLevelParameter.combo += GameLevelParameter.temporaryCombo - 2;
			comboTex.text = string.Format ("Combo x{0}", GameLevelParameter.combo);
			QuestHandler.questHandler.reachCombo = GameLevelParameter.combo; // For quest purposes
			GameLevelParameter.temporaryCombo = 2; // This is set tozero on the FinishCombo script.
			comboAni.Play ("Combo", -1, 0f); // reset animation.

		} else if (!comboObj.activeSelf && Time.time > updateCombo + 2f) {

			GameLevelParameter.temporaryCombo = 0;
			updateCombo = Time.time;
		}

	}

	public void UpdateTextTime(){

		timeText.text = string.Format("{0} {1}", GameLevelParameter.playTime, gameSetupL.secs);

	}

	public void PlayGetCoinAudio(){

		getCoinsAudio.Play ();

	}
}

public static class GameLevelParameter{

	public static float playerScore{ get; set;}
	public static int playTime{ get; set;}
	public static int plusTimeTemp{ get; set;}
	public static int totalTimeGain{ get; set;}
	public static int totalShoots{ get; set;}
	public static int shootsOnTarget{ get; set;}
	public static int coinsCollect{ get; set;}
	public static int bonusCoinValue{ get; set;}
	public static int combo{ get; set;}
	public static int temporaryCombo{ get; set;}

	public static void RestartVar(){

		#region Initialize var
		playerScore = 0;
		totalTimeGain = 0;
		playTime = 30;      // The start time of the game.
		plusTimeTemp = 0;
		totalShoots = 0;
		shootsOnTarget = 0;
		coinsCollect = 0;
		bonusCoinValue = 20; // Changes the valeu of freedoom the butterfly.
		combo = 0;
		temporaryCombo = 0;
		#endregion


		#region Restart Abilities
		Abilites.RapidFire.fireRate = 1f;
		Abilites.Explosion.isActive = false;
		GameControl.control.waitSpawnTime = 2f;
		#endregion
	}  

}
