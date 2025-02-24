using PowerUp;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mercante : MonoBehaviour
{
    public Item item1;
    public Item item2;
    public Item item3;
    public bool canBuy;
    [SerializeField] GameObject tradeCanvas;
    [SerializeField] GameObject backpackTradeCanvas;
    [SerializeField] GameObject tenisTradeCanvas;
    [SerializeField] int tenisPrice = 10;
    [SerializeField] int backpackPrice = 25;
    [SerializeField] GameObject backpackUI;
    bool backpackbought;

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
        {
            SellStuff();
            BuyStuff();
            tradeCanvas.SetActive(true);
        }
        else tradeCanvas.SetActive(false);
    }

    private void SellStuff()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            if (Inventory.instance.HasItem(item1))
            {
                Inventory.RemoveItem(item1);
                MoneyManager.instance.AddMoney(item1.price);
            }
            else if(Inventory.instance.HasItem(item2))
            {
                Inventory.RemoveItem(item2);
                MoneyManager.instance.AddMoney(item2.price);
            }
            else if (Inventory.instance.HasItem(item3))
            {
                Inventory.RemoveItem(item3);
                MoneyManager.instance.AddMoney(item3.price);
            }
        }
    }

    private void BuyStuff()
    {
        if(Input.GetKeyDown(KeyCode.F) && MoneyManager.instance.currentMoney >= tenisPrice)
        {
            PlayerController.instance.StartCoroutine(PlayerController.instance.ActivePowerUp(PowerUpType.Velocity));
            MoneyManager.instance.AddMoney(-tenisPrice);
        }
        if(Input.GetKeyDown(KeyCode.Q) && !backpackbought)
        {
            backpackbought = true;
            Inventory.instance.AddBackpackSpace(1);
            MoneyManager.instance.AddMoney(-backpackPrice);
            backpackTradeCanvas.SetActive(false);
            backpackUI.SetActive(true);
        }
    }
}
