using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class menu_always_face_object : MonoBehaviour {

    public GameObject object_to_follow;
    public GameObject child_object_with_floatText;
    private Transform temp_transform;
    private bool follow = true;

	// Use this for initialization
	void Start () {
        temp_transform = this.transform;
    }
	
	// Update is called once per frame
	void Update () {
        follow_controller();
    }

    private void follow_controller()
    {
        if (follow)
        {
            temp_transform.eulerAngles = new Vector3(0f, 1f * object_to_follow.transform.rotation.eulerAngles.y, 0f);
            transform.rotation = temp_transform.rotation;
        }
    }

    public void enableFollow()
    {
        follow = true;
    }

    public void disableFollow()
    {
        follow = false;
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            //enableFollow();

            //enable float in child
            child_object_with_floatText.GetComponent<menu_text_floaty>().enableFloaty();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            //enableFollow();

            //enable float in child
            child_object_with_floatText.GetComponent<menu_text_floaty>().enableFloaty();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            //disableFollow();

            //disable float in child
            child_object_with_floatText.GetComponent<menu_text_floaty>().disableFloaty();
        }
    }
}
