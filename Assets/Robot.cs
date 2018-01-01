﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Robot : MonoBehaviour {

	public GameObject Head;
	public RobotParts robotParts;
	public int type;
	public int nodeID = 0;

	public AudioSource audioSource;


	public int audioSpectrumValue = 1;
	public AudioSpectrum audioSpectrum;

	public GameObject body;
	public float transformSpeed = 0.5f;
	public float smoothTransform = 40f;
	public int nodes = 6;
	bool audioExists;

	private Vector3 audioValues; 



	public void Init(AudioClip audioClip, int type, int nodeID, Vector3 values, Vector3 pos) {

		audioSource.clip = audioClip;
		this.type = type;
		this.nodeID = nodeID;
		this.audioValues = values;
		body.transform.position = pos;


		Head.SetActive (true);
	



//		audioSource.Play();
//		SetAudioClipLoaded();


		robotParts = GetComponent<RobotParts> ();
		robotParts.Init (nodes, type, pos);

		SetAudioClipLoaded ();

//		if (SceneManager.GetActiveScene ().name == "Tablets")
//			audioSource.loop = true;
//		else
//			LoopAudio ();

//		Invoke ("CheckOnDestroy", Random.Range(120,300));
	}

	public void setPitch(float value)
	{
		audioValues[0] = value;
		Debug.Log ("pitch: " + value);
	}
	public void setStart(float value)
	{
		audioValues[1] = value;
	}

	public void setEnd(float value)
	{
		audioValues[2] = value;
	}

	void CheckOnDestroy()
	{
		if (World.Instance.GetComponent<RobotManager> ().robots.Count > 1) {
			Events.OnDestroyRobot (this);
		} else {
			Invoke ("CheckOnDestroy", 10);
		}
	}
	void SetAudioClipLoaded() {
		audioExists = true;
		audioSpectrum.Init (this);
	}

	void LateUpdate()
	{
		if (!audioExists)
			return;
		
		float currentNormalizedTime = audioSource.time / audioSource.clip.length;
		int currentNodeID = (int)Mathf.Lerp(0,nodes-1, currentNormalizedTime);
		float newValue = audioSpectrumValue / smoothTransform;
		robotParts.TransformPart (currentNodeID, newValue);

	}
	void LoopAudio()
	{
		Invoke ("LoopAudio", Random.Range (8, 20));
		audioSource.Play ();
		Events.OnCameraFollow (this);
	}
}
