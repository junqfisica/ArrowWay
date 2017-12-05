using UnityEngine;
using System.Collections.Generic;
using System.Collections;

public class EnemyControl : MonoBehaviour {

	public float score{ get; set;}
	public float pitch_value{ get; set;}
	public int plusTime{ get; set;}
	public float minDispertionAngle{ get; set;}
	public float maxDispertionAngle{ get; set;}
	public float speedyFactorChange;
	public enum Colors {White,Blue,Green,Red,Bonus};
	public Colors myColor;

	private Rigidbody2D rb;
	private Transform trans;
	private float speedyFactor;


	private float speedy;
	private SpriteRenderer render;

	void Awake(){

		rb = GetComponent<Rigidbody2D> ();
		render = GetComponent<SpriteRenderer> ();
		trans = GetComponent<Transform> ();

		int i = Random.Range (0, 10);

		if (gameObject.name == "BubblesBonus(Clone)") {// if is bonus 

			myColor = Colors.Bonus;
		
		} else {//otherwise

			if (GameControl.control.whiteList.Contains (i)) {
				myColor = Colors.White;
			} else if (GameControl.control.blueList.Contains (i)) {
				myColor = Colors.Blue;
			} else if (GameControl.control.greenList.Contains (i)) {
				myColor = Colors.Green;
			} else if (GameControl.control.redeList.Contains (i)) {
				myColor = Colors.Red;
			}

		}
		
		DifficultIncreaseByScore ();
		SetParameterByColor ();
		SetSpeedy ();
	}
		

	void SetParameterByColor(){

		switch (myColor){

		case Colors.White:

			Vector3 scale_w = new Vector3 (0.7f, 0.7f, 1f);
			Color color_w = new Color (0.37f, 0.37f, 0.37f, 1f);
			Setparm (1f, 0, 5f, 2f, scale_w, color_w);
			minDispertionAngle = 70f;
			maxDispertionAngle = 110f;
			rb.mass = 1.6f;
			break;

		case Colors.Blue:

			Vector3 scale_b = new Vector3 (0.55f, 0.55f, 1f);
			Color color_b = new Color (0.0f, 0.0f, 0.75f, 1f);
			Setparm (2f, 2, 10f, 1.5f, scale_b, color_b);
			minDispertionAngle = 75f;
			maxDispertionAngle = 105f;
			rb.mass = 1.3f;
			break;

		case Colors.Green:

			Vector3 scale_g = new Vector3 (0.45f, 0.45f, 1f);
			Color color_g = new Color (0.0f, 0.7f, 0.176f, 1f);
			Setparm (3f, 3, 15f, 1f, scale_g, color_g);
			minDispertionAngle = 80f;
			maxDispertionAngle = 100f;
			rb.mass = 1f;
			break;

		case Colors.Red:

			Vector3 scale_r = new Vector3 (0.35f, 0.35f, 1f);
			Color color_r = new Color (0.75f, 0f, 0f, 1f);
			Setparm (3.5f, 5, 30f, 0.5f, scale_r, color_r);
			minDispertionAngle = 82f;
			maxDispertionAngle = 98f;
			rb.mass = 0.8f;
			break;

		case Colors.Bonus:

			Vector3 scale_bonus = new Vector3 (0.5f, 0.5f, 1f);
			Color color_bonus = new Color (0.37f, 0.37f, 0.37f, 1f);
			Setparm (2f, 4, 25f, 1.7f, scale_bonus, color_bonus);
			minDispertionAngle = 85f;
			maxDispertionAngle = 130f;
			rb.mass = 1f;
			break;

		}
	}

	void SetSpeedy (){

		float angle = Random.Range (minDispertionAngle, maxDispertionAngle);
		Vector2 vel = new Vector2 (speedy * Mathf.Cos (angle*Mathf.Deg2Rad), speedy * Mathf.Sin (angle*Mathf.Deg2Rad));
		rb.velocity = vel;
	}

	//================================================================================================
	/// <summary>
	/// Set the attributes of the bubble, like speedy, score, ptich, etc..
	/// </summary>
	/// <param name="speedy">Speedy.</param>
	/// <param name="plusTime">Plus time.</param>
	/// <param name="score">Score.</param>
	/// <param name="pitch">Pitch.</param>
	/// <param name="scale">Scale.</param>
	/// <param name="color">Color.</param>
	//================================================================================================
	void Setparm (float speedy, int plusTime, float score, float pitch,Vector3 scale, Color color){

		this.speedy = speedyFactor*speedy;
		this.plusTime = plusTime;
		this.score = score;
		pitch_value = pitch;
		trans.localScale = scale;
		render.color = color;

	}

	void DifficultIncreaseByScore(){

		int lv = (int)GameLevelParameter.playerScore / 100;

		speedyFactor = 1f + (float)lv * speedyFactorChange; 

	}
		
}
