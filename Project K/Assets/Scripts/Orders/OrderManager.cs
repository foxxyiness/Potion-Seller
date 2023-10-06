using System;
using System.Collections;
using System.Collections.Generic;
using Items;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Serialization;
using Random = System.Random;

namespace Orders
{
    public class OrderManager : MonoBehaviour
    {
       
        public Orders _orders;
        
        private readonly int[] _levelState = { 1, 2, 3, 4, 5 };

        private enum Difficulty
        {
            VeryEasy,
            Easy,
            Medium,
            Hard,
            VeryHard
        }
        [SerializeField]
        private Difficulty currentState = Difficulty.VeryEasy;
        [SerializeField]
        public List<Item> _itemsOnOrder;

        private void Start()
        {
            StartCoroutine(StartOfDay());
        }

        // ReSharper disable Unity.PerformanceAnalysis
        private IEnumerator StartOfDay()
        {
            _itemsOnOrder.Clear();
            yield return new WaitForSeconds(2);
            GetOrders(currentState);
        }
        private void GetOrders(Difficulty difficulty)
        {
            var rand = new Random();
            switch (difficulty)
            {
                //Level 1 has all Easy items **************************************************************************
                case Difficulty.VeryEasy:
                    for (var i = 0; i < 5; i++)
                    {
                        _itemsOnOrder.Add(_orders.GetSingleEasyOrder());
                    }
                    break;
                //Adds Probability of having medium item to 40%, easy to 50%, hard 10%
                case Difficulty.Easy:
                    for (var i = 0; i <= rand.Next(5, 7); i++)
                    {
                        var probability = rand.Next(1, 101);
                        _itemsOnOrder.Add(probability <= 13 ? _orders.GetSingleMediumOrder() : _orders.GetSingleEasyOrder());
                    }
                    break;
                //Adds Probability of having medium item to 40%, easy to 50%, hard 10%
                case Difficulty.Medium:
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
                //Adds Probability of having medium to 35%, easy 45%, hard to 20%
                case Difficulty.Hard:
                    for (var i = 0; i <= rand.Next(10, 12); i++)
                    {
                        var probability = rand.Next(1, 101);
                        if (probability <= 35)
                        {
                            _itemsOnOrder.Add(_orders.GetSingleMediumOrder());
                        }
                        else if (probability is >= 36 and <= 80)
                        {
                            _itemsOnOrder.Add(_orders.GetSingleEasyOrder());
                        }
                        else
                        {
                            _itemsOnOrder.Add(_orders.GetSingleHardOrder());
                        }
                    }
                    break;
                //Easy, Med, Hard 33%
                case Difficulty.VeryHard:
                    for (var i = 0; i <= rand.Next(12, 15); i++)
                    {
                        var probability = rand.Next(1, 101);
                        if (probability <= 33)
                        {
                            _itemsOnOrder.Add(_orders.GetSingleMediumOrder());
                        }
                        else if (probability is >= 34 and <= 67)
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
                    goto case Difficulty.VeryEasy;
            }
            Debug.Log(_itemsOnOrder);
        }
    }
}
    
