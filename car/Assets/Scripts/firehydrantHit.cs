using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using EZCameraShake;

public class firehydrantHit : MonoBehaviour {

    public GameObject waterParticleSystem;

    public AudioSource source;
    public AudioClip hitClip;

    public Renderer hydrant_r;
    private Color hydrant_color;

    private bool hit = false;

    // Use this for initialization
    void Start () {
        //source.clip = hitClip;
        hydrant_color = hydrant_r.material.color;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!hit)
        {
            if (other.gameObject.CompareTag("Player") || other.gameObject.CompareTag("destructible") || other.gameObject.CompareTag("bullet") || other.gameObject.CompareTag("chainsaw"))
            {
                //flash white
                StartCoroutine(flashWhite());

                //Camera Shake
                CameraShaker.Instance.ShakeOnce(1f, 4f, 0.1f, 0.1f);

                //source.Play();
                Instantiate(waterParticleSystem, this.transform.position, Quaternion.Euler(-90, 0, 0));
                hit = true;
                //add score
                GameObject.Find("Director").GetComponent<PointGainControl>().increaseScore();
            }
        }
    }

    IEnumerator flashWhite()
    {
        hydrant_r.material.color = Color.white;
        yield return new WaitForSeconds(0.05f);
        hydrant_r.material.color = hydrant_color;
    }
}
