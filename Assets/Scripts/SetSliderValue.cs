using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class SetSliderValue : MonoBehaviour {

	public enum Set{Music,Effects,Sound};
	public Set setVaule;

	void Awake(){

		PlayAudio playAudio = GetComponent<PlayAudio> ();
		SetValue ();
		playAudio.source.Stop (); // Avoid the click sound to play.
	}

	void SetValue(){

		switch (setVaule) {

		case Set.Music:
			Slider musicSlider = GetComponent<Slider> ();
			musicSlider.value = ConvertValueFromdBToSlider(GameControl.control.musicVol);
			break;

		case Set.Effects:
			Slider effectSlider = GetComponent<Slider> ();
			effectSlider.value = ConvertValueFromdBToSlider(GameControl.control.effectsVol);
			break;

		case Set.Sound:
			Toggle toggle = GetComponent<Toggle> ();
			if (GameControl.control.masterVol <= -80f) {
				toggle.isOn = false;
			} else {
				toggle.isOn = true;
			}
			break;
		}

	}

	float ConvertValueFromdBToSlider(float x){

		// This function convert the x values (from -80 to 0 dB) to slidder values from 0 to 1 dB.
		// Using this convertion gives a more smooth valume change at the seeting rather than a liner 
		// conversion.
		// y = - log10((80 - 9x)/800)

		float y = 80f - 9f * x;
		y /= 800f;
		y = -Mathf.Log10 (y);

		return y;
	}

}
