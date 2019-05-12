using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class followPlayer : MonoBehaviour {

    public GameObject Player_gameobject;
    public Transform target;
    public float smoothspeed_normal = 0.1f;
    public Vector3 offset;
    public bool disable = false;

    private float smoothspeed;
    private bool accelerating = false;
    private Rigidbody myRB;

    private void Start()
    {
        myRB = Player_gameobject.GetComponent<Rigidbody>();
        smoothspeed = smoothspeed_normal;
    }

    // Update is called once per frame
    void LateUpdate()
    {
        playerFollow();
    }

    private void Update()
    {
        //checkAccelerating();
        //dynamicSmoothspeed();
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

    private void dynamicSmoothspeed()
    {
        //if accelerating
        if (accelerating)
        {
            //change camera follow speed
            smoothspeed = smoothspeed_normal * 0.5f;
        }
        else
        {
            smoothspeed = smoothspeed_normal;
        }
    }

    private void playerFollow()
    {
        if (!disable)
        {
            Vector3 desiredPosition = target.position + offset;
            Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothspeed);
            transform.position = smoothedPosition;
        }
    }
}
