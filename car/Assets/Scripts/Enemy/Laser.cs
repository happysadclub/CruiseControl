using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Laser : Enemy
{
    public bool isLasering;
    public float laserRotationSpeed;
    public float laserRange;
    public float shotCooldown;
    float lastShotTime = 0f;
    public float bulletForce;
    public int damage;
    public GameObject laserStart;
    LineRenderer lineRender;
    public float laserLength;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Player_Car");
        rb = GetComponent<Rigidbody>();
        isLasering = false;
        enemyManager = GameObject.Find("EnemyManager");
        lineRender = laserStart.GetComponent<LineRenderer>();

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

            lineRender.SetWidth(2f, 2f);
            lineRender.SetPosition(0, gameObject.transform.position);
            lineRender.SetPosition(1, gameObject.transform.position + transform.forward * laserLength);
            
            // Move our position a step closer to the target.
            transform.rotation = Quaternion.LookRotation(newDir);
            if ((Time.time - lastShotTime) > shotCooldown)
            {
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
            lineRender.SetWidth(0f, 0f);
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
}
