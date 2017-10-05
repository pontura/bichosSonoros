using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioFXManager : MonoBehaviour {

	public AudioSource audioSource;
	public AudioDistortionFilter distortion;
	public AudioEchoFilter echo;
	public AudioLowPassFilter lowPass;
	public AudioReverbFilter reverb;
    public AudioChorusFilter chorus;

    public types type;
	public enum types
	{		
		DISTORTION,
		PITCH,
		LOWPAS,
		ECHO_DELAY,
		ECHO_RECAY_RATIO,
		REVERB,
        CHORUS_RATE,
        CHORUS_DEPTH,
        REVERB_LEVEL,
        REVERB_DECAY
	}
	void Start () {
		Events.TurnSoundFX += TurnSoundFX;
	}
	void OnDestroy () {
		Events.TurnSoundFX -= TurnSoundFX;
	}

	public void TurnSoundFX (types type, bool isOn) {

        print(type +  " ison: " + isOn);

        switch (type) {
		case types.PITCH:
			if (!isOn) {
				GetComponent<AudioSource> ().pitch = 1;
			}
			break;
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
			echo.enabled = isOn;
            break;
        case types.CHORUS_DEPTH:
            chorus.enabled = isOn;
            break;
        case types.CHORUS_RATE:
            chorus.enabled = isOn;
            break;
		case types.REVERB:
			reverb.enabled = isOn;
			break;
		case types.REVERB_DECAY:
			reverb.enabled = isOn;
			break;
		case types.REVERB_LEVEL:
			reverb.enabled = isOn;
			break;
        }
	}

	public void ChangeFXValue( types type, float value)
	{
		switch(type)
		{
		case AudioFXManager.types.DISTORTION:
			distortion.enabled = true;
			distortion.distortionLevel = value;
			break;
		case AudioFXManager.types.PITCH:
			audioSource.pitch = value;
			break;
		case AudioFXManager.types.LOWPAS:
			lowPass.enabled = true;
			lowPass.cutoffFrequency = value;
			break;
		case AudioFXManager.types.ECHO_DELAY:
			echo.enabled = true;
			echo.delay = value;
			break;
		case AudioFXManager.types.ECHO_RECAY_RATIO:
			echo.enabled = true;
			echo.decayRatio =  value;
			break;
		case AudioFXManager.types.CHORUS_DEPTH:
			chorus.enabled = true;
			chorus.depth = value;
			break;
		case AudioFXManager.types.CHORUS_RATE:
			chorus.enabled = true;
			chorus.rate = value;
			break;
		case AudioFXManager.types.REVERB_DECAY:
			reverb.enabled = true;
			reverb.decayTime = value;
			break;
		case AudioFXManager.types.REVERB_LEVEL:
			reverb.enabled = true;
			reverb.reverbLevel = value;
			break;
		}
	}
}
