using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

public class MicRecorder : MonoBehaviour {

    public AudioClip clip;
    public Robot robot;

    bool isRecording = true;

    //temporary audio vector we write to every second while recording is enabled..
    List<float> tempRecording = new List<float>();

    //list of recorded clips...
    List<float[]> recordedClips = new List<float[]>();

    void Start()
    {
      //  SaveAudioClipToDisk(clip, "pontura");
      //  LoadAudioClipFromDisk(audioSource, "pontura");
        //set up recording to last a max of 1 seconds and loop over and over
		robot.audioSource.clip = Microphone.Start(null, true, 1, 44100);
		robot.audioSource.Play();
        //resize our temporary vector every second
        Invoke("ResizeRecording", 1);
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

    void Update()
    {
        //space key triggers recording to start...
        if (Input.GetKeyDown("space"))
        {
            isRecording = !isRecording;
            Debug.Log(isRecording == true ? "Is Recording" : "Off");

            if (isRecording == false)
            {
                //stop recording, get length, create a new array of samples
                int length = Microphone.GetPosition(null);

                Microphone.End(null);
                float[] clipData = new float[length];
				robot.audioSource.clip.GetData(clipData, 0);

                //create a larger vector that will have enough space to hold our temporary
                //recording, and the last section of the current recording
                float[] fullClip = new float[clipData.Length + tempRecording.Count];
                for (int i = 0; i < fullClip.Length; i++)
                {
                    //write data all recorded data to fullCLip vector
                    if (i < tempRecording.Count)
                        fullClip[i] = tempRecording[i];
                    else
                        fullClip[i] = clipData[i - tempRecording.Count];
                }

                recordedClips.Add(fullClip);
				robot.audioSource.clip = AudioClip.Create("recorded samples", fullClip.Length, 1, 44100, false);
				robot.audioSource.clip.SetData(fullClip, 0);
				robot.audioSource.loop = true;
				robot.audioSource.Play();
				robot.SetAudioClipLoaded ();
            }
            else
            {
                //stop audio playback and start new recording...
				robot.audioSource.Stop();
                tempRecording.Clear();
                Microphone.End(null);
				robot.audioSource.clip = Microphone.Start(null, true, 1, 44100);
                Invoke("ResizeRecording", 1);
            }

        }

        //use number keys to switch between recorded clips, start from 1!!
        for (int i = 0; i < 10; i++)
        {
            if (Input.GetKeyDown("" + i))
            {
                SwitchClips(i - 1);
            }
        }

    }

    //chooose which clip to play based on number key..
    void SwitchClips(int index)
    {
        if (index < recordedClips.Count)
        {
			robot.audioSource.Stop();
            int length = recordedClips[index].Length;
			robot.audioSource.clip = AudioClip.Create("recorded samples", length, 1, 44100, false);
			robot.audioSource.clip.SetData(recordedClips[index], 0);
			robot.audioSource.loop = true;
			robot.audioSource.Play();
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
}
