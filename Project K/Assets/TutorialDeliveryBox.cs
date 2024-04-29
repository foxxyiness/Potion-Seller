using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class TutorialDeliveryBox : MonoBehaviour
{
   private void OnCollisionEnter(Collision other)
   {
      if (other.collider.CompareTag("Untagged"))
      {
         Destroy(other.gameObject);
      }
   }
}
