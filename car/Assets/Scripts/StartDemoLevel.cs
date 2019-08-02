using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartDemoLevel : MonoBehaviour {

    public GameObject myCamera;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            //fade to black
            myCamera.GetComponent<fadeOut>().triggerFadeOut();

            StartCoroutine(loadLevel());
            
        }
    }

    IEnumerator loadLevel()
    {
        //wait 2 seconds
        yield return new WaitForSeconds(1f);
        //load level
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
}
