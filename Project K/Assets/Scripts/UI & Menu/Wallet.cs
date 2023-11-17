using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Wallet : MonoBehaviour
{
    
    public int income;
    public int payment;
    public TextMeshProUGUI balanceText;
    public GameObject failPurchase;
    public int balance;
    public bool isPurchased;
    // Start is called before the first frame update
    void Start()
    {
        balance = 0;
        
    }



    public void AddBalance()
    {
        balance = balance + income;
        balanceText.text = balance.ToString();
        income = 0;
    }

    public void RemoveBalance()
    {
        if (balance > 0)
        {
            balance = balance - payment;
            balanceText.text = balance.ToString();
            payment = 0;
            isPurchased = true;
        }
        else
        {
            isPurchased = false;
            failPurchase.SetActive(false);
            return;
        }
        
    }

    // Update is called once per frame
    void Update()
    {
        AddBalance();

        RemoveBalance();
    }
}
