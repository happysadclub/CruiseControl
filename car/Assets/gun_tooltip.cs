using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class gun_tooltip : MonoBehaviour
{
    //private bool showTooltip = true;
    //public GameObject gunTooltip_obj;
    public KeyCode interactKey = KeyCode.Mouse0;
    public float slow_lerp_speed = 0.1f;
    private bool showingTooltip = true;

    // Start is called before the first frame update
    void Start()
    {
        //Time.timeScale = 0.1f;
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(interactKey))
        {
            showingTooltip = false;
        }

        if (showingTooltip)
        {
            Time.timeScale = Mathf.Lerp(Time.timeScale, 0.1f, slow_lerp_speed);
        }
        else
        {
            Time.timeScale = 1.0f;
            this.gameObject.SetActive(false);
        }
    }
}
