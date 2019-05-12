using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class destroyAfterTime : MonoBehaviour {

    public float killAfter = 3;

	// Use this for initialization
	void Start () {
        Destroy(this.gameObject, killAfter);
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
