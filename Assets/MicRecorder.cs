using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System;
using UnityEngine.Assertions;

public class MicRecorder : MonoBehaviour {

	public AudioClip clip;
	public Robot robot;

	bool isRecording = false;

	List<float> tempRecording = new List<float>();
	List<float[]> recordedClips = new List<float[]>();

	public List<AudioClip> audiosRecorded;

	void Start()
	{
		Events.SetRecording += SetRecording;
	}

	void ResizeRecording()
	{
		if (isRecording)
		{
			//add the next second of recorded audio to temp vector
			int length = 44100;
			float[] clipData = new float[length];
			robot.audioSource.clip.GetData(clipData, 0);
			tempRecording.AddRange(clipData);
			Invoke("ResizeRecording", 1);
		}
	}
	void SetRecording(bool isRecording)
	{
		Debug.Log(isRecording == true ? "Is Recording" : "Off");

		if (isRecording == false)
		{
			int length = Microphone.GetPosition(null);

			Microphone.End(null);
			float[] clipData = new float[length];
			robot.audioSource.clip.GetData(clipData, 0);

			float[] fullClip = new float[clipData.Length + tempRecording.Count];
			for (int i = 0; i < fullClip.Length; i++)
			{
				if (i < tempRecording.Count)
					fullClip[i] = tempRecording[i];
				else
					fullClip[i] = clipData[i - tempRecording.Count];
			}

			recordedClips.Add(fullClip);
			AudioClip newAudioClip = AudioClip.Create("recorded samples", fullClip.Length, 1, 44100, false);
			newAudioClip.SetData(fullClip, 0);

			if (audiosRecorded.Count > 0) {
				newAudioClip = Combine (audiosRecorded [0], newAudioClip);
				audiosRecorded.RemoveAt (0);
			}

			audiosRecorded.Add (newAudioClip);

			robot.audioSource.clip = newAudioClip;
			robot.audioSource.loop = true;
			robot.audioSource.Play();
			robot.SetAudioClipLoaded ();

		}
		else
		{
			robot.audioSource.Stop();
			tempRecording.Clear();
			Microphone.End(null);
			robot.audioSource.clip = Microphone.Start(null, true, 10, 44100);
			//Invoke("ResizeRecording", 1);
		}
	}

	public static void LoadAudioClipFromDisk(AudioSource audioSource, string filename)
	{
		if (File.Exists(Application.persistentDataPath + "/" + filename))
		{
			//deserialize local binary file to AudioClipSample
			BinaryFormatter bf = new BinaryFormatter();
			FileStream file = File.Open(Application.dataPath + "/" + filename, FileMode.Open);
			AudioClipSample clipSample = (AudioClipSample)bf.Deserialize(file);
			file.Close();

			//create new AudioClip instance, and set the (name, samples, channels, frequency, [stream] play immediately without fully loaded)
			AudioClip newClip = AudioClip.Create(filename, clipSample.samples, clipSample.channels, clipSample.frequency, false);

			//set the acutal audio sample to the AudioClip (sample, offset)
			newClip.SetData(clipSample.sample, 0);

			//set to the AudioSource
			audioSource.clip = newClip;
			audioSource.Play();
		}
		else
		{
			Debug.Log("File Not Found!");
		}
	}
	public static void SaveAudioClipToDisk(AudioClip audioClip, string filename)
	{
		Debug.Log("SAVE " + filename);
		//create file
		BinaryFormatter bf = new BinaryFormatter();
		string url = Application.dataPath + "/" + filename;
		FileStream file = File.Create(url);

		//serialize by setting the sample, frequency, samples, and channels to the new AudioClipSample instance
		AudioClipSample newSample = new AudioClipSample();
		newSample.sample = new float[audioClip.samples * audioClip.channels];
		newSample.frequency = audioClip.frequency;
		newSample.samples = audioClip.samples;
		newSample.channels = audioClip.channels;

		//get the actual sample from the AudioClip
		audioClip.GetData(newSample.sample, 0);

		bf.Serialize(file, newSample);
		file.Close();

		Debug.Log("done " + url);
	}



	public AudioClip Combine(AudioClip clipA, AudioClip clipB)
	{
		float[] floatSamplesA = new float[clipA.samples*clipA.channels];
		clipA.GetData(floatSamplesA, 0);
		byte[] byteArrayA = floatToByte(floatSamplesA);

		float[] floatSamplesB = new float[clipB.samples*clipB.channels];
		clipB.GetData(floatSamplesB, 0);
		byte[] byteArrayB = floatToByte(floatSamplesB);

		float[] mixedFloatArray =  MixAndClampFloatBuffers(floatSamplesA, floatSamplesB);
		AudioClip result = AudioClip.Create("Combine", mixedFloatArray.Length, clipA.channels, clipA.frequency,
			false);
		result.SetData(mixedFloatArray, 0);
		return result;
	}
	private float ClampToValidRange(float value)
	{
		float min = -1.0f;
		float max = 1.0f;
		return (value < min) ? min : (value > max) ? max : value;
	}

	private float[] MixAndClampFloatBuffers(float[] bufferA, float[] bufferB)
	{
		int maxLength = Mathf.Min(bufferA.Length, bufferB.Length);
		float[] mixedFloatArray = new float[maxLength];

		for (int i = 0; i < maxLength; i++)
		{
			mixedFloatArray[i] = ClampToValidRange((bufferA[i] + bufferB[i])/2);
		}
		return mixedFloatArray;
	}

	private byte[] floatToByte(float[] floatArray)
	{
		byte[] byteArray = new byte[floatArray.Length*4];

		for (int i = 0; i < floatArray.Length; i++)
		{
			float currentFloat = floatArray[i];

			byte[] float2byte = BitConverter.GetBytes(currentFloat);
			Assert.IsTrue(float2byte.Length == 4);

			int offset = 4*i;
			byteArray[0 + offset] = float2byte[0];
			byteArray[1 + offset] = float2byte[1];
			byteArray[2 + offset] = float2byte[2];
			byteArray[3 + offset] = float2byte[3];
		}

		return byteArray;
	}
	private byte[] MixBuffers(byte[] bufferA, byte[] bufferB)
	{
		byte[] array = new byte[bufferA.Length];
		for (int i = 0; i < bufferA.Length; i++)
		{
			byte byteA = bufferA[i];
			byte byteB = bufferB[i];
			byte byteC = (byte)(((int)byteA + (int)byteB >> 1));
			array[i] = byteC;
		}
		return array;
	}
	private float[] byteToFloat(byte[] byteArray)
	{
		Assert.IsTrue(byteArray.Length % 4 == 0);
		float[] floatArray = new float[byteArray.Length/4];

		for (int i = 0; i < floatArray.Length; i++)
		{
			int offset = 4*i;
			byte[] byteArrayChunk = new byte[]
			{byteArray[0 + offset], byteArray[1 + offset], byteArray[2 + offset], byteArray[3 + offset]};
			floatArray[i] = BitConverter.ToSingle(byteArrayChunk,0);
		}

		return floatArray;
	}
}
