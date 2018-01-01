using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour {

	Camera cam;
	float cameraHeight = 400;
	public Robot followed;
	bool ejecting;
	float zoomSpeed = 100;
	public float newSize = 30;
//	public RobotManager robotManager;

	void Start()
	{
		cam = GetComponent<Camera> ();
		Events.OnCameraFollow += OnCameraFollow;
		ChangeZoom ();
	}
	void ChangeZoom()
	{
//		newSize = robotManager.robots.Count * 2;
		newSize += Random.Range (2, 38);
		Invoke ("ChangeZoom", Random.Range (5, 10));
	}
	void OnCameraFollow(Robot robot)
	{
		followed = robot;
	}
	public void Init(Robot _followed)
	{
		ejecting = false;
		ResetPosition ();
		this.followed = _followed;
	}
	public void Eject()
	{
		ejecting = true;
	}
	public void StopFollowing()
	{
		this.followed = null;
	}
	public void ResetPosition()
	{
		followed = null;
		cameraHeight = 400;
		transform.position = new Vector3 (0, cameraHeight, 0);
	}
	void Update () {

		cam.orthographicSize =  Mathf.Lerp(cam.orthographicSize, newSize, 0.01f);

		if (followed == null)
			return;
		if (ejecting)
			cameraHeight -= Time.deltaTime*zoomSpeed;
		
		Vector3 pos = followed.body.transform.position;
		pos.y = cameraHeight;
		transform.position = Vector3.Lerp(transform.position, pos, 0.01f);
	}
}
