using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIButton : MonoBehaviour {

	public Button button;
	public Text text;
	// Use this for initialization
public void Init () {
		Debug.Log ("UIButton: " + Data.Instance.bichoID);
		ColorBlock c = new ColorBlock ();
		c.normalColor = Data.Instance.config.GetBicho(Data.Instance.bichoID).colors[0];
		c.highlightedColor = Data.Instance.config.GetBicho(Data.Instance.bichoID).colors[1];
		c.pressedColor = Data.Instance.config.GetBicho (Data.Instance.bichoID).colors [1];
		c.colorMultiplier = 1.0f;
		button.colors = c;
		text.color = c.normalColor;
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
