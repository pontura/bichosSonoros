using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI : MonoBehaviour {

	public AudioClip sampleAudioClip;

	public states state;
	public enum states
	{
		CONFIG,
		INTRO,
		RECORDING,
		EDITING,
		SENDING
	}

	UIRecording uiRecording;
	UIEditing uiEditing;
	UIIntro uiIntro;
	UIEjecting uiSending;
	UIConfig uiConfig;

	bool isRecording;
	public Text LogText;

	void Start()
	{
		Events.Log += Log;
	}

	void Log(string _text)
	{
		LogText.text += ". " + _text;
	}

	void Awake () {
		uiConfig = GetComponent<UIConfig> ();

		uiIntro = GetComponent<UIIntro> ();
		uiRecording = GetComponent<UIRecording> ();
		uiEditing = GetComponent<UIEditing> ();
		uiSending = GetComponent<UIEjecting> ();

		ChangeState (states.CONFIG);

	}
	void Delayed()
	{
		Events.OnAddRobot (sampleAudioClip);		
	}


	public void ChangeState(states state)
	{
		setAllOff ();
		switch (state) {
			case states.CONFIG:				
			uiConfig.Init (this);
				
			break;
			case states.INTRO:
				uiIntro.Init (this);
			break;

//			case states.RECORDING:
//				uiEditing.SetOff ();
//				uiRecording.Init ();
//				break;
//			case states.EDITING:
//				uiEditing.Init ();
//				break;
//			case states.SENDING:
//				uiSending.Init ();
//				break;
		}
	}

	private void setAllOff(){
		uiConfig.SetOff ();
		uiIntro.SetOff ();
		uiRecording.SetOff ();
		uiEditing.SetOff ();
		uiSending.SetOff ();

	}
}
