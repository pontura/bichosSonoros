using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIRecording : MonoBehaviour {
	
	public GameObject panel;
	public Waveform waveform;
	public GameObject button;
	public Text label;

	bool isRecording;
	private GameController context;

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
		SetButton (true);
		Events.SetRecording (true);
		Invoke ("DoneRecording", 4);
	}
	public void DoneRecording()
	{
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
