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
   private bool canEffect;

   private void Start()
   {
      player = GameObject.FindGameObjectWithTag("Player");
      powerManager = player.gameObject.GetComponentInChildren<PowerManager>();
      dynamicMoveProvider = player.gameObject.GetComponentInChildren<DynamicMoveProvider>();
      dayManager = GameObject.FindGameObjectWithTag("Day_Manager").GetComponent<DayManager>();
      canEffect = false;
   }

   private void Update()
   {
      StartEffect(potionEffectsEnum);
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
            canEffect = true;
         }
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
      if (canEffect)
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
     
   }
   
   private void ManaRestore()
   {
      powerManager.RestoreMana(750);
      Debug.Log("Mana Restored Called.");
      Destroy(gameObject);
      
   }

   private void TimeBackwards()
   {
      dayManager.AddTime(120);
      Debug.Log("Time Backwards Called.");
      Destroy(gameObject);
   }

   private IEnumerator SpeedBoost()
   {
      dynamicMoveProvider.moveSpeed = 5;
      transform.localScale = Vector3.zero;
      yield return new WaitForSeconds(10f);
      dynamicMoveProvider.moveSpeed = 3;
      Debug.Log("MOVE SPEED DECREASED HOPEFULLY");
      Destroy(gameObject);
   }
   
}
