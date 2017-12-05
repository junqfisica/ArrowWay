using UnityEngine;
using System.Collections;

public class Mover : MonoBehaviour {

	public float speed;
	Rigidbody2D rg;
	
	void Start ()
	{
		rg = GetComponent<Rigidbody2D>();

		// Makes the object move forward when enter on the scene.
		rg.velocity = new Vector2(Mathf.Abs(transform.position.x*speed),0f);
	 
	}

}
