using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour {

	public int cameraHeight;
	public Transform followed;

	void Update () {
		Vector3 pos = followed.transform.position;
		pos.y = cameraHeight;
		transform.position = pos;
	}
}
