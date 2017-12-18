using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ImageUpdateColor : MonoBehaviour {

	void Start () {
		int bichoID = Data.Instance.bichoID;
		Color color = Data.Instance.config.GetBicho (bichoID).colors [0];
		Color disabledcolor = color;
		disabledcolor.a = 0.3f;

		Image img = GetComponent<Image> ();
		if (img != null)
			img.color = color;


		ColorBlock c;
		Button btn = GetComponent<Button> ();
		if (btn != null) {
			c = btn.colors;
			c.normalColor = color;
			c.highlightedColor = color;
			c.pressedColor = color;
			c.disabledColor = disabledcolor;
			btn.colors = c;
		}

		Slider s = GetComponent<Slider> ();
		if (s != null) {
			c = s.colors;
			c.normalColor = color;
			c.highlightedColor = color;
			c.pressedColor = color;
			c.disabledColor = disabledcolor;
			s.colors = c;
		}

		Text t = GetComponent<Text> ();
		if (t!= null)
			t.color = color;
		
	}
}
