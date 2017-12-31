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
		c.disabledColor = Color.grey;

		c.colorMultiplier = 1.0f;

		for (int i = 0; i < 4; i++) {
			Button b = Instantiate (button, Vector3.zero, Quaternion.identity, parent.transform);
			b.colors = c;
			((Text)b.GetComponentInChildren<Text> ()).text = i.ToString();
			b.name = i.ToString ();
			b.onClick.AddListener (() => {
				ButtonClicked (b);
			});
			//https://issuetracker.unity3d.com/issues/ui-button-doesnt-change-to-disabled-color-when-it-gets-disabled-by-toggle
			b.interactable = false;
		}			

		Events.OnShowRobot += OnShowRobot;
	}

	public void turnButtonOn(int i)
	{
//		Debug.Log (parent);
//		Debug.Log (parent.transform.Find(i.ToString()));
		Button b = parent.transform.Find(i.ToString()).gameObject.GetComponent<Button>();
		b.interactable = true;
	}

	void ButtonClicked (Button i)
	{			
		Events.OnShowRobot (int.Parse(i.name));
	}

	void OnShowRobot(int i)
	{
		Button b = parent.transform.Find(i.ToString()).gameObject.GetComponent<Button>();
		b.Select ();
	}

	// Update is called once per frame
	void Update ()
	{
		
	}
}
