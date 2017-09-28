using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIRecording : MonoBehaviour {
	
	public GameObject panel;
	public Waveform waveform;
	public Text field;
	public MicRecorder micRecorder;
	bool isRecording;
	public GameObject button;

	public void Init() 
	{		
		SetButton (false);
		panel.SetActive (true);
		waveform.Init ();
		button.SetActive (true);
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
			field.text = "GRABANDO...";
			button.SetActive (false);
			panel.SetActive (true);
			Events.Log ("Recording...");
		} else {
			field.text = "PULSA PARA GRABAR";
			panel.SetActive (false);
			Events.Log ("");
		}

	}
}
