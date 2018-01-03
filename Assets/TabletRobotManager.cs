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
	int currentRobotSelected = -1;
	public NetManager net;

	void Start () {
		Events.OnSetRecording += OnSetRecording;
		Events.OnCreateNewRobot += OnCreateNewRobot;
		Events.OnDestroyRobots += OnDestroyRobots;
		Events.OnCheckToDestroyRobot += OnCheckToDestroyRobot;
		Events.OnDestroyRobot += OnDestroyRobot;
		Events.OnShowRobot += OnShowRobot;
	}

	void OnCreateNewRobot (AudioClip audioClip, int type, int nodeID, Vector3 values) {

		currentRobotSelected = nodeID;
		newRobot = Instantiate (robot_to_initialize);
		newRobot.transform.SetParent (container);

		Vector3 pos = Vector3.zero;

		newRobot.Init (audioClip, Data.Instance.bichoID, nodeID, values, pos);
		cameraFollow.Init (newRobot);
		robots.Add (newRobot);
		Events.OnRobotAdded (newRobot);
		Events.OnShowRobot (currentRobotSelected);
	}

	public void OnShowRobot(int nodeID)
	{
		currentRobotSelected = nodeID;
		foreach (Robot r in robots) {
			r.SetActive (false);
			if(r.nodeID == nodeID)r.SetActive (true);
		}
	}

	public Robot getActiveRobot()
	{
		
		Robot activeRobot = null;
		foreach (Robot r in robots) {
			activeRobot = (r.nodeID == currentRobotSelected) ? r : activeRobot; 
		}
		return activeRobot;
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

	string currentRecording = "";
	bool canSend = true;
	public void SendToServer()
	{		
		Robot r = getActiveRobot ();
		if (!canSend)
			return;
		canSend = false;
		currentRecording = SystemInfo.deviceUniqueIdentifier + "_" + r.type + "_" + r.nodeID;
		NetManager.SaveAudioClipToDisk (r.audioSource.clip, currentRecording);
		Invoke ("load", 3);
	}

	void load()
	{
		//		NetManager.LoadAudioClipFromDisk (audioSource, currentRecording);
		net.SendRecording(currentRecording);
		canSend = true;


	}
}
