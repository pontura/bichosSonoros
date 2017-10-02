using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Robot : MonoBehaviour {

	public AudioFXManager audioFXManager;
	public int id;
	public int audioSpectrumValue = 1;
	public AudioSpectrum audioSpectrum;
	public AudioSource audioSource;
	public GameObject body;
	public float transformSpeed = 0.5f;
	public float smoothTransform = 40f;
	public int nodes = 6;
	public RobotParts robotParts;
	bool audioExists;
	int nodeID = 0;

	public void Init(AudioClip audioClip, int id, Vector3 values) {

		this.id = id;
		audioSource.clip = audioClip;

		audioSource.Play();
		SetAudioClipLoaded();

		audioSource = GetComponent<AudioSource> ();
		robotParts = GetComponent<RobotParts> ();
		robotParts.Init (nodes, id);
		SetAudioClipLoaded ();
		if (SceneManager.GetActiveScene ().name == "Tablets")
			audioSource.loop = true;
		else
			LoopAudio ();
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
		int currentNodeID = (int)Mathf.Lerp(0,nodes, currentNormalizedTime);
		float newValue = audioSpectrumValue / smoothTransform;
		robotParts.TransformPart (currentNodeID, newValue);

	}
	void LoopAudio()
	{
		print ("LOOP");
		Invoke ("LoopAudio", Random.Range (6, 12));
		audioSource.Play ();
	}
}
