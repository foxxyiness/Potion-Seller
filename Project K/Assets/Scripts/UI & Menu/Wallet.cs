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
    public GameObject item;
    // Start is called before the first frame update
    void Start()
    {
        balance = 0;
        
    }

    public IEnumerator enumerator ()
    {
        failPurchase.SetActive(true);
        yield return new WaitForSeconds(30);
        failPurchase.SetActive(false);
    }

    public void AddBalance()
    {
        balance = balance + income;
        balanceText.text = balance.ToString();
        income = 0;
    }

    public void RemoveBalance()
    {
        if (balance >= payment)
        {
            balance = balance - payment;
            balanceText.text = balance.ToString();
            payment = 0;
            isPurchased = true;
            item = Instantiate(failPurchase, transform.position, transform.rotation);

        }
        else
        {
            isPurchased = false;
            
            StartCoroutine(enumerator());

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
