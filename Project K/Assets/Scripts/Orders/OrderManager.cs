using System.Collections;
using System.Collections.Generic;
using Items;
using TMPro;
using UI___Menu;
using UnityEngine;
using Random = System.Random;

namespace Orders
{
    public class OrderManager : MonoBehaviour
    {
        [SerializeField] private MenuManager menuManager;
        [SerializeField] private AudioSource orderManagerAudio;
        [SerializeField] private AudioClip deathChime;
         public TextMeshProUGUI textContent;
         public GameObject orderUIContent;
        //private TextMeshProUGUI _textMeshProUGUI;
        public Orders _orders;
        
        private readonly int[] _levelState = { 1, 2, 3, 4, 5 };

       
        public enum Difficulty
        {
            VeryEasy,
            Easy,
            Medium,
            Hard,
            VeryHard
        }
        [SerializeField]
        private Difficulty currentState = Difficulty.VeryEasy;

        public Difficulty getCurrentState => currentState;

        public List<Item> _itemsOnOrder;
        private void ItemsOnOrderUI()
        {
            foreach (Item item in _itemsOnOrder)
            {
                TextMeshProUGUI text = Instantiate(textContent, orderUIContent.transform.position, orderUIContent.transform.rotation, orderUIContent.transform);
                text.text = item.GetName();
                //text.transform.SetParent(orderUIContent.transform);
                text.autoSizeTextContainer = true;
                text.transform.localScale = new Vector3(1f, 1f, 1f);
            }
            
        }

        private void Start()
        {
            StartCoroutine(StartOfDay());
            orderManagerAudio.clip = deathChime;
        }

        // ReSharper disable Unity.PerformanceAnalysis
        public IEnumerator StartOfDay()
        {
            if (_itemsOnOrder.Count > 0)
            {
                StartCoroutine(Death());
                _itemsOnOrder.Clear();
                yield break;
            }
            _itemsOnOrder.Clear();
            if (orderUIContent.GetComponentsInChildren<TextMeshProUGUI>() != null)
            {
                foreach (TextMeshProUGUI text in orderUIContent.GetComponentsInChildren<TextMeshProUGUI>())
                {
                    Destroy(text.gameObject);
                }
            }
            yield return new WaitForSeconds(2);
            GetOrders(currentState);
        }

        //Death Coroutine. Runs When day is complete but not all orders were complete. Starts Chime and loads Scene ****
        private IEnumerator Death()
        {
            orderManagerAudio.Play();
            DontDestroyOnLoad(orderManagerAudio);
            if (orderManagerAudio.isPlaying)
            {
                yield return new WaitForSeconds(12);
                menuManager.DeathLoad();
            }
            else
            {
                menuManager.Death();
            }
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
            ItemsOnOrderUI();
        }

    
        public void AddDifficulty()
        {
            currentState++;
        }
        
    }
}
    
