using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine;
using System.IO;

public class FileLoader : MonoBehaviour {

	public NetManager net;

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
		Debug.Log ("GetAllFiles");
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
		string[] soundFiles = data.Split ("|"[0]);
		foreach (string sound in soundFiles) {
			if (sound.Length > 1 && sound != "./.DS_Store") {
//				StartCoroutine(LoadItem(sound));
						net.GetRecording (sound);
				var url = Data.Instance.config.URL_SERVER + "move.php?file=" + sound;
							WWW www = new WWW (url);
			}
		}
	}


//	public IEnumerator LoadItem(string file)
//	{

//		net.GetRecording (file);



//		string finalPath;
//		WWW localFile;
//		Texture texture;
//		Sprite sprite;

//		finalPath = absoluteImagePath;
//		localFile = new WWW (finalPath);

//		yield return localFile;

//		print (imageName + " ___  " + localFile.url);

//		Events.OnNewFile(imageName);

//		if (Data.Instance.build != Data.builds.DEBUG) {
//			Debug.Log("delete " + imageName);
//			var url = URL + "delete.php?imageName=" + imageName;
//			WWW www = new WWW (url);
//		}
//
//	}
}
