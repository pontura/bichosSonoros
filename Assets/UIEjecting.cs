using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIEjecting : MonoBehaviour {

	public GameObject panel;
	void Start()
	{
		panel.SetActive (false);
	}

	public void Init() {
		panel.SetActive (true);
		Invoke ("Reset",3);
		Events.SendRecording ();
	}
	public void Reset()
	{
		GetComponent<UI> ().ChangeState (UI.states.RECORDING);
		SetOff ();
	}
	public void SetOff()
	{
		panel.SetActive (false);
	}

}