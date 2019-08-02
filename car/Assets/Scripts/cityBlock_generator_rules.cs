using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class cityBlock_generator_rules : MonoBehaviour {

    
    public int num_NORTH_placements = 1;
    public int num_SOUTH_placements = 1;
    public int num_WEST_placements = 1;
    public int num_EAST_placements = 1;
    public CityBlock[] NORTH_objects;
    public CityBlock[] SOUTH_objects;
    public CityBlock[] WEST_objects;
    public CityBlock[] EAST_objects;

    // Use this for initialization
    void Start () {
        NORTH_objects = new CityBlock[num_NORTH_placements];
        SOUTH_objects = new CityBlock[num_SOUTH_placements];
        WEST_objects = new CityBlock[num_WEST_placements];
        EAST_objects = new CityBlock[num_EAST_placements];
    }
}
