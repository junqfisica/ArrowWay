using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class FinishCombo : MonoBehaviour {


	public void CloseCombo(){// call from the animator event.

		GameLevelParameter.combo = 0;
		GameLevelParameter.temporaryCombo = 0;
		this.gameObject.SetActive(false);
	}
}
