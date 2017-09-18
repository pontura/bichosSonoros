using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UISlider : MonoBehaviour {

	public AudioFXManager.types type;
	public float defaultValue;
	public float initialValue;
	public float finalValue;
	public Text field;
	bool isActive;
	Slider slider;
	UIEditing uiEditing;

	void Start () {
		uiEditing = GetComponentInParent<UIEditing> ();
		slider = GetComponent<Slider> ();
		slider.value = defaultValue;
		SetText ();
	}
	float lastValue = 0;
	void Update()
	{
		float newValue = Mathf.Lerp(initialValue, finalValue, slider.value);
		if (lastValue == newValue)
			return;
		lastValue = newValue;

		uiEditing.ChangeFXValue(type, newValue);
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
