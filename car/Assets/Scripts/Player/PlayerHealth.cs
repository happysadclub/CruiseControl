using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    public int health = 100;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log(health);
    }
    public void TakeDamage(int damage)
    {
        health -= damage;
    }
    public void Heal(int healing)
    {
        health += healing;
    }
    private void OnCollisionEnter(Collision collision)
    {
        Debug.Log("SomethinmgHit");
        if(collision.gameObject.tag == "enemyBullet")
        {
            Debug.Log("Been Shot");
            TakeDamage(collision.gameObject.GetComponent<Bullet>().damage);
            Destroy(collision.gameObject);
        }
    }
}
