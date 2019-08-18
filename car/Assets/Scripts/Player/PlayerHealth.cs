using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{
    public int health = 100;
    public float lastHit = 0;
    public float iFrameTime;
    public int sprinterDamage;
    public int explodingDamage;
    public Slider healthSlider;
    // Start is called before the first frame update
    void Start()
    {
        healthSlider.maxValue = health;
    }

    // Update is called once per frame
    void Update()
    {
        healthSlider.value = health;
    }
    public void TakeDamage(int damage)
    {
        if(iFrameTime < Time.time - lastHit)
        {
            health -= damage;
            lastHit = Time.time;
        }
        
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
            if (iFrameTime < Time.time - lastHit)
            {
                Debug.Log("Been Shot");
                TakeDamage(collision.gameObject.GetComponent<Bullet>().damage);
                Destroy(collision.gameObject);
                lastHit = Time.time;
            }
        }
        if (collision.gameObject.tag == "sprinterEnemy")
        {
            if (iFrameTime < Time.time - lastHit)
            {
                Debug.Log("Been Sprinted At");
                TakeDamage(sprinterDamage);
                lastHit = Time.time;
            }
        }
    }
}
