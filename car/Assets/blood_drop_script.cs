using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class blood_drop_script : MonoBehaviour
{

    public GameObject blood_decal;

    public ParticleSystem part;
    public List<ParticleCollisionEvent> collisionEvents;

    // Start is called before the first frame update
    void Start()
    {
        part = GetComponent<ParticleSystem>();
        collisionEvents = new List<ParticleCollisionEvent>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnParticleCollision(GameObject other)
    {
        int numCollisionEvents = part.GetCollisionEvents(other, collisionEvents);
        int i = 0;

        //print(numCollisionEvents);

        while (i < numCollisionEvents)
        {
            Vector3 pos = collisionEvents[i].intersection;
            spawn_blood(other, pos);
            i++;
        }
    }

    void spawn_blood(GameObject other, Vector3 collision_pos)
    {
        if (other.CompareTag("ground"))
        {
            Instantiate(blood_decal, collision_pos, Quaternion.identity);
        }
        else if (other.CompareTag("wall"))
        {

        }
    }
}
