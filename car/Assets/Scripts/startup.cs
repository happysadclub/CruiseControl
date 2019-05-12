using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class startup : MonoBehaviour {

    public GameObject player;

    public AudioSource source;
    public AudioClip three_clip;
    public AudioClip two_clip;
    public AudioClip one_clip;
    public AudioClip go_clip;

    public GameObject one_sprite;
    public GameObject two_sprite;
    public GameObject three_sprite;
    public GameObject go_sprite;

	// Use this for initialization
	void Start () {
        playerMovement playerMoveScript = player.GetComponent<playerMovement>();
        playerMoveScript.startup = true;
        StartCoroutine(Three());
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    IEnumerator Three()
    {
        three_sprite.SetActive(true);
        source.clip = three_clip;
        source.Play();
        yield return new WaitForSeconds(1f);
        three_sprite.SetActive(false);
        StartCoroutine(Two());
    }

    IEnumerator Two()
    {
        two_sprite.SetActive(true);
        source.clip = two_clip;
        source.Play();
        yield return new WaitForSeconds(1f);
        two_sprite.SetActive(false);
        StartCoroutine(One());
    }

    IEnumerator One()
    {
        one_sprite.SetActive(true);
        source.clip = one_clip;
        source.Play();
        yield return new WaitForSeconds(1.5f);
        one_sprite.SetActive(false);
        StartCoroutine(Go());
    }

    IEnumerator Go()
    {
        go_sprite.SetActive(true);
        source.clip = go_clip;
        source.Play();
        //START GAME
        playerMovement playerMoveScript = player.GetComponent<playerMovement>();
        playerMoveScript.startup = false;
        yield return new WaitForSeconds(1f);
        go_sprite.SetActive(false);
    }
}
