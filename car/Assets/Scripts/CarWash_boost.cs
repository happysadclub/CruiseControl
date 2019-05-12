using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarWash_boost : MonoBehaviour
{

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            //get player
            GameObject player = GameObject.Find("Player_Car");

            //turn engine stuff on
            //player.transform.Find("Chainsaw_left").gameObject.SetActive(true);
            //player.transform.Find("Chainsaw_right").gameObject.SetActive(true);
            player.transform.Find("Engine").gameObject.SetActive(true);
            player.GetComponent<Movement_and_Boost_Powerup>().BoostPowerup = true;


            //turn everything else off
            player.transform.Find("leftGun").gameObject.SetActive(false);
            player.transform.Find("rightGun").gameObject.SetActive(false);
            player.GetComponent<Gun_Powerup>().GunPowerup = false;
        }
    }
}
