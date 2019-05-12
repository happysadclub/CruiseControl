using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PointGainSoundControl : MonoBehaviour {

    public AudioSource source;
    public AudioClip pointGain_sound;

	// Use this for initialization
	void Start () {
        source.clip = pointGain_sound;
	}
	
	public void playPointSound()
    {
        source.Play();
    }
}
