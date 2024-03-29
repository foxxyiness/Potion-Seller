using UnityEngine;

public class Sunbeam : MonoBehaviour
{
   private void OnCollisionEnter(Collision other)
   {
      
      if (other.collider.CompareTag("Crop"))
      {
         var otherObject = other.collider.GetComponentInChildren<GrowthStage>();
         otherObject.currentTimeBeforeStage--;
         Destroy(this.gameObject);
         Debug.Log("CROP HIT");
      }
      else if (other.collider.CompareTag("Crop_Sign"))
      {
         other.collider.GetComponent<EnableCrop>().StartCrop();
      }
      else
      {
         Destroy(this.gameObject);
      }
   }
}
