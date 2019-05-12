using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class play_levelMusic : MonoBehaviour {

    public AudioSource source;
    public AudioClip level_music;

	// Use this for initialization
	void Start () {
        source.clip = level_music;
        source.Play();
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
