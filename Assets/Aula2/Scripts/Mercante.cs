using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mercante : MonoBehaviour
{
    public Item item1;
    public Item item2;
    public int price;
    public bool canBuy;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Player")
            canBuy = true;
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if(collision.tag == "Player")
            canBuy = false;
    }

    private void Update()
    {
        if(canBuy)
        BuyStuff();
    }

    private void BuyStuff()
    {
        if (Input.GetKeyDown(KeyCode.E) && (Inventory.instance.HasItem(item1) || Inventory.instance.HasItem(item2)))
        {
            if (Inventory.instance.HasItem(item1))
            {
                Inventory.RemoveItem(item1);
            }
            else
            {
                Inventory.RemoveItem(item2);
            }

            MoneyManager.instance.AddMoney(price);
        }
    }
}
