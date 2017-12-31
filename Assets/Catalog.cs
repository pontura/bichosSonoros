using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Catalog : MonoBehaviour
{
	public GameObject parent;
	public Button button;
	// Use this for initialization
	void Start ()
	{
		ColorBlock c = new ColorBlock ();
		c.normalColor = Data.Instance.config.GetCurrentBicho().colors [0];
		c.highlightedColor = Data.Instance.config.GetCurrentBicho().colors [1];
		c.pressedColor = Data.Instance.config.GetCurrentBicho().colors [1];
		c.colorMultiplier = 1.0f;

		for (int i = 0; i < 4; i++) {
			Button b = Instantiate (button, Vector3.zero, Quaternion.identity, parent.transform);
			b.colors = c;
			((Text)b.GetComponentInChildren<Text> ()).text = i.ToString();
			b.name = i.ToString ();
			b.onClick.AddListener (() => {
				ButtonClicked (b);
			});
//			b.enabled = false;
		}			
	}

	void ButtonClicked (Button i)
	{
//		Debug.Log ("TypeSelector: " + i.name);
//		Data.Instance.bichoID = int.Parse (i.name);
//		Events.OnBichoSelected (Data.Instance.bichoID);
	}

	// Update is called once per frame
	void Update ()
	{
		
	}
}
