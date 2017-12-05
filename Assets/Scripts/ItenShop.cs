using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using MyClass;

public class ItenShop : MonoBehaviour {

	public Sprite imageSprite;
	public GameObject[] levelStars;
	public AudioSource coinAudio;

	private int level;
	private bool isForBuy;
	private bool isLevelUp;
	private int levelUpPrice;
	private int buyPrice;
	private int quantity;
	private string description;
	private string nextLevelDescription;
	private Text nameTex; 
	private Sprite fullStar;
	private Button buyButton;
	private Button levelUpButton;

	void Awake(){
			
		fullStar = Resources.Load<Sprite> ("Sprites/star big");

	}

	void OnEnable(){

		UpdateShop (); // Inicialize and Update the shop

		Text buyText = buyButton.gameObject.GetComponentInChildren<Text> ();
		Text levelUpText = levelUpButton.gameObject.GetComponentInChildren<Text> ();
		Text levelText = transform.Find("Image Frame").transform.Find("Level Panel").GetComponentInChildren<Text> ();
		buyText.text = GeneralLanguageSetup.setupLanguage.buy;
		levelUpText.text = GeneralLanguageSetup.setupLanguage.levelUp;
		levelText.text = GeneralLanguageSetup.setupLanguage.level;


		if (buyPrice <= GameControl.control.playerMoney)
			buyButton.interactable = true;
		

		if (levelUpPrice <= GameControl.control.playerMoney)
			levelUpButton.interactable = true;
		

		for (int i = 0; i < levelStars.Length; i++) {// Turn animation off for the stars

			Animator anim = levelStars [i].GetComponent<Animator> ();
			anim.enabled = false;
		}

		//UpdateShop ();
	}

	void UpdateShop(){

		SelectIten ();
		SetUpItenShop ();
		SetLevelStars ();
	}

	void OnGUI(){

		OnButtonClick ();

		if (buyPrice > GameControl.control.playerMoney)
			buyButton.interactable = false;
	

		if (levelUpPrice > GameControl.control.playerMoney)
			levelUpButton.interactable = false;
	}

	// Update here if include new itens
	void SelectIten(){

		switch (name) {

		default:
			Debug.Log ("The iten" + name + "doesn't exit at the ItensOnStore.");
			break;

		case "Bow":
			level = ItensOnStore.Bow.level;
			Debug.Log (name + " level: " + level);
			isForBuy = ItensOnStore.Bow.isForBuy;
			isLevelUp = ItensOnStore.Bow.isLevelUp;
			levelUpPrice = ItensOnStore.Bow.levelUpPrice;
			description = ItensOnStore.Bow.shopDescription;
			nextLevelDescription = ItensOnStore.Bow.shopNextLevelDescription;
			break;

		case "RapidFire":
			level = ItensOnStore.RapidFire.level;
			isForBuy = ItensOnStore.RapidFire.isForBuy;
			isLevelUp = ItensOnStore.RapidFire.isLevelUp;
			levelUpPrice = ItensOnStore.RapidFire.levelUpPrice;
			description = ItensOnStore.RapidFire.shopDescription;
			nextLevelDescription = ItensOnStore.RapidFire.shopNextLevelDescription;
			break;

		case "Explosion":
			level = ItensOnStore.Explosion.level;
			isForBuy = ItensOnStore.Explosion.isForBuy;
			isLevelUp = ItensOnStore.Explosion.isLevelUp;
			buyPrice = ItensOnStore.Explosion.BuyPrice;
			quantity = Abilites.Explosion.quantity;
			levelUpPrice = ItensOnStore.Explosion.levelUpPrice;
			description = ItensOnStore.Explosion.shopDescription;
			nextLevelDescription = ItensOnStore.Explosion.shopNextLevelDescription;
			break;

		case "SoapTime":
			level = ItensOnStore.SoapTime.level;
			isForBuy = ItensOnStore.SoapTime.isForBuy;
			isLevelUp = ItensOnStore.SoapTime.isLevelUp;
			buyPrice = ItensOnStore.SoapTime.BuyPrice;
			quantity = Abilites.SoapTime.quantity;
			levelUpPrice = ItensOnStore.SoapTime.levelUpPrice;
			description = ItensOnStore.SoapTime.shopDescription;
			nextLevelDescription = ItensOnStore.SoapTime.shopNextLevelDescription;
			break;

		}
	}
		
	void SetLevelStars(){

		for (int i = 0; i < level; i++) {

			Image st = levelStars [i].GetComponent<Image> ();
			st.sprite = fullStar;
			RectTransform rectTf = levelStars [i].transform.Find ("Glow").GetComponent<RectTransform> ();
			rectTf.gameObject.SetActive (true); // set the glow active.
		}

	}

	void GainLevelStar(int i){

		Animator anim = levelStars [i].GetComponent<Animator> ();
		anim.enabled = true;
	}

	void SetUpItenShop(){

		// Set image
		Image img = transform.Find ("Image Frame").transform.Find("Image").GetComponent<Image> ();
		img.sprite = imageSprite;

		// Set name text
		nameTex = transform.Find("Image Frame").transform.Find("Name").GetComponent<Text> ();
		nameTex.text = name;

		//Set description Text
		Text descpText = transform.Find ("Description Panel").transform.Find ("Description Text").GetComponent<Text> ();
		descpText.text = description;

		//Set nextLevelDescription
		Text nextLevelDescpText = transform.Find ("Description Panel").transform.Find ("NextLevel Text").GetComponent<Text> ();
		nextLevelDescpText.text = nextLevelDescription;

		//Set buy Button
		RectTransform buyRectT = transform.Find ("LevelUpBuy Panel").transform.Find("Buy Panel").GetComponent<RectTransform> ();
		buyRectT.gameObject.SetActive (isForBuy);
		buyButton = buyRectT.Find ("Buy Button").GetComponent<Button> ();

		if (isForBuy) {// If it's for buy set the price.
			Text priceForBuyText = buyRectT.Find ("Coin Panel").transform.Find ("Coin Amont").GetComponent<Text> ();
			priceForBuyText.text = buyPrice.ToString();
			nameTex.text = name + string.Format ("  (x{0})", quantity); // include the number of itens the player has.
		}

		//Set levelUp Button
		RectTransform levelRectT = transform.Find ("LevelUpBuy Panel").transform.Find("LevelUp Panel").GetComponent<RectTransform> ();
		levelRectT.gameObject.SetActive (isLevelUp);
		levelUpButton = levelRectT.Find ("LevelUp Button").GetComponent<Button> ();

		if (isLevelUp) {// If it's for levelUp set the price.
			Text levelUpText = levelRectT.Find ("Coin Panel").transform.Find ("Coin Amont").GetComponent<Text> ();
			levelUpText.text = levelUpPrice.ToString();
		}
			

	}

	// Update here if include new itens that has quantity
	void SaveQuantity(){

		quantity += 1; //bought one, and save after
		nameTex.text = name + string.Format ("  (x{0})", quantity); // include the number of itens the player has.

		switch (name) {

		default:
			Debug.Log ("The iten" + name + "doesn't exit at the ItensOnStore or there is no quantity for this iten.");
			break;

		case "Explosion":
			Abilites.Explosion.SetQuantity (quantity);
			break;

		case "SoapTime":
			Abilites.SoapTime.SetQuantity (quantity);
			break;

		}

	}

	// Update here if include new itens
	void LevelChange(){

		GainLevelStar (level); // start the star animation after buy.
		level += 1; // Update level;

		switch (name) {

		default:
			Debug.Log ("The iten" + name + "doesn't exit at the ItensOnStore.");
			break;

		case "Bow":
			ItensOnStore.Bow.LevelUp (level);
			break;

		case "RapidFire":
			ItensOnStore.RapidFire.LevelUp (level);
			break;

		case "Explosion":
			ItensOnStore.Explosion.LevelUp (level);
			break;

		case "SoapTime":
			ItensOnStore.SoapTime.LevelUp (level);
			break;

		}

		UpdateShop ();
	}

	void OnButtonClick(){

		buyButton.onClick.RemoveAllListeners ();
		buyButton.onClick.AddListener (Buy);

		levelUpButton.onClick.RemoveAllListeners ();
		levelUpButton.onClick.AddListener (LevelUP);

	}

	void PlayCoinAudio(){

		coinAudio.Play ();
	}

	void Buy(){

		if (buyPrice <= GameControl.control.playerMoney) {
			
			Debug.Log ("You Bought " + name);
			PlayCoinAudio ();
			GameControl.control.playerMoney -= buyPrice;
			SaveQuantity ();


		} else {
			
			Debug.Log ("U don't have enogh money");
		}
	}

	void LevelUP(){

		if (levelUpPrice <= GameControl.control.playerMoney) {

			PlayCoinAudio ();
			GameControl.control.playerMoney -= levelUpPrice;
			LevelChange ();
			Debug.Log ("You LevelUp " + name);
			if (name == "Bow"){
				QuestHandler.questHandler.upgradeItem++; // For the quest.
				QuestHandler.questHandler.CheckIfQuestIsComplet(false); // Check if it was complet after.
			}

		} else {
			
			Debug.Log ("U don't have enogh money");
		}
	}
}
