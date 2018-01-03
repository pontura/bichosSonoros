using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine;
using System.IO;

public class FileLoader : MonoBehaviour {

	// Use this for initialization
	void Start () {

	}

	void OnSettingsLoaded()
	{
		GetAllFiles ();
	}

	void Awake(){
		Events.OnSettingsLoaded += OnSettingsLoaded;
	}
	// Update is called once per frame
	void Update () {
		
	}


	void GetAllFiles()
	{
		var url = Data.Instance.config.URL_SERVER + "load.php";
		WWW www = new WWW(url);
		StartCoroutine(WaitForRequest(www));
	}

	IEnumerator WaitForRequest(WWW www)
	{
		yield return www;
		if (www.error == null)
		{
			ParseData (www.text);
		} else {
			Debug.Log("WWW Error: "+ www.error);
		}    
	}


	void ParseData(string data)
	{
		//Events.Log("Data Server Received");
		string[] soundFiles = data.Split ("|"[0]);

		foreach (string sound in soundFiles) {
			if (sound.Length > 1 && sound != "./.DS_Store") {
				Debug.Log (sound);
//				string file = (URL + "sounds/" + imageName);
//				StartCoroutine(LoadItem(file, imageName));
			}
		}
	}
}
