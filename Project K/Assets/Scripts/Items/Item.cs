using UnityEngine;

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
        
        [Header("Item Cost")]
        [SerializeField] private int price;
        [SerializeField] private int cost;
        

        [SerializeField] private Difficulty difficulty;
        
        private void Start()
        {
            _rb = GetComponent<Rigidbody>();
            if (GetComponentInChildren<Potion>() != null)
            {
                if (difficulty == Difficulty.Easy)
                {
                    price = 500;
                }
                else if (difficulty == Difficulty.Medium)
                {
                    price = 1000;
                }
                else if (difficulty ==Difficulty.Hard)
                {
                    price = 1500;
                }
            }
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
