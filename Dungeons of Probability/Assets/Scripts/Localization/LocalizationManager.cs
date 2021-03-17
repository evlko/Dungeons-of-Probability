using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class LocalizationManager : MonoBehaviour {
	public static LocalizationManager instance;
	Dictionary<string, string> localizedText;
	public bool isReady = false;
	//public string fileName;
	string dataAsJson;
	
	void Start () {
        if (PlayerPrefs.HasKey("Language") == false){
			SystemLanguage Lang = Application.systemLanguage;
            if (Lang == SystemLanguage.Russian)
            {
                PlayerPrefs.SetString("Language", "Russian");
            }
            else
            {
                PlayerPrefs.SetString("Language", "English");
            }
		}

		string fileName = PlayerPrefs.GetString("Language");

		if(instance == null){
			instance = this;
		}
		else if(instance != this){
			Destroy(gameObject);
		}
		DontDestroyOnLoad(gameObject);
		LoadLocalizedText(PlayerPrefs.GetString("Language")+".json");
	}
	
	public void LoadLocalizedText(string fileName){
		localizedText = new Dictionary<string, string>();
		StartCoroutine(loadStreamingAsset(fileName));
		//string filePath = (Application.streamingAssetsPath + "/" + fileName);
		/*
		if(dataAsJson != ""){
			//dataAsJson = File.ReadAllText(filePath);
			LocalizationData loadedData = JsonUtility.FromJson<LocalizationData>(dataAsJson);
			for (int i = 0; i < loadedData.items.Length; i++)
			{
				// Blue color:
				string value = loadedData.items[i].value.Replace(">", "/color>").Replace("<", "<color=#01579B>");
				value = value.Replace("/c", "</c");
				// Red color:
				value = value.Replace("]", "</color>").Replace("[", "<color=#B71C1C>");
				// Yellow color:
				value = value.Replace("}", "</color>").Replace("{", "<color=#F57F17>");
				localizedText.Add(loadedData.items[i].key, value);
			}
			Debug.Log("data loaded: " + localizedText.Count);
		}
		else{
			Debug.LogError("No Language!");
		}
		isReady = true;
		*/
	}
	public string GetLocalizedValue(string key){
		string result = "404";
		if(localizedText.ContainsKey(key)){
			result = localizedText[key];
		}
		return result;
	}
	public bool GetIsReady(){
		return isReady;
	}
	
	IEnumerator loadStreamingAsset(string fileName)
	{
		string filePath = System.IO.Path.Combine(Application.streamingAssetsPath, fileName);

		string result;
		if (filePath.Contains("://") || filePath.Contains(":///"))
		{
			WWW www = new WWW(filePath);
			yield return www;
			result = www.text;
		}
		else
			result = System.IO.File.ReadAllText(filePath);

		dataAsJson = result;
		if(dataAsJson != ""){
			LocalizationData loadedData = JsonUtility.FromJson<LocalizationData>(dataAsJson);
			for (int i = 0; i < loadedData.items.Length; i++)
			{
				// Blue color:
				string value = loadedData.items[i].value.Replace(">", "/color>").Replace("<", "<color=#01579B>");
				value = value.Replace("/c", "</c");
				// Red color:
				value = value.Replace("]", "</color>").Replace("[", "<color=#B71C1C>");
				// Yellow color:
				value = value.Replace("}", "</color>").Replace("{", "<color=#F57F17>");
				localizedText.Add(loadedData.items[i].key, value);
			}
		}
		isReady = true;
	}
}
