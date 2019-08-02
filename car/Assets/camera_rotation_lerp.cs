using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class camera_rotation_lerp : MonoBehaviour
{
    public GameObject player;
    public float verticle_lerp_amount = 20f;
    public float horizontal_lerp_amount = 25f;
    public float rotation_lerp_speed = 0.04f;

    private Vector3 target_rotation_vector;
    private Vector3 target_NORTH_rotation_vector;
    private Vector3 target_SOUTH_rotation_vector;
    private Vector3 target_EAST_rotation_vector;
    private Vector3 target_WEST_rotation_vector;

    // Start is called before the first frame update
    void Start()
    {
        target_NORTH_rotation_vector = new Vector3(verticle_lerp_amount * -1f, 0f, 0f);
        target_SOUTH_rotation_vector = new Vector3(verticle_lerp_amount, 0f, 0f);
        target_WEST_rotation_vector = new Vector3(0f, horizontal_lerp_amount * -1f, 0f);
        target_EAST_rotation_vector = new Vector3(0f, horizontal_lerp_amount, 0f);
    }

    // Update is called once per frame
    void Update()
    {
        //Check if player is pointed EAST
        if (player.transform.eulerAngles.y >= 45 && player.transform.eulerAngles.y < 135)
        {
            //print("EAST");
            target_rotation_vector = target_EAST_rotation_vector;
        }
        //Check if player is pointed SOUTH
        else if (player.transform.eulerAngles.y >= 135 && player.transform.eulerAngles.y < 225)
        {
            //print("SOUTH");
            target_rotation_vector = target_SOUTH_rotation_vector;
        }
        //Check if player is pointed WEST
        else if (player.transform.eulerAngles.y >= 225 && player.transform.eulerAngles.y < 315)
        {
            //print("WEST");
            target_rotation_vector = target_WEST_rotation_vector;
        }
        //Check if player is pointed NORTH
        else
        {
            //print("NORTH");
            target_rotation_vector = target_NORTH_rotation_vector;
        }

        //perform lerp
        Quaternion target_rotation = Quaternion.Euler(target_rotation_vector);
        //print(target_rotation_vector);
        target_rotation = Quaternion.Lerp(this.transform.localRotation, target_rotation, rotation_lerp_speed);
        this.transform.localRotation = target_rotation;
    }
}
