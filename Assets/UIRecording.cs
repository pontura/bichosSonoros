using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIRecording : MonoBehaviour {
	private GameController context;

	public Catalog catalogButtons;
	public GameObject panel;
	public Waveform waveform;
	public GameObject button;
	public Text label;
	public TabletRobotManager robots;

	int currentSamples = 0;
	int maxSamples = 4;

	bool isRecording;

	public void Awake()
	{
	}

	public void Init(GameController context) 
	{	
		panel.SetActive (true);	
		button.GetComponent<UIButton> ().Init ();
		SetButton (false);
		waveform.Init ();	
		Events.OnNewAudioClip += OnNewAudioClip;
	}

	public void SetOff()
	{
		panel.SetActive (false);
	}

	public void StartRecording()
	{
		if (isRecording)
			return;
		if (currentSamples >= maxSamples)
			return; // no puedo grabar mas audios
		SetButton (true);
		Events.OnSetRecording (true);
		Invoke ("DoneRecording", 4);
	}

	public void DoneRecording()
	{
		if (!isRecording) // ya se acabo
			return;
		CancelInvoke ("DoneRecording");
		SetButton (false);
		Events.OnSetRecording (false);

	}
		
	void SetButton(bool _isOn)
	{
		isRecording = _isOn;
		if (isRecording) {
			label.text = "GRABANDO...";
		} else {
			label.text = "PULSA PARA GRABAR";
		}
	}

	void OnNewAudioClip(AudioClip ac)
	{	
		catalogButtons.turnButtonOn(currentSamples);

		RobotDefinition rd = new RobotDefinition ();
		rd.ac = ac;
		rd.type = Data.Instance.bichoID;
		rd.node = currentSamples;
		rd.values = Vector3.zero;
		rd.loop = false;

//		Events.OnCreateNewRobot(ac, Data.Instance.bichoID, currentSamples, Vector3.zero, false);
		Events.OnCreateNewRobot(rd);
		currentSamples++;
	}
}
