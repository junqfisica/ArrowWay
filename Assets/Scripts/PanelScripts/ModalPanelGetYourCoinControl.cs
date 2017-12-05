using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using System.Collections;

public class ModalPanelGetYourCoinControl : MonoBehaviour {

	public GameObject modalPanelObject;
	public Button openChest;
	public Button fail;
	public Button skipped;
	public GameObject openChestPanelObj;
	public GameObject coinChestObj;
	public Text coinsText;

	[HideInInspector]
	public enum Status{Success,Skipped,Fail};

	private Transform rewardPanel;
	private Transform skippedPanel;
	private Transform failPanel;


	private static ModalPanelGetYourCoinControl modalPanel;

	public static ModalPanelGetYourCoinControl Instance(){

		if (!modalPanel) {

			modalPanel = FindObjectOfType (typeof(ModalPanelGetYourCoinControl)) as ModalPanelGetYourCoinControl;
			if (!modalPanel) // Make sure that Modalpanel existes.
				Debug.Log ("There needs to be one active ModalPanelGetYourCoinControl Script on a GameObject in your scene");
		}

		return modalPanel;
	}

	void Awake(){
		
		rewardPanel = modalPanelObject.transform.Find ("GetYourCoin Panel").GetComponent<Transform> ();
		skippedPanel =  modalPanelObject.transform.Find ("Skipped Panel").GetComponent<Transform> ();
		failPanel = modalPanelObject.transform.Find ("Fail Panel").GetComponent<Transform> ();
	}

	//================================================================================
	/// <summary>
	/// Opens the panel for given the reward in the case of success.
	/// </summary>
	/// <param name="status">Status.</param>
	/// <param name="Event">Event.</param>
	//================================================================================
	public void OpenPanel(Status status, UnityAction Event){


		modalPanelObject.SetActive (true);

		switch(status){

		default:
			Debug.Log ("Error this Status do not exist");
			break;

		case Status.Success:
			rewardPanel.gameObject.SetActive (true);
			skippedPanel.gameObject.SetActive (false);
			failPanel.gameObject.SetActive (false);
			coinsText.text = UnityAdsButton.coinsGot.ToString ();

			openChest.onClick.RemoveAllListeners ();
			openChest.onClick.AddListener (Event);
			openChest.onClick.AddListener (OpenChestPanel);
			break;

		case Status.Fail:
			rewardPanel.gameObject.SetActive (false);
			skippedPanel.gameObject.SetActive (false);
			failPanel.gameObject.SetActive (true);

			fail.onClick.RemoveAllListeners ();
			fail.onClick.AddListener (Event);
			fail.onClick.AddListener (ClosePanel);
			fail.onClick.AddListener (AdsIsOver);
			break;

		case Status.Skipped: 
			rewardPanel.gameObject.SetActive (false);
			skippedPanel.gameObject.SetActive (true);
			failPanel.gameObject.SetActive (false);

			skipped.onClick.RemoveAllListeners ();
			skipped.onClick.AddListener (Event);
			skipped.onClick.AddListener (ClosePanel);
			skipped.onClick.AddListener (AdsIsOver);
			break;
		}
			
	}

	void ClosePanel(){
		
		rewardPanel.gameObject.SetActive (false);
		skippedPanel.gameObject.SetActive (false);
		failPanel.gameObject.SetActive (false);

		modalPanelObject.SetActive (false);
	}

	void AdsIsOver(){

		UnityAdsButton.AdsOnIsrunnig = false;
		UnityAdsButton.AdsOnComplet = true;
	}
		

	void OpenChestPanel(){

		if (GameControl.control.musicVol > -34f)
			GameControl.control.SetMusicVol (-35f); // db the lower limit is -80db
		
		openChestPanelObj.SetActive (true);
		StartCoroutine ("WaitForClosePanel");

	}

	IEnumerator WaitForClosePanel(){

		yield return StartCoroutine(CoroutineUtil.WaitForRealSeconds(3.5f));
		coinChestObj.SetActive (true); // start the coin animation

		yield return StartCoroutine(CoroutineUtil.WaitForRealSeconds(3.8f));
		ClosePanel ();
		AdsIsOver ();
		coinChestObj.SetActive (false);
		openChestPanelObj.SetActive (false);
		GameControl.control.SetMusicVol (GameControl.control.musicVol); // db the lower limit is -80db
	}
		
}

public static class CoroutineUtil
{
	public static IEnumerator WaitForRealSeconds(float time)
	{
		float start = Time.realtimeSinceStartup;
		while (Time.realtimeSinceStartup < start + time)
		{
			yield return null;
		}
	}
}
