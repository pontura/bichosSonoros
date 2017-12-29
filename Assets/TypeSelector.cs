using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TypeSelector : MonoBehaviour {
	public Button button;

	// Use this for initialization
	void Start () {
		for(int i=0; i<4;i++){
			Button b = Instantiate (button, this.transform);
			ColorBlock c = new ColorBlock ();
			c.normalColor = Data.Instance.config.GetBicho(i).colors[0];
			c.highlightedColor = Data.Instance.config.GetBicho(i).colors[1];
			c.pressedColor = Data.Instance.config.GetBicho (i).colors [1];
			c.colorMultiplier = 1.0f;
			b.colors = c;
			((Text) b.GetComponentInChildren<Text>()).text =  Data.Instance.config.GetBicho (i).name;
//			b.GetComponent<Text> ().text = Data.Instance.config.GetBicho (i).name;
		}			
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
