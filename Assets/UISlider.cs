using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UISlider : MonoBehaviour {

	public AudioFXManager.types type;
	public float initialValue;
	public float finalValue;
	public Robot robot;
	public Text field;
	bool isActive;

	void Start () {
		SetText ();
	}
	public void Clicked()
	{
		isActive = !isActive;
		Events.TurnSoundFX (type, isActive);
		SetText ();
	}
	void SetText()
	{
		if(isActive)
			field.text = "ON";
		else
			field.text = "Off";
	}

}
