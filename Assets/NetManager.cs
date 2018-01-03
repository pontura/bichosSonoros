using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using UnityEngine.Assertions;
using System;
public class NetManager : MonoBehaviour {

	// Use this for initialization
	void Start () {
//		SaveAudioClipToDisk (newAudioClip, "demo");
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void LoadAudioClipFromDisk(AudioSource audioSource, string filename)
	{
		
		if (File.Exists(Path.Combine(Application.dataPath, filename))) //Application.persistentDataPath + "/" + filename
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
			Debug.Log("File Exists...");
		}
		else
		{
			Debug.Log("File Not Found!");
		}
	}


	public static void SaveAudioClipToDisk(AudioClip audioClip, string filename)
	{
		BinaryFormatter bf;
		byte[] bytes;
		string url;

		Debug.Log("Save AudioClip To Disk " + filename);
		//create file
		bf = new BinaryFormatter();
//		url = Application.persistentDataPath + "/" + filename;
		url = Path.Combine(Application.dataPath, filename);
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

		using (MemoryStream stream = new MemoryStream())
		{
			bf.Serialize(file, newSample);
			bytes = stream.ToArray();
		}

		Debug.Log (file.Length);
		file.Close();
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

	public void SendRecording(string filename)
	{
		if (File.Exists (Path.Combine (Application.dataPath, filename))) {
			string file = Path.Combine (Application.dataPath, filename);
			UploadFile(file, Data.Instance.config.URL_SERVER+ "upload.php");
		};

//		string url = File.Exists (Path.Combine (Application.dataPath, filename));
//		NetManager.UploadFile(url, Data.Instance.config.URL_SERVER+ "upload.php");
	
	}

	void UploadFile(string localFileName, string uploadURL)
	{
		StartCoroutine(UploadFileCo(localFileName, uploadURL));
	}


//	void UploadFile(string uploadURL)
//	{
//		StartCoroutine(UploadFileCo(uploadURL));
//	}

	IEnumerator UploadFileCo(string filename, string uploadURL)
	{
		WWW localFile = new WWW("file:///"+filename);
		Debug.Log (localFile.url);
		yield return localFile;
		WWWForm postForm = new WWWForm();
		postForm.AddBinaryData("file", localFile.bytes, filename, "text/plain");
		Debug.Log (uploadURL);
		WWW upload = new WWW(uploadURL, postForm);
		yield return upload;
		if (upload.error == null)
		{
			Debug.Log(upload.text);
//			Debug.Log ("upload error null");
		}
		else
		{
			Debug.Log("Error during upload: " + upload.error);
		}
	}








	IEnumerator UploadFileCos(string localFileName, string uploadURL)
	{

		Events.Log ("Upload:" + localFileName);
		WWW localFile = new WWW("file:///" + localFileName);
		yield return localFile;
		if (localFile.error == null)
			Events.Log("Loaded file successfully");
		else
		{
			Events.Log("Open file error: "+localFile.error);
			yield break; // stop the coroutine here
		}
		WWWForm postForm = new WWWForm();
		// version 1
		//postForm.AddBinaryData("theFile",localFile.bytes);
		// version 2
		string newName = Data.Instance.bichoID + "x" + Data.Instance.config.value1 + "x" + Data.Instance.config.value2 + "x" + Data.Instance.config.value3;
		postForm.AddField("imageName", newName);
		postForm.AddBinaryData("fileToUpload",localFile.bytes,localFileName,"text/plain");

		WWW upload = new WWW(uploadURL,postForm);        
		yield return upload;
		if (upload.error == null)
			Events.Log("upload done :" + upload.text);
		else
			Events.Log("Error during upload: " + upload.error + " url: " + uploadURL);
	}
}
