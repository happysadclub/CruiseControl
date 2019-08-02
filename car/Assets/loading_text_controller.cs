using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class loading_text_controller : MonoBehaviour
{
    public bool enableKeyboard = false;
    public bool enableGyroControl = true;

    private float turnValue;
    public float turnRate = 125;

    private Gyroscope gyroController;
    private Vector3 gyroVector = Vector3.zero;

    // Use this for initialization
    void Start()
    {
        gyroController = Input.gyro;
        gyroController.enabled = true;
    }

    // Update is called once per frame
    void Update()
    {
        steer();
    }

    private void steer()
    {
        if (enableKeyboard)
        {
            //turn right
            if (Input.GetKey(KeyCode.A))
            {
                turnValue += turnRate * Time.deltaTime;
                transform.rotation = Quaternion.Euler(0, turnValue, 0);
            }
            //turn left
            else if (Input.GetKey(KeyCode.D))
            {
                turnValue -= turnRate * Time.deltaTime;
                transform.rotation = Quaternion.Euler(0, turnValue, 0);
            }
        }
        else if (enableGyroControl)
        {
            gyroVector = gyroController.attitude.eulerAngles;
            gyroVector = gyroVector * -1f;
            gyroVector.x = 0.0f;
            gyroVector.y = 0.0f;
            //print("X: " + gyroVector.x + " Y: " + gyroVector.y + " Z: " + gyroVector.z);
            transform.rotation = Quaternion.Euler(0, gyroVector.z, 0);
        }
    }
}
