using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DynamicCameraZoom : MonoBehaviour {

    public GameObject Player_gameobject;

    public float original_FOV = 60f;
    public float accelerate_FOV = 110f;
    public float steady_FOV = 100f;

    public float speed_LERP = 0.1f;

    public float zoomInTimer = 5f;
    private float timerTemp;

    private bool accelerating = false;
    private bool notMoving = false;

    private Camera myCamera;
    private Rigidbody myRB;

    private void Start()
    {
        myRB = Player_gameobject.GetComponent<Rigidbody>();
        myCamera = GetComponent<Camera>();
        myCamera.fieldOfView = original_FOV;
        timerTemp = zoomInTimer;


    }

    private void Update()
    {
        checkMoving();
        checkAccelerating();
        zoomControl();
    }

    private void zoomControl()
    {
        //if accelerating
        if (accelerating)
        {
            //lerp camera fov to high
            myCamera.fieldOfView = Mathf.Lerp(myCamera.fieldOfView, accelerate_FOV, speed_LERP * 0.05f);
        }
        //if not moving
        else if (notMoving)
        {
            //wait 5 seconds
            timerTemp -= Time.deltaTime;
            if (timerTemp < 0)
            {
                //lerp camera fov to low
                myCamera.fieldOfView = Mathf.Lerp(myCamera.fieldOfView, original_FOV, speed_LERP);
            }
        }
        //else if moving at a steady speed lerp to medium
        else
        {
            myCamera.fieldOfView = Mathf.Lerp(myCamera.fieldOfView, steady_FOV, speed_LERP);
            timerTemp = zoomInTimer;
        }
    }

    private void checkMoving()
    {
        //print(myRB.velocity.sqrMagnitude);
        if (myRB.velocity.sqrMagnitude < 1f)
        {
            notMoving = true;
        }
        else
        {
            notMoving = false;
        }
    }

    private float lastVelocity = 0f, accelerationTemp;

    private void checkAccelerating()
    {
        accelerationTemp = (myRB.velocity.sqrMagnitude - lastVelocity) / Time.fixedDeltaTime;
        lastVelocity = myRB.velocity.sqrMagnitude;

        if (accelerationTemp > 4000f)
        {
            accelerating = true;
        }
        else
        {
            accelerating = false;
        }
    }

}
