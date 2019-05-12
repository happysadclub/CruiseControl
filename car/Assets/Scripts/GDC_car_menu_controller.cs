using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using EZCameraShake;

public class GDC_car_menu_controller : MonoBehaviour {

    public KeyCode interactKey = KeyCode.Mouse0;
    private bool canAccelerate = false;
    private bool canMove = false;

    //explosion
    public GameObject explosion_particle;

    //smoke
    public GameObject smoke_particle_object;
    public Transform smokeSpawnTransform;

    //acceleration
    public float forwardAcceleration = 8000f;
    float thrust = 0f;
    public float maxVelocity = 50;
    private Rigidbody myRB;

    //camera
    public GameObject camera;

    private void Start()
    {
        thrust = forwardAcceleration;
        myRB = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update () {
        checkForInput();
        createSmoke();
        accelerate();
    }

    private void checkForInput()
    {
        if (Input.GetKeyDown(interactKey) && canMove)
        {
            canAccelerate = true;
            disableCameraMove();
            this.gameObject.GetComponent<testCarController>().disableAllControls();

            //explosion and camera shake
            explosion_particle.SetActive(true);
            CameraShaker.Instance.ShakeOnce(5f, 5f, 0.1f, 1f);
        }
    }

    private void disableCameraMove()
    {
        //disable camera movement
        camera.GetComponent<menu_camera_hold>().enable_cameraHold();
    }

    private void createSmoke()
    {
        if (!canAccelerate)
        {
            Instantiate(smoke_particle_object, smokeSpawnTransform.position, smokeSpawnTransform.rotation);
        }
    }

    private void accelerate()
    {
        if (canAccelerate)
        {
            // Handle Forward and Reverse forces
            if (Mathf.Abs(thrust) > 0)
                myRB.AddForce(transform.forward * thrust);

            // Limit max velocity
            if (myRB.velocity.sqrMagnitude > (myRB.velocity.normalized * maxVelocity).sqrMagnitude)
            {
                myRB.velocity = myRB.velocity.normalized * maxVelocity;
            }
        }
    }

    public void enableMove()
    {
        canMove = true;
    }

    public void disableMove()
    {
        canMove = false;
    }
}
