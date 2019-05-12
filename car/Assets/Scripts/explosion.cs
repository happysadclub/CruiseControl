using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class explosion : MonoBehaviour {

	// Use this for initialization
	void Start () {
        gameObject.GetComponent<Rigidbody>().AddForce(Random.RandomRange(1, 15),Random.RandomRange(1, 15), Random.RandomRange(1, 15), ForceMode.Impulse);
    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
