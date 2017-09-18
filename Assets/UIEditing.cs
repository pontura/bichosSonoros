using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIEditing : MonoBehaviour {

	public GameObject panel;

	public void Init() {
		panel.SetActive (true);
	}
	public void SetOff()
	{
		panel.SetActive (false);
	}
	public void Ok()
	{

	}
	public void Cancel()
	{
		GetComponent<UI> ().ChangeState (UI.states.RECORDING);
	}

}
