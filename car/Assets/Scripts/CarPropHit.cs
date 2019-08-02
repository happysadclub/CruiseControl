using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using EZCameraShake;

public class CarPropHit : MonoBehaviour {

    public Rigidbody car_body_rb;
    public Rigidbody car_bumper_rb;
    public Rigidbody car_door_rb;
    public Rigidbody car_grill_rb;
    public Rigidbody car_seats_rb;
    public Rigidbody car_interior_rb;
    public Rigidbody car_lights_rb;
    public Rigidbody car_tailpipe_rb;
    public Rigidbody car_window_rb;
    public Rigidbody car_wheel1_rb;
    public Rigidbody car_wheel2_rb;
    public Rigidbody car_wheel3_rb;
    public Rigidbody car_wheel4_rb;

    public GameObject explosion;

    public Renderer car_body_MR;
    private Color car_body_color_0;
    private Color car_body_color_1;
    private Color car_body_color_2;
    private Color car_body_color_3;
    public Renderer car_bumper_MR;
    private Color car_bumper_color;
    public Renderer car_door_MR;
    private Color car_door_color_0;
    private Color car_door_color_1;
    private Color car_door_color_2;
    public Renderer car_grill_MR;
    private Color car_grill_color;
    public Renderer car_seats_MR;
    private Color car_seats_color;
    public Renderer car_interior_MR;
    private Color car_interior_color;
    public Renderer car_lights_MR;
    private Color car_lights_color;
    public Renderer car_tailpipe_MR;
    private Color car_tailpipe_color;
    public Renderer car_window_MR;
    private Color car_window_color;
    public Renderer car_wheel1_MR;
    private Color car_wheel1_color_0;
    private Color car_wheel1_color_1;
    private Color car_wheel1_color_2;
    public Renderer car_wheel2_MR;
    private Color car_wheel2_color_0;
    private Color car_wheel2_color_1;
    private Color car_wheel2_color_2;
    public Renderer car_wheel3_MR;
    private Color car_wheel3_color_0;
    private Color car_wheel3_color_1;
    private Color car_wheel3_color_2;
    public Renderer car_wheel4_MR;
    private Color car_wheel4_color_0;
    private Color car_wheel4_color_1;
    private Color car_wheel4_color_2;

    private bool hit = false;

    private void Start()
    {
        car_body_color_0 = car_body_MR.materials[0].color;
        car_body_color_1 = car_body_MR.materials[1].color;
        car_body_color_2 = car_body_MR.materials[2].color;
        car_body_color_3 = car_body_MR.materials[3].color;
        car_bumper_color = car_bumper_MR.material.color;
        car_door_color_0 = car_door_MR.materials[0].color;
        car_door_color_0 = car_door_MR.materials[1].color;
        car_door_color_0 = car_door_MR.materials[2].color;
        car_grill_color = car_grill_MR.material.color;
        car_seats_color = car_seats_MR.material.color;
        car_interior_color = car_interior_MR.material.color;
        car_lights_color = car_lights_MR.material.color;
        car_tailpipe_color = car_tailpipe_MR.material.color;
        car_window_color = car_window_MR.material.color;
        car_wheel1_color_0 = car_wheel1_MR.materials[0].color;
        car_wheel1_color_1 = car_wheel1_MR.materials[1].color;
        car_wheel1_color_2 = car_wheel1_MR.materials[2].color;
        car_wheel2_color_0 = car_wheel2_MR.materials[0].color;
        car_wheel2_color_1 = car_wheel2_MR.materials[1].color;
        car_wheel2_color_2 = car_wheel2_MR.materials[2].color;
        car_wheel3_color_0 = car_wheel3_MR.materials[0].color;
        car_wheel3_color_1 = car_wheel3_MR.materials[1].color;
        car_wheel3_color_2 = car_wheel3_MR.materials[2].color;
        car_wheel4_color_0 = car_wheel4_MR.materials[0].color;
        car_wheel4_color_1 = car_wheel4_MR.materials[1].color;
        car_wheel4_color_2 = car_wheel4_MR.materials[2].color;

    }

    private void OnTriggerEnter(Collider other)
    {
        if (!hit)
        {
            if (other.gameObject.CompareTag("Player") || other.gameObject.CompareTag("bullet") || other.gameObject.CompareTag("chainsaw"))
            {
                //flash white
                StartCoroutine(flashWhite());

                //Camera Shake
                CameraShaker.Instance.ShakeOnce(4f, 4f, 0.1f, 0.5f);

                //source.Play();
                disableKinematic();
                //explosion
                explosion.SetActive(true);
                hit = true;
                //add score
                GameObject.Find("Director").GetComponent<PointGainControl>().addXscore(1000);
            }
        }
    }

    private void disableKinematic()
    {
        car_body_rb.isKinematic = false;
        car_bumper_rb.isKinematic = false;
        car_door_rb.isKinematic = false;
        car_grill_rb.isKinematic = false;
        car_seats_rb.isKinematic = false;
        car_interior_rb.isKinematic = false;
        car_lights_rb.isKinematic = false;
        car_tailpipe_rb.isKinematic = false;
        car_window_rb.isKinematic = false;
        car_wheel1_rb.isKinematic = false;
        car_wheel2_rb.isKinematic = false;
        car_wheel3_rb.isKinematic = false;
        car_wheel4_rb.isKinematic = false;
    }

    IEnumerator flashWhite()
    {
        car_body_MR.materials[0].color = Color.white;
        car_body_MR.materials[1].color = Color.white;
        car_body_MR.materials[2].color = Color.white;
        car_body_MR.materials[3].color = Color.white;
        car_bumper_MR.material.color = Color.white;
        car_door_MR.materials[0].color = Color.white;
        car_door_MR.materials[1].color = Color.white;
        car_door_MR.materials[2].color = Color.white;
        car_grill_MR.material.color = Color.white;
        car_seats_MR.material.color = Color.white;
        car_interior_MR.material.color = Color.white;
        car_lights_MR.material.color = Color.white;
        car_tailpipe_MR.material.color = Color.white;
        car_window_MR.material.color = Color.white;
        car_wheel1_MR.materials[0].color = Color.white;
        car_wheel1_MR.materials[1].color = Color.white;
        car_wheel1_MR.materials[2].color = Color.white;
        car_wheel2_MR.materials[0].color = Color.white;
        car_wheel2_MR.materials[1].color = Color.white;
        car_wheel2_MR.materials[2].color = Color.white;
        car_wheel3_MR.materials[0].color = Color.white;
        car_wheel3_MR.materials[1].color = Color.white;
        car_wheel3_MR.materials[2].color = Color.white;
        car_wheel4_MR.materials[0].color = Color.white;
        car_wheel4_MR.materials[1].color = Color.white;
        car_wheel4_MR.materials[2].color = Color.white;
        yield return new WaitForSeconds(0.05f);
        car_body_MR.materials[0].color = car_body_color_0;
        car_body_MR.materials[1].color = car_body_color_1;
        car_body_MR.materials[2].color = car_body_color_2;
        car_body_MR.materials[3].color = car_body_color_3;
        car_bumper_MR.material.color = car_bumper_color;
        car_door_MR.materials[0].color = car_door_color_0;
        car_door_MR.materials[1].color = car_door_color_0;
        car_door_MR.materials[2].color = car_door_color_0;
        car_grill_MR.material.color = car_grill_color;
        car_seats_MR.material.color = car_seats_color;
        car_interior_MR.material.color = car_interior_color;
        car_lights_MR.material.color = car_lights_color;
        car_tailpipe_MR.material.color = car_tailpipe_color;
        car_window_MR.material.color = car_window_color;
        car_wheel1_MR.materials[0].color = car_wheel1_color_0;
        car_wheel1_MR.materials[1].color = car_wheel1_color_1;
        car_wheel1_MR.materials[2].color = car_wheel1_color_2;
        car_wheel2_MR.materials[0].color = car_wheel2_color_0;
        car_wheel2_MR.materials[1].color = car_wheel2_color_1;
        car_wheel2_MR.materials[2].color = car_wheel2_color_2;
        car_wheel3_MR.materials[0].color = car_wheel3_color_0;
        car_wheel3_MR.materials[1].color = car_wheel3_color_1;
        car_wheel3_MR.materials[2].color = car_wheel3_color_2;
        car_wheel4_MR.materials[0].color = car_wheel4_color_0;
        car_wheel4_MR.materials[1].color = car_wheel4_color_1;
        car_wheel4_MR.materials[2].color = car_wheel4_color_2;

    }
}
