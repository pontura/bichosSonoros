using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RobotPart : MonoBehaviour {

	private float speed = 20;
	public RobotPart parent;

	void Update () {
		if (parent == null)
			return;
		transform.LookAt (parent.transform);
		float dist = Vector3.Distance (parent.transform.position, transform.position);
		if (dist < 1.5f)
			return;
		transform.position = Vector3.MoveTowards (transform.position, parent.transform.position, 0.05f * (dist/2));
	}

}
