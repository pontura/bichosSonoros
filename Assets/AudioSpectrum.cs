using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class AudioSpectrum : MonoBehaviour
{
	Robot robot;
	public void Init(Robot robot)
	{
		this.robot = robot;
	}
	void Update()
	{
		if (robot == null)
			return;
		float[] spectrum = new float[256];

		robot.audioSource.GetSpectrumData(spectrum, 0, FFTWindow.BlackmanHarris);

		//AudioListener.GetSpectrumData(spectrum, 0, FFTWindow.Rectangular);
		float a = 0;
		for (int i = 1; i < spectrum.Length - 1; i++)
		{
			a += spectrum [i];
		//	Debug.DrawLine(new Vector3(i - 1, spectrum[i] + 10, 0), new Vector3(i, spectrum[i + 1] + 10, 0), Color.red);
		//	Debug.DrawLine(new Vector3(i - 1, Mathf.Log(spectrum[i - 1]) + 10, 2), new Vector3(i, Mathf.Log(spectrum[i]) + 10, 2), Color.cyan);
		//	Debug.DrawLine(new Vector3(Mathf.Log(i - 1), spectrum[i - 1] - 10, 1), new Vector3(Mathf.Log(i), spectrum[i] - 10, 1), Color.green);
		//	Debug.DrawLine(new Vector3(Mathf.Log(i - 1), Mathf.Log(spectrum[i - 1]), 3), new Vector3(Mathf.Log(i), Mathf.Log(spectrum[i]), 3), Color.blue);
		}

		//a /= spectrum.Length;

		int result = (int)Mathf.Lerp (1, 100, (a / spectrum.Length) * 1500);
		robot.audioSpectrumValue = result;

	}
}