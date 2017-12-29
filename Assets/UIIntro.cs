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
		Events.OnBichoSelected += onBichoSelected;

	}

	public void Init(GameController ctx) {		
		panel.SetActive (true);
		this.context = ctx;	
	}

	private void onBichoSelected(int bichoId)
	{
		Debug.Log ("UIintro: " + bichoId);
		context.ChangeState (GameController.states.RECORDING);
	}

	public void SetOff () {
		panel.SetActive (false);
	}
}
