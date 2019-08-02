using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DestroyMenuCar : MonoBehaviour {

    public GameObject menuCar;

    public GameObject myCamera;

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

    public GameObject explosion;

    public void initiateDestroyMenuCar()
    {
        //menuCar.GetComponent<menuCarController>().enabled = false;
        menuCar.GetComponent<Rigidbody>().velocity = new Vector3(0,0,0);
        disableKinematic();
        //explosion
        explosion.SetActive(true);

        //fade to black
        //myCamera.GetComponent<fadeOut>().triggerFadeOut();

        StartCoroutine(reloadLevel());
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
    }

    IEnumerator reloadLevel()
    {
        //wait 2 seconds
        yield return new WaitForSeconds(5f);
        //load level
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
