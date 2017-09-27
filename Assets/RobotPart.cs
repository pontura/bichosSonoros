using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RobotPart : MonoBehaviour {

	private float speed = 20;
	public RobotPart parent;
    public ParticleSystem particles;
	public Animation anim;

	void Start()
	{
		anim = GetComponent<Animation> ();
	}
	void Update () {
		if (parent == null)
			return;
		transform.LookAt (parent.transform);
		float dist = Vector3.Distance (parent.transform.position, transform.position);
		if (dist < 1.5f)
			return;
		transform.position = Vector3.MoveTowards (transform.position, parent.transform.position, 0.05f * (dist/2));
	}
    bool isOnPArticles;
	public void TurnOnParticles(float value)
    {
        if (particles == null) return;
		particles.startSize = value;
		if (isOnPArticles)
			return;
        particles.Play();
        isOnPArticles = true;
    }
    public void TurnOffParticles()
    {
        if (particles == null || !isOnPArticles) return;
        particles.Stop();
        isOnPArticles = false;
    }
}
