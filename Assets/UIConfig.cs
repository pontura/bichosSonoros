using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIConfig : MonoBehaviour {
	public InputField inputField;
	public GameObject panel;
	private UI context;

	void Start () {
		panel.SetActive (true);
		Events.OnSettingsLoaded += onSettingsLoaded;
		var se = new InputField.SubmitEvent ();
		se.AddListener (onSubmit);
		inputField.onEndEdit = se;
	}

	void onSettingsLoaded(){
		// el setting de la url del server lo saco del json
		inputField.text = Data.Instance.config.URL_SERVER;
	}

	public void onSubmit(string arg0){
//		PlayerPrefs.SetString ("URL_SERVER", inputField.text);
//		Debug.Log (inputField.text);

		context.ChangeState (UI.states.INTRO);
	}
	// Update is called once per frame
	void Update () {
		
	}

	public void Init(UI ctx) {
		panel.SetActive (true);
		this.context = ctx;	
	}
	public void SetOff () {
		panel.SetActive (false);
	}
}
