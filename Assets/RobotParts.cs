using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RobotParts : MonoBehaviour {

	public RobotPart robotPart;
	public Transform container;
	private float separation = 0.7f;
	public RobotPart initialPart;
	public List<RobotPart> parts;

	public void Init(int nodes, int id, Vector3 pos) {
		RobotPart nextParentRobotPart = initialPart;
		for(int a=0; a<nodes-1; a++)
		{
			RobotPart newRobotPart = Instantiate (robotPart);	
			newRobotPart.transform.SetParent (container);
			newRobotPart.transform.localPosition = pos + new Vector3(0,0,a*separation*-1);
			newRobotPart.parent = nextParentRobotPart;
			nextParentRobotPart = newRobotPart;
			newRobotPart.Init (id);
			parts.Add (newRobotPart);
		}
		initialPart.Init (id);
	}
	RobotPart lastTransformedRobotPart;
	RobotPart lastTransRobotPart;
	public void TransformPart(int id, float value)
	{
		if (parts == null || parts.Count <= id) {
			Events.Log ("ERROR: " + id + " value: " + value);
			return;
		}
		RobotPart robotPart = parts [id];

		if(robotPart.anim != null)
			robotPart.anim ["pingpong"].speed = value / 2;
		
		robotPart.TurnOnParticles(value);

        if (value < 1)
			value = 1;
		Vector3 newScale = new Vector3(value, value, value);
		robotPart.transform.localScale = Vector3.Lerp(robotPart.transform.localScale,newScale, 0.5f );
		if (lastTransRobotPart != robotPart)
			ResetTransform (lastTransRobotPart);
		lastTransRobotPart = robotPart;
		MoveHead (id, robotPart.transform.localScale.x);
	}
	void Update()
	{
		if (lastTransformedRobotPart == null)
			return;

        ResetLastTransformed();
    }
    void ResetLastTransformed()
    {
		if (robotPart.anim != null)
			robotPart.anim ["pingpong"].speed = 1;
		
        lastTransformedRobotPart.TurnOffParticles();
        Vector3 newScale = new Vector3(1, 1, 1);
        lastTransformedRobotPart.transform.localScale = Vector3.Lerp(lastTransformedRobotPart.transform.localScale, newScale, 5 * Time.deltaTime);
    }
	void MoveHead(int id, float value)
	{
		Vector3 pos = initialPart.transform.position;
		pos.x /= 1.001f;
		pos.z /= 1.001f;
		initialPart.transform.position = pos;

		initialPart.transform.Translate(initialPart.transform.forward * value*Time.deltaTime);
		float newRotationDriveMode = initialPart.gameObject.transform.localEulerAngles.y + (value/10) *(id-3);
		initialPart.gameObject.transform.localEulerAngles = new Vector3 (0,newRotationDriveMode , 0);
	}
	void ResetTransform(RobotPart robotPart)
	{
		lastTransformedRobotPart = robotPart;
	}
}
