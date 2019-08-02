using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class menu_change_PlayerMovement : MonoBehaviour {

    public GameObject player;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            player.gameObject.GetComponent<GDC_car_menu_controller>().enableMove();
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            player.gameObject.GetComponent<GDC_car_menu_controller>().enableMove();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            player.gameObject.GetComponent<GDC_car_menu_controller>().disableMove();
        }
    }
}
