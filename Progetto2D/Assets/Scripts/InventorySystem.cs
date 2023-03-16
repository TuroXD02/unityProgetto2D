using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class InventorySystem : MonoBehaviour
{
    [System.Serializable]
    public class InventoryItem
    {
        public GameObject obj;
        public int stack = 1;

        public InventoryItem(GameObject o, int s= 1)
        {
            obj = o;
            stack = s;
        }

    }

    [Header("General Fields")]
    public List<InventoryItem> items = new List<InventoryItem>();
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
        if(item.GetComponent<Item>().stackable)
        {
            InventoryItem existingItem = items.Find(x => x.obj.name == item.name);
            if(existingItem!=null)
            {
                existingItem.stack++;
            }
            else
            {
                InventoryItem i = new InventoryItem(item);
                items.Add(i);
            }
        }
        else
        {
            InventoryItem i = new InventoryItem(item);
            items.Add(i);
        }

        Update_UI();
    }

    void Update_UI()
    {
        HideAll();
        //mostra ogni item nella rispettiva casella
        for (int i = 0; i<items.Count;i++)
        {
                        
            items_images[i].sprite = items[i].obj.GetComponent<SpriteRenderer>().sprite;
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

        if (items[id].stack == 1)
        {
            description_Title.text = items[id].obj.name;
        }
        else
        {
            description_Title.text = items[id].obj.name + " x" + items[id].stack;
        }

        description_Text.text= items[id].obj.GetComponent<Item>().descriptionText;
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
        if (items[id].obj.GetComponent<Item>().type == Item.ItemType.Consumable)
        {
            items[id].obj.GetComponent<Item>().consumeEvent.Invoke();
            items[id].stack--;
            if (items[id].stack == 0)
            {
                Destroy(items[id].obj, 0.1f);
                items.RemoveAt(id);
            }
            Update_UI();

        }
    }

    public bool CanPickUp()
    {
        if (items.Count>=items_images.Length)
        {
            return false;
        }
        else
        {
            return true;
        }
    }

}
