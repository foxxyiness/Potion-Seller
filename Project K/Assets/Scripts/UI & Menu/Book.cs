using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Items;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Serialization;

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
        [SerializeField] private TextMeshProUGUI pageNumber;
        public List<Item> allPotions;
        [SerializeField] private int bookIndex = 0;
        void Start()
        {
            allPotions = new List<Item>();
            StartCoroutine(GrabAllPotions());
        }

        public void AddPotionToList(Item potion)
        {
            allPotions.Add(potion);
            ShowPotionList();
        }
        private IEnumerator GrabAllPotions()
        {
            yield return new WaitForSeconds(5);
            //Instantiating a Temp list to extract the items and append it to _allPotions
           // List<Item> easyItems = potionLists.GetEasyItems();
            foreach(var item in potionLists.GetEasyItems())
            {
                allPotions.Add(item);
            }

            //Should make temp list eligible for garbage collection
           // easyItems = null;
            ShowPotionList();       
        }
        //Changes the UI Text references to show current "Page"
        private void ShowPotionList()
        {
            potionText.text = allPotions[bookIndex].GetName();
            baseText.text = allPotions[bookIndex].GetComponent<Potion>().GetBaseText();
            strengthText.text = allPotions[bookIndex].GetComponent<Potion>().GetStrengthText();
            flavorText.text = allPotions[bookIndex].GetComponent<Potion>().GetFlavorText();
            toastyText.text = allPotions[bookIndex].GetComponent<Potion>().GetToastable();
            pageNumber.text = (1 + bookIndex).ToString();
        }

        public void NextPage()
        {
            if (bookIndex < allPotions.Count() - 1)
            {
                bookIndex++;
                ShowPotionList();
            }
            
        }

        public void PreviousPage()
        {
            if (bookIndex > 0)
            {
                bookIndex--;
                ShowPotionList();
            }
          
        }
    }
}
