using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class QuestContentControl : MonoBehaviour {

	public GameObject questHolderObj;
	public Slider progressBar;

	private List<GameObject> completQuestHolder;
	private int numberOfCompletQuests;
	private ScrollRect scrollRect;

	void Awake(){

		scrollRect = GetComponentInParent<ScrollRect>();
	}

	void OnEnable(){

		scrollRect.verticalNormalizedPosition = 1f; // Bring the content at the top again

		QuestHandler.questHandler.CheckIfQuestIsComplet (false);

		if (QuestHandler.questHandler.isActiveComplet) {
			progressBar.value = 1f;
		} else {
			progressBar.value = QuestHandler.questHandler.questProgress;
		}
			
		completQuestHolder = new List<GameObject> ();
		ActiveQuest();
		CompletQuestList ();
	}

	void ActiveQuest(){

		GameObject activeQuest = Instantiate (questHolderObj) as GameObject;
		activeQuest.transform.SetParent (this.transform,false); //false keeps the local orientation rather than the global orientation.

		QuestHolderSetUp activeSetup = activeQuest.GetComponent<QuestHolderSetUp>();
		activeSetup.SetupQuestHolder (QuestHandler.questHandler.activeQuest);

	}

	void CompletQuestList(){

		numberOfCompletQuests = QuestHandler.questHandler.numberOfCompletQuests;

		if (completQuestHolder.Count > 0) {// If a list exit, destroy all the elementes inside to be update.

			foreach (GameObject obj in completQuestHolder) {

				DestroyObject (obj);
			}

			completQuestHolder = new List<GameObject> ();
		
		}

		foreach (var quest in QuestHandler.questHandler.completedQuests) {

			GameObject completQuest = Instantiate (questHolderObj) as GameObject;
			completQuest.transform.SetParent (this.transform, false);
			completQuestHolder.Add (completQuest);

			QuestHolderSetUp completSetup = completQuest.GetComponent<QuestHolderSetUp> ();
			completSetup.SetupQuestHolder (quest);
		}

	}

	void OnGUI(){

		if (QuestHandler.questHandler.numberOfCompletQuests != numberOfCompletQuests) {//means that the active quest was finish
			progressBar.value = 0f;
			CompletQuestList ();
		}
	}


}
