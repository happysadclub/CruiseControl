using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using EZCameraShake;

public class streetLightHit : MonoBehaviour {

    private bool hit = false;

    public GameObject lightBlock;
    public GameObject light;
    public GameObject spark_object;

    public Renderer light_r;
    private Color light_color;

    private void Start()
    {
        light_color = light_r.material.color;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!hit)
        {
            if (other.gameObject.CompareTag("Player") || other.gameObject.CompareTag("destructible") || other.gameObject.CompareTag("bullet") || other.gameObject.CompareTag("chainsaw"))
            {
                //source.Play();

                //Camera Shake
                CameraShaker.Instance.ShakeOnce(1f, 4f, 0.1f, 0.1f);

                //flash white
                StartCoroutine(flashWhite());

                //enable particle
                spark_object.SetActive(true);

                //disable lights
                StartCoroutine(lightFlicker());

                hit = true;
                //add score
                GameObject.Find("Director").GetComponent<PointGainControl>().increaseScore();
            }
        }
    }

    private IEnumerator lightFlicker()
    {
        lightBlock.SetActive(false);
        light.SetActive(false);
        yield return new WaitForSeconds(0.1f);
        lightBlock.SetActive(true);
        light.SetActive(true);
        yield return new WaitForSeconds(0.1f);
        lightBlock.SetActive(false);
        light.SetActive(false);
        yield return new WaitForSeconds(0.05f);
        lightBlock.SetActive(true);
        light.SetActive(true);
        yield return new WaitForSeconds(0.05f);
        lightBlock.SetActive(false);
        light.SetActive(false);
    }

    IEnumerator flashWhite()
    {
        light_r.material.color = Color.white;
        yield return new WaitForSeconds(0.05f);
        light_r.material.color = light_color;
    }
}
