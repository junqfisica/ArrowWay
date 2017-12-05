using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using MyClass;


public class QuestHandler : MonoBehaviour {

	public  Quests activeQuest;
	public  List<Quests> completedQuests;
	public bool isActiveComplet;
	public float questProgress{ get; set;}

	#region quest variables 
	// Inclue the var at the methods ResetVarAferReward() and SetTheQuestsVar()
	public int popBubbles; 
	public int popBlueBubbles;
	public int popGreenBubbles;
	public int popRedBubbles;
	public int finish3matchs;
	public int upgradeItem;
	public int freeButterflies;
	public int extraSecs; // in game
	public int scorePoints; // in game
	public int earnCoins;
	public int useExplosion; //in game
	public int reachCombo; //in game
	#endregion

	public int numberOfCompletQuests{ get; set;}


	private Quests quests = new Quests();

	public static QuestHandler questHandler;

	void Awake ()
	{

		if (questHandler == null) {

			DontDestroyOnLoad (gameObject);
			questHandler = this;
			LoadIsActiveComplet ();
			SetTheQuestsVar ();

		} else if (questHandler != this) {

			Destroy (gameObject);

		}
	}

	void Start(){

		SetQuestList ();

	}

	public void ResetVarAferReward(){

		popBubbles = 0; 
		popBlueBubbles = 0;
		popGreenBubbles = 0;
		popRedBubbles = 0;
		finish3matchs = 0;
		upgradeItem = 0;
		freeButterflies = 0;
		extraSecs = 0; // in game
		scorePoints= 0; // in game
		earnCoins = 0;
		useExplosion = 0; //in game
		reachCombo = 0; //in game

		CheckIfQuestIsComplet (false); // Make the Check to save the var at zero.
	}

	public void SetTheQuestsVar(){

		popBubbles = LoadQuestVal(); 
		popBlueBubbles =  LoadQuestVal();
		popGreenBubbles =  LoadQuestVal();
		popRedBubbles = LoadQuestVal();
		finish3matchs =  LoadQuestVal();
		upgradeItem =  LoadQuestVal();
		freeButterflies =  LoadQuestVal();
		extraSecs = 0; // in game
		scorePoints= 0; // in game
		earnCoins =  LoadQuestVal();
		useExplosion = 0; //in game
		reachCombo = 0; //in game
	}

	void SaveQuestVal(int value){// Save the value for the current quest.

		PlayerPrefs.SetInt ("QuestVal", value);
		PlayerPrefs.Save ();
	}

	int LoadQuestVal(){

		if (PlayerPrefs.HasKey ("QuestVal")) {

			return PlayerPrefs.GetInt ("QuestVal");

		} else {

			return 0;
		}
	}
		
	void LoadIsActiveComplet(){

		if (!PlayerPrefs.HasKey ("QuestState")) {
			isActiveComplet = false;

		} else {
			string strg = PlayerPrefs.GetString ("QuestState");
			isActiveComplet = Boolean.Parse (strg);
		}
	}

	public void SaveIsActiveComplet(){

		string strg = isActiveComplet.ToString ();
		PlayerPrefs.SetString ("QuestState", strg);
		PlayerPrefs.Save ();

	}
		
	void SetQuestList(){

		quests.SetQuests (); // From MyClass Quests
		SetActive();
		CompletedQuestsList ();
	}

	public void UpdateList(){

		SetActive();
		CompletedQuestsList ();
	}

	void SetActive(){

		foreach(Quests qs in quests.questList){

			if (!qs.isActive && !qs.isDone) {
				qs.isActive = true;
				activeQuest = qs;
				break;
			}
		}
	}

	public void ChangeLanguageOnQuest(){

		quests.ChangeLanguage();
	}

	void CompletedQuestsList(){

		completedQuests = new List<Quests> ();

		foreach(Quests qs in quests.questList){

			if (!qs.isActive && qs.isDone) {
				completedQuests.Add(qs);
			}
		}

		numberOfCompletQuests = completedQuests.Count;
		completedQuests.Reverse (); // For estetical resons on the quest menu. 

	}

	void CollectRewardAvaible(){

		if (isActiveComplet)
			activeQuest.isDone = isActiveComplet;
	}

	float ComputeProgress(int val, int finalVal){

		float prog = (float)val / (float)finalVal;

		prog = Mathf.Clamp (prog, 0f, 1f);
		
		return prog;
	}

	//======================================================
	/// <summary>
	/// Checks if quest is complet. If isPause = true it checks the progress of in Game quests, otherwise not.
	/// </summary>
	/// <param name="isPause">If set to <c>true</c> is pause.</param>
	//======================================================
	public void CheckIfQuestIsComplet(bool isPause){

		switch (activeQuest.myGroup) {

		default:
			SaveQuestVal(0);
			questProgress = 0;
			break;

		case Quests.Groups.UpgradeIten:
			questProgress = ComputeProgress (upgradeItem, activeQuest.questEndsAt);
			SaveQuestVal (upgradeItem);
			if (upgradeItem >= activeQuest.questEndsAt)
				isActiveComplet = true;
			break;

		case Quests.Groups.PopBubbles:
			questProgress = ComputeProgress (popBubbles, activeQuest.questEndsAt);
			SaveQuestVal (popBubbles);
			if (popBubbles >=  activeQuest.questEndsAt)
				isActiveComplet = true;
			break;

		case Quests.Groups.FinishGame:
			questProgress = ComputeProgress (finish3matchs,activeQuest.questEndsAt);
			SaveQuestVal (finish3matchs);
			if (finish3matchs >= activeQuest.questEndsAt)
				isActiveComplet = true;
			break;

		case Quests.Groups.PopBlueBubbles:
			questProgress = ComputeProgress (popBlueBubbles, activeQuest.questEndsAt);
			SaveQuestVal (popBlueBubbles);
			if (popBlueBubbles >= activeQuest.questEndsAt)
				isActiveComplet = true;
			break;

		case Quests.Groups.FreeButterFlies:
			questProgress = ComputeProgress (freeButterflies, activeQuest.questEndsAt);
			SaveQuestVal (freeButterflies);
			if (freeButterflies >= activeQuest.questEndsAt)
				isActiveComplet = true;
			break;

		case Quests.Groups.GainExtraTime: // in Game 
			SaveQuestVal(0); // in Game has to be settle to zero
			if (extraSecs >= activeQuest.questEndsAt) {
				isActiveComplet = true;
			} else {
				if (isPause) {
					questProgress = ComputeProgress (extraSecs, activeQuest.questEndsAt);
				} else {
					extraSecs = 0; // In game var has to be reset if do not reach the "questEndsAt" value.
				}
			}
			break;

		case Quests.Groups.PopGreenBubbles:
			questProgress = ComputeProgress (popGreenBubbles, activeQuest.questEndsAt);
			SaveQuestVal (popGreenBubbles);
			if (popGreenBubbles >= activeQuest.questEndsAt)
				isActiveComplet = true;
			break;

		case Quests.Groups.ScorePoints: // in Game
			SaveQuestVal(0); // in Game has to be settle to zero
			if (scorePoints >= activeQuest.questEndsAt) {
				isActiveComplet = true;
			} else {
				if (isPause) {
					questProgress = ComputeProgress (scorePoints, activeQuest.questEndsAt);
				} else {
					scorePoints = 0; // In game var has to be reset if do not reach the "questEndsAt" value.
				}
			}
			break;

		case Quests.Groups.EarnGold:
			questProgress = ComputeProgress (earnCoins, activeQuest.questEndsAt);
			SaveQuestVal (earnCoins);
			if (earnCoins >= activeQuest.questEndsAt)
				isActiveComplet = true;
			break;

		case Quests.Groups.PopRedBubbles:
			questProgress = ComputeProgress (popRedBubbles, activeQuest.questEndsAt);
			SaveQuestVal (popRedBubbles);
			if (popRedBubbles >= activeQuest.questEndsAt)
				isActiveComplet = true;
			break;


		case Quests.Groups.UseExplosion: // in Game
			SaveQuestVal(0); // in Game has to be settle to zero
			if (useExplosion >= activeQuest.questEndsAt) {
				isActiveComplet = true;
			} else {
				if (isPause) {
					questProgress = ComputeProgress (useExplosion, activeQuest.questEndsAt);
				} else {
					useExplosion = 0; // In game var has to be reset if do not reach the "questEndsAt" value.
				}
			}
			break;		

		case Quests.Groups.ComboRank: // in Game
			SaveQuestVal(0); // in Game has to be settle to zero
			if (reachCombo >= activeQuest.questEndsAt) {
				isActiveComplet = true;
			} else {
				if (isPause) {
					questProgress = ComputeProgress (reachCombo, activeQuest.questEndsAt);
				} else {
					reachCombo = 0; // In game var has to be reset if do not reach the "questEndsAt" value.
				}
			}
			break;

		}

		CollectRewardAvaible ();
			
	}

}
