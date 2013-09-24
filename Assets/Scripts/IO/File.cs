using UnityEngine;
using System.IO;
using System.Collections;
using System.Collections.Generic;
using Ankit.Engine;

namespace Ankit.IO {
	public class File {
		protected internal static string ApplicationPath() {
			string docsPath;
			switch(Application.platform) {
			case RuntimePlatform.OSXEditor:
			case RuntimePlatform.OSXPlayer:
				docsPath = Application.dataPath.TrimEnd('/') + "/";
				break;
			case RuntimePlatform.IPhonePlayer:
				docsPath = Application.dataPath.TrimEnd('/') + "/";
				docsPath = docsPath.Replace("/Data/", "/Documents/");
				break;
				
			case RuntimePlatform.WindowsEditor:
			case RuntimePlatform.WindowsPlayer:
			case RuntimePlatform.WindowsWebPlayer:
				docsPath = Application.dataPath.TrimEnd('\\') + @"\";
				break;
				
			default:
				docsPath = Application.dataPath.TrimEnd('/');
				break;
			}
			return docsPath;
		}

		protected internal static string DataPath() {
			string docsPath;
			switch(Application.platform) {
			case RuntimePlatform.OSXEditor:
			case RuntimePlatform.OSXPlayer:
				docsPath = Application.persistentDataPath.TrimEnd('/') + "/";
				break;
			case RuntimePlatform.IPhonePlayer:
				docsPath = Application.persistentDataPath.TrimEnd('/') + "/";
				docsPath = docsPath.Replace("/Data/", "/Documents/");
				break;
	
			case RuntimePlatform.WindowsEditor:
			case RuntimePlatform.WindowsPlayer:
			case RuntimePlatform.WindowsWebPlayer:
				docsPath = Application.persistentDataPath.TrimEnd('\\') + @"\";
				break;
				
			default:
				docsPath = Application.persistentDataPath.TrimEnd('/');
				break;
			}
			return docsPath;
		}
	
		public static string ReadResourceFile(string fileName) {
			TextAsset fileData = (TextAsset)Resources.Load(fileName, typeof(TextAsset));
			return fileData.text;
		}
		public static List<string> ReadRecordsFromResources(string fileName) {
			return ReadRecordsFromResources(fileName, '\n');
		}
		public static List<string> ReadRecordsFromResources(string fileName, char recordDilimator) {
			string tempData = ReadResourceFile(fileName);
			return ReadRecordsText(tempData, recordDilimator);
		}

		public static List<List<string>> GetDataFromResources(string fileName) {
			return GetDataFromResources(fileName, '\n', ',');
		}
		public static List<List<string>> GetDataFromResources(string fileName, char fieldDilimator) {
			return GetDataFromResources(fileName, '\n', fieldDilimator);
		}
		public static List<List<string>> GetDataFromResources(string fileName, char recordDilimator, char fieldDilimator) {
			string tempData = string.Empty;
			tempData = ReadResourceFile(fileName);
			tempData = tempData.TrimEnd(recordDilimator);

			return GetRecrordsFromText(tempData, recordDilimator, fieldDilimator);
		}
		public static bool WriteFile(string fileName, string data) {
			Debug.Log(DataPath());
			return WriteFile(DataPath(), fileName, data, FileMode.CreateOverwirte);
		}
		public static bool WriteFile(string fileName, string data, FileMode mode) {
			return WriteFile(DataPath(), fileName, data, mode);
		}
		public static bool WriteFile(string path, string fileName, string data) {
			return WriteFile(path, fileName, data, FileMode.CreateOverwirte);
		}
		public static bool WriteFile(string path, string fileName, string data, FileMode mode) {
			bool retValue = false;
			string dataPath = path;
			try {
				if(!Directory.Exists(dataPath)) {
					Directory.CreateDirectory(dataPath);
				}
				dataPath += fileName;
				switch(mode) {
				case FileMode.Open:
					retValue = false;
					break;
				case FileMode.CreateOverwirte:
					System.IO.File.WriteAllText(dataPath, data);
					break;
				case FileMode.Append:
					System.IO.File.AppendAllText(dataPath, data);
					break;
				}
				retValue = true;
			} catch (System.Exception ex) {
				GameEngine.ErrorMessages = "File Write Error\n" + ex.Message;
				retValue = false;
			}
			return retValue;
		}

		public static string ReadFile(string fileName) {
			return ReadFile(DataPath(), fileName);
		}
		public static string ReadFile(string path, string fileName) {
			FileInfo theSourceFile = null;
			StreamReader reader = null;
			string fileData = string.Empty;
			string dataPath = path + fileName;
			theSourceFile = new FileInfo(dataPath);
		
			if(theSourceFile != null && theSourceFile.Exists) {
				reader = theSourceFile.OpenText();
			}
		
			if(reader == null) {
				Debug.Log(dataPath + " was not found or not readable");
				fileData = null;			
			} else {
				fileData = reader.ReadToEnd();
			}
			return fileData;
		}
		
		public static List<string> ReadRecordsFromFile(string fileName) {
			return ReadRecordsFromFile(DataPath(), fileName, '\n');
		}
		public static List<string> ReadRecordsFromFile(string path, string fileName) {
			return ReadRecordsFromFile(path, fileName, '\n');
		}
		public static List<string> ReadRecordsFromFile(string fileName, char recordDilimator) {
			return ReadRecordsFromFile(DataPath(), fileName, recordDilimator);
		}
		public static List<string> ReadRecordsFromFile(string path, string fileName, char recordDilimator) {
			string tempData = ReadFile(path, fileName);
			return ReadRecordsText(tempData, recordDilimator);
		}
		public static List<List<string>> GetDataFromFile(string fileName) {
			return GetDataFromFile(DataPath(), fileName, '\n', ',');
		}
		public static List<List<string>> GetDataFromFile(string path, string fileName) {
			return GetDataFromFile(path, fileName, '\n', ',');
		}
		public static List<List<string>> GetDataFromFile(string fileName, char fieldDilimator) {
			return GetDataFromFile(DataPath(), fileName, '\n', fieldDilimator);
		}
		public static List<List<string>> GetDataFromFile(string path, string fileName, char fieldDilimator) {
			return GetDataFromFile(path, fileName, '\n', fieldDilimator);
		}
		public static List<List<string>> GetDataFromFile(string fileName, char recordDilimator, char fieldDilimator) {
			return GetDataFromFile(DataPath(), fileName, recordDilimator, fieldDilimator);
		}
		public static List<List<string>> GetDataFromFile(string path, string fileName, char recordDilimator, char fieldDilimator) {
			string tempData = string.Empty;
			tempData = ReadFile(path, fileName);
			tempData = tempData.TrimEnd(recordDilimator);
			
			return GetRecrordsFromText(tempData, recordDilimator, fieldDilimator);
		}

		private static List<string> ReadRecordsText(string dataText, char recordDilimator) {
			string[] tempRecord = new string[0];
			List<string> retData = new List<string>();
			dataText = dataText.TrimEnd(recordDilimator);
			tempRecord = dataText.Split(recordDilimator);
			foreach(string st in tempRecord) {
				if(st.Trim().StartsWith("#")) {
					continue;
				}
				retData.Add(st);
			}		
			return retData;
		}
		private static List<List<string>> GetRecrordsFromText(string dataText, char recordDilimator, char fieldDilimator) {
			List<List<string>> retData = new List<List<string>>();
			List<string> recordList = new List<string>();
			List<string> fieldList = new List<string>();
			recordList.AddRange(dataText.Replace("\"", "").Split(recordDilimator));
			foreach(string st in recordList) {
				if(st.Trim().StartsWith("#")) {
					continue;
				}
				fieldList = new List<string>();
				fieldList.AddRange(st.Split(fieldDilimator));			
				retData.Add(fieldList);
			}
			return retData;
		}

		public static List<string> SpesificRecordFromData(string data, char dataDilimator, string recordNos, char recordNoDilimator) {
			List<string> newData = new List<string>();
			List<string> newRecNo = new List<string>();
		
			newData.AddRange(data.Split(dataDilimator));
			newRecNo.AddRange(recordNos.Split(recordNoDilimator));
			return SpesificRecordFromData(newData, newRecNo);
		}
		public static List<string> SpesificRecordFromData(string data, string recordNos, char recordNoDilimator) {
			List<string> newData = new List<string>();
			List<string> newRecNo = new List<string>();
		
			newData.AddRange(data.Split('\n'));

			newRecNo.AddRange(recordNos.Split(recordNoDilimator));
			return SpesificRecordFromData(newData, newRecNo);
		}
		public static List<string> SpesificRecordFromData(string data, string recordNos) {
			List<string> newData = new List<string>();
			List<string> newRecNo = new List<string>();
			newRecNo.AddRange(recordNos.Split(','));
			newData.AddRange(data.Split('\n'));
			return SpesificRecordFromData(newData, newRecNo);
		}
		public static List<string> SpesificRecordFromData(List<string> data, List<string> recordNos) {
			List<string> retData = new List<string>();
			int i = 1;
		
			foreach(string d in data) {
				if(d.Trim().StartsWith("#")) {
					continue;
				}
				foreach(string rn in recordNos) {
					if(rn == i.ToString()) {
						retData.Add(d.Replace("\"", ""));
					}
				}
				i++;
			}
			return retData;
		}
	
	
	
	}
	
	public enum FileMode {
		CreateOverwirte=0,
		Append=1,
		Open=2
	}
}