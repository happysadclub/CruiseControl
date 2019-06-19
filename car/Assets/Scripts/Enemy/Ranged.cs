using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Ranged : Enemy
{
    public float disengageRange;
    public float yeetAwayForce;
    public GameObject bullet;
    public float shotCooldown;
    float lastShotTime = 0f;
    public float bulletForce;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Player_Car");
        agent = GetComponent<NavMeshAgent>();
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        Engaging();
    }

    public void Engaging()
    {
        //finds distance between the two
        float distance = Vector3.Distance(player.transform.position, transform.position);

        //uses distance and attack range to determine if the enemy should move towards the enemy, or stop moving and attack the enemy
        if (distance <= aggroRange && distance >= attackRange)
        {
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


            if (distance <= disengageRange)
            {

               
            }
            
            else
            {
                agent.SetDestination(player.transform.position);
            }

        }
        else if(distance <= attackRange)
        {
            Vector3 targetDir = player.transform.position - transform.position;
            float step = turnSpeed * Time.deltaTime;

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
        if (distance <= disengageRange)
        {

            rb.AddForce(-transform.forward * yeetAwayForce);
        }

    }
    
    public void Shoot()
    {
        GameObject currentBullet = Instantiate(bullet, transform.position, transform.rotation);
        currentBullet.GetComponent<Rigidbody>().AddForce(gameObject.transform.forward * bulletForce);
        Debug.Log("Shoot");
    }

    private void OnDrawGizmosSelected()
    {
        //aggro range sphere
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, disengageRange);
    }
}
