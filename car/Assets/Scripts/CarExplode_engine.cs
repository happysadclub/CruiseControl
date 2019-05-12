using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CarExplode_engine : MonoBehaviour {

    public GameObject playerCar;

    public GameObject car_body;
    public GameObject car_bumper;
    public GameObject car_door;
    public GameObject car_grill;
    public GameObject car_seats;
    public GameObject car_interior;
    public GameObject car_lights;
    public GameObject car_tailpipe;
    public GameObject car_window;
    public GameObject car_wheel1;
    public GameObject car_wheel2;
    public GameObject car_wheel3;
    public GameObject car_wheel4;
    public GameObject car_hinges;
    public GameObject engine;

    public GameObject explosion_particle;

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void initiateDestroyMenuCar()
    {
        //stop movement
        stopMovement();

        //disable turning
        testCarController carScript = playerCar.GetComponent<testCarController>();
        carScript.disableAllControls();

        //disable powerup
        Movement_and_Boost_Powerup boostScript = playerCar.GetComponent<Movement_and_Boost_Powerup>();
        boostScript.BoostPowerup = false;

        //parts explode
        disableKinematic();

        //explosion particle
        explosion_particle.SetActive(true);

        //reset scene
        StartCoroutine(resetScene());
    }

    private void disableKinematic()
    {
        car_body.GetComponent<Rigidbody>().isKinematic = false;
        car_bumper.GetComponent<Rigidbody>().isKinematic = false;
        car_door.GetComponent<Rigidbody>().isKinematic = false;
        car_grill.GetComponent<Rigidbody>().isKinematic = false;
        car_seats.GetComponent<Rigidbody>().isKinematic = false;
        car_interior.GetComponent<Rigidbody>().isKinematic = false;
        car_lights.GetComponent<Rigidbody>().isKinematic = false;
        car_tailpipe.GetComponent<Rigidbody>().isKinematic = false;
        car_window.GetComponent<Rigidbody>().isKinematic = false;
        car_wheel1.GetComponent<Rigidbody>().isKinematic = false;
        car_wheel2.GetComponent<Rigidbody>().isKinematic = false;
        car_wheel3.GetComponent<Rigidbody>().isKinematic = false;
        car_wheel4.GetComponent<Rigidbody>().isKinematic = false;
        car_hinges.GetComponent<Rigidbody>().isKinematic = false;
        engine.GetComponent<Rigidbody>().isKinematic = false;

        car_body.GetComponent<BoxCollider>().isTrigger = false;
        car_bumper.GetComponent<BoxCollider>().isTrigger = false;
        car_door.GetComponent<BoxCollider>().isTrigger = false;
        car_grill.GetComponent<BoxCollider>().isTrigger = false;
        car_seats.GetComponent<BoxCollider>().isTrigger = false;
        car_interior.GetComponent<BoxCollider>().isTrigger = false;
        car_lights.GetComponent<BoxCollider>().isTrigger = false;
        car_tailpipe.GetComponent<BoxCollider>().isTrigger = false;
        car_window.GetComponent<BoxCollider>().isTrigger = false;
        car_wheel1.GetComponent<BoxCollider>().isTrigger = false;
        car_wheel2.GetComponent<BoxCollider>().isTrigger = false;
        car_wheel3.GetComponent<BoxCollider>().isTrigger = false;
        car_wheel4.GetComponent<BoxCollider>().isTrigger = false;
        car_hinges.GetComponent<BoxCollider>().isTrigger = false;
        engine.GetComponent<BoxCollider>().isTrigger = false;
    }

    private void stopMovement()
    {
        playerCar.GetComponent<Rigidbody>().isKinematic = true;
    }

    IEnumerator resetScene()
    {
        yield return new WaitForSeconds(3f);
        SceneManager.LoadScene(0);
    }
}
