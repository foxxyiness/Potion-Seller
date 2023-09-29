using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEditor;
using UnityEditor.Rendering;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.XR.Interaction.Toolkit.Inputs;

public class PowerManager : MonoBehaviour
{
   [SerializeField] private InputActionReference inputActionReference;
   [SerializeField] private float intensity, duration = 0.5F;
   [SerializeField] private XRBaseController rightController;
   [SerializeField] private GameObject fireBall;
   [SerializeField] private Transform rightPowerSpawnpoint;
   [SerializeField] private float shootForce = 10f;
   private bool _itemGrabbed = false;

   public void SetItemGrabTrue()
   { _itemGrabbed = true; Debug.Log("ITEM GRABBED");}

   public void SetItemGrabFalse()
   { _itemGrabbed = false; Debug.Log("ITEM DROPPED");}

   private void OnEnable()
   {
      inputActionReference.action.Enable();
   }

   private void OnDisable()
   {
      inputActionReference.action.Disable();
   }

   private void Start()
   {
      /*inputActionReference.action.triggered += _ =>
      {
         Debug.Log("Shift + W Performed");
      };*/
      /*inputActionReference.action.performed += _ =>
      {
         Debug.Log("Shift + W Performed");
      };*/
   }

   private void Update()
   {
      StartCoroutine(nameof(Fire));
   }

   private IEnumerator Fire()
   {
      if (inputActionReference.action.triggered && _itemGrabbed == false)
      {
         Debug.Log("FIRE");
         TriggerHaptic(rightController);
         GameObject fireBallShot = Instantiate(fireBall, rightPowerSpawnpoint.position, quaternion.identity);
         //Vector3 shootDir = (this.transform.position - rightPowerSpawnpoint.position).normalized;
         fireBallShot.GetComponent<Rigidbody>().AddForce(rightPowerSpawnpoint.transform.forward * shootForce, ForceMode.Impulse);
         yield return new WaitForSeconds(1);
      }
   }
   private void TriggerHaptic(XRBaseController controller)
   {
      controller.SendHapticImpulse(intensity, duration);
   }
}
