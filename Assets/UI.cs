using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI : MonoBehaviour {

	public AudioClip sampleAudioClip;
	public enum states
	{
		RECORDING,
		EDITING
	}
	public states state;
	UIRecording uiRecording;
	UIEditing uiEditing;

	bool isRecording;

	void Awake () {
		uiRecording = GetComponent<UIRecording> ();
		uiEditing = GetComponent<UIEditing> ();
		ChangeState (state);
		if (state == states.EDITING)
			Invoke ("Delayed", 0.1f);
	}
	void Delayed()
	{
		Events.OnAddRobot (sampleAudioClip);		
	}
	public void ChangeState(states state)
	{
		print ("SetOff (); " + state);

		uiEditing.SetOff ();
		uiRecording.SetOff ();

		switch (state) {
			case states.RECORDING:
				uiRecording.Init ();
				break;
			case states.EDITING:
				uiEditing.Init ();
				break;
		}
	}

}
