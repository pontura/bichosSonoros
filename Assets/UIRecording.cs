using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIRecording : MonoBehaviour {
	
	public GameObject panel;

	public Text field;

	bool isRecording;
	public void Init() 
	{		
		SetButton (false);
		panel.SetActive (true);
	}
	public void SetOff()
	{
		panel.SetActive (false);
	}
	public void StartRecording()
	{
		SetButton (true);
		Events.SetRecording (true);
		Invoke ("DoneRecording", 4);
	}
	void DoneRecording()
	{
		SetButton (false);
		Events.SetRecording (false);
		GetComponent<UI> ().ChangeState (UI.states.EDITING);
	}
	public void ToggleSaveButton () {		
		return;
		SetButton (!isRecording);
		Events.SetRecording (isRecording);
		if(!isRecording)
			GetComponent<UI> ().ChangeState (UI.states.EDITING);
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
