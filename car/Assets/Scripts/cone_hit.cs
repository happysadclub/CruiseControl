using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cone_hit : MonoBehaviour
{
    private bool hit = false;

    private void OnTriggerEnter(Collider other)
    {
        if (!hit)
        {
            if (other.gameObject.CompareTag("Player") || other.gameObject.CompareTag("destructible") || other.gameObject.CompareTag("bullet") || other.gameObject.CompareTag("chainsaw"))
            {
                hit = true;
                //add score
                GameObject.Find("Director").GetComponent<PointGainControl>().increaseScore();
            }
        }
    }

}
