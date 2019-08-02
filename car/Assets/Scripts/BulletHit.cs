using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletHit : MonoBehaviour {

    public GameObject bulletExplosion_system;

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("destructible") || collision.gameObject.CompareTag("wall"))
        {
            Instantiate(bulletExplosion_system, this.transform.position, this.transform.rotation);
            Destroy(this.gameObject);
        }
    }
}
