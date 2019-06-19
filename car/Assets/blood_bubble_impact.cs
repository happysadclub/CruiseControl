using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class blood_bubble_impact : MonoBehaviour
{
    public float fall_force;
    public float random_force_max;

    public GameObject blood_decal;

    private float random_force_x;
    private float random_force_z;

    private void Start()
    {
        random_force_x = Random.Range(-1 * random_force_max, random_force_max);
        random_force_z = Random.Range(-1 * random_force_max, random_force_max);
        this.gameObject.GetComponent<Rigidbody>().AddForce(random_force_x, fall_force, 0f);
    }

    private void Update()
    {
        
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("ground") || collision.gameObject.CompareTag("wall"))
        {
            Instantiate(blood_decal, this.transform.position, Quaternion.identity);
            Destroy(this.gameObject);
        }
    }

}
