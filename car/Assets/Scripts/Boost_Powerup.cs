using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boost_Powerup : MonoBehaviour {

    public bool BoostPowerup = false;
    public Rigidbody myRB;

    private bool moving = false;

    private void Update()
    {
        moveBoolControl();

        //Control Boost
        if (BoostPowerup)
        {
            boostController();
        }
    }

    private void boostController()
    {
        
    }

    private void moveBoolControl()
    {
        //if ((myRB.velocity.normalized * maxVelocity).sqrMagnitude < 1f)
        if (myRB.velocity.sqrMagnitude < 1f)
        {
            moving = false;
        }
        else
        {
            moving = true;
        }
    }
}
