using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class loading_text_floaty : MonoBehaviour
{
    // User Inputs
    public float vert_amplitude = 0.5f;
    private float vert_amp_temp;
    public float vert_frequency = 1f;
    private float vert_freq_temp;
    public float rot_amplitude = 0.5f;
    private float rot_amp_temp;
    public float rot_frequency = 1f;
    private float rot_freq_temp;

    //text size change
    public GameObject text_object;
    public float fontSize_original = 100f;
    public float fontSize_small = 40f;
    public float font_lerp_speed = 0.1f;

    // Position Storage Variables
    Vector3 posOffset = new Vector3();
    Vector3 tempPos = new Vector3();
    Quaternion rotOffset;
    Quaternion tempRot;

    private bool textFloat = false;

    // Use this for initialization
    void Start()
    {
        // Store the starting position & rotation of the object
        posOffset = transform.localPosition;
        rotOffset = transform.localRotation;

    }

    // Update is called once per frame
    void Update()
    {
        textFloat_function();
    }

    private void textFloat_function()
    {

            // Float up/down with a Sin()
            tempPos = posOffset;
            tempPos.y += Mathf.Sin(Time.fixedTime * Mathf.PI * vert_frequency) * vert_amplitude;

            //rotate math
            tempRot = rotOffset;
            tempRot.y += Mathf.Sin(Time.fixedTime * Mathf.PI * rot_frequency) * rot_amplitude;
            tempRot.x += Mathf.Sin(Time.fixedTime * Mathf.PI * rot_frequency) * rot_amplitude;

            //set values
            transform.localPosition = tempPos;
            transform.localRotation = tempRot;
    }

    public void enableFloaty()
    {
        textFloat = true;
    }

    public void disableFloaty()
    {
        textFloat = false;
    }
}
