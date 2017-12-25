using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIIntro : MonoBehaviour {

	public GameObject panel;

	public Text startingButtonField;
	private UI context;

	void Start()
	{
//		panel.SetActive (true);
		startingButtonField.text = "TABLET " + Data.Instance.bichoID.ToString();
	}

	public void Init(UI ctx) {
		panel.SetActive (true);
		this.context = ctx;	
	}

	public void StartDefault()
	{
		GetComponent<UI> ().ChangeState (UI.states.RECORDING);
	}
	public void Clicked(int bichoID)
	{
		Data.Instance.bichoID = bichoID;
		GetComponent<UI> ().ChangeState (UI.states.RECORDING);
		PlayerPrefs.SetInt ("bichoID", bichoID);
	}
	public void SetOff () {
		panel.SetActive (false);
	}
}
