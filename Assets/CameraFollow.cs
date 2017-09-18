using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour {

	public int cameraHeight;
	public Robot followed;

	public void Init(Robot _followed)
	{
		this.followed = _followed;
	}
	public void StopFollowing()
	{
		this.followed = null;
	}
	void Update () {
		if (followed == null)
			return;
		Vector3 pos = followed.body.transform.position;
		pos.y = cameraHeight;
		transform.position = pos;
	}
}
