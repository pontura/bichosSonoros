using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class World : MonoBehaviour {

	public Camera worldCamera;
	public GameObject background;
	int size =25;

	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		Vector3 pos = background.transform.position;

		float world_x = worldCamera.transform.position.x;
		float world_z = worldCamera.transform.position.z;
		float bg_x = background.transform.position.x;
		float bg_z = background.transform.position.z;

		if (
			(world_x > bg_x + size)
			||
			(world_x < bg_x - size)
		)
			pos.x = world_x;
		else if (
			(world_z > bg_z + size)
			||
			(world_z < bg_z - size)
		)
			pos.z = world_z;

		background.transform.position = pos;
	}
}
