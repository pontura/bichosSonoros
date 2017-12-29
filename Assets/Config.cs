using UnityEngine;
using System.Collections;
using System;
using System.Text;
using System.Collections.Generic;
using System.IO;
using SimpleJSON;

public class Config : MonoBehaviour
{
	public string URL_SERVER = "";


	public Vector2 limits = new Vector2 (8, 30);


//	[HideInInspector]
	public Bicho A;
	public Bicho B;
	public Bicho C;
	public Bicho D;

//	public Bicho[] types = {A, B, C, D};

	[Serializable]
	public class Bicho
	{
		public Color[] colors;
		public string name;
	}

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

	public float value1;
	public float value2;
	public float value3;


	void Start()
	{
		StartCoroutine(LoadData());
	}


	IEnumerator LoadData()
	{
		string settingsUrl = "file://" + Application.dataPath + "/../" + "settings.json";
		WWW www = new WWW(settingsUrl);
		yield return www;
		LoadSettings(www.text);
	}
		
	public void LoadSettings(string json_data)
	{
		var Json = SimpleJSON.JSON.Parse(json_data);
		getDataFromJson(Json);
	}

	private void getDataFromJson(JSONNode content)
	{		
		URL_SERVER = content["server"]["url"];
		Debug.Log ("getDataFromJson" + content["server"]["url"]);
		Events.OnSettingsLoaded ();

	}

	public Bicho GetBicho(int id)
	{
		switch (id) {
			case 0: return A;
			case 1: return B;
			case 2: return C;
			case 3: return D;
		default:
			return A;
		}

	}
}
