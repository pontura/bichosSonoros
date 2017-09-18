using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RobotManager : MonoBehaviour {

	public Transform container;
	public Robot robot_to_initialize;
	public CameraFollow cameraFollow;

	void Start () {
		Events.OnAddRobot += OnAddRobot;
	}

	void OnAddRobot (AudioClip audioClip) {
		
		Robot newRobot = Instantiate (robot_to_initialize);
		newRobot.transform.SetParent (container);
		newRobot.Init (audioClip);

		cameraFollow.Init (newRobot);
	}
}
