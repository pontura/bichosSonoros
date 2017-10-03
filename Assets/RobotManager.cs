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
		Events.OnDestroyRobot += OnDestroyRobot;
	}
	Robot newRobot;
	void OnAddRobot (AudioClip audioClip, int id, Vector3 values) {

		int bichoID = id;
		newRobot = Instantiate (robot_to_initialize);
		newRobot.transform.SetParent (container);

		Vector3 pos = Vector3.zero;
		if (robots.Count > 1) {
			foreach (Robot robot in robots)
				pos += robot.body.transform.position;
			pos /= robots.Count;
		}
		pos += new Vector3 (Random.Range (0, 10) - 5, 0, Random.Range (0, 10) - 5);

		newRobot.Init (audioClip, bichoID, values, pos);

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
	void OnDestroyRobot(Robot robot)
	{
		robots.Remove (robot);
		Destroy (robot.gameObject);
	}
	void OnDestroyRobots()
	{
		DestroyImmediate (newRobot.gameObject);
	}
}
