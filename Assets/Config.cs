using UnityEngine;
using System.Collections;
using System;
using System.Text;
using System.Collections.Generic;
using System.IO;
using SimpleJSON;

public class Config : MonoBehaviour
{
	public string URL_SERVER = "http://192.168.0.5/bichos/";
	public string url;
	public Vector2 limits = new Vector2 (8, 30);

	public FXData[] data1;
	public FXData[] data2;
	public FXData[] data3;
	public FXData[] data4;

	[Serializable]
	public class FXData
	{
		public AudioFXManager.types type;
		public float defaultData;
		public float initialData;
		public float finalData;
		public float value;
	}

	void Start()
	{
		StartCoroutine (LoadData ());
	}
	IEnumerator LoadData()
	{
		string directory = "file://" + Application.dataPath + "/../" + "settings.json";
		WWW www = new WWW(directory);
		yield return www;
		LoadDataromServer( www.text);
	}
	public void LoadDataromServer(string json_data)
	{
		var Json = SimpleJSON.JSON.Parse(json_data);
		fillArray(Json);
	}
	private void fillArray(JSONNode content)
	{
		url = content[0]["url"];
		Events.Log (url);
		Events.OnSettingsLoaded ();
	}
}
