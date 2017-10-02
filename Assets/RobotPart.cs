using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RobotPart : MonoBehaviour {

	private float speed = 20;
	public RobotPart parent;
    public ParticleSystem particles;
	public Animation anim;
	float distance = 1.5f;
	public SpriteRenderer spriteRenderer;
	public TrailRenderer trail;

	public void Init(int bichoID)
	{
		print ("Bicho id:_ " + bichoID);
		anim = GetComponent<Animation> ();
		spriteRenderer.color = Data.Instance.config.GetBicho(bichoID).colors [0];
		trail.startColor = Data.Instance.config.GetBicho(bichoID).colors [0];
		trail.endColor = Data.Instance.config.GetBicho(bichoID).colors [0];
		trail.material.color =  Data.Instance.config.GetBicho(bichoID).colors [0];

		if (particles != null) {
			var main = particles.main;
			main.startColor = Data.Instance.config.GetBicho (bichoID).colors [0];
		}
	}
	void Update () {
		if (parent == null)
			return;
		transform.LookAt (parent.transform);
		float dist = Vector3.Distance (parent.transform.position, transform.position);
		if (dist < distance)
			return;
		transform.position = Vector3.MoveTowards (transform.position, parent.transform.position, 0.05f * (dist/2));
	}
    bool isOnPArticles;
	public void TurnOnParticles(float value)
    {
		distance = value / 2;
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
