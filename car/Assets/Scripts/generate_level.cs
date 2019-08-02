using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class generate_level : MonoBehaviour {

    public int num_cityBlocks;
    public GameObject base_cityBlock;
    private Transform pointer_transform;
    private float shiftAmount;

	// Use this for initialization
	void Start () {
        //create starting point
        //pointer_transform = base_cityBlock.transform;
        //know how much to shift
        shiftAmount = base_cityBlock.transform.localScale.x * 10;

        //make as many city blocks as told
        for (int x = 0; x < num_cityBlocks; x++)
        {
            //check if current block can build NORTH
            //check if current block can build SOUTH
            //check if current block can build WEST
            //check if current block can build EAST

            //if you can build NORTH, do it
                //build in the "pointer_transform.z + shiftAmount direction"

            //if you can build SOUTH, do it
                //build in the "pointer_transform.z - shiftAmount direction"

            //if you can build WEST, do it
                //build in the "pointer_transform.x - shiftAmount direction"

            //if you can build EAST, do it
                //build in the "pointer_transform.x + shiftAmount direction"
        }
    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
