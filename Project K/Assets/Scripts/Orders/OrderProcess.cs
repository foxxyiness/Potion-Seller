using Items;
using Orders;
using TMPro;
using UnityEngine;


public class OrderProcess : MonoBehaviour
{
        private OrderManager orderManager;
        [SerializeField] private PlayerWallet playerWallet;
        
        private void Start()
        {
            orderManager = GameObject.FindGameObjectWithTag("Order_Manager").GetComponent<OrderManager>();
        }
        
        private void CheckCorrectItem(Item potion)
        {
            var index = orderManager._itemsOnOrder.FindIndex(item => item.GetName() == potion.GetName());
            //Check for Item Created was actually in list
            if (index > -1)
            {
                orderManager._itemsOnOrder.RemoveAt(index);
                TextMeshProUGUI []textList = orderManager.orderUIContent.GetComponentsInChildren<TextMeshProUGUI>();
                foreach (TextMeshProUGUI text in textList)
                {
                    if (text.text == potion.GetName())
                    {
                        playerWallet.AddBalance(potion.GetPrice());
                        Destroy(text.gameObject);
                        Destroy(potion.gameObject);
                        break;
                    }
                }
                Debug.Log("ITEM INDEX FOUND AND DESTROYED SUCCESSFULLY I BELIEVE");
            }
            else
            {
                Debug.Log("ITEM NOT FOUND IN LIST");
            }
        }
        
        private void OnCollisionEnter(Collision collision)
        {
            if (collision.gameObject.GetComponent<Item>() != null)
            {
                CheckCorrectItem(collision.gameObject.GetComponent<Item>());
            }
            
        }
    }
