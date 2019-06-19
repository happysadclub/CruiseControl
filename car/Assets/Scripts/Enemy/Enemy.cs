using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{
    public float attackRange;
    public float aggroRange;
    public float movementSpeed;
    public float fireRate;
    public float turnSpeed;
    public GameObject player;
    public NavMeshAgent agent;
    public Rigidbody rb;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Player_Car");
        agent = GetComponent<NavMeshAgent>();
        agent.speed = movementSpeed;
    }
    
    // Update is called once per frame
    void Update()
    {
        Attack();
    }

    //function for drawing debug lines for the aggro and attack range
    private void OnDrawGizmosSelected()
    {
        //aggro range sphere
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, aggroRange);
        //attack range sphere
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position, attackRange);
    }
    private void Attack()
    {
        Vector3 targetDir = player.transform.position - transform.position;

        // The step size is equal to speed times frame time.
        float step = turnSpeed * Time.deltaTime;

        Vector3 newDir = Vector3.RotateTowards(transform.forward, targetDir, step, 0.0f);
        Debug.DrawRay(transform.position, newDir, Color.red);

        // Move our position a step closer to the target.
        transform.rotation = Quaternion.LookRotation(newDir);
        

        //finds distance between the two
        float distance = Vector3.Distance(player.transform.position, transform.position);
        //uses distance and attack range to determine if the enemy should move towards the enemy, or stop moving and attack the enemy
        if (distance <= aggroRange && distance >= attackRange)
        {
            //generate random number for x and z
            //add that to player transform
            //over range of time so not jittery
            agent.SetDestination(player.transform.position);
        }
    }
}
