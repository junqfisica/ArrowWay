using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using System.Collections;
using MyClass;

public class SkillsTutorial : MonoBehaviour {

	public Image[] Star;
	public GameObject bubbles;
	public GameObject skillRed;
	public AudioSource skillCallAudio;
	public Button H2Tut;

	private Vector3 endPoint;
	private Vector3 startPoint;
	private PlayerControl playerControl;
	private Transform tutorialComplete;
	private Transform tutorialInstructions;
	private Transform HandAnim;
	private Text instructionText;
	private Animator HandAnimAnimator;
	private HabilitesHandler habilitesHandler;
	private GameSetupLanguages gameSetupL;
	private EventTrigger eventTrigger; 


	private bool isDraging = false;
	private bool isPart1Finish = false;

	void Awake(){

		eventTrigger = GetComponent<EventTrigger> ();
		GameObject go = GameObject.FindGameObjectWithTag ("Player");
		playerControl = go.GetComponent<PlayerControl> ();
		gameSetupL = GameSetupLanguages.Instance ();

		tutorialComplete = transform.FindChild ("Tutorial Complete");
		tutorialInstructions = transform.FindChild ("Tutorial Instructions");
		HandAnim = tutorialInstructions.FindChild ("HandAnim");
		instructionText = tutorialInstructions.FindChild ("InstructionText").GetComponent<Text>();
		HandAnimAnimator = HandAnim.gameObject.GetComponent<Animator> ();

		habilitesHandler = HabilitesHandler.Instance ();
	}

	void Start(){

		instructionText.text = gameSetupL.t2infoText1;
	}

	void OnGUI(){

		GameLevelParameter.playTime = Mathf.Clamp(GameLevelParameter.playTime,30,30); // fake time pause

		if (habilitesHandler.isSkillDescriptionVisualized && !isPart1Finish) {
			isPart1Finish = true;
			habilitesHandler.isSkillDescriptionVisualized = false;
			StartCoroutine("TutorialComplete", 0);
		}

	}

	public void OnClickH2(Button bt){

		if (HandAnimAnimator.GetBool ("infoFinish")) {
			HandAnimAnimator.SetBool ("infoFinish", false); // Avoid to use StartExplosion twice.
			HandAnim.gameObject.SetActive(false);
			habilitesHandler.ButtonsPressed (StartHabilityEmpty, 0f, StartExplosion, Abilites.Explosion.cooldown,
				StartHabilityEmpty, 0f);
			bt.onClick.Invoke ();
		}
	}

	#region Start/End Explosion
	void StartExplosion(){

		bubbles.SetActive (true); // Set the targets on
		bubbles.SetActive(false); // makes stops. Trick
		bubbles.SetActive (true); // set on again

		instructionText.text = gameSetupL.t2infoText3;


		if (Abilites.Explosion.quantity != 0) {
			Abilites.Explosion.isActive = true;
			PlaySound ();
			skillRed.SetActive (true);
			StartCoroutine ("EndExplosion");

		}
	}

	IEnumerator EndExplosion(){

		int numOfbubles = bubbles.transform.childCount;
		while (numOfbubles > 0) {
			numOfbubles = bubbles.transform.childCount;
			yield return null;
		}
		
		skillRed.SetActive (false);
		Abilites.Explosion.isActive = false;
		StartCoroutine("TutorialComplete", 1);

	}

	void StartHabilityEmpty(){

		Debug.Log ("Do nothing");
	}
	#endregion

	void PlaySound(){

		skillCallAudio.Play ();

	}

	public void OnBeginDrag(){

		isDraging = true;
		Ray ray = Camera.main.ScreenPointToRay (Input.mousePosition);
		startPoint = ray.origin;

	}

	public void OnEndDrag(){

		Ray ray = Camera.main.ScreenPointToRay (Input.mousePosition);
		endPoint = ray.origin;
		float dx =  endPoint.x - startPoint.x;
		float dy =  endPoint.y - startPoint.y;
		float angle = Mathf.Atan2 (dy, dx)*Mathf.Rad2Deg;

		if (Mathf.Abs (angle) < 30f) {

			isDraging = false;
			AttackByDrag ();
		}

		isDraging = false;
	}

	public void AttackByClick(){

		if (!isDraging) {

			playerControl.FireArrow ();
		}
	}


	void AttackByDrag(){

		playerControl.FireArrow ();

	}



	IEnumerator TutorialComplete(int i){

		Animator anim = Star[i].gameObject.GetComponent<Animator> ();
		tutorialInstructions.gameObject.SetActive (false);
		eventTrigger.enabled = false;

		yield return new WaitForSeconds (0.5f);

		tutorialComplete.gameObject.SetActive (true);
		anim.enabled = true;

		yield return new WaitForSeconds (4f);

		eventTrigger.enabled = true;

		switch (i){

		case 0:
			tutorialInstructions.gameObject.SetActive (true);
			instructionText.text = gameSetupL.t2infoText2;
			HandAnimAnimator.SetBool ("infoFinish", true);
			Abilites.Explosion.SetQuantity (Abilites.Explosion.quantity + 2); // Gives to the player
			Abilites.SoapTime.SetQuantity (Abilites.SoapTime.quantity + 2 );  // Gives to the player
			// Save here to avoid the player to play it again and get more skills.
			PlayerPrefs.SetString ("SkillTutorial", "SkillTutorial");
			PlayerPrefs.Save();
			break;

		case 1:
			tutorialInstructions.gameObject.SetActive (true);
			anim.enabled = false;
			tutorialComplete.gameObject.SetActive (false);
			instructionText.text = gameSetupL.t2infoText4;
			yield return new WaitForSeconds (10f);
			LevelManager.lm.LoadLevel ("Game");
			break;


		}

		anim.enabled = false;
		tutorialComplete.gameObject.SetActive (false);

	}
}
