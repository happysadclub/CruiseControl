using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VisualizeColliders : MonoBehaviour
{
    void OnDrawGizmos() {
        Gizmos.color = Color.green;
        Gizmos.matrix = transform.localToWorldMatrix;
        Gizmos.DrawWireCube(Vector3.zero + GetComponent<BoxCollider>().center, GetComponent<BoxCollider>().size);
    }
}
