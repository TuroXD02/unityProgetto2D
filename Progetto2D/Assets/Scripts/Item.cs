using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(BoxCollider2D))]
public class Item : MonoBehaviour
{
    public enum InteractionType { NONE, PickUp, Examine, Drag}
    public enum ItemType { Static, Consumable }

    [Header("Attribute")]
    public ItemType type;    
    public InteractionType interactType;

    [Header("Examine")]
    public string descriptionText;

    [Header("Custom event")]
    public UnityEvent customEvent;
    public UnityEvent consumeEvent;


    void Reset()
    {
        //spunta Is Trigger e assegna layer Item
        GetComponent<Collider2D>().isTrigger = true;
        gameObject.layer = 7;
        GetComponent<Transform>().position = new Vector3(0,0,1);
    }

    public void Interact()
    {
        switch(interactType)
        {
            case InteractionType.PickUp:
                FindObjectOfType<InventorySystem>().PickUp(gameObject); //aggiungi alla lista
                gameObject.SetActive(false);    
                break;
            case InteractionType.Examine:
                FindObjectOfType<InteractionSystem>().ExamineItem(this);
                break;
            case InteractionType.Drag:
                FindObjectOfType<InteractionSystem>().Drag();
                break;
            default:
                Debug.Log("NULL");
                break;
        }

        customEvent.Invoke();
    }
}
