using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public static UIManager instance;

    public ItemSlot[] itemSlotList;

    private void Awake()
    {
        if (instance == null) instance = this;
        else Destroy(gameObject);
    }

    public static void SetInventoryImages(Item item)
    {
        if (instance == null) return;
        Debug.Log(instance.itemSlotList.Length);

        foreach(ItemSlot itemSlot in instance.itemSlotList)
        {
            if(!itemSlot.gameObject.activeInHierarchy)
            {
                itemSlot.gameObject.SetActive(true);
                itemSlot.image.sprite = item.itemImage;
                break;
            }
        }


        /*for(int i = 0; i < instance.inventoryImages.Length; i++)
        {
            if (!instance.inventoryImages[i].gameObject.activeInHierarchy)
            {
                instance.inventoryImages[i].sprite = item.itemImage;
                instance.inventoryImages[i].gameObject.SetActive(true);
                break;
            }
        }*/
    }

    public static void SetItemAmount(Item item)
    {
        Sprite targetSprite = item.itemImage;

        foreach(ItemSlot itemSlot in instance.itemSlotList)
        {
            if(itemSlot.image.sprite == targetSprite)
            {
                int newNumber = int.Parse(itemSlot.itemAmountText.text) + 1;
                itemSlot.itemAmountText.text = newNumber.ToString();
                break;
            }
        }
    }

}
