using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioFXManager : MonoBehaviour {

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

	void TurnSoundFX (types type, bool isOn) {

        print(type +  " ison: " + isOn);

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
}
