using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InteractionSystem : MonoBehaviour
{

    public Transform detectionPoint;
    const float detectionRadius = 0.3f;
    public LayerMask detectionLayer;
    public GameObject detectedObject;

    [Header("Examine")]
    public GameObject examineWindow;
    public Image examineImage;
    public Text examineText;
    public bool isExamining = false;



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

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawSphere(detectionPoint.position, detectionRadius);
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


    public void ExamineItem(Item item)
    {
        if(isExamining)
        {
            examineWindow.SetActive(false);
            isExamining = false;     
        }
        else
        {
            examineImage.sprite = item.GetComponent<SpriteRenderer>().sprite;
            examineText.text = item.descriptionText;
            examineWindow.SetActive(true);
            isExamining = true;
        }
    }

}