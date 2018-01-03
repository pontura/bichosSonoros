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

	public Bicho A;
	public Bicho B;
	public Bicho C;
	public Bicho D;

	[Serializable]
	public class Bicho
	{
		public Color[] colors;
		public string name;
	}
	public float value1;
	public float value2;
	public float value3;

	public TextAsset json;

	void Start()
	{		
		StartCoroutine(LoadSettings());
	}


		
	IEnumerator LoadSettings()
	{
		var Json = JsonUtility.FromJson<settings> (json.ToString()); //SimpleJSON.JSON.Parse(json_data);
		URL_SERVER = Json.server;
		yield return json;
		Debug.Log (URL_SERVER);
		Events.OnSettingsLoaded ();
//		getDataFromJson(Json);
	}

	[Serializable]
	public class settings
	{
		public string server;	
	}

//	private void getDataFromJson(JSONNode content)
//	{		
//		URL_SERVER = content["server"]["url"];
//		Debug.Log ("getDataFromJson" + content["server"]["url"]);
//		Events.OnSettingsLoaded ();
//
//	}

	public Bicho GetCurrentBicho()
	{
		return GetBicho (Data.Instance.bichoID);
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
