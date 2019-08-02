using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class spawn_this_prop : MonoBehaviour
{
    [Range(0.0f, 1.0f)]
    public float object_spawn_chance_percent;

    // Start is called before the first frame update
    private void Start()
    {
        //percent chance to spawn object
        if (Random.value < (1 - object_spawn_chance_percent))
        {
            this.gameObject.SetActive(false);
        }
    }
}
