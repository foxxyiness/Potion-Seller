using System;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Items
{
    public class Item : MonoBehaviour
    {
        public bool isGrounded { get; private set; }
        private Rigidbody _rb;
        [Header("Item Type")]
        [SerializeField] private Type type = Type.Light;
        [SerializeField] private string itemName;
        [SerializeField] private string description;
        private ItemManager _itemManager;
        
        [Header("Item Cost")]
        [SerializeField] private int price;
        [SerializeField] private int cost;
        [SerializeField] private float velocityLimit;
        [SerializeField] private float currentVelocity;
        [SerializeField] private Vector3 previousPosition;
        [SerializeField] private Difficulty difficulty;
        
        private void Start()
        {
            if (SceneManager.GetActiveScene().name != "Tutorial")
            {
                _itemManager = GameObject.FindGameObjectWithTag("Item_Manager").GetComponent<ItemManager>();
                if (!gameObject.GetComponent<Potion>())
                {
                    if (_itemManager.allItemObjectsList.Count < 100)
                    {
                        _itemManager.allItemObjectsList.Add(gameObject);
                    }
                    else
                    {
                        Destroy(gameObject);
                    }
                }
            }
            
            _rb = GetComponent<Rigidbody>();
            velocityLimit = 125F;
            InvokeRepeating(nameof(GetPreviousPosition), 1f, 5f);
        }

        private void Update()
        {
            VelocityCheck();
        }
        //Gets and checks velocity to see if it exceeds velocity limit
        private void VelocityCheck()
        {
            currentVelocity = _rb.velocity.sqrMagnitude;
            if (currentVelocity > velocityLimit)
            {
                //Teleports GameObject back 5 seconds
                transform.position = previousPosition;
            }
        }
        //Gets Previous Position of 5 seconds ago for velocity check. If velocity of GameObject exceeds velocity limit, it will return to this location
        private void GetPreviousPosition()
        {
            previousPosition = transform.position;
        }
        
        //Ground Check for Gameobject
        private void OnCollisionEnter(Collision collision)
        {
            if (collision.collider.CompareTag("Ground"))
            {
                _rb.velocity = Vector3.zero;
                isGrounded = true;
            }
        }

        //Ground Check for Gameobject
        private void OnCollisionExit(Collision collision)
        {
            if (collision.collider.CompareTag("Ground"))
            {
                isGrounded = false;
            }
        }

        public enum Difficulty
        {
            Easy,
            Medium,
            Hard
        }
        private enum Type
        {
            Light,
            Healing,
            Poison,
            Death
        }

        
        public string GetName()
        {
            return itemName;
        }

        public Difficulty GetDifficulty()
        {
            return difficulty;
        }

        public int GetPrice()
        {
            return price;
        }

        public int GetCost()
        {
            return cost;
        }
    
    }
}
