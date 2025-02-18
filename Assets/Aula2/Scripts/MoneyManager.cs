using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class MoneyManager : MonoBehaviour
{
    public static MoneyManager instance;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }
    public int currentMoney;
    [SerializeField] TMP_Text moneyText;

    private void Start()
    {
        if(PlayerPrefs.HasKey("CurrentMoney"))
        {
            currentMoney = PlayerPrefs.GetInt("CurrentMoney");
        }
        else
        {
            currentMoney = 0;
            PlayerPrefs.SetInt("CurrentMoney", 0);
        }
        moneyText.text = "Gold " + currentMoney.ToString();
    }

    public void AddMoney(int moneyToAdd)
    {
        currentMoney += moneyToAdd;
        PlayerPrefs.SetInt("CurrentMoney", currentMoney);
        moneyText.text = "Gold: " + currentMoney.ToString();
    }
}
