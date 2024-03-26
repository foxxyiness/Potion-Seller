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
   public GameObject player;
   public PowerManager powerManager;
   public DayManager dayManager;
   public DynamicMoveProvider dynamicMoveProvider;

   private void Start()
   {
      player = GameObject.FindGameObjectWithTag("Player");
      powerManager = player.gameObject.GetComponentInChildren<PowerManager>();
      dynamicMoveProvider = player.gameObject.GetComponentInChildren<DynamicMoveProvider>();
      dayManager = GameObject.FindGameObjectWithTag("Day_Manager").GetComponent<DayManager>();
   }

   //Checks to see if potion hit the ground, destroys after checking for effects
   private void OnCollisionEnter(Collision other)
   {
      if (other.gameObject.CompareTag("Ground"))
      {
         float playerDistance = Vector3.Distance(player.transform.position, this.transform.position);
         Debug.Log("POTION EFFECT: GROUND HIT, PLAYER DISTANCE IS " + playerDistance);
         if (playerDistance <= 50)
         {
            StartEffect(potionEffectsEnum);
         }
         Destroy(gameObject);
      }
      
   }

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
      float timer = new float();
      timer += Time.deltaTime;
      while (timer < 10.0)
      {
         Debug.Log(timer + " seconds left of speed");
      }
      dynamicMoveProvider.moveSpeed = 3;
      Debug.Log("PLAYER SPEED SHOULDVE RETURNED");
      yield break;
   }
   
}
