using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Orders;
using UnityEditor;
using Random = System.Random;

public class OrderManager : MonoBehaviour
{
    private static Orders.Orders _orders;
    private readonly int[] _levelState = { 1, 2, 3, 4, 5 };
    private int _currentState;
    private List<Item> _itemsOnOrder;

    void Awake()
    {
        _currentState = _levelState[0];

    }

    private void GetOrders(int _currentState)
    {
        List<Item> _listOfItems;
        int _listCount;
        switch (_currentState)
        {
            case 1:
                _listOfItems = _orders.GetEasyItems();
                Random rand = new Random();
                for (int i = 0; i < 5; i++)
                {
                    int num = rand.Next(0, _listOfItems.Count);
                    _itemsOnOrder.Append(_listOfItems[num]);
                }

                break;
            default:
                goto case 1;
        }
        Debug.Log(_itemsOnOrder);
    }
}
    
