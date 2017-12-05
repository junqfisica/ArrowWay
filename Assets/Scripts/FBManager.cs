using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using Facebook.Unity;
using System.Linq;
using Facebook.MiniJSON;


public class FBManager : MonoBehaviour {

	private static FBManager _instance;

	public static FBManager Instance{

		get{

			if (_instance == null) {

				GameObject fbm = new GameObject("FBManager");
				fbm.AddComponent<FBManager> ();
			}

			return _instance;
		}

	}

	public static string AppLinkUrl{ get; set;}	
	public static string ImageAppLinkUrl{ get; set;}


	public bool isLoggedIn { get; set;}
	public bool havePublishActions{ get; set;}
	public string userName{ get; set;}
	public Texture userProfileImage{ get; set;}
	public List<Dictionary<string,object>> scoreQueryList{ get; set;}
	public bool scoreListIsFinishToLoad{ get; set;}
	private List<object> scoreList = null;

	void Awake(){

		DontDestroyOnLoad (this.gameObject);
		_instance = this;
		AppLinkUrl = "https://fb.me/647371218760374"; // Create on https://developers.facebook.com/quickstarts/641056969391799/?platform=app-links-host
		ImageAppLinkUrl = "https://i.imgsafe.org/cdc3d38b50.png";
	}


	#region Inicialize FB
	public void InitFB(){

		if (!FB.IsInitialized) {

			FB.Init (SetInit, OnHideUnity);
		} else {

			Debug.Log("FB is initialize");
			// Already initialized, signal an app activation App Event
			FB.ActivateApp();
		}

	}

	void SetInit(){

		if (FB.IsInitialized) {
			// Signal an app activation App Event
			FB.ActivateApp();
			isLoggedIn = FB.IsLoggedIn;
		} else {
			
			Debug.Log("Failed to Initialize the Facebook SDK");
		}

	}

	void OnHideUnity(bool isGameShown){

		if (!isGameShown) {

			Time.timeScale = 0f;

		} else {

			Time.timeScale = 1f;
		}
			
	}
	#endregion

	#region FB Login	
	public void FBLogin(){

		isLoggedIn = FB.IsLoggedIn;

		if (FB.IsLoggedIn) {

			if (AccessToken.CurrentAccessToken.Permissions.Contains("user_friends")) {
				Debug.Log("have user friends");
			} else {
				Debug.Log("no user friends");
				var perms = new List<string>(){"public_profile", "user_friends"};
				FB.LogInWithReadPermissions (perms, AuthCallBack);
			}


		} else {
			
			var perms = new List<string>(){"public_profile", "user_friends"};
			FB.LogInWithReadPermissions (perms, AuthCallBack);
		}
	}

	void AuthCallBack(IResult result){

		if (result.Error != null) {

			Debug.Log (result.Error);

		} else {

			if (FB.IsLoggedIn) {

				Debug.Log ("FB is logged in with ReadPermisson");
				isLoggedIn = FB.IsLoggedIn;
				GetProfile ();

			} else {

				isLoggedIn = FB.IsLoggedIn;
				Debug.Log ("FB is not logged in");
			}
		}
	}

	public void FBLoginWithPublishActions(){

		isLoggedIn = FB.IsLoggedIn;
		havePublishActions = false;

		if (FB.IsLoggedIn) {

			if (AccessToken.CurrentAccessToken.Permissions.Contains("publish_actions")) {
				Debug.Log("have publish actions");
				havePublishActions = true;
			} else {
				Debug.Log("no publish actions");
				var publishPerms = new List<string>(){"publish_actions"};
				FB.LogInWithPublishPermissions(publishPerms, PublishCallBack);
			}


		} else {
			
			var publishPerms = new List<string>(){"publish_actions"};
			FB.LogInWithPublishPermissions(publishPerms, PublishCallBack);
		}
			
	}

	void PublishCallBack(ILoginResult result){

		if (result.Error != null) {

			Debug.Log (result.Error);

		} else {

			if (FB.IsLoggedIn) {

				// AccessToken class will have session details
				var aToken = Facebook.Unity.AccessToken.CurrentAccessToken;

				// Print current access token's User ID
				Debug.Log(aToken.UserId);
				// Print current access token's granted permissions
				foreach (string perm in aToken.Permissions) {
					Debug.Log(perm);
				}

				Debug.Log ("FB is logged in with publish permisson");
				isLoggedIn = FB.IsLoggedIn;
				havePublishActions = true;

			} else {

				isLoggedIn = FB.IsLoggedIn;
				Debug.Log ("FB is not logged in");
			}
		}
	}

	public void LogOutFB(){

		//FB.LogOut ();
	}
	#endregion

	#region Deal with FB Profile
	public void GetProfile(){

		FB.API ("/me?fields=first_name", HttpMethod.GET, DisplayUserName); // Get the first name from the user
		FB.API("/me/picture?type=square&height=128&width=128",HttpMethod.GET,DisplayProfilePic);
		//FB.GetAppLink (DealWithAppLink); // Only after the app is published
	}

	void DisplayUserName(IResult result){

		if (result.Error == null) {

			userName = "" + result.ResultDictionary ["first_name"];
			Debug.Log ("User name was loaded");

		} else {
			Debug.Log (result.Error);
		}
		
	}

	void DisplayProfilePic(IGraphResult result){

		if (result.Texture != null) {
			Debug.Log ("Texture was loaded");
			userProfileImage = result.Texture;

		} else {
			Debug.Log (result.Error);
		}


	}

	/*void DealWithAppLink(IAppLinkResult result){

		if (result.Url != null) {// This will work properlly when the apk is aprroved bt FB
			
			AppLinkUrl = result.Url.ToString (); // to make sure result.Url is a string

		} else {// In mean while use this link

			AppLinkUrl = "https://www.dropbox.com/s/73mlik7q4cu7h90/ArrowWay.apk?dl=0";
		}

		Debug.Log ("AppLink= " + AppLinkUrl);
	}*/
	#endregion

	#region FB share
	public void Share(){

		string title = "ArrowWay";
		string caption = GeneralLanguageSetup.setupLanguage.captionShare;
		string description = GeneralLanguageSetup.setupLanguage.descriptionShare;

		FB.FeedShare (string.Empty, new Uri (AppLinkUrl), title,caption, description, 
			new Uri (ImageAppLinkUrl), string.Empty, ShareCallBack);
	}

	void ShareCallBack(IShareResult result){

		if (result.Cancelled) {
			Debug.Log ("Share Cancelled");
		} else if (result.Error != null) {
			Debug.Log ("Error on Share");
		} else if (result.RawResult != null) {
			Debug.Log ("Success on Share");
		}
			
	}

	public void ShareWithUsers(float score){

		string challengeMsg = GeneralLanguageSetup.setupLanguage.challengeMsg;
		string title = GeneralLanguageSetup.setupLanguage.challengeTitle;
		string scoreText = string.Format (GeneralLanguageSetup.setupLanguage.scoreChallenge, score);

		FB.AppRequest (challengeMsg, null, new List<object> (){ "app_users" }, 
			null, null,scoreText, title, ShareWithUsersCallBack);
	}

	void ShareWithUsersCallBack(IAppRequestResult result){

		Debug.Log (result.RawResult);

		if (result.Cancelled) {
			Debug.Log ("Challenge Cancelled");
		} else if (result.Error != null) {
			Debug.Log ("Error on Challenge");
		} else if (result.RawResult != null) {
			Debug.Log ("Success on Challenge");
		}

	}
	#endregion

	#region FB Invitation
	public void Invite(){

		FB.Mobile.AppInvite (new Uri(AppLinkUrl), new Uri(ImageAppLinkUrl), InviteCallBack);
	}

	void InviteCallBack(IResult result){

		if (result.Cancelled) {
			Debug.Log ("Invite Cancelled");
		} else if (result.Error != null) {
			Debug.Log ("Error on Invite");
		} else if (result.RawResult != null) {
			Debug.Log ("Success on Invite ");
		}

	}
	#endregion

	#region Score API

	public void QueryScore(){

		scoreListIsFinishToLoad = false;
		scoreQueryList = null;
		FB.API ("/app/scores?fields=score,user.limit(10)",HttpMethod.GET, ScoreCallBack);
	}

	void ScoreCallBack(IResult result){
		
		scoreQueryList = new List<Dictionary<string,object>>();
		var dic = Json.Deserialize(result.RawResult) as Dictionary<string,object>;  
		scoreList = (List<object>)(dic ["data"]);


		Dictionary<string,object> dicQuery = new Dictionary<string, object>(); // gonna be inside the scoreQueryList

		int count = 0;

		foreach (object score in scoreList) {


			var entry = (Dictionary<string,object>)score;
			var user = (Dictionary<string,object>)entry ["user"];

			dicQuery.Add ("id", user ["id"]);
			dicQuery.Add ("picture", null);
			dicQuery.Add ("name", user ["name"]);
			dicQuery.Add ("score", entry ["score"]);
			scoreQueryList.Add (dicQuery);
			dicQuery = new Dictionary<string, object> (); // clean the dictonary

		}

		#region Load the Friends Images
		foreach(Dictionary<string,object> dict in scoreQueryList){

			var user = (Dictionary<string,object>)dict;

			FB.API("/"+user["id"].ToString()+"/picture?type=square&height=128&width=128",HttpMethod.GET,delegate(IGraphResult picResult) {

				if (picResult.Error != null){
					Debug.Log(picResult.Error);
				
				}else{

					user["picture"] = picResult.Texture;
					count++;
				}

				if(count >= scoreList.Count)
					scoreListIsFinishToLoad = true;
			});
				
		}
		#endregion	
	}

	public void SetScore(string score){

		var scoreData = new Dictionary<string,string> ();
		scoreData.Add ("score", score);

		FB.API ("/me/scores", HttpMethod.POST, delegate(IGraphResult result) {
			Debug.Log ("Score submit result: " + result.RawResult);	
		}, scoreData);

	}

	#endregion
}
