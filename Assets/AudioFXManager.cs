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
		LOWPAS,
		ECHO,
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
		case types.ECHO:
			echo.enabled = isOn;
			break;
		case types.REVERB:
			reverb.enabled = isOn;
			break;
		}
	}
}
