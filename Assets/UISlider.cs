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
	public bool isActive;
	Slider slider;
	UIEditing uiEditing;
	public Image bgImage;
	public float value;

	void OnEnable () {
		isActive = true;
		uiEditing = GetComponentInParent<UIEditing> ();
		slider = GetComponent<Slider> ();
		slider.value = defaultValue;
		SetText ();
		slider.onValueChanged.AddListener(delegate {ValueChangeCheck(); });
	}
	void ValueChangeCheck()
	{
		value = Mathf.Lerp(initialValue, finalValue, slider.value);
//		uiEditing.ChangeFXValue(type, value);
	}
	public void Clicked()
	{
		isActive = !isActive;
		Events.TurnSoundFX (type, isActive);
		SetText ();

		if(isActive)
			ValueChangeCheck ();
	}
	void SetText()
	{
		if (isActive) {
			bgImage.enabled = true;
			field.text = "ON";
			field.color = Color.black;
			slider.interactable = true;
		} else {
			bgImage.enabled = false;
			field.text = "Off";
			field.color = Color.white;
			slider.interactable = false;
		}
	}

}
