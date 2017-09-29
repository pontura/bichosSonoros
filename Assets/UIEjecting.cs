using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIEjecting : MonoBehaviour {

	public GameObject panel;

	public void Init() {
		panel.SetActive (true);
		Invoke ("Reset",4);
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