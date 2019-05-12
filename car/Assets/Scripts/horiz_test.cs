using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class horiz_test : MonoBehaviour {

    //horizontal lerp variables
    public float horizontalLerp_distance;
    public float horizontalLerp_speed;
    public GameObject player;
    private Vector3 startPoint;

    private void Update()
    {
        //lerp point follows the car
        Vector3 lerpPoint = transform.position;

        //print(player.transform.eulerAngles.y);
        //Choose which direction to lerp to
        if (player.transform.eulerAngles.y >= 30 && player.transform.eulerAngles.y < 150)
        {
            print("RIGHT");
            lerpPoint.x += horizontalLerp_distance;
        }
        else if (player.transform.eulerAngles.y >= 210 && player.transform.eulerAngles.y < 330)
        {
            print("LEFT");
            lerpPoint.x -= horizontalLerp_distance;
        }
        else
        {
            print("CENTER");
            lerpPoint.x = this.transform.position.x;
        }

        //keep bounds in check
        if (lerpPoint.x <= this.transform.position.x - horizontalLerp_distance)
        {
            lerpPoint.x = this.transform.position.x - horizontalLerp_distance;
        }
        else if (lerpPoint.x >= this.transform.position.x + horizontalLerp_distance)
        {
            lerpPoint.x = this.transform.position.x + horizontalLerp_distance;
        }

        //perform lerp
        Vector3 desiredPosition = Vector3.Lerp(this.transform.position, lerpPoint, horizontalLerp_speed);
        this.transform.position = desiredPosition;
    }
}
