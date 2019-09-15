using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionCheck : MonoBehaviour
{
    public LayerMask m_LayerMask;

    public Collider[] collidersTouched;

    public GameObject myArea;

    public bool IsInappropriateTouchingHappening (GameObject allowedChunk)
    {   
        BoxCollider area = myArea.GetComponent<BoxCollider>();
        collidersTouched = Physics.OverlapBox(area.bounds.center, area.bounds.size/2, Quaternion.identity, m_LayerMask);

        foreach (Collider col in collidersTouched)
        {
            if (col.gameObject.tag == "area" 
                && !GameObject.ReferenceEquals(myArea, col.gameObject)
                && !GameObject.ReferenceEquals(allowedChunk, col.gameObject.transform.parent.gameObject))
            {
                Debug.Log(gameObject.name + " at " + transform.position + " inappropriately touched " + col.gameObject.transform.parent.gameObject.name + " at " + col.gameObject.transform.position);
                return true;
            }
        }
        return false;
    }

    void OnDrawGizmos ()
    {
        // Gizmos.color = Color.blue;
        BoxCollider area = transform.Find("area").gameObject.GetComponent<BoxCollider>();
        // Gizmos.DrawWireCube(area.bounds.center, area.bounds.size);
        Gizmos.matrix = Matrix4x4.TRS(area.bounds.center, Quaternion.identity, area.bounds.size);
        Gizmos.color = Color.red;
        Gizmos.DrawCube(Vector3.zero, Vector3.one);
    }
}
