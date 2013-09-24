using UnityEngine;
using System;
using System.IO;
using System.Xml.Serialization;
using System.Collections.Generic;
using Ankit.Engine;

// PlayerPreference is the class which will be serialized and stored.. Write and save your variables here
	[Serializable]
		public class PlayerPreference {
		//Credit Options
		public string Name;
		public string Age;
		public string Work;

		public void SetDefaults () {
			Name = "Ankit Jain";
			Age = "23";
			Work = "Game Development";
		}
		
		public PlayerPreference () {
			this.SetDefaults ();
		}	

	
}