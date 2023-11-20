using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.Serialization;


public class Wallet : MonoBehaviour
{
    
    public int income;
    [SerializeField] int purchaseCost;
    public TextMeshProUGUI balanceText;
    public GameObject failPurchase;
    [SerializeField] private int balance;
    public bool isPurchased;
    public GameObject item;
    // Start is called before the first frame update
    void Start()
    {
        balance = 100;
        UpdateBalance();
    }

    private IEnumerator FailPurchase()
    {
        failPurchase.SetActive(true);
        yield return new WaitForSeconds(30);
        failPurchase.SetActive(false);
    }

    public void AddBalance()
    {
        balance += income;
       UpdateBalance();
    }

    private void UpdateBalance()
    {
        balanceText.text = balance.ToString();
    }

    public void RemoveBalance()
    {
        if (purchaseCost <= balance)
        {
            balance -= purchaseCost;
            UpdateBalance();
            //isPurchased = true;
            item = Instantiate(failPurchase, transform.position, transform.rotation);

        }
        else
        {
            //isPurchased = false;
            StartCoroutine(FailPurchase());
        }
        
    }
    
}
