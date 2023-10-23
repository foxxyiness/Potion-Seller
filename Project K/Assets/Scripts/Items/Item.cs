using Unity.VisualScripting;
using UnityEngine;

namespace Items
{
    public class Item : MonoBehaviour
    {
        public bool isGrounded { get; private set; }
        private Rigidbody rb;
        
        private void Awake()
        {
            rb = GetComponent<Rigidbody>();
        }
    

        private void OnCollisionEnter(Collision collision)
        {
            if (collision.collider.CompareTag("Ground"))
            {
                rb.velocity = Vector3.zero;
                isGrounded = true;
            }
        }

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

  
        [SerializeField]
        private Type type = Type.Light;
        [SerializeField]
        private string itemName;
        [SerializeField]
        private string description;
        [SerializeField] 
        private Difficulty difficulty = Difficulty.Easy;
        public string GetName()
        {
            return itemName;
        }

        public Difficulty GetDifficulty()
        {
            return difficulty;
        }
    
    }
}
