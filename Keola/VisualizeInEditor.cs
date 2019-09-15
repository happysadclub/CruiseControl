using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VisualizeInEditor : MonoBehaviour
{
    void OnDrawGizmos () {
        Gizmos.color = Color.red;
        Gizmos.DrawRay (transform.position, transform.forward);
    }
}
