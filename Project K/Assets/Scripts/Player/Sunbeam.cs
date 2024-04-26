using System;
using System.Collections;
using UnityEngine;

public class Sunbeam : MonoBehaviour
{
   [SerializeField] private ParticleSystem sunParticleSystem;
   [SerializeField] private Rigidbody rb;

   private void Start()
   {
      sunParticleSystem.Play();
   }

   //Delay for Particle System
   private IEnumerator ParticleDelay()
   {
      //sunParticleSystem.Play();
      
      yield return new WaitForSeconds(.6f);
      Destroy(this.gameObject);
   }
   
   private void OnCollisionEnter(Collision other)
   {
      if (other.collider.CompareTag("Crop"))
      {
         var otherObject = other.collider.GetComponentInChildren<GrowthStage>();
         otherObject.currentTimeBeforeStage--;
         Destroy(gameObject);
         Debug.Log("CROP HIT");
      }
      else
      {
        Destroy(gameObject);
      }
   }
}
