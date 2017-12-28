﻿using UnityEngine;
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



	public bicho bicho1;
	public bicho bicho2;
	public bicho bicho3;
	public bicho bicho4;

	[Serializable]
	public class bicho
	{
		public Color[] colors;
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
//		StartCoroutine (LoadData ());
		LoadData();
	}

//	IEnumerator
	void LoadData()
	{
		string settingsUrl = "file://" + Application.dataPath + "/../" + "settings.json";
		WWW www = new WWW(settingsUrl);
//		yield return www;
		LoadSettings(www.text);
	}
		
	public void LoadSettings(string json_data)
	{
		var Json = SimpleJSON.JSON.Parse(json_data);
		fillArray(Json);
	}

	private void fillArray(JSONNode content)
	{		
		URL_SERVER = content["server"]["url"];
		Events.OnSettingsLoaded ();
	}
	public bicho GetBicho(int id)
	{
		switch (id) {
		case 1: return bicho1;
		case 2: return bicho2;
		case 3: return bicho3;
		default: return bicho4;
		}

	}
}
