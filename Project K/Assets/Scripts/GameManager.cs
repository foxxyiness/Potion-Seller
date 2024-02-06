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
    private List<Item> _orderList;
    private List<Vector3> _spawnPositions;
    private int _runningTotal;

    public void Start()
    {
        _orderList = orderManager.orders.GetAllItems();
    }

    //Spawn Pages determines the game difficulty and randomizes the pages to spawn in the random locations, but limiting the amount that spawns at a time. 
    //Pages spawned will be added to the pool of potential orders to be on the list
    public void SpawnPages(OrderManager.Difficulty difficulty)
    {
        _spawnPositions = lostPages.getMediumPages();
        var random = new Random();
        switch (difficulty)
        {
            case OrderManager.Difficulty.Easy:
                _runningTotal = 4;
                _orderList = orderManager.orders.GetMediumItems();
                foreach (var item in _orderList)
                {
                    if (_runningTotal == 0)
                    {
                        break;
                    }
                    var index = random.Next(0, _spawnPositions.Count - 1);
                    Instantiate(item, _spawnPositions[index], Quaternion.identity);
                    _orderList.Remove(item);
                    _runningTotal--;
                }
                break;
            
        }
    }

    public void Spawn(Item _item)
    {
        _spawnPositions = lostPages.getMediumPages();
        var random = new Random();
        if (!_orderList.Contains(_item)) return;
        if (_item.GetDifficulty() == Item.Difficulty.Easy)
            return;
        var index = random.Next(0, _spawnPositions.Count - 1);
        Instantiate(_item, _spawnPositions[index], Quaternion.identity);
        _orderList.Remove(_item);

    }
    
}
