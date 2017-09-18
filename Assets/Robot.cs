using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Robot : MonoBehaviour {

	public AudioFXManager audioFXManager;

	public int audioSpectrumValue = 1;
	public AudioSpectrum audioSpectrum;
	public AudioSource audioSource;
	public GameObject body;
	public float transformSpeed = 0.5f;
	public float smoothTransform = 80f;
	public int nodes = 6;
	public RobotParts robotParts;
	bool audioExists;
	int nodeID = 0;

	public void Init(AudioClip audioClip) {

		audioSource.clip = audioClip;
		audioSource.loop = true;
		audioSource.Play();
		SetAudioClipLoaded ();

		audioSource = GetComponent<AudioSource> ();
		robotParts = GetComponent<RobotParts> ();
		robotParts.Init (nodes);
		SetAudioClipLoaded ();
	}
	void SetAudioClipLoaded() {
		audioExists = true;
		audioSpectrum.Init (this);
	}
	void Update()
	{
		if (!audioExists)
			return;
		
		float currentNormalizedTime = audioSource.time / audioSource.clip.length;
		int currentNodeID = (int)Mathf.Lerp(0,nodes, currentNormalizedTime);
		//nodeID++;
		//if (nodeID > nodes - 2)
		//	nodeID = 0;
		float newValue = audioSpectrumValue / smoothTransform;
		robotParts.TransformPart (currentNodeID, newValue);

	}

}
