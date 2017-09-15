using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI : MonoBehaviour {
	
	public Text logText;
	public Text field;
	public GameObject masker;

	bool isRecording;

	void Start () {
		Events.Log += Log;
		SetButton (false);
		masker.SetActive (true);
	}
	public void ToggleSaveButton () {		
		SetButton (!isRecording);
		Events.SetRecording (isRecording);
	}
	void Log(string _text)
	{
		logText.text = _text;
	}
	void SetButton(bool _isOn)
	{
		isRecording = _isOn;
		if (isRecording) {
			field.text = "ON";
			masker.SetActive (true);
			Events.Log ("Recording...");
		} else {
			field.text = "OFF";
			masker.SetActive (false);
			Events.Log ("");
		}
		
	}
}
