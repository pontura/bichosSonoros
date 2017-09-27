using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BigWorld : MonoBehaviour {

	public AudioClip  defaultAudioClip;

	void Start () {
		Events.OnAddRobot (defaultAudioClip);
	}
}
