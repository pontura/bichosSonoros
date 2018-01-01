using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIEditing : MonoBehaviour {

	public GameObject panel;
	public Robot robot;

	void Start()
	{
		panel.SetActive (false);
		Events.OnShowRobot += OnShowRobot;
	}

	void OnShowRobot(int robot)
	{
//		this.robot = _robot;

	}

	public void OnPitchChange(float pitch)
	{
		robot.setPitch (pitch);
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
