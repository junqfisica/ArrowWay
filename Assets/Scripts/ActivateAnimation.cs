using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ActivateAnimation : MonoBehaviour {

	private Animator anim;

	void Awake (){

		anim = GetComponent<Animator> ();
		anim.enabled = false;
	}


	void OnGUI(){

		if (GameLevelParameter.playTime == 14 && !anim.isActiveAndEnabled)
			ActiveAnimation ();

		if (GameLevelParameter.playTime <= 0)
			DeactivateAnimation ();
	}

	void ActiveAnimation(){

		anim.enabled = true;
	}

	public void DeactivateAnimation(){

		if (GameLevelParameter.playTime >= 15 || GameLevelParameter.playTime <= 0) {
			RectTransform rectransform = GetComponent<RectTransform>();
			Text text = GetComponent<Text>();
			rectransform.localScale = new Vector3 (1f, 1f, 1f);
			text.color = new Color (1f, 1f, 1f, 1f);
			anim.enabled = false;

		}
			
	} 
		
}
