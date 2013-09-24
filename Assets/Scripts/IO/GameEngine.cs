using UnityEngine;
using System;
using System.Collections;
using System.IO;
using System.Xml.Serialization;
using System.Runtime.Serialization.Formatters.Binary;

//I have implemented basic Input/Output Functions here
namespace Ankit.Engine {
	public class GameEngine {

	public static string ErrorMessages = string.Empty;

		
		public static bool SavePlayerData(string playerID, string gameData) {
			bool retValue = false;
			retValue = IO.File.WriteFile(playerID + ".anky", gameData);		
			return retValue;
		}
		
		public static bool LoadPlayerData(string playerID, ref string gameData) {
			bool retValue = false;
			if(System.IO.File.Exists(IO.File.DataPath() + playerID + ".anky")) {
				gameData = IO.File.ReadFile(playerID + ".anky");
				retValue = true;
			} else {
				gameData = "File Not Found...";
				retValue = false;
			}
			return retValue;
		}
		
		public static string Serialize(Type objectType, object serializableObject, SerializationType typeOfSerialization) {
			string returnData = string.Empty;
			switch(typeOfSerialization) {
			case SerializationType.XML:
				XmlSerializer serializer = new XmlSerializer(objectType);
				TextWriter writer = new StringWriter();
				serializer.Serialize(writer, serializableObject);
				returnData = writer.ToString();
				serializer = null;
				writer.Close();
				writer = null;
				break;
			case SerializationType.Binnary:
				//Binary Format Deserialize using Memorystreem
				var formatter = new BinaryFormatter();
				var mf = new System.IO.MemoryStream();
				formatter.Serialize(mf, serializableObject);
				returnData = System.Convert.ToBase64String(mf.ToArray());
				mf.Close();
				formatter = null;
				mf = null;
				break;
			}
			return returnData;
		}
		
		public static object Deserialize(Type objectType, string serializedString, SerializationType typeOfSerialization) {
			object retValue = new object();
			switch(typeOfSerialization) {
			case SerializationType.XML:	
				XmlSerializer serializer = new XmlSerializer(objectType);
				TextReader reader = new StringReader(serializedString);
				retValue = serializer.Deserialize(reader);
				reader.Close();
				reader = null;
				serializer = null;
				break;
			case SerializationType.Binnary:
				//Binary Format Deserialize using Memorystreem
				var formatter = new BinaryFormatter();
				var mf = new System.IO.MemoryStream(System.Convert.FromBase64String(serializedString));
				retValue = formatter.Deserialize(mf);
				mf.Close();
				mf = null;
				formatter = null;
				break;
			}
			return retValue;
		}
		
	
		
	}
}