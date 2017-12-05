using UnityEngine;
using System.Collections;
using MyClass;

public class ExploderCollider : MonoBehaviour {

	private CircleCollider2D circle;

	void Awake(){

		circle = GetComponent<CircleCollider2D> ();
		circle.radius = 0f;
		StartCoroutine ("GrowExplosion");
	}

	IEnumerator GrowExplosion(){

		float maxRadius = Abilites.Explosion.explosionRadius;
		float dr = 0.4f;

		while (circle.radius < maxRadius) {

			circle.radius += dr;
			yield return null;
		}

		circle.enabled = false;
	}

}
