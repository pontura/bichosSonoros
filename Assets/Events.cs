using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public static class Events {

	public static System.Action<Robot> OnRobotAdded = delegate { };
	public static System.Action<Robot> OnDestroyRobot = delegate { };
	public static System.Action<int> OnShowRobot = delegate{};


	public static System.Action<AudioClip> OnNewAudioClip = delegate {};



	public static System.Action<Robot> OnCameraFollow = delegate { };

	public static System.Action OnCheckToDestroyRobot = delegate { };
	public static System.Action OnDestroyRobots = delegate { };

//	public static System.Action<AudioClip, int, int, Vector3> OnCreateNewRobot = delegate { };
	public static System.Action<RobotDefinition> OnCreateNewRobot = delegate { };

	public static System.Action OnSettingsLoaded = delegate { };
	public static System.Action<string> Log = delegate { };

	public static System.Action<bool> OnSetRecording = delegate { };


	public static System.Action OnSendRobot = delegate { };
	public static System.Action<bool> OnSendOk = delegate { };

	public static System.Action<AudioFXManager.types, bool> TurnSoundFX = delegate { };

	public static System.Action<string> OnNewFile = delegate { };

	public static System.Action<int> OnBichoSelected = delegate { };
}
