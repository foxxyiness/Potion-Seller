using System;
using System.Collections;
using System.Collections.Generic;
using Orders;
using Player;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.XR.Interaction.Toolkit.Samples.StarterAssets;

public class PotionEffects : MonoBehaviour
{
   [SerializeField] private PowerManager powerManager;
   [SerializeField] private DayManager dayManager;
   [SerializeField] private DynamicMoveProvider dynamicMoveProvider;

   private enum PotionEffectsEnum
   {
      manaRestore,
      timeBackwards,
      speed
   }

   [SerializeField] private PotionEffectsEnum potionEffectsEnum = PotionEffectsEnum.manaRestore;
   
   private void StartEffect(PotionEffectsEnum potionEffectsEnum)
   {
      switch (potionEffectsEnum)
      {
         case PotionEffectsEnum.manaRestore:
         {
            ManaRestore();
            break;
         }
         case PotionEffectsEnum.timeBackwards:
         {
            TimeBackwards();
            break;
         }
         case PotionEffectsEnum.speed:
         {
            StartCoroutine(SpeedBoost());
            break;
         }
      }
   }
   
   private void ManaRestore()
   {
      powerManager.RestoreMana(750);
      Debug.Log("Mana Restored Called.");
   }

   private void TimeBackwards()
   {
      dayManager.AddTime(120);
      Debug.Log("Time Backwards Called.");
   }

   private IEnumerator SpeedBoost()
   {
      dynamicMoveProvider.moveSpeed = 5;
      yield return new WaitForSeconds(15f);
      dynamicMoveProvider.moveSpeed = 3;
   }
   
}
