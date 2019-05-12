using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class CityBlock : MonoBehaviour
{
    public GameObject cityBlock_object;
    public float spawn_percent;
    public bool isStreet;
    public bool instantiated = false;
    public int size = 0; //0 = 1x1, 1 = 2x2, etc
    public GameObject[] ONorth;
    public GameObject[] OSouth;
    public GameObject[] OEast;
    public GameObject[] OWest;

    //constructor for new cityBlock
    //bool street if using set of street blocks
    //int index for index of block objects
    public CityBlock(bool street, int index, GameObject[] blocklist)
    {
        if (street)
        {
            cityBlock_object = blocklist[index];
            isStreet = true;
        }
        else
        {
            cityBlock_object = GameObject.Find("OWP_Controller").GetComponent<OWP_Controller>().otherBlocks[index];
            isStreet = true;
        }


    }

    void Start()
    {
        
    }
}