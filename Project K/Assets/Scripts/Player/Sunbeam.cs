using UnityEngine;

public class Sunbeam : MonoBehaviour
{
   private void OnCollisionEnter(Collision other)
   {
      Destroy(this.gameObject);
      if (other.collider.CompareTag("Crop"))
      {
         var otherObject = other.collider.GetComponentInChildren<GrowthStage>();
         otherObject.currentTimeBeforeStage--;
         Destroy(this.gameObject);
         Debug.Log("CROP HIT");
      }
   }
}
