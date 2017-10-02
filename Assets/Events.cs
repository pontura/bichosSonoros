using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public static class Events {

	public static System.Action<Robot> OnRobotAdded = delegate { };

	public static System.Action OnCheckToDestroyRobot = delegate { };
	public static System.Action OnDestroyRobots = delegate { };
	public static System.Action<AudioClip, int, Vector3> OnAddRobot = delegate { };
	public static System.Action OnSettingsLoaded = delegate { };
	public static System.Action<string> Log = delegate { };

	public static System.Action<bool> SetRecording = delegate { };
	public static System.Action SendRecording = delegate { };
	public static System.Action<AudioFXManager.types, bool> TurnSoundFX = delegate { };

	public static System.Action<string> OnNewFile = delegate { };
}
