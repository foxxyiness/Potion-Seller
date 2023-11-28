using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Unity.Mathematics;
using UnityEngine.Serialization;


public class Wallet : MonoBehaviour
{
    
    public int income;
    [SerializeField] int purchaseCost;
    public TextMeshProUGUI balanceText;
    public GameObject failPurchase;
    [SerializeField] private PlayerWallet playerWallet;
    private int balance;
    public bool isPurchased;
    public GameObject item;
    // Start is called before the first frame update
    void Start()
    {
        UpdateBalance();
    }

    private IEnumerator FailPurchase()
    {
        failPurchase.SetActive(true);
        yield return new WaitForSeconds(30);
        failPurchase.SetActive(false);
    }
    private void UpdateBalance()
    {
        balance = playerWallet.GetBalance();
        balanceText.text = balance.ToString();
    }

    public void RemoveBalance()
    {
        if (purchaseCost <= balance)
        {
            playerWallet.RemoveBalance(purchaseCost);
            SpawnItem();
        }
        else
        {
            //isPurchased = false;
            StartCoroutine(FailPurchase());
        }
        
    }

    private void SpawnItem()
    {
        var spawnItem = Instantiate(item, playerWallet.GetComponentInParent<Transform>().position, quaternion.identity);
    }
}
