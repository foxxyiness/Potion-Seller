using System;
using System.Collections;
using Unity.Mathematics;
using UnityEngine;

public class Sunbeam : MonoBehaviour
{
   [SerializeField] private ParticleSystem sunParticleSystem;
   
   
   private void OnCollisionEnter(Collision other)
   {
      if (other.collider.CompareTag("Crop"))
      {
         var otherObject = other.collider.GetComponentInChildren<GrowthStage>();
         otherObject.currentTimeBeforeStage -= 20;
         Instantiate(sunParticleSystem, transform.position, quaternion.identity);
         Destroy(gameObject);
         Debug.Log("CROP HIT");
      }
      else
      {
        Instantiate(sunParticleSystem, transform.position, quaternion.identity); 
        Destroy(gameObject);
      }
   }
}
