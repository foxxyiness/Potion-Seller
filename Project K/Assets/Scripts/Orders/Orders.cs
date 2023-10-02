using System.Collections.Generic;
using UnityEngine;

namespace Orders
{
   [CreateAssetMenu]
   public class Orders : ScriptableObject
   {
      [SerializeField]
      private List<Item> easyItems;
      [SerializeField]
      private List<Item> mediumItems;
      [SerializeField]
      private List<Item> hardItems;

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
   
   }
}
