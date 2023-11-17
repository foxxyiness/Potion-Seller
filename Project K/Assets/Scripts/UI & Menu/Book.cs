using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Items;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

namespace UI___Menu
{
    public class Book : MonoBehaviour
    {
        [SerializeField] private Orders.Orders potionLists;
        [SerializeField] private TMP_Text potionText;
        [SerializeField] private TextMeshProUGUI baseText;
        [SerializeField] private TextMeshProUGUI strengthText;
        [SerializeField] private TextMeshProUGUI flavorText;
        [SerializeField] private TextMeshProUGUI toastyText;
        private List<Item> _allPotions;
        [SerializeField] private int _bookIndex = 0;
        void Start()
        {
            _allPotions = new List<Item>();
            StartCoroutine(GrabAllPotions());
        }

        private IEnumerator GrabAllPotions()
        {
            yield return new WaitForSeconds(5);
            //Instantiating a Temp list to extract the items and append it to _allPotions
           // List<Item> easyItems = potionLists.GetEasyItems();
            foreach(var item in potionLists.GetEasyItems())
            {
                _allPotions.Add(item);
            }

            //Should make temp list eligible for garbage collection
           // easyItems = null;
            ShowPotionList();       
        }
        //Changes the UI Text references to show current "Page"
        private void ShowPotionList()
        {
            potionText.text = _allPotions[_bookIndex].GetName();
            baseText.text = _allPotions[_bookIndex].GetComponent<Potion>().GetBaseText();
            strengthText.text = _allPotions[_bookIndex].GetComponent<Potion>().GetStrengthText();
            flavorText.text = _allPotions[_bookIndex].GetComponent<Potion>().GetFlavorText();
            toastyText.text = _allPotions[_bookIndex].GetComponent<Potion>().GetToastable();
        }

        public void NextPage()
        {
            if (_bookIndex < _allPotions.Count() - 1)
            {
                _bookIndex++;
                ShowPotionList();
            }
            
        }

        public void PreviousPage()
        {
            if (_bookIndex > 0)
            {
                _bookIndex--;
                ShowPotionList();
            }
          
        }
    }
}
