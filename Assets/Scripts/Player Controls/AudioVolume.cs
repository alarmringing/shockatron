using UnityEngine;
using System.Collections;

public class AudioVolume : MonoBehaviour {


	public float hSliderValue = 10.0f;


	private void AudioOptions()
	{
		hSliderValue = GUI.HorizontalSlider (new Rect (370, 220, 546, 30), hSliderValue, 0.0f, 10.0f);
		AudioListener.volume = hSliderValue/10.0f;
	}
}
