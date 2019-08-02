using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CanShootMenuCar : MonoBehaviour {

	private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            //disable canMove
            other.GetComponent<menuCarController>().canMove = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            //enable canMove
            other.GetComponent<menuCarController>().canMove = false;
        }
    }
}
