using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;

using Ankit.Engine;
using Ankit.IO;

public class DemoScript : MonoBehaviour {
	
	// This is the object where you will get your data
		public static PlayerPreference PlayerSelection = new PlayerPreference();
	
	
	
		public static bool isPlayerDataFound = false;
		private static string currentPlayerPreference = string.Empty;
		
		// Use this for initialization
		void OnGUI(){
		if(GUI.Button(new Rect(20,20,100,50),"Save Data")){
			SavePlayerPreference();
		}
		
		if(GUI.Button(new Rect(20,100,100,50),"Load Data")){
			LoadPlayerPreference();
		}
		
	}
		
		//Function to save data to xml File
		public static bool SavePlayerPreference () {
			bool retValue = false;
			try {
				//XML Serialize
				string playerSerializeInfo = string.Empty;
				playerSerializeInfo = GameEngine.Serialize (typeof(PlayerPreference), PlayerSelection, SerializationType.XML);
				retValue = GameEngine.SavePlayerData ("Test", playerSerializeInfo);
			Debug.Log("Successfully Saved");
			} catch (UnityException ex){
			Debug.Log(ex.Message);
				retValue = false;
			}
			return retValue;
		}
		// Function to Load data from xml file
		public static bool LoadPlayerPreference () {
			bool retValue = false;
			try {
				isPlayerDataFound = GameEngine.LoadPlayerData ("Test", ref currentPlayerPreference);
				if (isPlayerDataFound) {
					PlayerPreference tempPrefabs = new PlayerPreference ();
					tempPrefabs = (PlayerPreference)GameEngine.Deserialize (typeof(PlayerPreference), currentPlayerPreference, SerializationType.XML);
					if (tempPrefabs != null) {
						PlayerSelection = (PlayerPreference)tempPrefabs;
						currentPlayerPreference = string.Empty;
						retValue = true;
			Debug.Log("Successfully Loaded");
										}
				}
				retValue = true;
			} catch (UnityException ex){
			Debug.Log(ex.Message);
				retValue = false;
			}
			return  retValue;
		}

	

	[System.Serializable]
	public class ImageData {
		public string FileName = string.Empty;
		public string Answer = string.Empty;
		public int LevelNo = -1;
		
		public ImageData () {
			this.FileName = string.Empty;
			this.Answer = string.Empty;
		}
		
		public ImageData (string fileName, string answer) {
			this.FileName = fileName;
			this.Answer = answer;
			this.LevelNo = -1;
		}
		public ImageData (string fileName, string answer, int levelNo) {
			this.FileName = fileName;
			this.Answer = answer;
			this.LevelNo = levelNo;
		}
	}
}
