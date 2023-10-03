using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = System.Random;

namespace Orders
{
    public class OrderManager : MonoBehaviour
    {
        private static Orders _orders;
        private readonly int[] _levelState = { 1, 2, 3, 4, 5 };
        private int _currentState;
        private List<Item> _itemsOnOrder;


        private void Awake()
        {
            _currentState = _levelState[0];
        }

        internal IEnumerator StartOfDay()
        {
            _itemsOnOrder.Clear();
            yield return new WaitForSeconds(2);
            GetOrders(_currentState);
        }
        private void GetOrders(int currentState)
        {
            var rand = new Random();
            switch (currentState)
            {
                //Level 1 has all Easy items **************************************************************************
                case 1:
                    for (var i = 0; i <= 5; i++)
                    {
                        _itemsOnOrder.Add(_orders.GetSingleEasyOrder());
                    }
                    break;
                //Adds Probability of having medium item to 40%, easy to 50%, hard 10%
                case 2:
                    for (var i = 0; i <= rand.Next(5, 7); i++)
                    {
                        var probability = rand.Next(1, 101);
                        _itemsOnOrder.Add(probability <= 13 ? _orders.GetSingleMediumOrder() : _orders.GetSingleEasyOrder());
                    }
                    break;
                //Adds Probability of having medium item to 40%, easy to 50%, hard 10%
                case 3:
                    for (var i = 0; i <= rand.Next(7, 10); i++)
                    {
                        var probability = rand.Next(1, 101);
                        if (probability <= 40)
                        {
                            _itemsOnOrder.Add(_orders.GetSingleMediumOrder());
                        }
                        else if (probability is >= 41 and <= 90)
                        {
                            _itemsOnOrder.Add(_orders.GetSingleEasyOrder());
                        }
                        else
                        {
                            _itemsOnOrder.Add(_orders.GetSingleHardOrder());
                        }
                    }
                    break;
                default:
                    Debug.LogWarning("State Difficulty not equal 1-5, defaulting to easy.");
                    goto case 1;
            }
            Debug.Log(_itemsOnOrder);
        }
    }
}
    
