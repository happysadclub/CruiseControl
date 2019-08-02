using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class prop
{
    public GameObject prop_object;
    public Transform[] spawnLocation_transform_list;
}

public class spawn_block_props : MonoBehaviour
{
    [Range(0.0f, 1.0f)]
    public float object_spawn_chance_percent;
    public prop[] prop_list;

    // Start is called before the first frame update
    void Start()
    {
        //for ever prop in the prop list
        foreach (prop propTemp in prop_list)
        {
            //for every transform in the props transform list
            foreach (Transform transTemp in propTemp.spawnLocation_transform_list)
            {
                //50% shot of spawning object;
                if (Random.value < object_spawn_chance_percent)
                {
                    //spawn prop at that transform and make it child of parent
                    GameObject temp_object = Instantiate(propTemp.prop_object, transTemp.transform.position, transTemp.transform.rotation);
                    temp_object.transform.SetParent(this.transform, true);
                }
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
