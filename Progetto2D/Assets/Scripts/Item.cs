using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BoxCollider2D))]
public class Item : MonoBehaviour
{
    public enum InteractionType { NONE, PickUp, Examine}
    public InteractionType type;

    void Reset()
    {
        //spunta Is Trigger e assegna layer Item
        GetComponent<Collider2D>().isTrigger = true;
        gameObject.layer = 7;
    }

    public void Interact()
    {
        switch(type)
        {
            case InteractionType.PickUp:
                Debug.Log("PickUp");
                break;

            case InteractionType.Examine:
                Debug.Log("Examine");
                break;
            default:
                Debug.Log("NULL");
                break;
        }
    }
}
