using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class load_next_scene : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(load_next());
    }

    IEnumerator load_next()
    {
        //wait 2 seconds
        yield return new WaitForSeconds(2f);
        // Start an asynchronous operation to load the scene that was passed to the LoadNewScene coroutine.
        AsyncOperation async = Application.LoadLevelAsync(SceneManager.GetActiveScene().buildIndex + 1);
    }
}
