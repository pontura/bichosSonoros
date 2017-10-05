using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Robot : MonoBehaviour {

	public GameObject bicho1Head;
	public GameObject bicho2Head;
	public GameObject bicho3Head;
	public GameObject bicho4Head;

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

	public void Init(AudioClip audioClip, int id, Vector3 values, Vector3 pos) {

	
		bicho1Head.SetActive (false);
		bicho2Head.SetActive (false);
		bicho3Head.SetActive (false);
		bicho4Head.SetActive (false);

		Config.FXData[] fxData = null;

		switch (id) {
		case 1:
			bicho1Head.SetActive (true);
			fxData = Data.Instance.config.data1;
			break;
		case 2:
			bicho2Head.SetActive (true);
			fxData = Data.Instance.config.data2;
			break;
		case 3:
			bicho3Head.SetActive (true);
			fxData = Data.Instance.config.data3;
			break;
		case 4:
			bicho4Head.SetActive (true);
			fxData = Data.Instance.config.data4;
			break;
		}
		if (fxData != null) {
			int fxID = 0;
			foreach (Config.FXData data in fxData) {
				if (values [fxID] != 0)	
					audioFXManager.ChangeFXValue (data.type, values [fxID]);
				fxID++;
			}
		}

		this.id = id;
		audioSource.clip = audioClip;

		audioSource.Play();
		SetAudioClipLoaded();

		body.transform.position = pos;

		audioSource = GetComponent<AudioSource> ();
		robotParts = GetComponent<RobotParts> ();

		robotParts.Init (nodes, id, pos);

		SetAudioClipLoaded ();

		if (SceneManager.GetActiveScene ().name == "Tablets")
			audioSource.loop = true;
		else
			LoopAudio ();

		Invoke ("OnDestroy", Random.Range(120,300));
	}
	void OnDestroy()
	{
		Events.OnDestroyRobot (this);
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
