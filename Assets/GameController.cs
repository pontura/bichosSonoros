using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameController : MonoBehaviour {

	public AudioClip sampleAudioClip;

	[SerializeField]
	public states state;
	public enum states
	{
		CONFIG,
		INTRO,
		RECORDING,
		EDITING,
		SENDING
	}

	public GameObject config;
	UIConfig uiConfig;
	public GameObject intro;
	UIIntro uiIntro;

	public GameObject recording;
	UIRecording uiRecording;



//	UIEjecting uiSending;

	bool isRecording;


	void Start()
	{
		ChangeState (state);
	}
		
	void Awake () {
		uiConfig = config.GetComponent<UIConfig> ();
		uiIntro = intro.GetComponent<UIIntro> ();
		uiRecording = recording.GetComponent<UIRecording> ();

//		uiSending = GetComponent<UIEjecting> ();
//		ChangeState (states.CONFIG);

	}
	void Delayed()
	{
//		Events.OnAddRobot (sampleAudioClip);		
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

			case states.RECORDING:
				uiRecording.Init (this);
				break;
	
//			case states.SENDING:
//				uiSending.Init ();
//				break;
		}
	}

	private void setAllOff(){
		uiConfig.SetOff ();
		uiIntro.SetOff ();
		uiRecording.SetOff ();
//		uiSending.SetOff ();

	}
}
