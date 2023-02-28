using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(BoxCollider2D))]
public class Item : MonoBehaviour
{
    public enum InteractionType { NONE, PickUp, Examine}
    public InteractionType type;

    [Header("Examine")]
    public string descriptionText;
    public UnityEvent customEvent;

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
                FindObjectOfType<InventorySystem>().PickUp(gameObject); //aggiungi alla lista
                gameObject.SetActive(false);    
                break;
            case InteractionType.Examine:
                FindObjectOfType<InteractionSystem>().ExamineItem(this); 
                break;
            default:
                Debug.Log("NULL");
                break;
        }

        customEvent.Invoke();
    }
}
