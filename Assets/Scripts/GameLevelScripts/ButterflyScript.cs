using UnityEngine;
using System.Collections;

public class ButterflyScript : MonoBehaviour {

	public float speedy;
	public float waitToChangeSpeed;

	private string sceneName;

	private Rigidbody2D rb;

	void Awake(){

		rb = GetComponent<Rigidbody2D> ();
		StartCoroutine ("ChangeSpeedy");

	}

	IEnumerator ChangeSpeedy(){

		float angle = Random.Range (0, 2f * Mathf.PI);

		while (true) {

			rb.velocity = new Vector2 (speedy*Mathf.Cos(angle), speedy*Mathf.Sin(angle));

			yield return new WaitForSeconds (waitToChangeSpeed);
			float delangle = Random.Range (0, 10f * Mathf.PI*Mathf.Deg2Rad);
			angle += Mathf.PI + delangle;

		}

	}


}
