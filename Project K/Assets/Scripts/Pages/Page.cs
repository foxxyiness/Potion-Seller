using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Items;
using TMPro;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using UnityEngine.Serialization;

namespace UI___Menu
{
    public class Page : MonoBehaviour
    {
        [SerializeField] private GameManager _gameManager;
        [SerializeField] private Orders.Orders potionLists;
        [SerializeField] private TMP_Text potionText;
        [SerializeField] private TextMeshProUGUI baseText;
        [SerializeField] private TextMeshProUGUI strengthText;
        [SerializeField] private TextMeshProUGUI flavorText;
        [SerializeField] private TextMeshProUGUI toastyText;
        [FormerlySerializedAs("_potion")] public Potion potion;

        private void Start()
        {
            _gameManager = GameObject.FindWithTag("Order_Manager").GetComponent<GameManager>();
            
        }

        //Changes the UI Text references to show current "Page"
        public void ShowPotionList()
        {
            potionText.text = potion.gameObject.GetComponent<Item>().GetName();
            baseText.text = potion.GetBaseText();
            strengthText.text = potion.GetStrengthText();
            flavorText.text = potion.GetFlavorText();
            toastyText.text = potion.GetToastable();
          //  pageNumber.text = (1 + _bookIndex).ToString();
        }

        //If Page is found and destroyed by fire, page will be destroyed and added to the book of spells
        private void OnCollisionEnter(Collision other)
        {
            if (other.collider.CompareTag("Fire"))
            {
                _gameManager.PageFound(potion.gameObject.GetComponent<Item>());
            }
        }
        
    }
}
