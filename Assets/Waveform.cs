using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using B83.MathHelpers;

[RequireComponent(typeof(AudioSource))]
public class Waveform : MonoBehaviour {

	public int totalLines;
	bool isRunning;
	public GameObject pointer;
	public int moveInX;
	public float secs;
	float initialPos;
	public AudioSource audioSource;



	// [ ... ]
	float[] spec = new float[1024];
	float[] tmp = new float[2048];
	Complex[] spec2 = new Complex[2048];

	void Start () {
		
		Events.SetRecording += SetRecording;


	}
	float timer;
	void SetRecording(bool _isRunning)
	{
		timer = 0;
		initialPos = pointer.GetComponent<RectTransform>().anchoredPosition.x;
		this.isRunning = _isRunning;
	}

	void Update()
	{
		if (!isRunning)
			return;
		timer += Time.deltaTime;

		Vector2 pos = pointer.GetComponent<RectTransform>().anchoredPosition;
		pos.x = initialPos + (timer * moveInX / secs);

		pointer.GetComponent<RectTransform>().anchoredPosition = pos;

	}


}




