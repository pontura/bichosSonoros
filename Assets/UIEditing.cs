using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIEditing : MonoBehaviour {

	public GameObject panel;

	public UISlider slider1;
	public UISlider slider2;
	public UISlider slider3;

	public CameraFollow camera;
	Robot robot;

	void Start()
	{
		panel.SetActive (false);
		Events.OnRobotAdded += OnRobotAdded;
	}
	void OnRobotAdded(Robot _robot)
	{
		this.robot = _robot; 
	}
	public void Init() {
		panel.SetActive (true);
		Config.FXData[] data;

		switch (Data.Instance.bichoID) {
		case 1:
			data = Data.Instance.config.data1;
			break;
		case 2:
			data = Data.Instance.config.data2;
			break;
		case 3:
			data = Data.Instance.config.data3;
			break;
		default:
			data = Data.Instance.config.data4;
			break;
		}
		slider1.type = data[0].type;
		slider1.defaultValue = data[0].defaultData;
		slider1.initialValue = data[0].initialData;
		slider1.finalValue = data[0].finalData;

		slider2.type = data[1].type;
		slider2.defaultValue = data[1].defaultData;
		slider2.initialValue = data[1].initialData;
		slider2.finalValue = data[1].finalData;

		slider3.type = data[2].type;
		slider3.defaultValue = data[2].defaultData;
		slider3.initialValue = data[2].initialData;
		slider3.finalValue = data[2].finalData;
	}
	public void SetOff()
	{
		panel.SetActive (false);
	}
	public void Ok()
	{
		
		if(slider1.isActive)
			Data.Instance.config.value1 = slider1.value;
		if(slider2.isActive)
			Data.Instance.config.value2 = slider2.value;
		if(slider3.isActive)
			Data.Instance.config.value3 = slider3.value;

		SetOff ();
		camera.Eject ();
		Invoke ("ResetScreen",7);
	}
	void ResetScreen()
	{
		GetComponent<GameController> ().ChangeState (GameController.states.SENDING);
		Events.OnDestroyRobots ();
	}
	public void Cancel()
	{
		GetComponent<GameController> ().ChangeState (GameController.states.RECORDING);
		Events.OnDestroyRobots ();
	}
	public void ChangeFXValue( AudioFXManager.types type, float value)
	{
		if (robot == null)
			return;
		robot.audioFXManager.ChangeFXValue (type, value);
	}

}

//honorino 173
