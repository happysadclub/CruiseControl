using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarWash_reset : MonoBehaviour {

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            //get player
            GameObject player = GameObject.Find("Player_Car");

            //turn everything else off
            player.GetComponent<Movement_and_Boost_Powerup>().BoostPowerup = false;
            player.GetComponent<Gun_Powerup>().GunPowerup = false;
            player.transform.Find("leftGun").gameObject.SetActive(false);
            player.transform.Find("rightGun").gameObject.SetActive(false);
            player.transform.Find("Chainsaw_left").gameObject.SetActive(false);
            player.transform.Find("Chainsaw_right").gameObject.SetActive(false);
            player.transform.Find("Engine").gameObject.SetActive(false);
        }
    }
}
