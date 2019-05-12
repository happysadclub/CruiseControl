using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Horizontal_Camera_Lerp : MonoBehaviour {

    //horizontal lerp variables
    public float horizontalLerp_distance;
    public float horizontalLerp_speed;
    public GameObject player;
    private Vector3 startPoint;

    private void Update()
    {
        //lerp point follows the car
        Vector3 lerpPoint = transform.localPosition;

        //print(player.transform.eulerAngles.y);
        //Choose which direction to lerp to
        if (player.transform.eulerAngles.y >= 30 && player.transform.eulerAngles.y < 150)
        {
                //print("RIGHT");
                lerpPoint.x += horizontalLerp_distance;
        }
        else if (player.transform.eulerAngles.y >= 210 && player.transform.eulerAngles.y < 330)
        {
                //print("LEFT");
                lerpPoint.x -= horizontalLerp_distance;
        }
        else
        {
            //print("CENTER");
            lerpPoint.x = 0f;
        }

        //keep bounds in check
        if (lerpPoint.x <= horizontalLerp_distance * -1f)
        {
            lerpPoint.x = horizontalLerp_distance * -1f;
        }
        else if (lerpPoint.x >= horizontalLerp_distance)
        {
            lerpPoint.x = horizontalLerp_distance;
        }

        //perform lerp
        Vector3 desiredPosition = Vector3.Lerp(this.transform.localPosition, lerpPoint, horizontalLerp_speed);
        this.transform.localPosition = desiredPosition;
    }
}