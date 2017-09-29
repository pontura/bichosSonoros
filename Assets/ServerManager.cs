using System.Collections;
using System.IO;
using System.Collections.Generic;
using UnityEngine;

public class ServerManager : MonoBehaviour {

	string URL;
	public List<string> files;
	public List<string> itemsLoaded;

	void Awake()
	{
		Events.OnSettingsLoaded += OnSettingsLoaded;
	}
	void OnSettingsLoaded()
	{
		Events.Log ("settings loaded");
		URL = Data.Instance.config.url;		
		Loop ();
	}
	void Loop()
	{
		GetAllFiles ();
		Invoke ("Loop", 2);
	}
	void GetAllFiles()
	{
		var url = URL + "load.php";
		WWW www = new WWW(url);
		StartCoroutine(WaitForRequest(www));
	}

	IEnumerator WaitForRequest(WWW www)
	{
		yield return www;
		if (www.error == null)
		{
			ParseData ( www.data);
		} else {
			Debug.Log("WWW Error: "+ www.error);
		}    
	}
	void ParseData(string data)
	{
		//Events.Log("Data Server Received");
		string[] imageData = data.Split ("-"[0]);
		foreach (string imageName in imageData) {
			if (imageName.Length > 1) {
				string file = (URL + "sounds/" + imageName);
				StartCoroutine(LoadItem(file, imageName));
			}
		}
	}
	public IEnumerator LoadItem(string absoluteImagePath, string imageName)
	{
		Debug.Log (absoluteImagePath);
		//if (!fileWasLoaded (imageName)) {

			itemsLoaded.Add (imageName);

			string finalPath;
			WWW localFile;
			Texture texture;
			Sprite sprite;

			finalPath = absoluteImagePath;
			localFile = new WWW (finalPath);

			yield return localFile;

			print (imageName + " ___  " + localFile.url);

			Events.OnNewFile(imageName);

			if (Data.Instance.build != Data.builds.DEBUG) {
				Debug.Log("delete " + imageName);
				var url = URL + "delete.php?imageName=" + imageName;
				WWW www = new WWW (url);
			}
	//	} else {
		//	yield return null;
		//}
	}
	bool fileWasLoaded(string imageName)
	{
		foreach (string oldImageName in itemsLoaded)
			if (oldImageName == imageName)
				return true;
		return false;
	}

}
