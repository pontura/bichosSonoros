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
		Events.OnAddRobot (defaultAudioClip);
		Events.OnNewFile += OnNewFile;
		Events.Log += Log;
	}
	void Start() {
		Events.OnAddRobot (defaultAudioClip);
	}
	void Log(string text)
	{
		field.text = text;
	}
	public void OnNewFile(string filename)
	{
		Events.Log("OnNewFile");
		string nerwFileURL = "C:/wamp/www/bichos/sounds/" + filename;
		LoadAudioClipFromDisk (nerwFileURL);
	}
	public void LoadAudioClipFromDisk(string filename)
	{
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
			Events.OnAddRobot (newClip);
		}
		else
		{
			Debug.Log("File Not Found!");
		}
	}
}
