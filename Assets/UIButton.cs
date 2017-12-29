using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIButton : MonoBehaviour {

	public Button b;
	// Use this for initialization
	void Start () {
		b.colors.normalColor = Data.Instance.config.GetBicho (Data.Instance.bichoID).colors[0];
		b.colors.highlightedColor = Data.Instance.config.GetBicho (Data.Instance.bichoID).colors[1];
		b.colors.colorMultiplier = 1.0f;
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
