using System.Collections;
using UnityEngine;

public class Sunbeam : MonoBehaviour
{
   [SerializeField] private ParticleSystem sunParticleSystem;
   
   //Delay for Particle System
   private IEnumerator ParticleDelay()
   {
      sunParticleSystem.Play();
      yield return new WaitForSeconds(.6f);
   }
   
   private void OnCollisionEnter(Collision other)
   {
      
      if (other.collider.CompareTag("Crop"))
      {
         var otherObject = other.collider.GetComponentInChildren<GrowthStage>();
         otherObject.currentTimeBeforeStage--;
         StartCoroutine(ParticleDelay());
         Destroy(this.gameObject);
         Debug.Log("CROP HIT");
      }
      else if (other.collider.CompareTag("Crop_Sign"))
      {
         other.collider.GetComponent<EnableCrop>().StartCrop();
      }
      else
      {
         StartCoroutine(ParticleDelay());
         Destroy(this.gameObject);
      }
   }
}
