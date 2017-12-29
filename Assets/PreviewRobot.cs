using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PreviewRobot : MonoBehaviour {

	public Transform container;
	public Robot robot_to_initialize;
	public CameraFollow cameraFollow;
	public AudioClip audioClip;
	public Vector3 values;

	void Start () {
		Events.OnAddPreviewRobot += OnAddPreviewRobot;
	}
	Robot newRobot;
	void OnAddPreviewRobot (int id) {

		Utils.RemoveAllChildsIn (container);

		int bichoID = id;
		newRobot = Instantiate (robot_to_initialize);
		newRobot.transform.SetParent (container);

		Vector3 pos = Vector3.zero;

		newRobot.Init (audioClip, bichoID, values, pos);

		cameraFollow.Init (newRobot);
		Events.OnRobotAdded (newRobot);

	}
	void OnDestroyRobots()
	{
		DestroyImmediate (newRobot.gameObject);
	}
}
