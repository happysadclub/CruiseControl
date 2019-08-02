using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class menuCarController : MonoBehaviour {

    public KeyCode interactKey;

    public GameObject menuControl;

    public bool canMove = false;

    //smoke variables
    private float smokeTimer = 0;
    public float smokeSpawnTimer = 0.05f;
    public GameObject smokeEmitter;
    public Transform smokeSpawnTransform;
    private bool makeSmoke = true;

    private bool canAccelerate = false;

    private void Update()
    {
        accelerate();

        if(Input.GetKeyDown(interactKey) && canMove)
        {
            enableAccelerate();

            //disable map movement
            menuControl.GetComponent<MenuController>().enableKeyboard = false;
            menuControl.GetComponent<MenuController>().enableGyroControl = false;
        }
    }

    //acceleration
    public float forwardAcceleration = 8000f;
    public float reverseAcceleration = 4000f;
    float thrust = 0f;
    float deadZone = 0.1f;
    public float maxVelocity = 50;
    private Rigidbody myRB;

    private void Start()
    {
        thrust = forwardAcceleration;
        myRB = GetComponent<Rigidbody>();
    }

    private void enableAccelerate()
    {
        canAccelerate = true;
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
}
