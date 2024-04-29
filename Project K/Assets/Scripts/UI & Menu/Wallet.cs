using System.Collections;
using System.Collections.Generic;
using Items;
using UnityEngine;
using TMPro;
using Unity.Mathematics;
using UnityEngine.Serialization;

//Class intended for each Specific Shop
public class Wallet : MonoBehaviour
{
    
    public int income;
    [SerializeField] int purchaseCost;
    public TextMeshProUGUI balanceText;
    public GameObject failPurchase;
    [SerializeField] private PlayerWallet playerWallet;
    [SerializeField]private int balance;
    public bool isPurchased;
    public GameObject item;
    // Start is called before the first frame update
    void Start()
    {
        UpdateBalance();
        //Gets purchase Cost from Item
        purchaseCost = item.GetComponent<Item>().GetCost();
    }

    private IEnumerator FailPurchase()
    {
        failPurchase.SetActive(true);
        yield return new WaitForSeconds(30);
        failPurchase.SetActive(false);
    }
    private void UpdateBalance()
    {
       // balance = playerWallet.GetBalance();
        balanceText.text = balance.ToString();
    }

    public void RemoveBalance()
    {
        if (purchaseCost <= balance)
        {
            playerWallet.RemoveBalance(purchaseCost);
            UpdateBalance();
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
        var spawnItem = Instantiate(item, playerWallet.GetComponentInParent<Transform>().position + Vector3.up, quaternion.identity);
    }
}
