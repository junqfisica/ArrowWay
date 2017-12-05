using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using CameraBounds;


public class SkyMover : MonoBehaviour {

	public float minSpeedy, maxSpeedy;
	public enum Direction{random,right,left};
	public Direction direction;
	public Camera cam;

	private int numberOfClouds;
	private List<GameObject> Clouds = new List<GameObject> ();
	private float[] optionDirection;
	private Vector3 dir;
	private float minBoundx;
	private float maxBoundx;
	private Dictionary<string,float> maxsize = new Dictionary<string,float> ();
	private Dictionary<string,float> minsize = new Dictionary<string,float> ();


	void Awake(){

		numberOfClouds = this.transform.childCount;

		for (int i = 0; i < numberOfClouds; i++) {

			Transform trans = this.transform.GetChild (i);
			Clouds.Add (trans.gameObject);
		}

		optionDirection = new float[2] {-1f,1f};
		SetSpeedy ();
		SetBoundares ();
		GetSizes ();
	
	}

	void SetBoundares(){

		if (cam == null) {

			minBoundx = GetCameraBoundaries.BoundsMin(Camera.main).x;
			maxBoundx = GetCameraBoundaries.BoundsMax(Camera.main).x;
		} else {
			
			minBoundx = GetCameraBoundaries.BoundsMin(cam).x;
			maxBoundx = GetCameraBoundaries.BoundsMin(cam).x;
		}

	}

	void GetSizes(){
		
		foreach (GameObject obj in Clouds) {

			float size = obj.GetComponent<SpriteRenderer> ().sprite.bounds.extents.x;
			string name = obj.name;

			float minx = minBoundx - size;
			float maxx = maxBoundx + size;

			minsize.Add (name, minx);
			maxsize.Add (name, maxx);

		} 

	}
		


	void LateUpdate(){

		ClampPosition ();

	}

	void SetSpeedy(){

		foreach (GameObject obj in Clouds) {
			
			MoveClound (obj);
		}
	}
		

	void MoveClound(GameObject obj){

		Rigidbody2D rb = obj.GetComponent<Rigidbody2D> ();

		switch (direction) {

		case Direction.random:
			float i = optionDirection [Random.Range (0, 2)];
			dir = i * obj.transform.right; 
			break;

		case Direction.left:

			dir = -1f*obj.transform.right; 
			break;

		case Direction.right:

			dir = obj.transform.right; 
			break;
		}


		float speedyMag = Random.Range (minSpeedy, maxSpeedy);
		rb.velocity = dir * speedyMag;

	}

	void ClampPosition(){
		
		foreach (GameObject obj in Clouds) {
			
			Rigidbody2D rb = obj.GetComponent<Rigidbody2D> ();
			string name = obj.name;	

			float posx = Mathf.Clamp (rb.position.x, minsize[name], maxsize[name]);



			if (posx == minsize[name]) {
				
				rb.MovePosition (new Vector2 (maxsize[name], rb.position.y));
			} else if (posx == maxsize[name]) {

				rb.MovePosition (new Vector2 (minsize[name], rb.position.y));

			}

		} 

	}
		
}

