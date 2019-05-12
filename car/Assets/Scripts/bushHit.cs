using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using EZCameraShake;

public class bushHit : MonoBehaviour {

    public GameObject limbModel;
    public GameObject bushModel;
    public GameObject leafParticleSystem;

    public AudioSource source;
    public AudioClip hitClip;

    public Renderer bush_r;
    private Color bush_color;

    private bool hit = false;

	// Use this for initialization
	void Start () {
        source.clip = hitClip;
        bush_color = bush_r.material.color;
	}

    private void OnTriggerEnter(Collider other)
    {
        if (!hit)
        {
            if (other.gameObject.CompareTag("Player") || other.gameObject.CompareTag("destructible") || other.gameObject.CompareTag("bullet") || other.gameObject.CompareTag("chainsaw"))
            {
                //Camera Shake
                CameraShaker.Instance.ShakeOnce(1f, 4f, 0.1f, 0.1f);

                StartCoroutine(flashWhiteAndExplode());
                //add score
                GameObject.Find("Director").GetComponent<PointGainControl>().increaseScore();
            }  
        }
    }

    IEnumerator flashWhiteAndExplode()
    {
        hit = true;
        source.Play();
        bush_r.material.color = Color.white;

        yield return new WaitForSeconds(0.05f);
        bushModel.SetActive(false);
        limbModel.SetActive(true);
        Instantiate(leafParticleSystem, this.transform.position, Quaternion.Euler(-90, 0, 0));
        bush_r.material.color = bush_color;
    }
}
