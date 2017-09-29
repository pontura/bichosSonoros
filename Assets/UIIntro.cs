using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIIntro : MonoBehaviour {

	public GameObject panel;
	public InputField inputField;
	public Text startingButtonField;

	void Start()
	{
		panel.SetActive (true);
		inputField.text = Data.Instance.config.URL_SERVER;
		startingButtonField.text = "TABLET " + Data.Instance.bichoID.ToString();
	}
	public void Init() {
		panel.SetActive (true);
	}
	public void StartDefault()
	{
		GetComponent<UI> ().ChangeState (UI.states.RECORDING);
	}
	public void Clicked(int bichoID)
	{
		Data.Instance.bichoID = bichoID;
		GetComponent<UI> ().ChangeState (UI.states.RECORDING);
		PlayerPrefs.SetString ("URL_SERVER", inputField.text);
		PlayerPrefs.SetInt ("bichoID", bichoID);
	}
	public void SetOff () {
		panel.SetActive (false);
	}
}
