using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIRecording : MonoBehaviour {
	
	public GameObject panel;

	public Text logText;
	public Text field;

	bool isRecording;

	void Start () {
		Events.Log += Log;
	} 
	public void Init() 
	{		
		SetButton (false);
		panel.SetActive (true);
	}
	public void SetOff()
	{
		panel.SetActive (false);
	}
	public void ToggleSaveButton () {		
		SetButton (!isRecording);
		Events.SetRecording (isRecording);
		if(!isRecording)
			GetComponent<UI> ().ChangeState (UI.states.EDITING);
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
			panel.SetActive (true);
			Events.Log ("Recording...");
		} else {
			field.text = "OFF";
			panel.SetActive (false);
			Events.Log ("");
		}

	}
}
