using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Crossbow : MonoBehaviour
{
    public int life;
    public int hits;
    public int totalCollides;
    public GameObject currentTarget;
    public float rotationSpeed;
    public NavMeshAgent agent;
    public List<GameObject> enemies;
    public float arrowSpeed;
    public float arrowRange;
    public int maxTargets;
    int currentTargetIndex;

    // Start is called before the first frame update
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        Collider[] hitEnemies = Physics.OverlapSphere(gameObject.transform.position, arrowRange);
        foreach(Collider col in hitEnemies)
        {
            if(col.gameObject.tag == "enemy")
            {
                enemies.Add(col.gameObject);
            }
            
        }
        enemies.Sort((x, y) =>
        {
            return (this.transform.position - x.transform.position).sqrMagnitude.CompareTo((this.transform.position - y.transform.position).sqrMagnitude);
        });
        agent.speed = arrowSpeed;
       
    }

    // Update is called once per frame
    void Update()
    {
        
        if (hits >= totalCollides || !(currentTargetIndex < enemies.Count))
        {
            Destroy(this.gameObject);
        }
        agent.SetDestination(enemies[currentTargetIndex].transform.position);
        currentTarget = enemies[currentTargetIndex];
        Vector3 targetDir = enemies[currentTargetIndex].transform.position - transform.position;

        // The step size is equal to speed times frame time.
        float step = rotationSpeed * Time.deltaTime;
        Vector3 newDir = Vector3.RotateTowards(transform.forward, targetDir, step, 0.0f);

        newDir.z = 90;
        transform.rotation = Quaternion.LookRotation(newDir);



    }
    private void OnTriggerEnter(Collider other)
    {
       if(other.gameObject == enemies[currentTargetIndex])
        {
            hits++;
            currentTargetIndex++;
        }
        else
        {
            if(other.tag == "enemy" || other.tag == "prop")
            {
                hits++;
            }
        }
    }
}
