using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class pause_script_test : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.P))
        {
            Time.timeScale = 0.1f;
        }
        else if (Input.GetKeyUp(KeyCode.P))
        {
            Time.timeScale = 1.0f;
        }
    }
}
