using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarWash_gun : MonoBehaviour {

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            //get player
            GameObject player = GameObject.Find("Player_Car");

            //turn gun stuff on
            player.transform.Find("leftGun").gameObject.SetActive(true);
            player.transform.Find("rightGun").gameObject.SetActive(true);
            player.GetComponent<Gun_Powerup>().GunPowerup = true;

            //turn everything else off
            player.transform.Find("Chainsaw_left").gameObject.SetActive(false);
            player.transform.Find("Chainsaw_right").gameObject.SetActive(false);
            player.transform.Find("Engine").gameObject.SetActive(false);
            player.GetComponent<Movement_and_Boost_Powerup>().BoostPowerup = false;
        }
    }
}
