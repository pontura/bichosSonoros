using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TabletRobotManager : MonoBehaviour {

	public Transform container;
	public Robot robot_to_initialize;
	public CameraFollow cameraFollow;
	public List<Robot> robots;
	public int totalRobots = 2;
	Robot newRobot;

	void Start () {
		Events.OnSetRecording += OnSetRecording;
		Events.OnCreateNewRobot += OnCreateNewRobot;
		Events.OnDestroyRobots += OnDestroyRobots;
		Events.OnCheckToDestroyRobot += OnCheckToDestroyRobot;
		Events.OnDestroyRobot += OnDestroyRobot;
		Events.OnShowRobot += OnShowRobot;
	}

	void OnCreateNewRobot (AudioClip audioClip, int type, int nodeID, Vector3 values) {

		newRobot = Instantiate (robot_to_initialize);
		newRobot.transform.SetParent (container);

		Vector3 pos = Vector3.zero;
//		if (robots.Count > 0) {
//			foreach (Robot robot in robots)
//				pos += robot.body.transform.position;
//			pos /= robots.Count;
//		}
//		pos += new Vector3 (Random.Range (0, 10) - 5, 0, Random.Range (0, 10) - 5);
//
		newRobot.Init (audioClip, Data.Instance.bichoID, nodeID, values, pos);

		cameraFollow.Init (newRobot);
		robots.Add (newRobot);
		Events.OnRobotAdded (newRobot);
	}

	public void OnShowRobot(int nodeID)
	{
		foreach (Robot r in robots) {
			r.gameObject.SetActive (false);
			if(r.nodeID == nodeID)r.gameObject.SetActive (true);
		}
	}

	void OnSetRecording (bool recording)
	{
		if(recording) StopPlayback ();
	}

	void StopPlayback()
	{
		foreach (Robot r in robots) {
			r.gameObject.SetActive (false);
		}
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
