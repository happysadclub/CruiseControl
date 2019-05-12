using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using EZCameraShake;

public class Movement_and_Boost_Powerup : MonoBehaviour {

    //General Variables
    public bool BoostPowerup = false;
    public KeyCode interactKey = KeyCode.Mouse0;
    public Rigidbody myRB;

    //General Movement Variables
    private bool moving = false;
    private bool usingBrake = false;
    public float thrust = 0f;
    private float lastVelocity = 0f, accelerationTemp;
    public float forwardAcceleration = 20000f;
    public float reverseAcceleration = -2000f;
    public float boostAcceleration = 30000f;

    //smoke variables
    private bool makeSmoke = false;
    private float smokeTimer = 0;
    public float smokeSpawnTimer = 0.05f;
    public GameObject smokeEmitter;
    public Transform smokeSpawnTransform;

    //hyperSpeed effect
    public ParticleSystem hyperSpeedFX_gameObject;

    //Boost System
    private bool canBoostParticle = true;
    private float velocityTimer = 0f;
    public GameObject BoostDust;
    private bool canBoost = true;
    public float maxSpeed = 100f;
    public float normalSpeed = 75f;
    public float waitBeforeLoweringMaxVelocity_seconds = 1f;

    //NEW Boost System
    private float brakeBeforeBoostTimer = 0f;
    public float waitWhileBrakeBeforeBoost_seconds = 0.5f;
    public bool boosting;
    public bool slowdown = false;
    public ParticleSystem boostReady_particle;

    //Engine Model and Particle
    public GameObject engine_model;
    public ParticleSystem engine_particle;

    //boost explode
    private float boostExplodeTimer = 0f;
    public float waitBeforeExplode_boost_seconds = 1f;

    private void Start()
    {
        //Camera Blast Off at Start
        CameraShaker.Instance.ShakeOnce(1.5f, 1.5f, 0.1f, 0.5f);
    }

    private void Update()
    {
        boostParticleControl();
        calculateAcceleration();
        hyperSpeedEffectControl();
        moveBoolControl();
    }

    private void FixedUpdate()
    {
        accelerate();
    }

    private void accelerate()
    {
        //check for brake
        if (Input.GetKey(interactKey) && BoostPowerup)
        {
            thrust = reverseAcceleration;

            //get that smoke goin'
            createSmoke();

            //timer for how long brake needs to be held down before "canBoost = true;"
            //don't forget to reset brakeBeforeBoostTimer to 0f
            brakeBeforeBoostTimer += Time.deltaTime;
            if (brakeBeforeBoostTimer > waitWhileBrakeBeforeBoost_seconds)
            {
                //spawn ready effect
                boostReady_particle.Play();

                //get ready to boost
                canBoost = true;
                brakeBeforeBoostTimer = 0f;
            }

            boostExplodeTimer += Time.deltaTime;
            if (boostExplodeTimer > waitBeforeExplode_boost_seconds)
            {
                CarExplode_engine destroyScript = this.gameObject.GetComponent<CarExplode_engine>();
                destroyScript.initiateDestroyMenuCar();
            }
        }
        else if (!Input.GetKey(interactKey) && canBoost && BoostPowerup)
        {
            //reset boost explode timer
            boostExplodeTimer = 0f;

            //Boost is occurring
            boosting = true;
            
            //switch thrust
            thrust = boostAcceleration;

            //play effects
            engine_particle.Play();
            Instantiate(BoostDust, smokeSpawnTransform.position, smokeSpawnTransform.rotation);
            CameraShaker.Instance.ShakeOnce(1.5f, 1.5f, 0.1f, 0.5f);

            //wait some time (seconds)
            //don't forget to reset velocityTimer to 0f
            velocityTimer += Time.deltaTime;
            if (velocityTimer > waitBeforeLoweringMaxVelocity_seconds)
            {
                //turn off boost and effects
                boosting = false;
                canBoost = false;
                engine_particle.Stop();
                velocityTimer = 0f;
            }
        }
        else if (slowdown)
        {
            
        }
        else
        {
            thrust = forwardAcceleration;

            //reset timers
            brakeBeforeBoostTimer = 0f;
            boostExplodeTimer = 0f;
        }

        // Applies thrust to movement.
        if (!usingBrake)
        {
            myRB.AddForce(transform.forward * thrust);
        }

        // Limit the HIGH max velocity
        if (myRB.velocity.sqrMagnitude > (myRB.velocity.normalized * maxSpeed).sqrMagnitude && boosting)
        {
            //limit velocity to maxVelocity_accelerate
            myRB.velocity = myRB.velocity.normalized * maxSpeed;
        }

        // Limit LOWER max velocity
        if (myRB.velocity.sqrMagnitude > (myRB.velocity.normalized * normalSpeed).sqrMagnitude && !boosting)
        {
            //limit velocity to maxVelocity_normal
            myRB.velocity = myRB.velocity.normalized * normalSpeed;
        }

            /*
            // Limit LOWER max velocity
            if (myRB.velocity.sqrMagnitude > (myRB.velocity.normalized * normalSpeed).sqrMagnitude)
            {
                if (BoostPowerup)
                {
                    engine_particle.Play();
                }

                //wait 1f second
                //don't forget to reset velocityTimer to 0f
                velocityTimer += Time.deltaTime;
                if (velocityTimer > waitBeforeLoweringMaxVelocity_seconds)
                {
                    if (BoostPowerup)
                    {
                        engine_particle.Stop();
                    }
                    //limit velocity to maxVelocity_normal
                    myRB.velocity = myRB.velocity.normalized * normalSpeed;
                    canBoost = false;
                }
            }

            // Reset ability to boost
            if (!moving)
            {
                velocityTimer = 0f;
                canBoost = true;
            }
            else
            {
                canBoostParticle = true;
            }
            */

            // Print current velocity
            //print("my velocity: " + myRB.velocity.sqrMagnitude);
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

    private void boostParticleControl()
    {
        if (canBoost && canBoostParticle)
        {
            Instantiate(BoostDust, smokeSpawnTransform.position, smokeSpawnTransform.rotation);
            canBoostParticle = false;
        }
    }

    private void hyperSpeedEffectControl()
    {
        //if (myRB.velocity.sqrMagnitude < (myRB.velocity.normalized * maxVelocity).sqrMagnitude)
        if (accelerationTemp > 5000f)
        {
            hyperSpeedFX_gameObject.Play();
        }

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

    /*
    private void brakeControl()
    {
        Vector3 localVelocity = transform.InverseTransformVector(myRB.velocity);

        if (localVelocity.z <= 0)
        {
            usingBrake = true;
            myRB.velocity = new Vector3(0, 0, 0);
        }
        else
        {
            usingBrake = false;
        }
    }
    */

    private void calculateAcceleration()
    {
        accelerationTemp = (myRB.velocity.sqrMagnitude - lastVelocity) / Time.fixedDeltaTime;
        lastVelocity = myRB.velocity.sqrMagnitude;
        //print(accelerationTemp);
    }
}
