using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIConfig : MonoBehaviour {
	// en el patron State, el estado guarda una refe al contexto (el que maneja los estados)
	// https://sourcemaking.com/design_patterns/state
	private GameController context;

	public GameObject panel;

	public InputField inputField;

	void Start () {
		panel.SetActive (true);
		Events.OnSettingsLoaded += onSettingsLoaded;

		var se = new InputField.SubmitEvent ();
		se.AddListener (onSubmit);
		inputField.onEndEdit = se;
	}

	void onSettingsLoaded(){
		// el setting de la url del server lo saco del json
		// entonces si actualizo la app con testFairy me actualiza el setting también (no tengo que configurar 1 por 1)
		inputField.text = Data.Instance.config.URL_SERVER;
	}


	public void onSubmit(string arg0){
		context.ChangeState (GameController.states.INTRO);
	}

	// Update is called once per frame
	void Update () {
		
	}

	public void Init(GameController ctx) {
		panel.SetActive (true);
		this.context = ctx;	
	}
	public void SetOff () {
		panel.SetActive (false);
	}
}
