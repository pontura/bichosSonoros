using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIRecording : MonoBehaviour {
	
	public GameObject panel;
	public Waveform waveform;

	public MicRecorder micRecorder;
	bool isRecording;
	public GameObject button;
	private GameController context;
	private Text field;

	public void Awake()
	{
		

	}
	public void Init(GameController context) 
	{	
		panel.SetActive (true);	
		SetButton (false);
		waveform.Init ();
		button.SetActive (true);
		field = button.GetComponent<Text> ();
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
//		GetComponent<GameController> ().ChangeState (GameController.states.EDITING);
	}


	public void ToggleSaveButton () {		
//		return;
		SetButton (!isRecording);
		Events.SetRecording (isRecording);
//		if(!isRecording)
//			GetComponent<GameController> ().ChangeState (GameController.states.EDITING);
	}

	void SetButton(bool _isOn)
	{
		isRecording = _isOn;
		if (isRecording) {
			field.text = "GRABANDO...";
//			button.SetActive (false);
//			panel.SetActive (true);
//			Events.Log ("Recording...");
		} else {
			field.text = "PULSA PARA GRABAR";
//			panel.SetActive (false);
//			Events.Log ("");
		}

	}
}
