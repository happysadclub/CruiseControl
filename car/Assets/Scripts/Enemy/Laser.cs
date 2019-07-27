using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Laser : Enemy
{
    public bool isLasering;
    public float laserRotationSpeed;
    public float laserRange;
    public GameObject bullet;
    public float shotCooldown;
    float lastShotTime = 0f;
    public float bulletForce;
    public int damage;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Player_Car");
        rb = GetComponent<Rigidbody>();
        isLasering = false;
        enemyManager = GameObject.Find("EnemyManager");
    }

    // Update is called once per frame
    void Update()
    {
        if (isLasering)
        {
            Vector3 targetDir = player.transform.position - transform.position;
            float step = laserRotationSpeed * Time.deltaTime;

            Vector3 newDir = Vector3.RotateTowards(transform.forward, targetDir, step, 0.0f);
            Debug.DrawRay(transform.position, newDir, Color.red);

            // Move our position a step closer to the target.
            transform.rotation = Quaternion.LookRotation(newDir);
            if ((Time.time - lastShotTime) > shotCooldown)
            {
                Shoot();
                lastShotTime = Time.time;
            }
            
        }
        Engaging();
    }

    public void Engaging()
    {
        //finds distance between the two
        float distance = Vector3.Distance(player.transform.position, transform.position);

        //uses distance and attack range to determine if the enemy should move towards the enemy, or stop moving and attack the enemy
        if (distance <= aggroRange && distance >= attackRange)
        {
            isLasering = false;
            Vector3 targetDir = player.transform.position - transform.position;

            // The step size is equal to speed times frame time.
            float step = turnSpeed * Time.deltaTime;

            Vector3 newDir = Vector3.RotateTowards(transform.forward, targetDir, step, 0.0f);
            Debug.DrawRay(transform.position, newDir, Color.red);

            // Move our position a step closer to the target.
            transform.rotation = Quaternion.LookRotation(newDir);

            //generate random number for x and z
            //add that to player transform
            //over range of time so not jittery
           
                agent.SetDestination(player.transform.position);
            
                
            

        }
        else if (distance<=laserRange)
        {
            if (distance <= attackRange)
            {
                isLasering = true;
                
            }

        }
        

    }
    public void Shoot()
    {
        GameObject currentBullet = Instantiate(bullet, transform.position, transform.rotation);
        currentBullet.GetComponent<Rigidbody>().AddForce(gameObject.transform.forward * bulletForce);
        currentBullet.GetComponent<Bullet>().damage = damage;
    }
}
