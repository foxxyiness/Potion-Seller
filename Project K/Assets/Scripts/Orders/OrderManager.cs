using System.Collections;
using System.Collections.Generic;
using Items;
using TMPro;
using UI___Menu;
using UnityEngine;
using UnityEngine.Serialization;
using Random = System.Random;

namespace Orders
{
    public class OrderManager : MonoBehaviour
    {
        [SerializeField] private MenuManager menuManager;
        [SerializeField] private GameManager gameManager;
        [SerializeField] private AudioSource orderManagerAudio;
        [SerializeField] private AudioClip deathChime;
         public TextMeshProUGUI textContent;
         public GameObject orderUIContent;
        //private TextMeshProUGUI _textMeshProUGUI;
        [FormerlySerializedAs("_orders")] public Orders orders;
        private readonly int[] levelState = { 1, 2, 3, 4, 5 };

       
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

        [FormerlySerializedAs("_itemsOnOrder")] public List<Item> itemsOnOrder;
        private void ItemsOnOrderUI()
        {
            foreach (Item item in itemsOnOrder)
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
            if (itemsOnOrder.Count > 0)
            {
                StartCoroutine(Death());
                itemsOnOrder.Clear();
                yield break;
            }
            itemsOnOrder.Clear();
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
                yield return new WaitForSeconds(16);
                menuManager.DeathLoad();
            }
            else
            {
                menuManager.Death();
            }
        }
        private void GetOrders(Difficulty difficulty)
        {
            Item item;
            var rand = new Random();
            switch (difficulty)
            {
                //Level 1 has all Easy items **************************************************************************
                case Difficulty.VeryEasy:
                    for (var i = 0; i < 5; i++)
                    {
                        itemsOnOrder.Add(orders.GetSingleEasyOrder());
                    }
                    break;
                //Adds Probability of having medium item to 40%, easy to 50%, hard 10%
                case Difficulty.Easy:
                    for (var i = 0; i <= rand.Next(5, 7); i++)
                    {
                        var probability = rand.Next(1, 101);
                        item = probability <= 13 ? orders.GetSingleMediumOrder() : orders.GetSingleEasyOrder();
                        itemsOnOrder.Add(item);
                        gameManager.Spawn(item);
                    }
                    break;
                //Adds Probability of having medium item to 40%, easy to 50%, hard 10%
                case Difficulty.Medium:
                    for (var i = 0; i <= rand.Next(7, 10); i++)
                    {
                        var probability = rand.Next(1, 101);
                        if (probability <= 40)
                        {
                            item = orders.GetSingleMediumOrder();
                            itemsOnOrder.Add(item);
                        }
                        else if (probability is >= 41 and <= 90)
                        {
                            item = orders.GetSingleEasyOrder();
                            itemsOnOrder.Add(item);
                        }
                        else
                        {
                            item = orders.GetSingleHardOrder();
                            itemsOnOrder.Add(item);
                        }
                        gameManager.Spawn(item);
                    }
                    break;
                //Adds Probability of having medium to 35%, easy 45%, hard to 20%
                case Difficulty.Hard:
                    for (var i = 0; i <= rand.Next(10, 12); i++)
                    {
                        var probability = rand.Next(1, 101);
                        if (probability <= 35)
                        {
                            item = orders.GetSingleMediumOrder();
                            itemsOnOrder.Add(item);
                        }
                        else if (probability is >= 36 and <= 80)
                        {
                            item = orders.GetSingleEasyOrder();
                            itemsOnOrder.Add(item);
                        }
                        else
                        {
                            item = orders.GetSingleHardOrder();
                            itemsOnOrder.Add(item);
                        }

                        gameManager.Spawn(item);
                    } 
                    break;
                //Easy, Med, Hard 33%
                case Difficulty.VeryHard:
                    for (var i = 0; i <= rand.Next(12, 15); i++)
                    {
                        var probability = rand.Next(1, 101);
                        if (probability <= 33)
                        {
                            item = orders.GetSingleMediumOrder();
                            itemsOnOrder.Add(item);
                        }
                        else if (probability is >= 34 and <= 67)
                        {
                            item = orders.GetSingleEasyOrder();
                            itemsOnOrder.Add(item);
                        }
                        else
                        {
                            item = orders.GetSingleHardOrder();
                            itemsOnOrder.Add(item);
                        } 
                        gameManager.Spawn(item);
                    }
                    break;
                default:
                    Debug.LogWarning("State Difficulty not equal 1-5, defaulting to easy.");
                    goto case Difficulty.VeryEasy;
            }
            
            Debug.Log(itemsOnOrder);
            ItemsOnOrderUI();
        }

    
        public void AddDifficulty()
        {
            currentState++;
        }
        
    }
}
    
