using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public static class Events {

	public static System.Action OnDestroyRobots = delegate { };
	public static System.Action<AudioClip> OnAddRobot = delegate { };
	public static System.Action OnSettingsLoaded = delegate { };
	public static System.Action<string> Log = delegate { };

	public static System.Action<bool> SetRecording = delegate { };
	public static System.Action<AudioFXManager.types, bool> TurnSoundFX = delegate { };
}
