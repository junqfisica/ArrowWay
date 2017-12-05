using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class DeactivateAnimationAfterFinish : MonoBehaviour {

	public void Deactivate(){

		// Clen the text component
		Text text = this.gameObject.GetComponent<Text> ();
		text.text = null;
		//

		this.gameObject.SetActive (false);
	}
}
