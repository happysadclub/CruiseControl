using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class chainsaw_script : MonoBehaviour {

    public ParticleSystem spark_particle;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private void OnTriggerExit(Collider other)
    {
        spark_particle.Stop();
    }

    private void OnTriggerStay(Collider other)
    {
        spark_particle.Play();
    }
}
