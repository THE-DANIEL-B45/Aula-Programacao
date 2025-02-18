using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CraftingManagerAula4 : MonoBehaviour
{
    private ItemAula4 currentItem;
    public Image customCursor;
    public SlotAula4[] craftingSlots;
    public List<ItemAula4> itemList;
    public SORecipes[] recipes;
    public ItemAula4[] recipeResults;
    public SlotAula4 resultSlot;

    private void Update()
    {
        if(Input.GetMouseButtonUp(0))
        {
            if(currentItem != null)
            {
                customCursor.gameObject.SetActive(false);
                SlotAula4 nearestSlot = null;
                float shortestDistance = float.MaxValue;
                foreach(SlotAula4 slot in craftingSlots)
                {
                    float dist = Vector2.Distance(Input.mousePosition, slot.transform.position);
                    if(dist < shortestDistance)
                    {
                        shortestDistance = dist;
                        nearestSlot = slot;
                    }
                }

                if (shortestDistance > 20)
                {
                    currentItem = null;
                    return;

                }

                nearestSlot.gameObject.SetActive(true);
                nearestSlot.GetComponent<Image>().sprite = currentItem.GetComponent<Image>().sprite;
                nearestSlot.item = currentItem;

                itemList[nearestSlot.index] = currentItem;

                currentItem = null;

                CheckForCompletedRecipes();
            }
        }
    }

    private void CheckForCompletedRecipes()
    {
        resultSlot.gameObject.SetActive(true);
        resultSlot.item = null;
        string currentRecipeString = "";
        foreach(ItemAula4 item in itemList)
        {
            if(item != null)
            {
                currentRecipeString += item.itemSO.itemName;
            }
            else
            {
                currentRecipeString += "null";
            }
        }
        Debug.Log(currentRecipeString);
        for(int i = 0; i < recipes.Length; i++)
        {
            if (recipes[i].recipe == currentRecipeString)
            {
                resultSlot.gameObject.SetActive(true);
                resultSlot.GetComponent<Image>().sprite = recipeResults[i].GetComponent<Image>().sprite;
                resultSlot.item = recipeResults[i];
            }
        }
    }

    public void OnClickSlot(SlotAula4 slot)
    {
        slot.item = null;
        itemList[slot.index] = null;
        slot.gameObject.SetActive(false);
        CheckForCompletedRecipes();
    }

    public void OnMouseDownItem(ItemAula4 item)
    {
        if(currentItem == null)
        {
            currentItem = item;
            customCursor.gameObject.SetActive(true);
            customCursor.sprite = currentItem.GetComponent<Image>().sprite;
        }
    }
}
