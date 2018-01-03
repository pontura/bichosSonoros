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
		
		if (File.Exists(filename)) //Application.persistentDataPath + "/" + filename
		{
			//deserialize local binary file to AudioClipSample
			BinaryFormatter bf = new BinaryFormatter();
			FileStream file = File.Open(filename, FileMode.Open);
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
		string url;

		#if UNITY_ANDROID
		url = Path.Combine(Application.persistentDataPath, filename);
		#else
		url = Path.Combine(Application.dataPath, filename);
		#endif

		BinaryFormatter bf;
		byte[] bytes;


		//save files in android
		// https://forum.unity.com/threads/help-with-access-write-files-on-android.72819/

		Debug.Log("Save AudioClip To Disk " + filename);
		//create file
		bf = new BinaryFormatter();


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




	public void SendRecording(string filename)
	{
		string url;
		#if UNITY_ANDROID
		url = Path.Combine(Application.persistentDataPath, filename);
		#else
		url = Path.Combine(Application.dataPath, filename);
		#endif

		if (File.Exists (url)) {
			StartCoroutine(UploadFileCo(url, Data.Instance.config.URL_SERVER+ "upload.php"));
		};
	}


	IEnumerator UploadFileCo(string filename, string uploadURL)
	{
		WWW localFile = new WWW("file://"+ filename);  // le quite una / a file porque filename ya trae una
		yield return localFile;

		WWWForm postForm = new WWWForm();
		postForm.AddBinaryData("file", localFile.bytes, filename, "text/plain");

		WWW upload = new WWW(uploadURL, postForm);
		Debug.Log (localFile.url);
		Debug.Log (uploadURL);
		yield return upload;

		if (upload.error == null)
		{
			Debug.Log("no errors: " + upload.text);
		}
		else
		{
			Debug.Log("Error during upload: " + upload.error);
		}
	}

	public AudioClip GetRecording(string filename)
	{
		string url = Data.Instance.config.URL_SERVER+filename;
		Debug.Log (url);

		return null;

	}
}
