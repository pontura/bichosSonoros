using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIEditing : MonoBehaviour {

	public GameObject panel;
	public TabletRobotManager robotManager;

	public Slider slider1;
	public Slider slider2;
	public Slider slider3;
	public Toggle loop;
	public Button send;


//	Slider[] sliders;

	void Start()
	{
		panel.SetActive (false);


		Events.OnShowRobot += OnShowRobot;
	}

	void OnShowRobot(int robot)
	{
		panel.SetActive (true);
		// RECALL
		setColors();

		recall ();

	}

	private void recall()
	{
		slider1.value = robotManager.getActiveRobot ().audioValues[0];
		slider2.value = robotManager.getActiveRobot ().audioValues[1];
		slider3.value = robotManager.getActiveRobot ().audioValues[2];
		loop.isOn = robotManager.getActiveRobot ().looped;
	}

	private void setColors()
	{

		ColorBlock c = new ColorBlock ();
		c.normalColor = Data.Instance.config.GetCurrentBicho ().colors [0];
		c.highlightedColor = Data.Instance.config.GetCurrentBicho ().colors [1];
		c.pressedColor = Data.Instance.config.GetCurrentBicho ().colors [1];
		c.disabledColor = Color.grey;

		c.colorMultiplier = 1.0f;

		setSliderColors (slider1, c);
		setSliderColors (slider2, c);
		setSliderColors (slider3, c);
		loop.colors = c;
		send.colors = c;

		Debug.Log ("setColors");
	}

	private void setSliderColors(Slider s, ColorBlock c)
	{
		s.GetComponentInChildren<Image>().color = c.normalColor;
		s.fillRect.GetComponentInChildren<Image>().color = c.highlightedColor;
		s.targetGraphic.color = c.highlightedColor;

	}

	public void OnPitchChange(Slider pitch)
	{
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

	public void OnLoop(Toggle loop)
	{		
		robotManager.getActiveRobot ().setLoop (loop.isOn);
	}

	public void Init() {
		panel.SetActive (true);
	}

	public void SetOff()
	{
		panel.SetActive (false);
	}
		
	public void SendRobot(Button b)
	{
		robotManager.SendToServer ();
	}
}

//honorino 173
