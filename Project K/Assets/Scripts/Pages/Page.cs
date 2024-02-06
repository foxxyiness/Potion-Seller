using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Items;
using TMPro;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;

namespace UI___Menu
{
    public class Page : MonoBehaviour
    {
        [SerializeField] private GameManager _gameManager;
        [SerializeField] private Potion _potion;
        [SerializeField] private Orders.Orders potionLists;
        [SerializeField] private TMP_Text potionText;
        [SerializeField] private TextMeshProUGUI baseText;
        [SerializeField] private TextMeshProUGUI strengthText;
        [SerializeField] private TextMeshProUGUI flavorText;
        [SerializeField] private TextMeshProUGUI toastyText;
        [SerializeField] private Book book;

     
        //Changes the UI Text references to show current "Page"
        public void ShowPotionList()
        {
            potionText.text = _potion.gameObject.GetComponentInParent<Item>().GetName();
            baseText.text = _potion.GetBaseText();
            strengthText.text = _potion.GetStrengthText();
            flavorText.text = _potion.GetFlavorText();
            toastyText.text = _potion.GetToastable();
          //  pageNumber.text = (1 + _bookIndex).ToString();
        }

        //If Page is found and destroyed by fire, page will be destroyed and added to the book of spells
        private void OnCollisionEnter(Collision other)
        {
            if (other.collider.CompareTag("Fire"))
            {
                PageFound(_potion.gameObject.GetComponent<Item>());
            }
        }

        private void PageFound(Item potion)
        {
            book.AddPotionToList(potion);
        }
    }
}
