using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    public static Inventory instance;

    [Range(1, 10)] public int inventorySpacesAmount;

    public List<Item> items;

    [SerializeField] ItemSlot itemSlotPrefab;

    public List<ItemSlot> itemSlotList;

    public GameObject inventoryObj;


    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        CreateInventory();
    }

    void CreateInventory()
    {
        for (int i = 0; i < inventorySpacesAmount; i++)
        {
            ItemSlot obj = Instantiate(itemSlotPrefab, inventoryObj.transform);
            obj.gameObject.SetActive(false);
            itemSlotList.Add(obj);
        }
    }

    public void AddBackpackSpace(int amountToAdd)
    {
        for(int i = 0; i < amountToAdd; i++)
        {
            ItemSlot obj = Instantiate(itemSlotPrefab, inventoryObj.transform);
            obj.gameObject.SetActive(false);
            itemSlotList.Add(obj);
        }

        inventorySpacesAmount += amountToAdd;
    }

    public static void SetItem(Item item)
    {
        if (instance == null) return;

        if (instance.HasItem(item))
        {
            instance.items.Add(item);
            SetItemAmount(item);
        }
        else
        {
            instance.items.Add(item);
            SetInventoryImages(item);
        }
    }

    public static void RemoveItem(Item item)
    {
        if (instance == null) return;

        instance.items.Remove(item);
        SetItemAmount(item, true);

    }

    public bool HasItem(Item item)
    {
        return instance.items.Contains(item);
    }


    public static void SetInventoryImages(Item item)
    {
        if (instance == null) return;

        foreach (ItemSlot itemSlot in instance.itemSlotList)
        {
            if (!itemSlot.gameObject.activeInHierarchy)
            {
                itemSlot.gameObject.SetActive(true);
                itemSlot.image.sprite = item.itemImage;
                break;
            }
        }
    }

    public static void SetItemAmount(Item item, bool remove = false)
    {
        Sprite targetSprite = item.itemImage;

        foreach (ItemSlot itemSlot in instance.itemSlotList)
        {
            if (itemSlot.image.sprite == targetSprite)
            {
                if (remove)
                {
                    if (int.Parse(itemSlot.itemAmountText.text) == 1)
                    {
                        int newNumber = int.Parse(itemSlot.itemAmountText.text) - 1;
                        itemSlot.image.sprite = null;
                        itemSlot.gameObject.SetActive(false);
                        break;
                    }
                    else
                    {
                        int newNumber = int.Parse(itemSlot.itemAmountText.text) - 1;
                        itemSlot.itemAmountText.text = newNumber.ToString();
                        break;
                    }
                }
                else
                {
                    int newNumber = int.Parse(itemSlot.itemAmountText.text) + 1;
                    itemSlot.itemAmountText.text = newNumber.ToString();
                    break;
                }

            }
        }

    }

    public bool InventoryFull()
    {
        int activeSlots = 0;

        foreach(ItemSlot itemSlot in itemSlotList)
        {
            if(itemSlot.gameObject.activeInHierarchy)
            {
                activeSlots++;
            }
        }

        if(activeSlots == inventorySpacesAmount) return true;
        else return false;
    }
}
