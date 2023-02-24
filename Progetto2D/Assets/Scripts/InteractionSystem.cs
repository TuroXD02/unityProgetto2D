using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractionSystem : MonoBehaviour
{

    public Transform detectionPoint;
    const float detectionRadius = 0.3f;
    public LayerMask detectionLayer;
    //cached trigegr object
    public GameObject detectedObject;

    void Update()
    {
        if (DetectObject())
        {
            if (InteractInput())
            {
                detectedObject.GetComponent<Item>().Interact();
            }
        }
    }

    bool InteractInput()
    {
        return Input.GetButtonDown("Interact");
    }

    bool DetectObject()
    {
        Collider2D obj = Physics2D.OverlapCircle(detectionPoint.position, detectionRadius, detectionLayer);
        if (obj == null)
        {
            detectedObject = null;

            return false;
        }
        else
        {
            detectedObject = obj.gameObject;
            return true;
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawSphere(detectionPoint.position, detectionRadius);
    }

}