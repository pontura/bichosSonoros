using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System;
using UnityEngine.Assertions;
using UnityEngine.UI;



using System.Text;
using System.Runtime.Serialization.Formatters.Binary;

using System;
using System.Runtime.Serialization;
using System.Reflection;

public class BigWorld : MonoBehaviour {

	public AudioClip defaultAudioClip;
	public List<AudioClip> clips;
	public Text field;

	void Awake () {
		Events.OnNewFile += OnNewFile;
		Events.Log += Log;
	}
	void Start() {
		Events.OnAddRobot (defaultAudioClip, 1, Vector3.zero);
	}
	void Log(string text)
	{
		field.text = text;
	}
	public void OnNewFile(string filename)
	{
		Events.Log("OnNewFile");
		LoadAudioClipFromDisk ("C:/wamp/www/bichos/sounds/", filename);
	}
	public void LoadAudioClipFromDisk(string url, string onlyname)
	{
		string filename = url + onlyname;
		Events.OnCheckToDestroyRobot ();
		Events.Log("cargo: " + clips.Count);
		if (File.Exists(filename))
		{
			BinaryFormatter bf = new BinaryFormatter();
			FileStream file = File.Open(filename, FileMode.Open);
			AudioClipSample clipSample = (AudioClipSample)bf.Deserialize(file);
			file.Close();
			AudioClip newClip = AudioClip.Create(filename, clipSample.samples, clipSample.channels, clipSample.frequency, false);
			newClip.SetData(clipSample.sample, 0);
			clips.Add(newClip);
			string[] data = onlyname.Split ("x"[0]);

			foreach (string a in data) {
				print ("::: " + a);
			}
			int bichoID = int.Parse(data [0]);
			float value1 = float.Parse(data [1]);
			float value2 = float.Parse(data [2]);
			float value3 = float.Parse(data [3]);
			Events.OnAddRobot (newClip, bichoID, new Vector3(value1,value2,value3));
		}
		else
		{
			Debug.Log("File Not Found!");
		}
	}
}
