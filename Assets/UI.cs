using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI : MonoBehaviour {

	public AudioClip sampleAudioClip;
	public enum states
	{
		INTRO,
		RECORDING,
		EDITING,
		SENDING
	}
	public states state;
	UIRecording uiRecording;
	UIEditing uiEditing;
	UIIntro uiIntro;
	UIEjecting uiSending;

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
		uiIntro = GetComponent<UIIntro> ();
		uiRecording = GetComponent<UIRecording> ();
		uiEditing = GetComponent<UIEditing> ();
		uiSending = GetComponent<UIEjecting> ();

		if (state == states.INTRO)
			ChangeState (states.INTRO);
		if (state == states.EDITING)
			Invoke ("Delayed", 0.1f);
	}
	void Delayed()
	{
		Events.OnAddRobot (sampleAudioClip);		
	}
	public void ChangeState(states state)
	{

		uiIntro.SetOff ();
		uiEditing.SetOff ();
		uiRecording.SetOff ();
		uiSending.SetOff ();

		switch (state) {
			case states.RECORDING:
				uiRecording.Init ();
				break;
			case states.EDITING:
				uiEditing.Init ();
				break;
			case states.INTRO:
				uiIntro.Init ();
				break;
			case states.SENDING:
				uiSending.Init ();
				break;
		}
	}

}
