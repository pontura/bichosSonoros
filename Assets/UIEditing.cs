using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIEditing : MonoBehaviour {

	public GameObject panel;
	public TabletRobotManager robotManager;

	void Start()
	{
		panel.SetActive (false);
		Events.OnShowRobot += OnShowRobot;
	}

	void OnShowRobot(int robot)
	{
//		this.robot = _robot;

		panel.SetActive (true);

	}

	public void OnPitchChange(Slider pitch)
	{
		Debug.Log (pitch.value);
		robotManager.getActiveRobot ().setPitch (pitch.value);
//		robot.setPitch (pitch);
	}
	public void OnStartChange(float pitch)
	{

	}
	public void OnEndChange(float pitch)
	{

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
