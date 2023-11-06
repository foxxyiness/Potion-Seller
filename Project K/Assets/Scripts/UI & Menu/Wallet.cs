using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Wallet : MonoBehaviour
{
    
    public int income;
    public int payment;
    public TextMeshProUGUI balanceText;
    public int balance;
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
        balance = balance - payment;
        balanceText.text = balance.ToString();
        payment = 0;
    }

    // Update is called once per frame
    void Update()
    {
        AddBalance();

        RemoveBalance();
    }
}
