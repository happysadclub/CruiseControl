using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class activate_tooltip : MonoBehaviour
{
    public GameObject tooltip_obj;
    private bool showTooltip = true;

    // Start is called before the first frame update
    void Start()
    {
        if (showTooltip)
        {
            //show tooltip once
            tooltip_obj.SetActive(true);
            showTooltip = false;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
