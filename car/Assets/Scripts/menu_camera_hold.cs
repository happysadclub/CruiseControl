using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class menu_camera_hold : MonoBehaviour {

    private Vector3 myPosition_vector;
    private Quaternion myRotate_quat;
    private bool doOnce = true;
    private bool hold_camera = false;
	
	// Update is called once per frame
	void Update () {
        
        if (hold_camera)
        {
            hold_camera_function();
        }
	}

    private void hold_camera_function()
    {
        if (doOnce)
        {
            //store position and rotation
            myPosition_vector = transform.position;
            myRotate_quat = transform.rotation;
            doOnce = false;
        }

        //keep position and rotation
        transform.position = myPosition_vector;
        transform.rotation = myRotate_quat;
    }

    public void enable_cameraHold()
    {
        hold_camera = true;
    }
}
