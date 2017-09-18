using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioFXManager : MonoBehaviour {

	public AudioDistortionFilter distortion;
	public AudioEchoFilter echo;
	public AudioLowPassFilter lowPass;
	public AudioReverbFilter reverb;

	public types type;
	public enum types
	{		
		DISTORTION,
		PITCH,
		LOWPAS,
		ECHO_DELAY,
		ECHO_RECAY_RATIO,
		REVERB
	}
	void Start () {
		Events.TurnSoundFX += TurnSoundFX;
	}

	void TurnSoundFX (types type, bool isOn) {
		switch (type) {
		case types.DISTORTION:
			distortion.enabled = isOn;
			break;
		case types.LOWPAS:
			lowPass.enabled = isOn;
			break;
		case types.ECHO_DELAY:
			echo.enabled = isOn;
			break;
		case types.ECHO_RECAY_RATIO:
			reverb.enabled = isOn;
			break;
		}
	}
}
