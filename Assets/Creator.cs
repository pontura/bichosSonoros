using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using UnityEngine.UI;

public class Creator : MonoBehaviour {

	public InputField urlField;
	public string URL = "http://192.168.0.6/fiesta/";

	public int id;
	public int id2;
	public int totalStyles;
	public WebcamPhoto webcamPhoto;

	void Start()
	{
		string newURL = PlayerPrefs.GetString ("url", URL);
		urlField.text = newURL;
		URL = newURL;
		OnSettingsLoaded ();
	}
	public void SaveNewURL()
	{
		PlayerPrefs.SetString ("url", urlField.text);
	}
	void OnSettingsLoaded()
	{
	}

	public void CharacterSelected(int id)
	{
		Restart ();
	}
	void Restart()
	{
	}
	public void IntroDone()
	{
	}
	public void Create()
	{
		StartCoroutine (UploadPNG ());

	}

	IEnumerator UploadPNG()
	{
		int width = Screen.width;
		int height = Screen.height;
		Texture2D tex = webcamPhoto.photoTexture;

		// Encode texture into PNG
		byte[] bytes = tex.EncodeToPNG();
		Object.Destroy(tex);

		string file_Name = System.DateTime.Now.ToString("yyyyMMddhhmmss") + "_" + id + "_" + id2 + ".png";
		var fileName = Application.dataPath + "/" + file_Name;

		//File.WriteAllBytes(fileName, bytes);

		// Create a Web Form
		WWWForm form = new WWWForm();
		form.AddField("imageName", file_Name);
		form.AddBinaryData("fileToUpload", bytes);

		WWW w = new WWW(URL + "upload.php", form);
		yield return w;

		if (w.error != null)
		{
			Debug.Log(w.error);
		}
		else
		{
			Debug.Log("Finished Uploading Screenshot to " + URL);
			Done ();
		}
		yield return new WaitForSeconds(3);
	}
	void Done()
	{	
	}

	void DoneReady()
	{		
	}
}
