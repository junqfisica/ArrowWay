using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;
using System.Collections;
using System.Collections.Generic;


public class GameControl : MonoBehaviour {


	public int whitefeq, bluefreq, greenfreq, redfreq; // frequencies that the colors will appear. 1 = 10%, 2 = 20%, 5 = 50%, etc...
	public float waitSpawnTime;
	public AudioMixer audioMixer;
	public int playerMoney{ get; set;}

	[HideInInspector]
	public List<int> whiteList, blueList, greenList, redeList;

	public float musicVol{ get; set;} 
	public float effectsVol{get; set;}
	public float masterVol{ get; set;}

	private List<int> list = new List<int>();



	public static GameControl control;

	void Awake (){

		if (control == null) {
			
			DontDestroyOnLoad (gameObject);
			control = this;

			SetPlayerMoney (); // Set how much money the player have.

		} else if(control != this) {

			Destroy (gameObject);
			
		}

		System.DateTime dat = System.DateTime.Now; // Gets the Time from the system
		Random.seed = dat.Millisecond; // Sets the millisencons as a seed to generate more random numbers.

		#region Create 4 list that controls the frequencies levels of apperance for each color 
		int[] ii = new int[10]{0,1,2,3,4,5,6,7,8,9};
		list.AddRange (ii);
		whiteList =  CreateNewList (whitefeq);
		blueList = CreateNewList (bluefreq);
		greenList = CreateNewList (greenfreq);
		redeList =   CreateNewList (redfreq);
		#endregion
		 
	}

	void Start(){

		// ====== Set sound volumes ======
		SetSoundsValue(); // Don't work at awake "Some unity bug"
		//=================================
	}

	void SetSoundsValue(){

		//===== Set muscic volume =======
		if (PlayerPrefs.HasKey ("musicVol")) {
			musicVol = PlayerPrefs.GetFloat ("musicVol");
		} else {
			musicVol = 0f; // db, where 0 is the maximum
		}
		//===== Set effects volume =======
		if (PlayerPrefs.HasKey ("effectsVol")) {
			effectsVol = PlayerPrefs.GetFloat ("effectsVol");
		} else {
			effectsVol = 0f; // db
		}

		//===== Set master volume =======
		if (PlayerPrefs.HasKey ("masterVol")) {
			masterVol = PlayerPrefs.GetFloat ("masterVol");
		} else {
			masterVol = 0f; // db 
		}

		SetMasterSoundVol(masterVol);
		control.SetMusicVol (musicVol);
		control.SetEffectsVol (effectsVol);

	}
		

	//=============================================================
	/// <summary>
	/// Creates a list of a given size from list.
	/// </summary>
	/// <returns>The new list.</returns>
	/// <param name="size">Size.</param>
	//=============================================================
	List<int> CreateNewList(int size){

		List<int> tempList = new List<int> ();

		for (int i = 0; i < size; i++) {

			int j = Random.Range (0, list.Count);

			tempList.Add(list[j]);
			list.RemoveAt(j);
		}

		return tempList;
	}

	public void SavePlayerMoney(){
			
		PlayerPrefs.SetInt ("coin", playerMoney);
		PlayerPrefs.Save ();

	}

	void SetPlayerMoney(){
		
		if (PlayerPrefs.HasKey ("coin")) {
			
			playerMoney = PlayerPrefs.GetInt ("coin");

		} else {

			playerMoney = 0;
		}
	}

	public void ResetAllPlayerPrefs(){// Meant for dev fase only

		PlayerPrefs.DeleteAll ();
		SetPlayerMoney (); // Set how much money the player have.

	}

	public void Pause(){

		Time.timeScale = 0f;

		if (musicVol > -34f)
			SetMusicVol (-35f); // db the lower limit is -80db

		SetAuraVol(-80f); //db

			
	}

	public void UnPause(){

		Time.timeScale = 1f;

		SetMusicVol (musicVol); // db
		SetAuraVol(0f);

	}

	private void SetAuraVol(float auraVol){

		audioMixer.SetFloat ("auraVol", auraVol);
	}

	public void SetMusicVol(float musicVol){

		audioMixer.SetFloat ("musicVol", musicVol);
	}

	public void SetEffectsVol(float effectsVol){
		
		audioMixer.SetFloat ("effectsVol", effectsVol);
	}

	public void SetMasterSoundVol(float masterVol){

		audioMixer.SetFloat ("masterVol", masterVol);

	}


		
}
