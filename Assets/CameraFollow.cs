using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour {

	float cameraHeight = 400;
	public Robot followed;
	bool ejecting;
	float zoomSpeed = 100;

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
		if (followed == null)
			return;
		if (ejecting)
			cameraHeight -= Time.deltaTime*zoomSpeed;
		Vector3 pos = followed.body.transform.position;
		pos.y = cameraHeight;
		transform.position = pos;
	}
}
