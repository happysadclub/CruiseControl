using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float bulletLife;
    public float spawnTime;
    public int damage;
    // Start is called before the first frame update
    void Start()
    {
        spawnTime = Time.time;
    }

    // Update is called once per frame
    void Update()
    {
        if((Time.time - spawnTime) > bulletLife)
        {
            Destroy(gameObject);
        }
    }
}
