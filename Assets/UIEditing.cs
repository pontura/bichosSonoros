using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIEditing : MonoBehaviour {

	public GameObject panel;

	public GameObject bicho1;
	public GameObject bicho2;
	public GameObject bicho3;
	public GameObject bicho4;

	Robot robot;

	void Start()
	{
		Events.OnRobotAdded += OnRobotAdded;
		bicho1.SetActive (true);
		bicho2.SetActive (false);
		bicho3.SetActive (false);
		bicho4.SetActive (false);
	}
	public void BichoClicked(int id)
	{
		bicho1.SetActive (false);
		bicho2.SetActive (false);
		bicho3.SetActive (false);
		bicho4.SetActive (false);
		switch (id) {
		case 1:
			bicho1.SetActive (true);
			break;
		case 2:
			bicho2.SetActive (true);
			break;
		case 3:
			bicho3.SetActive (true);
			break;
		case 4:
			bicho4.SetActive (true);
			break;
		}
	}
	void OnRobotAdded(Robot _robot)
	{
		this.robot = _robot; 
	}
	public void Init() {
		panel.SetActive (true);
	}
	public void SetOff()
	{
		panel.SetActive (false);
	}
	public void Ok()
	{

	}
	public void Cancel()
	{
		GetComponent<UI> ().ChangeState (UI.states.RECORDING);
		Events.OnDestroyRobots ();
	}
	public void ChangeFXValue( AudioFXManager.types type, float value)
	{
		if (robot == null)
			return;
		switch(type)
		{
		case AudioFXManager.types.DISTORTION:
			robot.audioFXManager.distortion.enabled = true;
			robot.audioFXManager.distortion.distortionLevel = value;
			break;
		case AudioFXManager.types.PITCH:
			robot.audioSource.pitch = value;
			break;
		case AudioFXManager.types.LOWPAS:
			robot.audioFXManager.lowPass.enabled = true;
			robot.audioFXManager.lowPass.cutoffFrequency = value;
			break;
		case AudioFXManager.types.ECHO_DELAY:
			robot.audioFXManager.echo.enabled = true;
			robot.audioFXManager.echo.delay = value;
			break;
		case AudioFXManager.types.ECHO_RECAY_RATIO:
			robot.audioFXManager.echo.enabled = true;
			robot.audioFXManager.echo.decayRatio =  value;
			break;
		}
	}

}

//honorino 173
