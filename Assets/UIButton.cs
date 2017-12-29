using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIButton : MonoBehaviour {

	public Button b;
	public Text t;
	// Use this for initialization
	void Start () {
		ColorBlock c = new ColorBlock ();
		c.normalColor = Data.Instance.config.GetBicho(Data.Instance.bichoID).colors[0];
		c.highlightedColor = Data.Instance.config.GetBicho(Data.Instance.bichoID).colors[1];
		c.pressedColor = Data.Instance.config.GetBicho (Data.Instance.bichoID).colors [1];
		c.colorMultiplier = 1.0f;
		b.colors = c;

		t.color = c.normalColor;
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
