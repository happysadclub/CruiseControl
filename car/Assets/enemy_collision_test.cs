using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemy_collision_test : MonoBehaviour
{

    public GameObject blood_fx;

    private bool dead = false;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("bullet") && !dead)
        {
            GameObject blood = Instantiate(blood_fx, collision.transform.position, Quaternion.Euler(-90f,0,0));
            this.gameObject.GetComponent<Rigidbody>().isKinematic = false;
            dead = true;
        }
    }
}
