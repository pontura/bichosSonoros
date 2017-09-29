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
	public Image bgImage;

	void Start () {
		uiEditing = GetComponentInParent<UIEditing> ();
		slider = GetComponent<Slider> ();
		slider.value = defaultValue;
		SetText ();
		slider.onValueChanged.AddListener(delegate {ValueChangeCheck(); });
	}
	void ValueChangeCheck()
	{
		float newValue = Mathf.Lerp(initialValue, finalValue, slider.value);
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
		if (isActive) {
			bgImage.enabled = true;
			field.text = "ON";
			field.color = Color.black;
			slider.interactable = true;
		} else {
			bgImage.enabled = false;
			field.text = "Off";
			field.color = Color.red;
			slider.interactable = false;
		}
	}

}
