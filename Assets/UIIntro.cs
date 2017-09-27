using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIIntro : MonoBehaviour {

	public GameObject panel;

	public void Init() {
		panel.SetActive (true);
	}
	public void Clicked()
	{
		GetComponent<UI> ().ChangeState (UI.states.RECORDING);
	}
	public void SetOff () {
		panel.SetActive (false);
	}
}
