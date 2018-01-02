using System.Collections;
using System.Collections.Generic;
using UnityEngine;





public class MicRecorder : MonoBehaviour {

	float MicLoudness;
	bool isRecording = false;
	public int value;
	AudioSource audioSource;

	AudioClip newAudioClip;

	void Start()
	{
		audioSource = GetComponent<AudioSource> ();
		Events.OnSetRecording += SetRecording;
//		Events.SendRecording += SendRecording;
	}

	void Update()
	{
	
	}

	void SetRecording(bool _isRecording)
	{
		this.isRecording = _isRecording;
	
		if (!isRecording)	
		{
		
			int length = Microphone.GetPosition(null);	// se acabó la grabación, cuanto duro?
			Microphone.End(null);						// apaga el mic

			float[] clipData = new float[length];
			audioSource.clip.GetData(clipData, 0); 		// me guardo lo que estaba en el audioSource

			newAudioClip = AudioClip.Create("recorded samples", clipData.Length, 1, 44100, false);
			newAudioClip.SetData(clipData, 0);



			Events.OnNewAudioClip (newAudioClip);

		}
		else
		{
			audioSource.Stop();
			Microphone.End(null);
			audioSource.clip = Microphone.Start(null, true, 10, 44100);
		}
	}








}
