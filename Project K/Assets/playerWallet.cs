using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PlayerWallet : MonoBehaviour
{
    [SerializeField] private int balance;
    public TextMeshProUGUI balanceText;

    private void Awake()
    {
        balance = 10000;
    }

    public int GetBalance()
    {
        return balance;
    }
    public void RemoveBalance(int purchaseCost)
    {
        balance -= purchaseCost;
        //balanceText.text = balance.ToString();
    }
    public void AddBalance(int price)
    {
        balance += price;
        balanceText.text = balance.ToString();
    }
}
