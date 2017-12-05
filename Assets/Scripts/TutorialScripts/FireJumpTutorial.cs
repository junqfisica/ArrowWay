using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.Collections;

public class FireJumpTutorial : MonoBehaviour {

	public Image[] Star;


	private Vector3 endPoint;
	private Vector3 startPoint;
	private PlayerControl playerControl;
	private Transform tutorialComplete;
	private Transform tutorialInstructions;
	private Transform click;
	private Text instructionText;
	private Animator clickAnim;
	private Sprite cancelSprite;
	private Sprite okSprite;
	private Image hlArrow; 
	private EventTrigger eventTrigger; 

	private bool attackByClick = true;
	private bool attackByDrag = false;
	private bool jump = false;
	private bool isjumping = false;
	private bool isDraging = false;
	private bool attackIsLoaded = true;
	private bool attackNotLoadedFinished = false;

	void Awake(){

		GameObject go = GameObject.FindGameObjectWithTag ("Player");
		playerControl = go.GetComponent<PlayerControl> ();

		eventTrigger = GetComponent<EventTrigger> ();
		tutorialComplete = transform.FindChild ("Tutorial Complete");
		tutorialInstructions = transform.FindChild ("Tutorial Instructions");
		click = tutorialInstructions.FindChild ("Click");
		instructionText = tutorialInstructions.FindChild ("InstructionText").GetComponent<Text>();
		instructionText.text = GeneralLanguageSetup.setupLanguage.fireArrow;
		clickAnim = click.gameObject.GetComponent<Animator> ();
		cancelSprite = Resources.Load<Sprite> ("Sprites/Icons/Cancel");
		okSprite =  Resources.Load<Sprite> ("Sprites/Icons/check");
	}

	void OnGUI(){

		GameLevelParameter.playTime = Mathf.Clamp(GameLevelParameter.playTime,30,30); // fake time pause

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

		if (angle >= 60f && angle <= 120f) {
			if (!isjumping)
				Jump ();
		} else if (Mathf.Abs (angle) < 30f) {

			isDraging = false;
			AttackByDrag ();
		}

		isDraging = false;
	}

	public void AttackByClick(){

		if (attackByClick && !isDraging) {

			if (attackIsLoaded) {
				
				playerControl.FireArrow ();
				StartCoroutine ("AttackNotLoaded");

			} else if (attackNotLoadedFinished) {

				playerControl.FireArrow ();
				StartCoroutine (TutorialComplete(0));
				clickAnim.SetTrigger ("dragRight");
				attackByClick = false;
				attackByDrag = true;

			}
		}
	}

	IEnumerator AttackNotLoaded(){

		attackIsLoaded = false;
		hlArrow = tutorialInstructions.FindChild ("Highlight Arrow").GetComponent<Image>();
		hlArrow.gameObject.SetActive (true);
		Image checkMark = tutorialInstructions.FindChild ("Highlight Arrow").transform.FindChild("Check Mark").GetComponent<Image> ();
		hlArrow.color = Color.red;
		checkMark.sprite = cancelSprite;

		yield return new WaitForSeconds (0.8f);


		Time.timeScale = 0.2f;

		yield return new WaitForSeconds (0.5f);

		Time.timeScale = 1f;
		hlArrow.color = Color.green;
		checkMark.sprite = okSprite;
		attackNotLoadedFinished = true;

	}


	void AttackByDrag(){

		if (attackByDrag) {

			instructionText.text = GeneralLanguageSetup.setupLanguage.jump;
			playerControl.FireArrow ();
			StartCoroutine (TutorialComplete(1));
			clickAnim.SetBool ("dragUp",true);
			hlArrow.gameObject.SetActive (false);
			attackByDrag = false;
			jump = true;
		
		}

	}

	void Jump(){

		if (jump) {
			
			playerControl.Jump ();
			StartCoroutine (TutorialComplete(2));
			jump = false;
		}
	}

	IEnumerator TutorialComplete(int i){

		Animator anim = Star[i].gameObject.GetComponent<Animator> ();
		tutorialInstructions.gameObject.SetActive (false);
		eventTrigger.enabled = false; // turnoff the event trigger.

		yield return new WaitForSeconds (0.5f);

		tutorialComplete.gameObject.SetActive (true);
		anim.enabled = true;
		yield return new WaitForSeconds (4f);

		eventTrigger.enabled = true; // After animation is finish turn on the event trigger.

		switch (i){

		case 0:
			tutorialInstructions.gameObject.SetActive (true);
			clickAnim.SetBool ("dragRight", true);
			break;

		case 1:
			tutorialInstructions.gameObject.SetActive (true);
			clickAnim.SetBool ("dragUp", true);
			break;

		case 2:
			tutorialInstructions.gameObject.SetActive (false);
			PlayerPrefs.SetString ("Fire&Jump", "Fire&Jump");
			PlayerPrefs.Save();
			LevelManager.lm.LoadLevel ("Game");
			break;

		}
		anim.enabled = false;
		tutorialComplete.gameObject.SetActive (false);

	}

}
