using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIEditing : MonoBehaviour {

	public GameObject panel;
	public TabletRobotManager robotManager;

	Slider[] sliders;

	void Start()
	{
		panel.SetActive (false);
		ColorBlock c = new ColorBlock ();
		c.normalColor = Data.Instance.config.GetCurrentBicho().colors [0];
		c.highlightedColor = Data.Instance.config.GetCurrentBicho().colors [1];
		c.pressedColor = Data.Instance.config.GetCurrentBicho().colors [1];
		c.disabledColor = Color.grey;

		c.colorMultiplier = 1.0f;

		sliders = GetComponents<Slider> ();
		foreach (Slider s in sliders) {
			s.colors = c;
		}

		Events.OnShowRobot += OnShowRobot;
	}

	void OnShowRobot(int robot)
	{
		panel.SetActive (true);
	}

	public void OnPitchChange(Slider pitch)
	{
//		Debug.Log (pitch.value);
//		Debug.Log (robotManager.getActiveRobot ());
//		Debug.Log (pitch);
		robotManager.getActiveRobot ().setPitch (pitch.value);

	}

	public void OnStartChange(Slider sampleStart)
	{
		robotManager.getActiveRobot ().setStart (sampleStart.value);
	}

	public void OnEndChange(Slider sampleEnd)
	{
		robotManager.getActiveRobot ().setEnd (sampleEnd.value);
	}

	public void Init() {
		panel.SetActive (true);
	}

	public void SetOff()
	{
		panel.SetActive (false);
	}
		
}

//honorino 173
