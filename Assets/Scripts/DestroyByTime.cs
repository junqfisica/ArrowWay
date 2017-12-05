using UnityEngine;
using System.Collections;

public class DestroyByTime : MonoBehaviour {

	public float destroyAfterXSec;
	private float time;

	void Awake(){

		time = Time.time;
	}

	void Update(){

		if (Time.time > time + destroyAfterXSec)
			Destroy (this.gameObject);
	}
}
