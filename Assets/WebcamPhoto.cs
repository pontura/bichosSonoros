using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.IO;

public class WebcamPhoto : MonoBehaviour
{
	public Texture2D photoTexture;
	WebCamTexture webCamTexture;
	private Animation anim;
	public Creator creator;
	MeshRenderer rawImage;

	private bool photoTaken;
	private bool ready;

	void Start()
	{
		anim = GetComponent<Animation> ();
		webCamTexture = new WebCamTexture(WebCamTexture.devices[WebCamTexture.devices.Length-1].name, 400, 300, 12);
		anim.Play ("Idle");
	}
	public void SetRawImage(MeshRenderer _rawImage)
	{
		this.rawImage = _rawImage;
		Vector3 scale = rawImage.transform.localScale;

		rawImage.transform.localScale = scale;
		rawImage.material.mainTexture = webCamTexture;
	
	}
	void CreatorReset()
	{
		anim.Play ("Idle");
		photoTexture = null;

		if (webCamTexture.isPlaying)
			webCamTexture.Stop();
		else
			webCamTexture.Play();

		Vector3 scale = rawImage.transform.localScale;

		rawImage.transform.localScale = scale;
		rawImage.material.mainTexture = webCamTexture;
	}
	void Ready()
	{
		ready = true;
	}
	void OnDestroy()
	{
		webCamTexture.Stop();
	}
	public void TakePhoto()
	{
		photoTaken = true;
		photoTexture = new Texture2D(webCamTexture.width, webCamTexture.height);
		photoTexture.SetPixels(webCamTexture.GetPixels());
		photoTexture.Apply();
		webCamTexture.Stop();
		anim.Play ("ZoomOut");
	}
}