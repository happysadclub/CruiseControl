using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{

    public int health = 100;
    public float lastHit = 0;
    public float iFrameTime;
    public int playerBulletDamage;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(health <= 0)
        {
            Destroy(gameObject);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        Debug.Log("SomethinmgHit");
        if (collision.gameObject.tag == "playerBullet")
        {
            if (iFrameTime < Time.time - lastHit)
            {
                health -= playerBulletDamage;
                Destroy(collision.gameObject);
                lastHit = Time.time;
            }
        }
    }
}
