using System;
using System.Collections;
using System.Collections.Generic;
using Items;
using Orders;
using Pages;
using UI___Menu;
using Unity.Mathematics;
using UnityEditor;
using UnityEngine;
using UnityEngine.Serialization;
using static System.Random;
using Random = System.Random;

public class GameManager : MonoBehaviour
{
    [FormerlySerializedAs("_orderManager")] [SerializeField] OrderManager orderManager;
    [SerializeField] private GameObject pageObject;
    [SerializeField] private Book book;
    public LostPagesList lostPages;
    private List<Item> allPotionList;
    private List<Vector3> spawnPositions;
    private int runningTotal;

    public void Start()
    {
        allPotionList = orderManager.orders.GetAllItems();
    }

    //When new item is selected to order list without page being unlocked, it will spawn the lost page in a random lost page spawn point.
    public void Spawn(Item potion)
    {
        spawnPositions = lostPages.GetLostPages();
        var random = new Random();
        if (!allPotionList.Contains(potion)) return;
        if (potion.GetDifficulty() == Item.Difficulty.Easy)
            return;
        var index = random.Next(0, spawnPositions.Count - 1);
        var potionPage = Instantiate(pageObject, spawnPositions[index], Quaternion.identity);
        potionPage.GetComponent<Page>().potion = potion.GetComponent<Potion>();
        Debug.Log("Lost Page Spawned for: " + potion.GetName());
        potionPage.GetComponent<Page>().ShowPotionList();
        allPotionList.Remove(potion);

    }
    
    public void PageFound(Item potion)
    {
        book.AddPotionToList(potion);
        Debug.Log("Page Found called ");
    }
    
}
