using System;
using System.Collections;
using Unity.Mathematics;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Serialization;
using UnityEngine.XR.Interaction.Toolkit;

namespace Player
{
   public class PowerManager : MonoBehaviour
   {
      [Header("Power Input Action Map")]
      [SerializeField] private InputActionMap inputAction;

      //private XRIDefaultInputActions _defaultAction;
      [Header("References for Game Objects")]
      [SerializeField] private DayManager dayManager;
      //[SerializeField] private GameMenuManager gameMenuManager;
      [SerializeField] private XRBaseController leftController;
      [SerializeField] private XRBaseController rightController;
      [SerializeField] private GameObject fireBall;
      [SerializeField] private GameObject sunBeam;
      [SerializeField] private Transform rightPowerSpawnPoint;
      [SerializeField] private Transform leftPowerSpawnPoint;
      [Header("Float Values ")]
      [SerializeField] private float intensity = 0.5f; 
      [SerializeField] private float duration = 0.5F;
      [SerializeField] private float shootForce = 10f;
      [SerializeField] private float speed = 20.0f;

      [Header("State Check Booleans")]
      public bool timePower;
      private bool _itemGrabbed;
      public bool sunPower;
      [FormerlySerializedAs("_canFire")] public bool canFire;
      private void Awake()
      {
         canFire = true;
         sunPower = true;
         timePower = false;
         inputAction["Fire_Power"].performed += Fire;
         inputAction["Time_Power"].performed += TimeForward;
         inputAction["Sun_Power"].started += SunPower;
         /*_defaultAction = new XRIDefaultInputActions();
         _defaultAction.XRIPower.Fire_Power.performed += Fire;
         _defaultAction.XRIPower.Time_Power.performed += TimeForward;*/
      }

      public void SetItemGrabTrue()
      { _itemGrabbed = true; Debug.Log("ITEM GRABBED");}

      public void SetItemGrabFalse()
      { _itemGrabbed = false; Debug.Log("ITEM DROPPED");}

      private void OnEnable()
      {
        inputAction.Enable();
         //_defaultAction.Enable();
         //timeActionReference.action.Enable();
      }

      private void SunPower(InputAction.CallbackContext context)
      {
         
         if (context.started && sunPower)
         {
            var position = leftPowerSpawnPoint.position;
            if (Camera.main != null)
            {
               Ray ray = Camera.main.ScreenPointToRay(position);
            }

            if (Physics.Raycast(position, leftPowerSpawnPoint.transform.forward,
                   out RaycastHit hitInfo, 20f))
            {
               GameObject laser = Instantiate(sunBeam, leftPowerSpawnPoint.position, quaternion.identity);
               laser.transform.position = Vector3.MoveTowards(laser.transform.position, hitInfo.point, speed * Time.deltaTime);
               
               Debug.DrawRay(position, leftPowerSpawnPoint.TransformDirection(Vector3.forward) * hitInfo.distance, Color.red);
               Debug.Log(hitInfo.collider.name);
            }
            
         }
      }
      private void Fire(InputAction.CallbackContext context)
      {
         StartCoroutine(FireCoroutine(context));
      }

      private void TimeForward(InputAction.CallbackContext context)
      {
         StartCoroutine(TimeForwardCoroutine(context));
      }
      private void OnDisable()
      {
         inputAction.Disable();
         //_defaultAction.Disable();
      }

      private IEnumerator FireCoroutine(InputAction.CallbackContext context)
      {
         if ( context.performed && !_itemGrabbed && canFire)
         {
            canFire = false;
            Debug.Log("FIRE");
            TriggerHaptic(rightController);
            GameObject fireBallShot = Instantiate(fireBall, rightPowerSpawnPoint.position, Quaternion.identity);
            fireBallShot.GetComponent<Rigidbody>().AddForce(rightPowerSpawnPoint.transform.forward * shootForce, ForceMode.Impulse);
            yield return new WaitForSeconds(1);
            canFire = true;
         }
      }

      //Time Power
      private IEnumerator TimeForwardCoroutine(InputAction.CallbackContext context)
      {
         if (context.performed && timePower)
         {
            Debug.Log("RAH RHA RAH RAH RAH ARHA RAH ");
            leftController.SendHapticImpulse(1, 10);
            rightController.SendHapticImpulse(1, 10);
            yield return new WaitForSeconds(3);
            //Reference to enable DayManager Time forward
            dayManager.doFastForward = true;
         }
      }
      
      private void TriggerHaptic(XRBaseController controller)
      {
         controller.SendHapticImpulse(intensity, duration);
      }
   }
}
