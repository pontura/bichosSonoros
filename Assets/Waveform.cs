using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using B83.MathHelpers;

[RequireComponent(typeof(AudioSource))]
public class Waveform : MonoBehaviour {

	public GameObject panel;
	public int totalLines;
	bool isRunning;
	public GameObject pointer;
	public int moveInX;
	public float secs;
	float initialPos;
	public AudioSource audioSource;
	MicRecorder micRecorder;
	public float timer;
	// [ ... ]
	float[] spec = new float[1024];
	float[] tmp = new float[2048];
	Complex[] spec2 = new Complex[2048];

	void Start () {		
		micRecorder = GetComponent<MicRecorder> ();
		Events.SetRecording += SetRecording;
		panel.gameObject.SetActive (false);
	}

	void SetRecording(bool _isRunning)
	{
		this.isRunning = _isRunning;

		if (_isRunning) {
			pointer.GetComponent<TrailRenderer> ().enabled = true;
			panel.gameObject.SetActive (true);
			timer = 0;
		} else {
			panel.gameObject.SetActive (false);
			pointer.GetComponent<TrailRenderer> ().Clear ();
			pointer.GetComponent<TrailRenderer> ().enabled = false;
		}
	}
	public void Init()
	{
		print ("init" + timer + " _isRunning: " +isRunning);
		timer = 0;
		initialPos = -10;
		GetComponent<World> ().worldCamera.GetComponent<CameraFollow>().ResetPosition();
		panel.gameObject.SetActive (true);
		Vector2 pos =  pointer.transform.localPosition;
		pos.x = -10;
		pointer.transform.localPosition = pos;
	}
	void Update()
	{
		if (!isRunning)
			return;
		
		timer += Time.deltaTime;

		Vector3 pos =  pointer.transform.localPosition;
		pos.z = micRecorder.value;
		pos.x = initialPos + (timer * moveInX / secs);
		pointer.transform.localPosition = pos;

	}


}




