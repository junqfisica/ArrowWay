using UnityEngine;
using System.Collections;
using CameraBounds;

public class DestroyOnExit : MonoBehaviour {

	public Camera cam;

	private Vector2 minBound;
	private Vector2 maxBound;

	void Awake(){

		SetBoundares ();
		BoxCollider2D collider = this.GetComponent<BoxCollider2D> ();
		collider.size = new Vector2 (Mathf.Abs(minBound.x) + Mathf.Abs(maxBound.x), (Mathf.Abs(minBound.y) + Mathf.Abs(maxBound.y))*1.3f);
	}

	void SetBoundares(){

		if (cam == null) {

			minBound = GetCameraBoundaries.BoundsMin(Camera.main);
			maxBound = GetCameraBoundaries.BoundsMax(Camera.main);
		} else {

			minBound = GetCameraBoundaries.BoundsMin(cam);
			maxBound = GetCameraBoundaries.BoundsMin(cam);
		}

	}

	void OnTriggerExit2D (Collider2D other){

		if(other.gameObject.tag != "Ground")
			Destroy (other.gameObject);
	}
}
