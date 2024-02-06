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
    public LostPagesList lostPages;
    private List<Item> _allPotionList;
    private List<Vector3> _spawnPositions;
    private int _runningTotal;

    public void Start()
    {
        _allPotionList = orderManager.orders.GetAllItems();
    }

    //When new item is selected to order list without page being unlocked, it will spawn the lost page in a random lost page spawn point.
    public void Spawn(Item potionPage)
    {
        _spawnPositions = lostPages.GetLostPages();
        var random = new Random();
        if (!_allPotionList.Contains(potionPage)) return;
        if (potionPage.GetDifficulty() == Item.Difficulty.Easy)
            return;
        var index = random.Next(0, _spawnPositions.Count - 1);
        Instantiate(potionPage, _spawnPositions[index], Quaternion.identity);
        _allPotionList.Remove(potionPage);

    }
    
}
