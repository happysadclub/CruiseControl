using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerMovement : MonoBehaviour {

    public KeyCode interactKey;
    public Transform selfTransform;
    public Rigidbody selfRB;
    public float speedometer = 0f;
    public float power;
    public float friction;
    public float turnRate;

    public bool enableKeyboard = false;
    public bool enableGyroControl = true;

    private float turnValue;
    private float gyroTurnValue;
    private Gyroscope gyroController;
    private Vector3 gyroVector = Vector3.zero;
    private float smokeTimer = 0;
    private float brakeTimer = 0;
    private bool driving = false;
    public bool canZoomIn = true;

    public Camera currentCam;
    private float cameraStartSize;
    public float cameraZoomSize;
    public float cameraZoomSpeed;
    public float cameraReturnSpeed;
    public float cameraZoomWaitTime;

    public GameObject smokeEmitter;
    public float smokeSpawnTimer;
    public Transform smokeSpawnTransform;

    public bool startup = true;
    private bool stopHeld = false;

    public float brakeTimerTime;

    private void Start()
    {
        gyroController = Input.gyro;
        gyroController.enabled = true;
        cameraStartSize = currentCam.orthographicSize;
    }

    // Use this for initialization
    void FixedUpdate()
    {
        if (!startup)
        {
            //checkForInput();
            checkForBrake();
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (startup)
        {
            duringStartup();
        }
        else
        {
            moveForward();
            alwaysDrivingSystem();
            keepSpeedAtZero();
            zoomIn();
            //checkMoving();
        }
        turnControl();
        gyroControl();
    }

    private void checkForInput()
    {
        if (Input.GetKey(interactKey))
        {
            canZoomIn = false;
            speedometer += power;
            driving = true;
            currentCam.orthographicSize = Mathf.Lerp(currentCam.orthographicSize, cameraZoomSize, cameraZoomSpeed);
        }
        else if (Input.GetKeyUp(interactKey))
        {
            speedometer -= power;
            StartCoroutine(triggerZoomIn());
            driving = false;
        }
    }

    private void moveForward()
    {
        speedometer *= friction;
        transform.Translate(Vector3.forward * speedometer);
    }

    private void turnControl()
    {
        
        if(enableKeyboard)
        {
            //turn right
            if (Input.GetKey(KeyCode.D))
            {
                turnValue += turnRate * Time.deltaTime;
                selfTransform.rotation = Quaternion.Euler(0, turnValue, 0);
                //print("smoke");
                if (!driving)
                {
                    createSmoke();
                }
            }
            //turn left
            else if (Input.GetKey(KeyCode.A))
            {
                turnValue -= turnRate * Time.deltaTime;
                selfTransform.rotation = Quaternion.Euler(0, turnValue, 0);
                //print("smoke");
                if (!driving)
                {
                    createSmoke();
                }
            }
        }
    }

    private void gyroControl()
    {
        if(enableGyroControl)
        {
            gyroVector = gyroController.attitude.eulerAngles;
            gyroVector.x = 0.0f;
            gyroVector.y = 0.0f;
            //print("X: " + gyroVector.x + " Y: " + gyroVector.y + " Z: " + gyroVector.z);
            selfTransform.rotation = Quaternion.Euler(0, gyroVector.z, 0);

            if (!driving)
            {
                createSmoke();
            }
        }
    }

    private void checkMoving()
    {
        if (selfRB.velocity.magnitude == 0f)
        {
            print("not moving");
            currentCam.orthographicSize = Mathf.Lerp(currentCam.orthographicSize, cameraStartSize, cameraReturnSpeed);
        }
    }

    private void createSmoke()
    {
        smokeTimer += Time.deltaTime;
        if (smokeTimer > smokeSpawnTimer)
        {
            Instantiate(smokeEmitter, smokeSpawnTransform.position, smokeSpawnTransform.rotation);
            // Remove the recorded 2 seconds.
            smokeTimer = 0;
        }
    }

    private void zoomIn()
    {
        if (canZoomIn)
        {
            currentCam.orthographicSize = Mathf.Lerp(currentCam.orthographicSize, cameraStartSize, cameraReturnSpeed);
        }
    }

    
    IEnumerator triggerZoomIn()
    {
        yield return new WaitForSeconds(cameraZoomWaitTime);
        canZoomIn = true;
    }
    

    private void keepMoving()
    {
        canZoomIn = false;
        speedometer += power;
        driving = true;
        currentCam.orthographicSize = Mathf.Lerp(currentCam.orthographicSize, cameraZoomSize, cameraZoomSpeed);
    }

    private void checkForBrake()
    {
        if(Input.GetKey(interactKey))
        {
            stopHeld = true;
        }
        else
        {
            stopHeld = false;
        }
    }

    private void activateBrake()
    {
        speedometer -= power;
        StartCoroutine(triggerZoomIn());
        driving = false;
    }

    private void alwaysDrivingSystem()
    {
        if (stopHeld)
        {
            activateBrake();
            createSmoke();
        }
        else if (!stopHeld)
        {
            keepMoving();
        }
    }

    private void keepSpeedAtZero()
    {
        if (speedometer < 0)
        {
            speedometer = 0;
        }
    }

    private void duringStartup()
    {
         createSmoke();
    }
}
