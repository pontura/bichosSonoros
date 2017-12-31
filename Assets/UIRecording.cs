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

	int currentSamples = 0;
	int currentSampleSelected = -1;
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
		Events.SetRecording (true);
		catalogButtons.turnButtonOn (currentSamples++);
		Invoke ("DoneRecording", 4);
	}

	public void DoneRecording()
	{
		if (!isRecording) // ya se acabo
			return;
		SetButton (false);
		Events.SetRecording (false);
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
}
