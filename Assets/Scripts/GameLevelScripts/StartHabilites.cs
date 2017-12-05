using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using MyClass;

public class StartHabilites: MonoBehaviour{

	public AudioSource skillCallAudio;
	public GameObject skillGreen; // for visual effects
	public GameObject skillRed;   // for visual effects
	public GameObject skillBlue;

	public List<Dictionary<string,object>> SkillList = new List<Dictionary<string,object>>(); 

	private float waitSpawnTime;

	private static StartHabilites starthabilite;

	public static StartHabilites Instance(){

		if (!starthabilite) {

			starthabilite = FindObjectOfType (typeof(StartHabilites)) as StartHabilites;
			if (!starthabilite) // Make sure that Modalpanel existes.
				Debug.Log ("There needs to be one active StartHabilites Script on a GameObject in your scene");
		}

		return starthabilite;
	}

	private HabilitesHandler habiliteHandler;

	void Awake(){

		habiliteHandler = HabilitesHandler.Instance ();
	}

	void OnGUI(){

		habiliteHandler.ButtonsPressed (StartRapidFire,Abilites.RapidFire.cooldown,StartExplosion,Abilites.Explosion.cooldown,
			StartSoapTime,Abilites.SoapTime.cooldown);

	}
		

	void PlaySound(){

		skillCallAudio.Play ();

	}


	#region Start/End RapiFire
	void StartRapidFire(){

		Abilites.RapidFire.fireRate = Abilites.RapidFire.reductionFire;
		PlaySound ();
		skillGreen.SetActive (true);
		StartCoroutine ("EndRapidFire");
	}

	IEnumerator EndRapidFire(){

		yield return new WaitForSeconds (Abilites.RapidFire.activeTime);
		skillGreen.SetActive (false);
		Abilites.RapidFire.fireRate = 1f;

	}
	#endregion

	#region Start/End Explosion
	void StartExplosion(){

		if (Abilites.Explosion.quantity != 0) {
			Abilites.Explosion.isActive = true;
			PlaySound ();
			skillRed.SetActive (true);
			Abilites.Explosion.SetQuantity (Abilites.Explosion.quantity - 1);			
			StartCoroutine ("EndExplosion");
			QuestHandler.questHandler.useExplosion++; // For quest proporse. There is no influence on the ability itself.
		}
	}

	IEnumerator EndExplosion(){

		yield return new WaitForSeconds (Abilites.Explosion.activeTime);
		skillRed.SetActive (false);
		Abilites.Explosion.isActive = false;

	}
	#endregion

	#region Start/End SoapTime
	void StartSoapTime(){

		if (Abilites.SoapTime.quantity != 0) {
			waitSpawnTime = GameControl.control.waitSpawnTime; // storege the original value here
			GameControl.control.waitSpawnTime = Abilites.SoapTime.rateOfBubbles;
			PlaySound ();
			skillBlue.SetActive (true);
			Abilites.SoapTime.SetQuantity (Abilites.SoapTime.quantity - 1);
			StartCoroutine ("EndSoapTime");
		}
	}

	IEnumerator EndSoapTime(){

		yield return new WaitForSeconds (Abilites.SoapTime.activeTime);
		skillBlue.SetActive (false);
		GameControl.control.waitSpawnTime = waitSpawnTime;
	}
	#endregion
}
