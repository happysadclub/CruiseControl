using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gatekeeper : MonoBehaviour
{
    private bool closed = false;
    [SerializeField] private BoxCollider gate;

    void Start()
    {
        gate = GetComponent<BoxCollider>();
    }

    public void Close() 
    {
        closed = true;
        gate.enabled = true;
    }

    public bool isClosed() 
    {
        return closed;
    }

}
