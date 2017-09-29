using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RobotManager : MonoBehaviour {

	public Transform container;
	public Robot robot_to_initialize;
	public CameraFollow cameraFollow;
	public List<Robot> robots;
	public int totalRobots = 2;

	void Start () {
		Events.OnAddRobot += OnAddRobot;
		Events.OnDestroyRobots += OnDestroyRobots;
		Events.OnCheckToDestroyRobot += OnCheckToDestroyRobot;
	}
	Robot newRobot;
	void OnAddRobot (AudioClip audioClip) {
		
		newRobot = Instantiate (robot_to_initialize);
		newRobot.transform.SetParent (container);
		newRobot.Init (audioClip);

		cameraFollow.Init (newRobot);
		Events.OnRobotAdded (newRobot);

		robots.Add (newRobot);
	}
	void OnCheckToDestroyRobot()
	{
		if(robots.Count>=totalRobots)
		{			
			Destroy (robots[0].gameObject);
			robots.RemoveAt (0);
		}
	}
	void OnDestroyRobots()
	{
		DestroyImmediate (newRobot.gameObject);
	}
}
