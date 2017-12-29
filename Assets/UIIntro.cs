using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIIntro : MonoBehaviour {

	public GameObject panel;
	private GameController context;

	void Start()
	{
		panel.SetActive (true);
		Events.OnBichoSelected += Clicked;

	}

	public void Init(GameController ctx) {
		
		panel.SetActive (true);
		this.context = ctx;	
	}

	public void StartDefault()
	{
		GetComponent<GameController> ().ChangeState (GameController.states.RECORDING);
	}

	public void Clicked(int bichoID)
	{
		Data.Instance.bichoID = bichoID;
		PlayerPrefs.SetInt ("bichoID", bichoID);
		context.ChangeState (GameController.states.RECORDING);

//		GetComponent<UI> ().ChangeState (UI.states.RECORDING);
//		startingButtonField.text = "TABLET " + Data.Instance.bichoID.ToString();

	}
	public void SetOff () {
		panel.SetActive (false);
	}
}
