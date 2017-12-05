using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using MyClass;

public class DestroyBubblesByPlayerCollision : MonoBehaviour {

	public GameObject enemyExplosion;
	public GameObject scorePoint;
	public GameObject bonusPoint;
	public GameObject butterfly;

	private GameLevelControl gamelc;

	void Awake(){

		gamelc = GameLevelControl.Instance ();

	}

	void OnTriggerEnter2D (Collider2D other){

		if (other.gameObject.tag == "enemy") {

			EnemyControl en = other.gameObject.GetComponent<EnemyControl> ();
			GameLevelParameter.playerScore += en.score + en.score*GameLevelParameter.combo*0.1f;
			gamelc.scoreText.text = GameLevelParameter.playerScore.ToString();
			GameLevelParameter.plusTimeTemp += en.plusTime;
			GameLevelParameter.temporaryCombo++;

			// =============== For Quest ==============
			QuestHandler.questHandler.popBubbles++; // For Quest pop bubles;
			QuestHandler.questHandler.extraSecs = GameLevelParameter.totalTimeGain;
			QuestHandler.questHandler.scorePoints = (int)GameLevelParameter.playerScore; 

			if (en.myColor == EnemyControl.Colors.Blue) {
				QuestHandler.questHandler.popBlueBubbles++;
			} else if (en.myColor == EnemyControl.Colors.Green) {
				QuestHandler.questHandler.popGreenBubbles++;
			} else if (en.myColor == EnemyControl.Colors.Red) {
				QuestHandler.questHandler.popRedBubbles++;
			}

			// Get TextMesh component to set the text
			TextMesh textMesh = scorePoint.GetComponent<TextMesh> ();
			TextMesh textMeshBonus = bonusPoint.GetComponent<TextMesh> ();
			textMesh.text = "+ " + en.score; 
			textMeshBonus.text = "+ " + GameLevelParameter.bonusCoinValue.ToString ();


			gamelc.explodeAudio.pitch = en.pitch_value;
			gamelc.explodeAudio.Play ();


			Instantiate (scorePoint, other.gameObject.transform.position, transform.rotation);
			Instantiate (enemyExplosion, other.gameObject.transform.position, transform.rotation);

			if (other.gameObject.name == "BubblesBonus(Clone)") {// Execute if is the bonus bubble 
				Instantiate (butterfly, other.gameObject.transform.position, transform.rotation);
				Instantiate (bonusPoint, other.gameObject.transform.position + new Vector3 (0f, 1f, 0f), transform.rotation);
				GameControl.control.playerMoney += GameLevelParameter.bonusCoinValue;
				gamelc.PlayGetCoinAudio ();
				QuestHandler.questHandler.freeButterflies++;
			}

			Destroy (other.gameObject);


		}

	}
}
