using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class simpleCarController : MonoBehaviour {

    public KeyCode interactKey;
    public bool enableKeyboard = false;
    public bool enableGyroControl = true;

    private Rigidbody myRB;

    public WheelCollider frontWheel_R, frontWheel_L;
    public WheelCollider backWheel_R, backWheel_L;

    private float turnValue;
    public float turnRate = 125;
    public float motorForce = 50;

    private bool moving = false;

    private Gyroscope gyroController;
    private Vector3 gyroVector = Vector3.zero;

    //smoke variables
    private float smokeTimer = 0;
    public float smokeSpawnTimer = 0.05f;
    public GameObject smokeEmitter;
    public Transform smokeSpawnTransform;
    private bool makeSmoke = false;

    private void Start()
    {
        gyroController = Input.gyro;
        gyroController.enabled = true;
        myRB = GetComponent<Rigidbody>();
    }

    private void getInput()
    {

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

    private void createSmoke()
    {
        if (!moving || makeSmoke)
        {
            smokeTimer += Time.deltaTime;
            if (smokeTimer > smokeSpawnTimer)
            {
                Instantiate(smokeEmitter, smokeSpawnTransform.position, smokeSpawnTransform.rotation);
                // Remove the recorded 2 seconds.
                smokeTimer = 0;
            }
        }
    }

    private void accelerate()
    {
            frontWheel_L.motorTorque = motorForce;
            frontWheel_R.motorTorque = motorForce;
    }

    private void FixedUpdate()
    {
        getInput();
        steer();
        createSmoke();
        accelerate();
    }
}