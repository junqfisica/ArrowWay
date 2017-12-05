using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using MyClass;

public class QuestHolderSetUp : MonoBehaviour {


	private Text infoText;
	private Text quantityText;
	private Button rewardBut;
	private Toggle markBox;
	private Image rewardIcon;
	private PlayAudio playAudio;
	private Quests thisQuest;

	void Awake(){

		infoText =  transform.Find("questInfo").GetComponent<Text> ();
		quantityText = transform.Find("Reward Button").Find("quantityText").GetComponent<Text> ();
		rewardBut = transform.Find ("Reward Button").GetComponent<Button> ();
		markBox = transform.Find ("MarkBox").GetComponent<Toggle>();
		rewardIcon = transform.Find ("Reward Button").Find("Icon").GetComponent<Image>();
	}

	void Start(){
		//must be at the Start in Awake it doens's find PlayAudio
		playAudio = GetComponentInParent<PlayAudio> ();
	}

	void OnDisable(){
		DestroyObject (this.gameObject);
	}

	void OnGUI(){

		OnClick ();

	}

	void OnClick(){

		rewardBut.onClick.RemoveAllListeners ();
		rewardBut.onClick.AddListener (playAudio.PlaySound);
		rewardBut.onClick.AddListener (GetReward);
		rewardBut.onClick.AddListener (thisQuest.Reward);
		rewardBut.onClick.AddListener (QuestHandler.questHandler.ResetVarAferReward);
	}
		

	public void SetupQuestHolder(Quests quest){

		thisQuest = quest; // Add quest to this questHolder.
		infoText.text = quest.description;
		quantityText.text = quest.rewardValue.ToString();
		markBox.isOn = quest.isDone;
		rewardIcon.sprite = quest.rewardSprite;

		if (quest.isDone && quest.isActive) {
			rewardBut.interactable = true;

		} else {
			rewardBut.interactable = false;
		} 

		if (quest.name == "End"){// End Of the Quest TurnOff Toggle and button.
			rewardBut.gameObject.SetActive (false);
			markBox.gameObject.SetActive (false);
			// Change Layout to a bigger value.
			LayoutElement layout = infoText.gameObject.GetComponent<LayoutElement> ();
			layout.minWidth = 260f;
			layout.preferredWidth = 260f;
		}
	}

	void GetReward(){

		QuestHandler.questHandler.activeQuest.QuestIsDone(); // set the active quest as isDone = true
		QuestHandler.questHandler.UpdateList ();             // Update the list of quests
		SetupQuestHolder (QuestHandler.questHandler.activeQuest); // it will trun the button off.
		QuestHandler.questHandler.isActiveComplet = false;
	}


}
