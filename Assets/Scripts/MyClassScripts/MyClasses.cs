using UnityEngine;
using System;
using System.Reflection;
using System.Collections;
using System.Collections.Generic;

namespace MyClass{


	public static class Abilites{

		public static Dictionary<string,string> SkillsDescriptions = new Dictionary<string,string>();
		public static Dictionary<string,int> SkillsQuantity;

		public static void SetSkillsDescritions(){

			if (SkillsDescriptions.Count == 0) {
				
				SkillsDescriptions.Add (RapidFire.name, RapidFire.description);
				SkillsDescriptions.Add (Explosion.name, Explosion.description);
				SkillsDescriptions.Add (SoapTime.name, SoapTime.description);
			}
		}

		public static void SetSkillsQuantity(){

			SkillsQuantity = new Dictionary<string,int>(); // Clean the dictionary to update values;

			SkillsQuantity.Add (Explosion.name, Explosion.quantity);
			SkillsQuantity.Add (SoapTime.name, SoapTime.quantity);
		}

		#region Infinity Skills

		#region RapidFire
		public static class RapidFire{

			public static float fireRate = 1f; // normal rate, when Ability is activate fireRate = reductionFire
		 
			//===================================
			/// <summary>
			/// Gets the name.
			/// </summary>
			/// <value>The name.</value>
			//===================================
			public static string name{
				get{ 
					return "rapidFire";
				}
			}

			//====================================
			/// <summary>
			/// Gets the level.
			/// </summary>
			/// <value>The level.</value>
			//====================================
			public static int level{
				get{

					int level;

					if (!PlayerPrefs.HasKey (name)) {
						level = 0;
					}
					else {
						level = PlayerPrefs.GetInt (name);
					}

					return level;
				}
			}

			//===================================
			/// <summary>
			/// Gets the cooldown.
			/// </summary>
			/// <value>The cooldown.</value>
			//===================================
			public static float cooldown{// If change values here must change on ItenOnStore as well.
				get{
					switch (level){

					case 1:
						return 17f;
					case 2:
						return 16f;
					case 3:
						return 16f;
					default:
						return 18f;
					}
				}
			}

			//==================================
		    /// <summary>
		    /// Gets the active time.
		    /// </summary>
		    /// <value>The active time.</value>
			//==================================
			public static float activeTime{// If change values here must change on ItenOnStore as well.
				get{

					switch (level){

					case 1:
						return 10f;
					case 2:
						return 12f;
					case 3:
						return 15f;
					default:
						return 8f;
					}
				}
			}

			public static string description{

				get{

					float fireIncrese = 200f * (1f - reductionFire);

					/*
					return string.Format("Level: {0}\n",level) +
						string.Format("Active Time: {0} secs.\n", activeTime) +
						string.Format("CoolDown: {0} secs.\n",cooldown) +
						string.Format("Description: Increase your fire rate in {0:F0}%", fireIncrese);*/

					return GeneralLanguageSetup.setupLanguage.level + 
						string.Format(": {0}\n",level) +
						string.Format(GeneralLanguageSetup.setupLanguage.activeTime+"\n", activeTime) +
						string.Format(GeneralLanguageSetup.setupLanguage.cooldown+"\n",cooldown) +
						string.Format(GeneralLanguageSetup.setupLanguage.description+": ") +
						string.Format(GeneralLanguageSetup.setupLanguage.rapidFireDescp, fireIncrese);
					
				}

			}

			//===============================================
			/// <summary>
			/// Gets or sets the reduction fire.
			/// </summary>
			/// <value>The reduction fire.</value>
			//===============================================
			public static float reductionFire {// If change values here must change on ItenOnStore as well.

				get {

					switch (level) {

					case 1:
						return 0.7f;
					case 2:
						return 0.6f;
					case 3:
						return 0.5f;

					default:
						return 0.8f;
					}
				
				}
					
			}
				
			//=======================================
			/// <summary>
			/// Sets the level.
			/// </summary>
			/// <param name="level">Level.</param>
			//=======================================
			public static void SetLevel(int level){

				PlayerPrefs.SetInt (name, level);
				PlayerPrefs.Save();

			}


		}
		#endregion

		#endregion

		#region Usable Skills

		#region Explositon
		public static class Explosion{


			public static bool isActive{ get; set; }

			//===================================
			/// <summary>
			/// Gets the name.
			/// </summary>
			/// <value>The name.</value>
			//===================================
			public static string name{
				get{ 
					return "explosion";
				}
			}

			//====================================
			/// <summary>
			/// Gets the level.
			/// </summary>
			/// <value>The level.</value>
			//====================================
			public static int level{
				get{

					int level;

					if (!PlayerPrefs.HasKey (name+"Level")) {
						level = 0;
					}
					else {
						level = PlayerPrefs.GetInt (name+"Level");
					}

					return level;
				}
			}

			//=================================================
			/// <summary>
			/// Gets the quantity.
			/// </summary>
			/// <value>The quantity.</value>
			//=================================================
			public static int quantity{get{

					int qt;
				
					if (!PlayerPrefs.HasKey (name+"qt")) {
						qt = 0;
					}
					else {
						qt = PlayerPrefs.GetInt (name+"qt");
					}

					return qt;
				}
			}

			//===================================
			/// <summary>
			/// Gets the cooldown.
			/// </summary>
			/// <value>The cooldown.</value>
			//===================================
			public static float cooldown{// If change values here must change on ItenOnStore as well.
				get{
					switch (level){

					case 1:
						return 14f;
					case 2:
						return 13f;
					case 3:
						return 13f;
					default:
						return 15f;
					}
				}
			}

			//===================================
			/// <summary>
			/// Gets the explosion radius.
			/// </summary>
			/// <value>The explosion radius.</value>
			//===================================
			public static float explosionRadius{// If change values here must change on ItenOnStore as well.
				get{
					switch (level){

					case 1:
						return 2.5f;
					case 2:
						return 3f;
					case 3:
						return 4f;
					default:
						return 2f;
					}
				}
			}

			//==================================
			/// <summary>
			/// Gets the active time.
			/// </summary>
			/// <value>The active time.</value>
			//==================================
			public static float activeTime{// If change values here must change on ItenOnStore as well.
				get{

					switch (level){

					case 1:
						return 6f;
					case 2:
						return 8f;
					case 3:
						return 10f;
					default:
						return 5f;
					}
				}
			}

			public static string description{

				get{

					/*
					return  string.Format("Level: {0}\n",level) +
						string.Format("Active Time: {0} secs.\n", activeTime) +
						string.Format("CoolDown: {0} secs.\n",cooldown) +
						string.Format("Description: Release an arrow that explodes the bubbles within a radius of  {0} m.", explosionRadius);*/
					
					return GeneralLanguageSetup.setupLanguage.level+
						string.Format(": {0}\n",level) +
						string.Format(GeneralLanguageSetup.setupLanguage.activeTime+"\n", activeTime) +
						string.Format(GeneralLanguageSetup.setupLanguage.cooldown+"\n",cooldown) +
						string.Format(GeneralLanguageSetup.setupLanguage.description+": ") +
						string.Format(GeneralLanguageSetup.setupLanguage.explosionDescp, explosionRadius);
				}

			}
				

			//=======================================
			/// <summary>
			/// Sets the level.
			/// </summary>
			/// <param name="level">Level.</param>
			//=======================================
			public static void SetLevel(int level){

				PlayerPrefs.SetInt (name+"Level", level);
				PlayerPrefs.Save();

			}

			//=====================================
			/// <summary>
			/// Sets the quantity.
			/// </summary>
			/// <param name="qt">Quantity.</param>
			/// ===================================
			public static void SetQuantity(int qt){

				PlayerPrefs.SetInt (name + "qt", qt);
				PlayerPrefs.Save ();

			}


		}
		#endregion

		#region SoapTime
		public static class SoapTime{


			//===================================
			/// <summary>
			/// Gets the name.
			/// </summary>
			/// <value>The name.</value>
			//===================================
			public static string name{
				get{ 
					return "soapTime";
				}
			}

			//====================================
			/// <summary>
			/// Gets the level.
			/// </summary>
			/// <value>The level.</value>
			//====================================
			public static int level{
				get{

					int level;

					if (!PlayerPrefs.HasKey (name+"Level")) {
						level = 0;
					}
					else {
						level = PlayerPrefs.GetInt (name+"Level");
					}

					return level;
				}
			}

			//=================================================
			/// <summary>
			/// Gets the quantity.
			/// </summary>
			/// <value>The quantity.</value>
			//=================================================
			public static int quantity{get{ 

					int qt;

					if (!PlayerPrefs.HasKey (name+"qt")) {
						qt = 0;
					}
					else {
						qt = PlayerPrefs.GetInt (name+"qt");
					}

					return qt;
				}
			}

			//===================================
			/// <summary>
			/// Gets the cooldown.
			/// </summary>
			/// <value>The cooldown.</value>
			//===================================
			public static float cooldown{// If change values here must change on ItenOnStore as well.
				get{
					switch (level){

					case 1:
						return 8f;
					case 2:
						return 7f;
					case 3:
						return 7f;
					default:
						return 10f;
					}
				}
			}

			public static float rateOfBubbles{// If change values here must change on ItenOnStore as well.
				get{

					switch (level){

					case 1:
						return 1f;
					case 2:
						return 0.8f;
					case 3:
						return 0.5f;
					default:
						return 1f;
					}
				}
			}

			//==================================
			/// <summary>
			/// Gets the active time.
			/// </summary>
			/// <value>The active time.</value>
			//==================================
			public static float activeTime{// If change values here must change on ItenOnStore as well.
				get{

					switch (level){

					case 1:
						return 5f;
					case 2:
						return 6f;
					case 3:
						return 7f;
					default:
						return 5f;
					}
				}
			}

			public static string description{

				get{

					float bps = 1f / rateOfBubbles;
					/*
					return  string.Format("Level: {0}\n", level) +
						string.Format("Active Time: {0} secs.\n", activeTime) +
						string.Format("CoolDown: {0} secs.\n", cooldown) +
						string.Format("Description: Increase the rate of bubbles to {0:F2} per secs.\n",bps);*/

					return GeneralLanguageSetup.setupLanguage.level+
						string.Format(": {0}\n",level) +
						string.Format(GeneralLanguageSetup.setupLanguage.activeTime+"\n", activeTime) +
						string.Format(GeneralLanguageSetup.setupLanguage.cooldown+"\n",cooldown) +
						string.Format(GeneralLanguageSetup.setupLanguage.description+": ") +
						string.Format(GeneralLanguageSetup.setupLanguage.soapTimeDescp, bps);

				}

			}


			//=======================================
			/// <summary>
			/// Sets the level.
			/// </summary>
			/// <param name="level">Level.</param>
			//=======================================
			public static void SetLevel(int level){

				PlayerPrefs.SetInt (name + "Level", level);
				PlayerPrefs.Save();

			}

			//=====================================
			/// <summary>
			/// Sets the quantity.
			/// </summary>
			/// <param name="qt">Quantity.</param>
			/// ===================================
			public static void SetQuantity(int qt){

				PlayerPrefs.SetInt (name + "qt", qt);
				PlayerPrefs.Save ();

			}


		}
		#endregion

		#endregion

	}


	public static class ItensOnStore{

		#region Bow
		public static class Bow{

			//================================
			/// <summary>
			/// Gets the name.
			/// </summary>
			/// <value>The name.</value>
			//================================
			public static string name{get{

					return "Bow";
				}
			}

			public static float fireRate{get{ 

					switch (level) {

					case 1:
						return 1.1f;

					case 2:
						return 0.9f;

					case 3:
						return 0.8f;

					default:
						return 1.2f;
					}
				
				}
			}

			private static float FireRateOnLevel(int lv){

				switch (lv) {

				case 1:
					return 1.1f;

				case 2:
					return 0.9f;

				case 3:
					return 0.8f;

				default:
					return 1.1f;
				}

			}

			//=================================
			/// <summary>
			/// Gets a value indicating whether this <see cref="MyClass.ItensOnStore+Bow"/> is for buy.
			/// </summary>
			/// <value><c>true</c> if is for buy; otherwise, <c>false</c>.</value>
			//=================================
			public static bool isForBuy{get{ 
					return false;
				}
			}

			//===================================
			/// <summary>
			/// Gets a value indicating whether this <see cref="MyClass.ItensOnStore+Bow"/> is level up.
			/// </summary>
			/// <value><c>true</c> if is level up; otherwise, <c>false</c>.</value>
			//===================================
			public static bool isLevelUp{get{

					if (level < 3) {
						return true;
					} else {
						return false;
					}
				}
			}

			//=======================================
			/// <summary>
			/// Gets the level up price.
			/// </summary>
			/// <value>The level up price.</value>
			//=======================================
			public static int levelUpPrice{get{ 

					switch (level) {

					case 1:
						return 2000;

					case 2:
						return 6000;

					default:
						return 100;
					}
				}
			}

			public static string shopDescription{get{ 

					float fireIncrese = 1f / fireRate;
					//return string.Format("Description: Increase your fire rate to  {0:F2}  arrows per sec.", fireIncrese);
					return GeneralLanguageSetup.setupLanguage.description + ": " + 
						string.Format(GeneralLanguageSetup.setupLanguage.bowDescp, fireIncrese);
				}
			}

			public static string shopNextLevelDescription{get{ 

					if (level < 3) {
						
						float fireIncrese = 1f / FireRateOnLevel(level +1);

						//return string.Format("Next level: Increase your fire rate to {0:F2} arrows per sec.",fireIncrese);
						return GeneralLanguageSetup.setupLanguage.nextLevel + ": " + 
							string.Format(GeneralLanguageSetup.setupLanguage.bowDescp, fireIncrese);

					} else {

						return "";
					}
				
				}
			}

			//====================================
			/// <summary>
			/// Gets the level.
			/// </summary>
			/// <value>The level.</value>
			//====================================
			public static int level {
				get {

					int level;

					if (!PlayerPrefs.HasKey (name+"Level")) {
						level = 0;
					} else {
						level = PlayerPrefs.GetInt (name+"Level");
					}

					return level;
				}
			}

			//=======================================
			/// <summary>
			/// Level Up the level.
			/// </summary>
			/// <param name="level">Level.</param>
			//=======================================
			public static void LevelUp(int level){

				PlayerPrefs.SetInt (name+"Level", level);
				PlayerPrefs.Save();

			}

		}
		#endregion

		#region Skill Itens
		#region RapidFire
		public static class RapidFire{

			//================================
			/// <summary>
			/// Gets the name.
			/// </summary>
			/// <value>The name.</value>
			//================================
			public static string name{get{

					return "RapidFire";
				}
			}
				

			//=================================
			/// <summary>
			/// Gets a value indicating whether this <see cref="MyClass.ItensOnStore+Bow"/> is for buy.
			/// </summary>
			/// <value><c>true</c> if is for buy; otherwise, <c>false</c>.</value>
			//=================================
			public static bool isForBuy{get{ 
					return false;
				}
			}

			//===================================
			/// <summary>
			/// Gets a value indicating whether this <see cref="MyClass.ItensOnStore+Bow"/> is level up.
			/// </summary>
			/// <value><c>true</c> if is level up; otherwise, <c>false</c>.</value>
			//===================================
			public static bool isLevelUp{get{

					if (level < 3) {
						return true;
					} else {
						return false;
					}
				}
			}

			//=======================================
			/// <summary>
			/// Gets the level up price.
			/// </summary>
			/// <value>The level up price.</value>
			//=======================================
			public static int levelUpPrice{get{ 

					switch (level) {

					case 1:
						return 800;

					case 2:
						return 3000;

					default:
						return 200;
					}
				}
			}

			private static float ReductionFireOnLevel(int lv){

				switch (lv) {

				case 1:
					return 0.7f;
				case 2:
					return 0.6f;
				case 3:
					return 0.5f;

				default:
					return 0.7f;
				}

			}

			private static string ActiveTimeOnLevel(int lv){

				switch (lv){

				case 1:
					return "10";
				case 2:
					return "12";
				case 3:
					return "15";
				default:
					return "8";
				}

			}

			private static string CooldownTimeOnLevel(int lv){

				switch (lv){

				case 1:
					return "17";
				case 2:
					return "16";
				case 3:
					return "16";
				default:
					return "18";
				}

			} 

			public static string shopDescription{get{ 

					float fireIncrese = 200f * (1f - Abilites.RapidFire.reductionFire);
					/*
					return string.Format("Description: Increase your fire rate in {0:F0}%.\n",fireIncrese) +
						string.Format("Active Time: {0} secs.\n",Abilites.RapidFire.activeTime)  +
						string.Format("CoolDown: {0} secs.",Abilites.RapidFire.cooldown);*/
					return string.Format(GeneralLanguageSetup.setupLanguage.description +": ") +
						string.Format(GeneralLanguageSetup.setupLanguage.rapidFireDescp+"\n",fireIncrese) +
						string.Format(GeneralLanguageSetup.setupLanguage.activeTime+"\n",Abilites.RapidFire.activeTime)  +
						string.Format(GeneralLanguageSetup.setupLanguage.cooldown,Abilites.RapidFire.cooldown);
					
					
				}
			}

			public static string shopNextLevelDescription{get{ 

					if (level < 3) {

						float fireIncrese = 200f * (1f - ReductionFireOnLevel(level + 1));
						/*
						return string.Format("Next level: Increase your fire rate in {0:F0}%.\n",fireIncrese) +
							string.Format("Active Time: {0} secs.\n",ActiveTimeOnLevel(level + 1))  +
							string.Format("CoolDown: {0} secs.",CooldownTimeOnLevel(level + 1));*/
						return GeneralLanguageSetup.setupLanguage.nextLevel +": "+
							string.Format(GeneralLanguageSetup.setupLanguage.rapidFireDescp+"\n",fireIncrese) +
							string.Format(GeneralLanguageSetup.setupLanguage.activeTime+"\n",ActiveTimeOnLevel(level + 1))  +
							string.Format(GeneralLanguageSetup.setupLanguage.cooldown,CooldownTimeOnLevel(level + 1));

					} else {

						return "";
					}

				}
			}

			//====================================
			/// <summary>
			/// Gets the level.
			/// </summary>
			/// <value>The level.</value>
			//====================================
			public static int level {
				get {

					return Abilites.RapidFire.level;
				}
			}

			//=======================================
			/// <summary>
			/// Level Up the level.
			/// </summary>
			/// <param name="level">Level.</param>
			//=======================================
			public static void LevelUp(int level){

				Abilites.RapidFire.SetLevel (level);

			}

		}
		#endregion

		#region Explosion
		public static class Explosion{

			//================================
			/// <summary>
			/// Gets the name.
			/// </summary>
			/// <value>The name.</value>
			//================================
			public static string name{get{

					return "Explosion";
				}
			}


			//=================================
			/// <summary>
			/// Gets a value indicating whether this <see cref="MyClass.ItensOnStore+Bow"/> is for buy.
			/// </summary>
			/// <value><c>true</c> if is for buy; otherwise, <c>false</c>.</value>
			//=================================
			public static bool isForBuy{get{ 
					return true;
				}
			}

			//===================================
			/// <summary>
			/// Gets a value indicating whether this <see cref="MyClass.ItensOnStore+Bow"/> is level up.
			/// </summary>
			/// <value><c>true</c> if is level up; otherwise, <c>false</c>.</value>
			//===================================
			public static bool isLevelUp{get{

					if (level < 3) {
						return true;
					} else {
						return false;
					}
				}
			}

			//=======================================
			/// <summary>
			/// Gets the level up price.
			/// </summary>
			/// <value>The level up price.</value>
			//=======================================
			public static int levelUpPrice{get{ 

					switch (level) {

					case 1:
						return 2000;

					case 2:
						return 5000;

					default:
						return 300;
					}
				}
			}

			public static int BuyPrice{get{ 

					switch (level) {

					case 1:
						return 150;

					case 2:
						return 200;

					case 3:
						return 250;

					default:
						return 100;
					}
				}
			}

			private static float ExplosionRadiusOnLevel(int lv){

				switch (lv) {

				case 1:
					return 2.5f;
				case 2:
					return 3f;
				case 3:
					return 4f;
				default:
					return 2f;
				}

			}

			private static string ActiveTimeOnLevel(int lv){

				switch (lv){

				case 1:
					return "6";
				case 2:
					return "8";
				case 3:
					return "10";
				default:
					return "5";
				}

			}

			private static string CooldownTimeOnLevel(int lv){

				switch (lv){

				case 1:
					return "14";
				case 2:
					return "13";
				case 3:
					return "13";
				default:
					return "15";
				}

			} 

			public static string shopDescription{get{ 

					/*
					return string.Format("Description: Shoots an arrow that explodes the bubbles within a radius of {0} m.\n", Abilites.Explosion.explosionRadius) +
						string.Format("Active Time: {0} secs.\n", Abilites.Explosion.activeTime) +
						string.Format("CoolDown: {0} secs.", Abilites.Explosion.cooldown);*/

					return string.Format(GeneralLanguageSetup.setupLanguage.description+": ") + 
						string.Format(GeneralLanguageSetup.setupLanguage.explosionDescp+"\n", Abilites.Explosion.explosionRadius) +
						string.Format(GeneralLanguageSetup.setupLanguage.activeTime+"\n", Abilites.Explosion.activeTime) +
						string.Format(GeneralLanguageSetup.setupLanguage.cooldown, Abilites.Explosion.cooldown);
				}
			}

			public static string shopNextLevelDescription{get{ 

					if (level < 3) {

						/*
						return string.Format("Next level: Shoots an arrow that explodes the bubbles within a radius of {0} m.\n", ExplosionRadiusOnLevel(level + 1)) +
							string.Format("Active Time: {0} secs.\n", ActiveTimeOnLevel(level + 1)) +
							string.Format("CoolDown: {0} secs.",CooldownTimeOnLevel(level + 1));*/
						
						return string.Format(GeneralLanguageSetup.setupLanguage.nextLevel+": ") + 
							string.Format(GeneralLanguageSetup.setupLanguage.explosionDescp+"\n", ExplosionRadiusOnLevel(level + 1)) +
							string.Format(GeneralLanguageSetup.setupLanguage.activeTime+"\n", ActiveTimeOnLevel(level + 1)) +
							string.Format(GeneralLanguageSetup.setupLanguage.cooldown, CooldownTimeOnLevel(level + 1));

					} else {

						return "";
					}

				}
			}

			//====================================
			/// <summary>
			/// Gets the level.
			/// </summary>
			/// <value>The level.</value>
			//====================================
			public static int level {
				get {

					return Abilites.Explosion.level;
				}
			}

			//=======================================
			/// <summary>
			/// Level Up the level.
			/// </summary>
			/// <param name="level">Level.</param>
			//=======================================
			public static void LevelUp(int level){

				Abilites.Explosion.SetLevel (level);

			}

		}
		#endregion

		#region SoapTime
		public static class SoapTime{

			//================================
			/// <summary>
			/// Gets the name.
			/// </summary>
			/// <value>The name.</value>
			//================================
			public static string name{get{

					return "SoapTime";
				}
			}


			//=================================
			/// <summary>
			/// Gets a value indicating whether this <see cref="MyClass.ItensOnStore+Bow"/> is for buy.
			/// </summary>
			/// <value><c>true</c> if is for buy; otherwise, <c>false</c>.</value>
			//=================================
			public static bool isForBuy{get{ 
					return true;
				}
			}

			//===================================
			/// <summary>
			/// Gets a value indicating whether this <see cref="MyClass.ItensOnStore+Bow"/> is level up.
			/// </summary>
			/// <value><c>true</c> if is level up; otherwise, <c>false</c>.</value>
			//===================================
			public static bool isLevelUp{get{

					if (level < 3) {
						return true;
					} else {
						return false;
					}
				}
			}

			//=======================================
			/// <summary>
			/// Gets the level up price.
			/// </summary>
			/// <value>The level up price.</value>
			//=======================================
			public static int levelUpPrice{get{ 

					switch (level) {

					case 1:
						return 2000;

					case 2:
						return 5000;

					default:
						return 300;
					}
				}
			}

			public static int BuyPrice{get{ 

					switch (level) {

					case 1:
						return 150;

					case 2:
						return 200;

					case 3:
						return 250;

					default:
						return 100;
					}
				}
			}

			private static float RateOfBubblesOnLevel(int lv){

				switch (lv) {

				case 1:
					return 1f;
				case 2:
					return 0.8f;
				case 3:
					return 0.5f;
				default:
					return 1f;
				}

			}

			private static string ActiveTimeOnLevel(int lv){

				switch (lv){

				case 1:
					return "5";
				case 2:
					return "6";
				case 3:
					return "7";
				default:
					return "5";
				}

			}

			private static string CooldownTimeOnLevel(int lv){

				switch (lv){

				case 1:
					return "8";
				case 2:
					return "7";
				case 3:
					return "7";
				default:
					return "10";
				}

			} 

			public static string shopDescription{get{ 

					float bps = 1f / Abilites.SoapTime.rateOfBubbles;

					/*
					return string.Format("Description: Increase the rate of bubbles to {0:F2} per secs.\n",bps)+
						string.Format("Active Time: {0} secs.\n", Abilites.SoapTime.activeTime) +
						string.Format("CoolDown: {0} secs.",Abilites.SoapTime.cooldown);*/

					return string.Format(GeneralLanguageSetup.setupLanguage.description+": ") +
						string.Format(GeneralLanguageSetup.setupLanguage.soapTimeDescp+"\n",bps) +
						string.Format(GeneralLanguageSetup.setupLanguage.activeTime+"\n", Abilites.SoapTime.activeTime) +
						string.Format(GeneralLanguageSetup.setupLanguage.cooldown,Abilites.SoapTime.cooldown);
				}
			}

			public static string shopNextLevelDescription{get{ 

					if (level < 3) {

						float bps = 1f / RateOfBubblesOnLevel(level + 1);

						/*
						return string.Format("Next level: Increase the rate of bubbles to {0:F2} per secs.\n",bps)+
							string.Format("Active Time: {0} secs.\n", ActiveTimeOnLevel(level + 1)) +
							string.Format("CoolDown: {0} secs.",CooldownTimeOnLevel(level + 1));*/

						return string.Format(GeneralLanguageSetup.setupLanguage.nextLevel+": ") +
							string.Format(GeneralLanguageSetup.setupLanguage.soapTimeDescp+"\n",bps) +
							string.Format(GeneralLanguageSetup.setupLanguage.activeTime+"\n", ActiveTimeOnLevel(level+1)) +
							string.Format(GeneralLanguageSetup.setupLanguage.cooldown,CooldownTimeOnLevel(level+1));
					} else {

						return "";
					}

				}
			}

			//====================================
			/// <summary>
			/// Gets the level.
			/// </summary>
			/// <value>The level.</value>
			//====================================
			public static int level {
				get {

					return Abilites.SoapTime.level;
				}
			}

			//=======================================
			/// <summary>
			/// Level Up the level.
			/// </summary>
			/// <param name="level">Level.</param>
			//=======================================
			public static void LevelUp(int level){

				Abilites.SoapTime.SetLevel (level);

			}

		}
		#endregion
		#endregion
	}

	public class Quests{

		public List<Quests> questList;

		public enum Groups{EndQuest,UpgradeIten,PopBubbles,FinishGame,PopBlueBubbles,FreeButterFlies,GainExtraTime,ComboRank,
			ScorePoints,PopGreenBubbles,EarnGold,PopRedBubbles,UseExplosion};

		public Groups myGroup{ get; set;}
		public string name{ get; set;}
		public string description{ get; set;}
		public bool isDone{ get; set;}
		public bool isActive{ get; set;}
		public Sprite rewardSprite{ get; set;}
		public int rewardValue{ get; set;}
		public int questEndsAt{ get; set;}

		public delegate void ChangeDescpDelegate();
		public ChangeDescpDelegate changeDescpDelegate;

		public void QuestIsDone(){

			Debug.Log (string.Format ("Quest {0} was complet", name));
			isDone = true;
			isActive = false; // After Click in QuestHolderSetUp the quest is set to false

			PlayerPrefs.SetString (name+"isDone", "yes"); 
			PlayerPrefs.Save();

		}

		public void ChangeLanguage(){// Change the description language of all quests.

			foreach (Quests qs in questList)
				qs.changeDescpDelegate();
		}

		public void SetQuests(){// Add the new quests in this list.

			// !!! Important... the quests must be add in crescent order, from first to last.
			questList = new List<Quests> ();

			questList.Add (new Quests.Q1());
			questList.Add (new Quests.Q2 ());
			questList.Add (new Quests.Q3 ());
			questList.Add (new Quests.Q4 ());
			questList.Add (new Quests.Q5());
			questList.Add (new Quests.Q6());
			questList.Add (new Quests.E1());
			questList.Add (new Quests.E2());
			questList.Add (new Quests.Q7());
			questList.Add (new Quests.Q8());
			questList.Add (new Quests.Q9());
			questList.Add (new Quests.Q10());
			questList.Add (new Quests.Q11());
			questList.Add (new Quests.Q12());
			questList.Add (new Quests.Q13());
			questList.Add (new Quests.Q14());
			questList.Add (new Quests.E3());
			questList.Add (new Quests.Q15());
			questList.Add (new Quests.Q16());
			questList.Add (new Quests.Q17());
			questList.Add (new Quests.Q18());
			questList.Add (new Quests.End ());
		}

		public void Reward(){ // Change here if include more types of rewards.

			switch (rewardSprite.name) {

			default:
				Debug.Log ("Error, no quest with this reward price.");
				break;

			case "coin icon":
				GameControl.control.playerMoney += rewardValue;
				break;

			case "explosion":
				int explosionQuantity = Abilites.Explosion.quantity + rewardValue;
				Abilites.Explosion.SetQuantity (explosionQuantity);
				break;

			case "soapTime":
				int soapQuantity = Abilites.SoapTime.quantity + rewardValue;
				Abilites.SoapTime.SetQuantity (soapQuantity);
				break;

			}
		}

		public class Q1: Quests{


			public Q1(){ 

				name = "Q1";
				myGroup = Groups.UpgradeIten;
				changeDescpDelegate = ChangeDescription;
				isActive = false; // Allway start with false.
				questEndsAt = 1;
				description = GeneralLanguageSetup.setupLanguage.upgradeStore;
				rewardSprite = Resources.Load<Sprite>("Sprites/Icons/explosion");
				rewardValue = 4;
				if (PlayerPrefs.HasKey (name+"isDone")) {

					isDone = true;

				} else {

					isDone = false;
				}
			}

			void ChangeDescription(){// This is intent to modif the Language.

				description =  GeneralLanguageSetup.setupLanguage.upgradeStore;
			}
		}

		public class Q2: Quests{


			public Q2(){

				name = "Q2";
				myGroup = Groups.PopBubbles;
				changeDescpDelegate = ChangeDescription;
				isActive = false; // Always Start with False. It will be handle by QuestHandler.
				questEndsAt = 50;
				description = string.Format (GeneralLanguageSetup.setupLanguage.popBubbles, questEndsAt);
				rewardSprite = Resources.Load<Sprite>("Sprites/Icons/coin icon");
				rewardValue = 200;
				if (PlayerPrefs.HasKey (name+"isDone")) {

					isDone = true;

				} else {

					isDone = false;
				}
			}

			void ChangeDescription(){// This is intent to modif the Language.

				description =  string.Format (GeneralLanguageSetup.setupLanguage.popBubbles, questEndsAt);
			}
		}

		public class Q3: Quests{

			public Q3(){

				name = "Q3";
				myGroup = Groups.FinishGame;
				changeDescpDelegate = ChangeDescription;
				isActive = false; // Allway start with false.
				questEndsAt = 3;
				description = string.Format (GeneralLanguageSetup.setupLanguage.finishGames, questEndsAt);
				rewardSprite = Resources.Load<Sprite>("Sprites/Icons/coin icon");
				rewardValue = 200;
				if (PlayerPrefs.HasKey (name+"isDone")) {

					isDone = true;

				} else {

					isDone = false;
				}
			}

			void ChangeDescription(){// This is intent to modif the Language.

				description =  string.Format (GeneralLanguageSetup.setupLanguage.finishGames, questEndsAt);
			}

		}

		public class Q4: Quests{


			public Q4(){ 

				name = "Q4";
				myGroup = Groups.PopBlueBubbles;
				changeDescpDelegate = ChangeDescription;
				isActive = false; // Allway start with false.
				questEndsAt = 30;
				description = string.Format (GeneralLanguageSetup.setupLanguage.popBlueBubbles, questEndsAt);
				rewardSprite = Resources.Load<Sprite>("Sprites/Icons/coin icon");
				rewardValue = 200;
				if (PlayerPrefs.HasKey (name+"isDone")) {

					isDone = true;

				} else {

					isDone = false;
				}
			}

			void ChangeDescription(){// This is intent to modif the Language.

				description =  string.Format (GeneralLanguageSetup.setupLanguage.popBlueBubbles, questEndsAt);
			}
		}
			
		public class Q5: Quests{


			public Q5(){ 

				name = "Q5";
				myGroup = Groups.FreeButterFlies;
				changeDescpDelegate = ChangeDescription;
				isActive = false; // Allway start with false.
				questEndsAt = 10;
				description = string.Format (GeneralLanguageSetup.setupLanguage.freeButterflies,questEndsAt);
				rewardSprite = Resources.Load<Sprite>("Sprites/Icons/coin icon");
				rewardValue = 250;
				if (PlayerPrefs.HasKey (name+"isDone")) {

					isDone = true;

				} else {

					isDone = false;
				}
			}

			void ChangeDescription(){// This is intent to modif the Language.

				description =  string.Format (GeneralLanguageSetup.setupLanguage.freeButterflies, questEndsAt);
			}
		}

		public class Q6: Quests{


			public Q6(){ 

				name = "Q6";
				myGroup = Groups.GainExtraTime;
				changeDescpDelegate = ChangeDescription;
				isActive = false; // Allway start with false.
				questEndsAt = 45;
				description = string.Format (GeneralLanguageSetup.setupLanguage.extraSecs,questEndsAt);
				rewardSprite = Resources.Load<Sprite>("Sprites/Icons/soapTime");
				rewardValue = 5;
				if (PlayerPrefs.HasKey (name+"isDone")) {

					isDone = true;

				} else {

					isDone = false;
				}
			}

			void ChangeDescription(){// This is intent to modif the Language.

				description =  string.Format (GeneralLanguageSetup.setupLanguage.extraSecs, questEndsAt);
			}
		}

		public class Q7: Quests{


			public Q7(){ 

				name = "Q7";
				myGroup = Groups.PopGreenBubbles;
				changeDescpDelegate = ChangeDescription;
				isActive = false; // Allway start with false.
				questEndsAt = 20;
				description = string.Format (GeneralLanguageSetup.setupLanguage.popGreenBubbles,questEndsAt);
				rewardSprite = Resources.Load<Sprite>("Sprites/Icons/coin icon");
				rewardValue = 300;
				if (PlayerPrefs.HasKey (name+"isDone")) {

					isDone = true;

				} else {

					isDone = false;
				}
			}

			void ChangeDescription(){// This is intent to modif the Language.

				description =  string.Format (GeneralLanguageSetup.setupLanguage.popGreenBubbles, questEndsAt);
			}
		}

		public class Q8: Quests{


			public Q8(){ 

				name = "Q8";
				myGroup = Groups.PopBubbles;
				changeDescpDelegate = ChangeDescription;
				isActive = false; // Allway start with false.
				questEndsAt = 100;
				description = string.Format (GeneralLanguageSetup.setupLanguage.popBubbles,questEndsAt);
				rewardSprite = Resources.Load<Sprite>("Sprites/Icons/coin icon");
				rewardValue = 300;
				if (PlayerPrefs.HasKey (name+"isDone")) {

					isDone = true;

				} else {

					isDone = false;
				}
			}

			void ChangeDescription(){// This is intent to modif the Language.

				description =  string.Format (GeneralLanguageSetup.setupLanguage.popBubbles, questEndsAt);
			}
		}

		public class Q9: Quests{

			public Q9(){ 

				name = "Q9";
				myGroup = Groups.ScorePoints;
				changeDescpDelegate = ChangeDescription;
				isActive = false; // Allway start with false.
				questEndsAt = 500;
				description = string.Format (GeneralLanguageSetup.setupLanguage.scorePoints,questEndsAt);
				rewardSprite = Resources.Load<Sprite>("Sprites/Icons/coin icon");
				rewardValue = 350;
				if (PlayerPrefs.HasKey (name+"isDone")) {

					isDone = true;

				} else {

					isDone = false;
				}
			}

			void ChangeDescription(){// This is intent to modif the Language.
				
				description =  string.Format ( GeneralLanguageSetup.setupLanguage.scorePoints, questEndsAt);
			}
		}

		public class Q10: Quests{

			public Q10(){ 

				name = "Q10";
				myGroup = Groups.EarnGold;
				changeDescpDelegate = ChangeDescription;
				isActive = false; // Allway start with false.
				questEndsAt = 2000;
				description = string.Format (GeneralLanguageSetup.setupLanguage.earnCoins,questEndsAt);
				rewardSprite = Resources.Load<Sprite>("Sprites/Icons/coin icon");
				rewardValue = 350;
				if (PlayerPrefs.HasKey (name+"isDone")) {

					isDone = true;

				} else {

					isDone = false;
				}
			}

			void ChangeDescription(){// This is intent to modif the Language.

				description =  string.Format ( GeneralLanguageSetup.setupLanguage.earnCoins, questEndsAt);
			}
		}

		public class Q11: Quests{

			public Q11(){ 

				name = "Q11";
				myGroup = Groups.PopRedBubbles;
				changeDescpDelegate = ChangeDescription;
				isActive = false; // Allway start with false.
				questEndsAt = 15;
				description = string.Format (GeneralLanguageSetup.setupLanguage.popRedBubbles,questEndsAt);
				rewardSprite = Resources.Load<Sprite>("Sprites/Icons/coin icon");
				rewardValue = 350;
				if (PlayerPrefs.HasKey (name+"isDone")) {

					isDone = true;

				} else {

					isDone = false;
				}
			}

			void ChangeDescription(){// This is intent to modif the Language.

				description =  string.Format ( GeneralLanguageSetup.setupLanguage.popRedBubbles, questEndsAt);
			}
		}
			
		public class Q12: Quests{

			public Q12(){ 

				name = "Q12";
				myGroup = Groups.FreeButterFlies;
				changeDescpDelegate = ChangeDescription;
				isActive = false; // Allway start with false.
				questEndsAt = 30;
				description = string.Format (GeneralLanguageSetup.setupLanguage.freeButterflies,questEndsAt);
				rewardSprite = Resources.Load<Sprite>("Sprites/Icons/coin icon");
				rewardValue = 400;
				if (PlayerPrefs.HasKey (name+"isDone")) {

					isDone = true;

				} else {

					isDone = false;
				}
			}

			void ChangeDescription(){// This is intent to modif the Language.

				description =  string.Format ( GeneralLanguageSetup.setupLanguage.freeButterflies, questEndsAt);
			}
		}

		public class Q13: Quests{

			public Q13(){ 

				name = "Q13";
				myGroup = Groups.PopBlueBubbles;
				changeDescpDelegate = ChangeDescription;
				isActive = false; // Allway start with false.
				questEndsAt = 40;
				description = string.Format (GeneralLanguageSetup.setupLanguage.popBlueBubbles,questEndsAt);
				rewardSprite = Resources.Load<Sprite>("Sprites/Icons/coin icon");
				rewardValue = 400;
				if (PlayerPrefs.HasKey (name+"isDone")) {

					isDone = true;

				} else {

					isDone = false;
				}
			}

			void ChangeDescription(){// This is intent to modif the Language.

				description =  string.Format ( GeneralLanguageSetup.setupLanguage.popBlueBubbles, questEndsAt);
			}
		}

		public class Q14: Quests{

			public Q14(){ 

				name = "Q14";
				myGroup = Groups.ScorePoints;
				changeDescpDelegate = ChangeDescription;
				isActive = false; // Allway start with false.
				questEndsAt = 700;
				description = string.Format (GeneralLanguageSetup.setupLanguage.scorePoints,questEndsAt);
				rewardSprite = Resources.Load<Sprite>("Sprites/Icons/coin icon");
				rewardValue = 400;
				if (PlayerPrefs.HasKey (name+"isDone")) {

					isDone = true;

				} else {

					isDone = false;
				}
			}

			void ChangeDescription(){// This is intent to modif the Language.

				description =  string.Format ( GeneralLanguageSetup.setupLanguage.scorePoints, questEndsAt);
			}
		}

		public class Q15: Quests{

			public Q15(){ 

				name = "Q15";
				myGroup = Groups.GainExtraTime;
				changeDescpDelegate = ChangeDescription;
				isActive = false; // Allway start with false.
				questEndsAt = 90;
				description = string.Format (GeneralLanguageSetup.setupLanguage.extraSecs,questEndsAt);
				rewardSprite = Resources.Load<Sprite>("Sprites/Icons/coin icon");
				rewardValue = 400;
				if (PlayerPrefs.HasKey (name+"isDone")) {

					isDone = true;

				} else {

					isDone = false;
				}
			}

			void ChangeDescription(){// This is intent to modif the Language.

				description =  string.Format ( GeneralLanguageSetup.setupLanguage.extraSecs, questEndsAt);
			}
		}

		public class Q16: Quests{

			public Q16(){ 

				name = "Q16";
				myGroup = Groups.UseExplosion;
				changeDescpDelegate = ChangeDescription;
				isActive = false; // Allway start with false.
				questEndsAt = 4;
				description = string.Format (GeneralLanguageSetup.setupLanguage.useExplosion,questEndsAt);
				rewardSprite = Resources.Load<Sprite>("Sprites/Icons/explosion");
				rewardValue = 6;
				if (PlayerPrefs.HasKey (name+"isDone")) {

					isDone = true;

				} else {

					isDone = false;
				}
			}

			void ChangeDescription(){// This is intent to modif the Language.

				description =  string.Format ( GeneralLanguageSetup.setupLanguage.useExplosion, questEndsAt);
			}
		}

		public class Q17: Quests{

			public Q17(){ 

				name = "Q17";
				myGroup = Groups.ScorePoints;
				changeDescpDelegate = ChangeDescription;
				isActive = false; // Allway start with false.
				questEndsAt = 850;
				description = string.Format (GeneralLanguageSetup.setupLanguage.scorePoints,questEndsAt);
				rewardSprite = Resources.Load<Sprite>("Sprites/Icons/coin icon");
				rewardValue = 450;
				if (PlayerPrefs.HasKey (name+"isDone")) {

					isDone = true;

				} else {

					isDone = false;
				}
			}

			void ChangeDescription(){// This is intent to modif the Language.

				description =  string.Format ( GeneralLanguageSetup.setupLanguage.scorePoints, questEndsAt);
			}
		}

		public class Q18: Quests{

			public Q18(){ 

				name = "Q18";
				myGroup = Groups.PopBubbles;
				changeDescpDelegate = ChangeDescription;
				isActive = false; // Allway start with false.
				questEndsAt = 200;
				description = string.Format (GeneralLanguageSetup.setupLanguage.popBubbles,questEndsAt);
				rewardSprite = Resources.Load<Sprite>("Sprites/Icons/coin icon");
				rewardValue = 500;
				if (PlayerPrefs.HasKey (name+"isDone")) {

					isDone = true;

				} else {

					isDone = false;
				}
			}

			void ChangeDescription(){// This is intent to modif the Language.

				description =  string.Format ( GeneralLanguageSetup.setupLanguage.popBubbles, questEndsAt);
			}
		}

		public class E1: Quests{

			public E1(){ 

				name = "E1";
				myGroup = Groups.ComboRank;
				changeDescpDelegate = ChangeDescription;
				isActive = false; // Allway start with false.
				questEndsAt = 2;
				description = string.Format (GeneralLanguageSetup.setupLanguage.reachCombo,questEndsAt);
				rewardSprite = Resources.Load<Sprite>("Sprites/Icons/coin icon");
				rewardValue = 300;
				if (PlayerPrefs.HasKey (name+"isDone")) {

					isDone = true;

				} else {

					isDone = false;
				}
			}

			void ChangeDescription(){// This is intent to modif the Language.

				description =  string.Format ( GeneralLanguageSetup.setupLanguage.reachCombo, questEndsAt);
			}
		}

		public class E2: Quests{

			public E2(){ 

				name = "E2";
				myGroup = Groups.ScorePoints;
				changeDescpDelegate = ChangeDescription;
				isActive = false; // Allway start with false.
				questEndsAt = 350;
				description = string.Format (GeneralLanguageSetup.setupLanguage.scorePoints,questEndsAt);
				rewardSprite = Resources.Load<Sprite>("Sprites/Icons/explosion");
				rewardValue = 5;
				if (PlayerPrefs.HasKey (name+"isDone")) {

					isDone = true;

				} else {

					isDone = false;
				}
			}

			void ChangeDescription(){// This is intent to modif the Language.

				description =  string.Format ( GeneralLanguageSetup.setupLanguage.scorePoints, questEndsAt);
			}
		}

		public class E3: Quests{

			public E3(){ 

				name = "E3";
				myGroup = Groups.ComboRank;
				changeDescpDelegate = ChangeDescription;
				isActive = false; // Allway start with false.
				questEndsAt = 4;
				description = string.Format (GeneralLanguageSetup.setupLanguage.reachCombo,questEndsAt);
				rewardSprite = Resources.Load<Sprite>("Sprites/Icons/soapTime");
				rewardValue = 5;
				if (PlayerPrefs.HasKey (name+"isDone")) {

					isDone = true;

				} else {

					isDone = false;
				}
			}

			void ChangeDescription(){// This is intent to modif the Language.

				description =  string.Format ( GeneralLanguageSetup.setupLanguage.reachCombo, questEndsAt);
			}
		}

		public class End: Quests{


			public End(){ 

				name = "End";
				myGroup = Groups.EndQuest;
				changeDescpDelegate = ChangeDescription;
				isActive = false;
				description = GeneralLanguageSetup.setupLanguage.endQuest;
				rewardSprite = null;
				rewardValue = 0;
				questEndsAt = 0;
				isDone = false;
			}

			void ChangeDescription(){// This is intent to modif the Language.

				description =  GeneralLanguageSetup.setupLanguage.endQuest;
			}
		}
	}
		
}


