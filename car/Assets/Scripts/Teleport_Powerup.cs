using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Teleport_Powerup : MonoBehaviour {

    //general variables
    public bool TeleportPowerup;
    public KeyCode interactKey = KeyCode.Mouse0;
    public Collider playerCar_collider;
    public GameObject car_model;
    public GameObject ghostCar_model;
    private Vector3 carModelOriginal_position;

    //movement variables
    private Vector3 startPoint;
    public LineRenderer lineRenderer_component;

    //sound variables
    public AudioSource soundSource;
    public AudioClip stretchSound;
    public AudioClip snapClip;
    public float sound_interpolationPeriod = 0.1f;
    private float soundTimer = 0f;

    // Use this for initialization
    void Start () {
        car_model.transform.localPosition = carModelOriginal_position;
	}
	
	// Update is called once per frame
	void Update () {
		if (TeleportPowerup)
        {
            teleportControl();
        }
	}

    private void teleportControl()
    {
        if (Input.GetKeyDown(interactKey))
        {
            //Initialize
            startPoint = this.transform.position;

            //disable collider
            playerCar_collider.isTrigger = true;

            //enable ghost car model
            ghostCar_model.SetActive(true);
        }

        if (Input.GetKey(interactKey))
        {
            //Hold car model
            car_model.transform.position = startPoint;

            //Draw Line
            lineRenderer_component.SetWidth(10f, 2f);
            lineRenderer_component.SetPosition(0, startPoint);
            lineRenderer_component.SetPosition(1, this.transform.position);

            //Sound control
            incrementSound();


        }

        if (Input.GetKeyUp(interactKey))
        {
            //reset sound
            soundSource.pitch = 1f;

            //play snap sound
            soundSource.clip = snapClip;
            soundSource.Play();

            //fix collider
            playerCar_collider.isTrigger = false;

            //disable ghost car model
            ghostCar_model.SetActive(false);

            //reset car model position
            car_model.transform.localPosition = carModelOriginal_position;
        }
    }

    private void incrementSound()
    {
        soundTimer += Time.deltaTime;

        if (soundTimer >= sound_interpolationPeriod)
        {
            soundTimer = soundTimer - sound_interpolationPeriod;

            // execute block of code here
            soundSource.clip = stretchSound;
            soundSource.pitch += 0.1f;
            soundSource.Play();
        }
    }
}
