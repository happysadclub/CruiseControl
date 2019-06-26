using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemy_collision_test : MonoBehaviour
{

    public GameObject blood_fx;

    public float hit_force = 500f;

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
        else if (collision.gameObject.CompareTag("Player") && !dead)
        {

            GameObject blood = Instantiate(blood_fx, collision.transform.position, Quaternion.Euler(-90f, 0, 0));
            this.gameObject.GetComponent<Rigidbody>().isKinematic = false;
            dead = true;

            // Calculate Angle Between the collision point and the player
            Vector3 dir = collision.contacts[0].point - transform.position;
            // We then get the opposite (-Vector3) and normalize it
            dir = -dir.normalized;
            // And finally we add force in the direction of dir and multiply it by force. 
            // This will push back the player
            GetComponent<Rigidbody>().AddForce(dir * hit_force);
        }
    }
}
