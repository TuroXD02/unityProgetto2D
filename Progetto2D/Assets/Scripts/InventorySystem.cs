using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class InventorySystem : MonoBehaviour
{
    [Header("General Fields")]
    public List<GameObject> items = new List<GameObject>();
    public bool isOpen;

    [Header("UI Inventory")]
    public GameObject ui_Window;
    public Image[] items_images;    

    [Header("UI Item Description")]
    public GameObject ui_Description_Window;
    public Image description_Image;
    public Text description_Title;
    public Text description_Text;


    void Update ()
    {
        if(Input.GetButtonDown("Inventory"))
        {
            ToggleInventory();
        }
    }

    void ToggleInventory()
    {
        isOpen = !isOpen;   
        ui_Window.SetActive(isOpen);
        Update_UI();
    }

    public void PickUp(GameObject item)
    {
        items.Add(item);
        Update_UI();
    }

    void Update_UI()
    {
        HideAll();
        //mostra ogni item nella rispettiva casella
        for (int i = 0; i<items.Count;i++)
        {
                        
            items_images[i].sprite = items[i].GetComponent<SpriteRenderer>().sprite;
            items_images[i].gameObject.SetActive(true);
        }
    }

    void HideAll()
    {
        foreach (var i in items_images)
        {
            i.gameObject.SetActive(false);
        }
        HideDescription();
    }
    
    public void ShowDescription(int id)
    {
        description_Image.sprite = items_images[id].sprite;
        description_Title.text= items[id].name;
        description_Text.text= items[id].GetComponent<Item>().descriptionText;

        description_Image.gameObject.SetActive(true);  
        description_Title.gameObject.SetActive(true);
        description_Text.gameObject.SetActive(true);
    }

    public void HideDescription()
    {
        description_Image.gameObject.SetActive(false);
        description_Title.gameObject.SetActive(false);
        description_Text.gameObject.SetActive(false);
    }

    public void Consume(int id)
    {
        if (items[id].GetComponent<Item>().type== Item.ItemType.Consumable)
        {
            items[id].GetComponent<Item>().consumeEvent.Invoke();
            Destroy(items[id], 0.1f);
            items.RemoveAt(id);
            Update_UI();
        }
    }

}
