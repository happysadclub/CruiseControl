using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class holdYposition : MonoBehaviour {

    private float y_pos;

	void Start () {
        y_pos = transform.position.y;
	}

    private void FixedUpdate()
    {
        transform.position = new Vector3(transform.position.x, y_pos, transform.position.z);
    }
}
