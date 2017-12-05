using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using System.Collections;
using MyClass;

public class HabilitesHandler : MonoBehaviour, IPointerDownHandler  {

	public Button H1;
	public Button H2;
	public Button H3;
	public AudioSource clickSound;
	public bool isSkillDescriptionVisualized{ get; set;} // Used by Tutorial2;

	private float pressedDownStarted;
	private bool isFingerStillDown;
	private string skillName;
	private ModalPanelSkillDescription skillDescription;

	private Text TextH2;
	private Text TextH3;

	private static HabilitesHandler habiliteH;

	public static HabilitesHandler Instance(){

		if (!habiliteH) {

			habiliteH = FindObjectOfType (typeof(HabilitesHandler)) as HabilitesHandler;
			if (!habiliteH) // Make sure that Modalpanel existes.
				Debug.Log ("There needs to be one active HabilitesHandler Script on a GameObject in your scene");
		}

		return habiliteH;
	}

	void Awake(){

		Abilites.SetSkillsDescritions ();
		skillDescription = ModalPanelSkillDescription.Instance ();
		SetQuantityText ();
		isSkillDescriptionVisualized = false;
	}
		
	//================================================================================
	/// <summary>
	/// Handle with the Abilities buttons when those are pressed. There are 3 buttons, 
	/// each one needs the action and the coolDownTime to perform.  
	/// </summary>
	/// <param name="Action1">Action1.</param>
	/// <param name="coolDownTime">Cool down time.</param>
	/// <param name="Action2">Action2.</param>
	/// <param name="coolDownTime2">Cool down time2.</param>
	/// <param name="Action3">Action3.</param>
	/// <param name="coolDownTime3">Cool down time3.</param>
	//================================================================================
	public void ButtonsPressed(UnityAction Action1, float coolDownTime, UnityAction Action2, float coolDownTime2,
		UnityAction Action3, float coolDownTime3){
		
		H1.onClick.RemoveAllListeners (); 
		H1.onClick.AddListener (PlayAudio);
		H1.onClick.AddListener (Action1);
		H1.onClick.AddListener (() => {CoolDown(H1,coolDownTime);});

		if (TextH2.text == "0")
			H2.interactable = false;
		H2.onClick.RemoveAllListeners (); 
		H2.onClick.AddListener (PlayAudio);
		H2.onClick.AddListener (Action2);
		H2.onClick.AddListener (() => {CoolDown(H2,coolDownTime2);});
		H2.onClick.AddListener (SetQuantityText);

		if (TextH3.text == "0")
			H3.interactable = false;
		H3.onClick.RemoveAllListeners (); 
		H3.onClick.AddListener (PlayAudio);
		H3.onClick.AddListener (Action3);
		H3.onClick.AddListener (() => {CoolDown(H3,coolDownTime3);});
		H3.onClick.AddListener (SetQuantityText);
	
	}
		
	public void  OnPointerDown(PointerEventData data){
		Debug.Log (data.button);

	}

	public void OnFingerDown(Button bt){

		RawImage rw = bt.transform.Find ("Icon").GetComponent<RawImage> ();
		skillName = rw.texture.name;
		pressedDownStarted = Time.time;
		isFingerStillDown = true;
		StartCoroutine ("IsFingerStillDown",bt);
	}

	IEnumerator IsFingerStillDown(Button bt){

		while (isFingerStillDown) {

			if (Time.time > pressedDownStarted + 0.8f) {

				Debug.Log (bt +" was hold");
				GameControl.control.Pause ();
				skillDescription.SetHeadText(skillName);
				skillDescription.SetDescription (Abilites.SkillsDescriptions[skillName]);
				skillDescription.OpenPanel (OnExitSkillPanel);
				isFingerStillDown = false;
			}

			yield return null;
		}
	}

	public void OnFingerUp(Button bt){

		isFingerStillDown = false;
	}

	void OnExitSkillPanel(){

		isSkillDescriptionVisualized = true;
		GameControl.control.UnPause ();
	}

	void PlayAudio(){

		clickSound.Play ();
	}

	void CoolDown(Button bt, float coolDownTime){

		Transform btrans = bt.transform.FindChild ("CastingMask");
		Image img =  btrans.gameObject.GetComponent<Image> ();
		img.fillAmount = 1;
		StartCoroutine (CoolDownRotine(bt,img,coolDownTime));

	}

	IEnumerator CoolDownRotine(Button bt, Image image, float coolDownTime){
		
		bt.enabled = false;
		float speedy = 1f/coolDownTime;

		while (image.fillAmount > 0f) {

			image.fillAmount -= speedy*Time.deltaTime;
			yield return null;
		}

		bt.enabled = true;
			
	}

	//===================================================
	/// <summary>
	/// Sets the number of itens in the Text attacthed to the Red/Blue button .
	/// </summary>
	/// <param name="qt">Quantity of itens.</param>
	//===================================================
	public void SetQuantityText(){

		Abilites.SetSkillsQuantity (); // build the dictionary and update to be used.

		// Set number of itens at H2
		TextH2 = H2.transform.Find("QuantityFrame").transform.Find ("Quantity Text").GetComponent<Text> ();
		RawImage rwH2 = H2.transform.Find ("Icon").GetComponent<RawImage> ();
		string skillNameInH2 = rwH2.texture.name;
		if (Abilites.SkillsQuantity [skillNameInH2] < 100) {
			TextH2.text = Abilites.SkillsQuantity [skillNameInH2].ToString();
		} else {
			TextH2.text = "+99"; // don't show more than 99
		}

		// Set number of itens at H3
		TextH3 = H3.transform.Find("QuantityFrame").transform.Find ("Quantity Text").GetComponent<Text> ();
		RawImage rwH3 = H3.transform.Find ("Icon").GetComponent<RawImage> ();
		string skillNameInH3 = rwH3.texture.name;
		if (Abilites.SkillsQuantity [skillNameInH3] < 100) {
			TextH3.text = Abilites.SkillsQuantity [skillNameInH3].ToString();
		} else {
			TextH3.text = "+99"; // don't show more than 99
		}
	}
		
}

