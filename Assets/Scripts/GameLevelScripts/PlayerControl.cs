using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using MyClass;

public class PlayerControl : MonoBehaviour {

	public float fireRate;
	public float fallGravity;
	public Transform shotSpawn;
	public Transform shotSpawn2;
	public GameObject arrow;
	public Image loadingArrow;


	private Animator anim;
	private Rigidbody2D rb;
	private AudioSource audioSorce;
	private float nextFire = 0f;
	private int touchtheground = 0;
	private bool isjumping = false;
	private bool isDraging = false;

	private Vector3 endPoint;
	private Vector3 startPoint;
	private Transform mainCamTrans;
	private AudioClip jumpClip;
	private AudioClip attackClip;

	void Awake() {

		fireRate = ItensOnStore.Bow.fireRate;
		mainCamTrans = Camera.main.GetComponent<Transform> ();
		anim = GetComponent<Animator> ();
		rb = GetComponent<Rigidbody2D> ();
		audioSorce = GetComponent<AudioSource> ();
		jumpClip = Resources.Load<AudioClip> ("Sounds/JumpMe");
		attackClip = Resources.Load<AudioClip> ("Sounds/ShootingArrow");

	}
		

	public void OnBeginDrag(){

		isDraging = true;
		Ray ray = Camera.main.ScreenPointToRay (Input.mousePosition);
		startPoint = ray.origin;

	}

	public void OnEndDrag(){

		Ray ray = Camera.main.ScreenPointToRay (Input.mousePosition);
		endPoint = ray.origin;
		float dx =  endPoint.x - startPoint.x;
		float dy =  endPoint.y - startPoint.y;
		float angle = Mathf.Atan2 (dy, dx)*Mathf.Rad2Deg;

		if (angle >= 60f && angle <= 120f) {
			if (!isjumping)
				Jump ();
		} else if (Mathf.Abs (angle) < 30f) {

			isDraging = false;
			FireArrow ();
		}

		isDraging = false;
	}

	public void FireArrow(){

		if (Time.time > nextFire && !isDraging) {

			nextFire = Time.time + fireRate*Abilites.RapidFire.fireRate;
			GameLevelParameter.totalShoots++;
			anim.SetTrigger ("Attack");
			audioSorce.pitch = 1f;
			audioSorce.volume = 0.2f;
			audioSorce.clip = attackClip;
			audioSorce.Play ();
			loadingArrow.fillAmount = 0;
			StartCoroutine ("LoadingImage");
		}
	}

	IEnumerator LoadingImage(){

		float speedy = 1f/(fireRate*Abilites.RapidFire.fireRate);

		while (loadingArrow.fillAmount < 1f) {

			loadingArrow.fillAmount += speedy*Time.deltaTime;
			yield return null;
		}
	}
		
	void OnCollisionEnter2D (Collision2D  other){

		if (other.gameObject.tag == "Ground") {
			
			touchtheground++;

			if (touchtheground == 2) {
				
				touchtheground = 1;
				anim.SetTrigger ("Down"); // At the end of this clip isjump is set false;
			}
		}
		

	}

	void Attack(){ // This function is called within the animation clip from Attack

		Instantiate(arrow,shotSpawn.position,shotSpawn.rotation);

	}

	void AttackOnAir(){

		Instantiate(arrow,shotSpawn2.position,shotSpawn2.rotation);
	}


	public void Jump(){

		isjumping = true;
		audioSorce.clip = jumpClip;
		audioSorce.pitch = 1.2f;
		audioSorce.volume = 0.4f;
		audioSorce.Play ();
		anim.SetTrigger ("Jump");
		rb.velocity = new Vector2 (0f,10.5f);
		StartCoroutine ("ChangeGravity");
		StartCoroutine ("MoveMainCamera");
	}

	IEnumerator MoveMainCamera(){


		while (mainCamTrans.position.y < 1.32f) { //Moving Up
			
			mainCamTrans.Translate (Vector3.up * 8f * Time.deltaTime, Space.World);
			float y = Mathf.Clamp (mainCamTrans.position.y, 0f, 1.33f);
			mainCamTrans.position = new Vector3 (0f, y, -10f); // Makes sure the camera don't pass a certain point
			yield return null;
		}

		yield return new WaitForSeconds(2f);


		while (mainCamTrans.position.y > -0.23f) { // Moving Down
			mainCamTrans.Translate (-Vector3.up * 1f * Time.deltaTime, Space.World);
			yield return null;
		}

		mainCamTrans.position = new Vector3 (0f, -0.23f, -10f); // Makes sure the camera is back to the right position

	}

	IEnumerator ChangeGravity(){

		float y;

		while (isjumping) {

			y = Mathf.Clamp (rb.position.y, -Mathf.Infinity, 4.4f);
			rb.position = new Vector2 (rb.position.x, y);

			if (rb.velocity.y < 0f)
				rb.gravityScale = fallGravity;
			yield return null;		
		}

		rb.gravityScale = 1f;
	}

	public void NotJump(){

		isjumping = false;
	}
		
}
