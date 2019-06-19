﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class testCarController : MonoBehaviour {

    //General Variables
    public bool enableKeyboard = false;
    public bool enableGyroControl = true;
    private Gyroscope gyroController;
    private Vector3 gyroVector = Vector3.zero;

    //General Movement Variables
    private float turnValue;
    public float turnRate = 125;

    //Exhaust System
    //public GameObject exhaust_obj;
    //public Transform exhaustPoint_transform;

    private void Start()
    {
        gyroController = Input.gyro;
        gyroController.enabled = true;
    }

    private void FixedUpdate()
    {
        steer();
    }

    private void steer()
    {
        if (enableKeyboard)
        {
            //turn right
            if (Input.GetKey(KeyCode.D))
            {
                turnValue += turnRate * Time.deltaTime;
                transform.rotation = Quaternion.Euler(0, turnValue, 0);
            }
            //turn left
            else if (Input.GetKey(KeyCode.A))
            {
                turnValue -= turnRate * Time.deltaTime;
                transform.rotation = Quaternion.Euler(0, turnValue, 0);
            }
        }
        else if (enableGyroControl)
        {
            gyroVector = gyroController.attitude.eulerAngles;
            gyroVector.x = 0.0f;
            gyroVector.y = 0.0f;
            //print("X: " + gyroVector.x + " Y: " + gyroVector.y + " Z: " + gyroVector.z);
            transform.rotation = Quaternion.Euler(0, gyroVector.z, 0);
        }
    }

    /*
    private void exhaustCreation()
    {
        Instantiate(exhaust_obj, exhaustPoint_transform.position, exhaustPoint_transform.rotation);
    }
    */

    public void disableAllControls()
    {
        enableKeyboard = false;
        enableGyroControl = false;
    }
}