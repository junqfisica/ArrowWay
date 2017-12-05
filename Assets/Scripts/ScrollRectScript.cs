using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ScrollRectScript : MonoBehaviour {

	public int categoryIndex;
	public int itemIndex;
	public Button[] categoryButtons;
	public Button[] moveHorizontalBt; // 0 for Forward, 1 for Backward.


	private ScrollRect scrollRect;
	private RectTransform[] categoryTr;
	private float dx;
	private int numOfCetegories;
	private int[] numOfItens;

	private ColorBlock cbMoveH;

	bool isForwardAllow{get{ 

			bool cond = true;

			if (scrollRect.horizontalNormalizedPosition == 1 || numOfItens[categoryIndex] == 1)
				cond = false;

			return cond;
		}
	}

	bool isBackwardAllow{get{ 

			bool cond = true;

			if (scrollRect.horizontalNormalizedPosition == 0)
				cond = false;

			return cond;

		}
	}

	void Awake(){

		scrollRect = GetComponent<ScrollRect> ();
		int numOfChild = scrollRect.content.transform.childCount;
		numOfCetegories = numOfChild;
		categoryTr = new RectTransform[numOfChild];
		numOfItens = new int[numOfChild];

		for (int i =0; i < numOfChild; i++) {

			categoryTr[i] = scrollRect.content.transform.GetChild(i).GetComponent<RectTransform>();
			numOfItens[i] = categoryTr [i].childCount;
		}

		cbMoveH = moveHorizontalBt[0].colors;
		ChangeButtonColor ();
		scrollRect.content = categoryTr [categoryIndex];
		dx = 1f / (float)(numOfItens [categoryIndex] - 1);
			
	}

	void OnGUI(){

		OnClick ();

		if (!isForwardAllow) {
			moveHorizontalBt [0].interactable = isForwardAllow;
			cbMoveH.normalColor = new Color (1f, 1f, 1f, 0.5f);
			moveHorizontalBt [0].colors = cbMoveH;
		} else {
			moveHorizontalBt [0].interactable = isForwardAllow;
			cbMoveH.normalColor = new Color (1f, 1f, 1f, 1f);
			moveHorizontalBt [0].colors = cbMoveH;
		}

		if (!isBackwardAllow) {
			moveHorizontalBt [1].interactable = isBackwardAllow;
			cbMoveH.normalColor = new Color (1f, 1f, 1f, 0.5f);
			moveHorizontalBt [1].colors = cbMoveH;
		} else {
			moveHorizontalBt [1].interactable = isBackwardAllow;
			cbMoveH.normalColor = new Color (1f, 1f, 1f, 1f);
			moveHorizontalBt [1].colors = cbMoveH;
		}
	}
		

	void OnClick(){

		categoryButtons [0].onClick.RemoveAllListeners ();
		categoryButtons [0].onClick.AddListener (() => {MoveVertical(categoryButtons[0]);});


		categoryButtons [1].onClick.RemoveAllListeners ();
		categoryButtons [1].onClick.AddListener (() => {MoveVertical(categoryButtons[1]);});

		categoryButtons [2].onClick.RemoveAllListeners ();
		categoryButtons [2].onClick.AddListener (() => {MoveVertical(categoryButtons[2]);});

		categoryButtons [3].onClick.RemoveAllListeners ();
		categoryButtons [3].onClick.AddListener (() => {MoveVertical(categoryButtons[3]);});


	}
		
		

	public void MoveHorizontalFoarwd(){

		scrollRect.content = categoryTr [categoryIndex];
		dx = 1f / (float)(numOfItens [categoryIndex] - 1);


		if (scrollRect.horizontalNormalizedPosition != 1)
			scrollRect.horizontalNormalizedPosition += dx;

	}

	public void MoveHorizontalBackarwd(){


		if (scrollRect.horizontalNormalizedPosition != 0)
			scrollRect.horizontalNormalizedPosition -= dx;

	}




	void MoveVertical(Button bt){


		Text indexText = bt.gameObject.GetComponentInChildren<Text> ();
		string cateroryIndexText = indexText.text;


		scrollRect.horizontalNormalizedPosition = 0f;
		scrollRect.content = scrollRect.transform.FindChild ("Content").transform.GetComponent<RectTransform>();
		scrollRect.horizontalNormalizedPosition = 0f;

		categoryIndex = int.Parse (cateroryIndexText);

		ChangeButtonColor ();

		float dy = 1f / (float)(numOfCetegories - 1);
		float posy = 1f - (float)categoryIndex * dy;

		scrollRect.verticalNormalizedPosition = posy;

	}

	void ChangeButtonColor (){

		for (int i = 0; i < numOfCetegories; i++) {

			ColorBlock cb = categoryButtons[i].colors;

			if (i == categoryIndex) {// highlight the selected button
 
				cb.normalColor = new Color (1f,0.52f,0f,1f);
			} else {// turn off the highlight button

				cb.normalColor = Color.white;
			}

			categoryButtons[i].colors = cb;

		}
	}
}
