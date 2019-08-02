﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class SprintEnemy : Enemy
{
    public float sprintRange;
    public float tooCloseToSprint;
    public float normalSpeed;
    public float yeetSpeed;
    public int damage;
    public float sprintDistance;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Player_Car");
        agent = GetComponent<NavMeshAgent>();
        rb = GetComponent<Rigidbody>();
        enemyManager = GameObject.Find("EnemyManager");
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
            float step = turnSpeed* Time.deltaTime;

            Vector3 newDir = Vector3.RotateTowards(transform.forward, targetDir, step, 0.0f);
            Debug.DrawRay(transform.position, newDir, Color.red);

            // Move our position a step closer to the target.
            transform.rotation = Quaternion.LookRotation(newDir);

            //generate random number for x and z
            //add that to player transform
            //over range of time so not jittery
           

            if(distance<sprintRange)
            {
                Vector3 target = gameObject.transform.position + gameObject.transform.forward * sprintDistance;
                agent.speed = yeetSpeed;
                agent.SetDestination(target);
               
            }
            else
            {
                agent.speed = normalSpeed;
                agent.SetDestination(player.transform.position);
            }
        }
        
    }
    private void OnDrawGizmosSelected()
    {
        //aggro range sphere
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, sprintRange);
        //attack range sphere
        Gizmos.color = Color.magenta;
        Gizmos.DrawWireSphere(transform.position, tooCloseToSprint);
    }
}
