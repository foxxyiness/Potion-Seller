using System.Collections.Generic;
using Items;
using UnityEngine;
using Random = System.Random;
namespace Orders
{
   public class Orders : MonoBehaviour
   {
      [SerializeField]
      private List<Item> easyItems;
      [SerializeField]
      private List<Item> mediumItems;
      [SerializeField]
      private List<Item> hardItems;

      private Random _rand = new Random();

      //Get methods for retriving the list of easy, med, and hard potions
      public List<Item> GetEasyItems()
      {
         return easyItems;
      }
   
      public List<Item> GetMediumItems()
      {
         return mediumItems;
      }
   
      public List<Item> GetHardItems()
      {
         return hardItems;
      }

      //Gets called when probability number hits the correct number for Easy
      public Item GetSingleEasyOrder()
      {
         int index = _rand.Next(0, easyItems.Count);
         return easyItems[index];
      }
      //Gets called when probability number hits the correct number for Medium
      public Item GetSingleMediumOrder()
      {
         int index = _rand.Next(0, mediumItems.Count);
         return mediumItems[index];
      }
       
      //Gets called when probability number hits the correct number for Hard
      public Item GetSingleHardOrder()
      {
         int index = _rand.Next(0, hardItems.Count);
         return hardItems[index];
         
      }
   }
}
