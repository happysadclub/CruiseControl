using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class aim_assist_test_script : MonoBehaviour
{

    [Header("Components")]
    public Transform projectile_spawn_transform_R;
    public Transform projectile_spawn_transform_L;

    public List<GameObject> enemy_in_range_list;

    // Start is called before the first frame update
    void Awake()
    {
        instantiate_enemy_list();
    }

    // Update is called once per frame
    void Update()
    {
        //create local variable that represents closest enemy
        GameObject close_enemy = find_closer_enemy();

        //if the enemy list is empty -- set gun rotation back to normal
        if (enemy_in_range_list.Count < 1)
        {
            projectile_spawn_transform_R.localRotation = Quaternion.identity;
            projectile_spawn_transform_L.localRotation = Quaternion.identity;
        }
        //if enemy list has enemies in it -- face them
        else
        {
            projectile_spawn_transform_R.LookAt(close_enemy.transform);
            projectile_spawn_transform_L.LookAt(close_enemy.transform);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        //if gameobject is enemy
        if (other.gameObject.CompareTag("enemy"))
        {
            //add them to the list
            enemy_in_range_list.Add(other.gameObject);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        //if gameobject is enemy
        if (other.gameObject.CompareTag("enemy"))
        {
            //take them off the list
            enemy_in_range_list.Remove(other.gameObject);
        }
    }

    private void instantiate_enemy_list()
    {
        enemy_in_range_list = new List<GameObject>(25);
    }

    //Function used to return the closest enemy out of a list of enemies.
    private GameObject find_closer_enemy()
    {
        //components
        GameObject closest_enemy = null;
        float closest_distance_sqr = Mathf.Infinity;
        Vector3 current_position = transform.position;

        //for each enemy in list of enemies -- find the closest
        foreach (GameObject current_enemy in enemy_in_range_list)
        {
            Vector3 direction_to_target = current_enemy.transform.position - current_position;
            float d_sqr_to_enemy = direction_to_target.sqrMagnitude;
            if (d_sqr_to_enemy < closest_distance_sqr)
            {
                closest_distance_sqr = d_sqr_to_enemy;
                closest_enemy = current_enemy;
            }
        }

        //return the closest enemy object
        return closest_enemy;
    }
}
