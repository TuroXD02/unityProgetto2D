using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class InventorySystem : MonoBehaviour
{
    public List<GameObject> items = new List<GameObject>();
    public bool isOpen;

    [Header("UI Inventory")]
    public GameObject ui_Window;
    public Image[] items_images;    

    [Header("UI Item Description")]
    public GameObject ui_Description_Window;
    public Image description_Image;
    public Text description_Title;

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
            Debug.Log(items_images);            
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
    }

}
