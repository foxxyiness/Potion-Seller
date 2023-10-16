using System;
using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Serialization;
using UnityEngine.XR.Interaction.Toolkit;

   public class PowerManager : MonoBehaviour
   {
      [SerializeField] private DayManager dayManager;
      private XRIDefaultInputActions _inputActions;
      [SerializeField] private InputActionReference fireActionReference;
      [SerializeField] private float intensity, duration = 0.5F;
      [SerializeField] private XRBaseController leftController;
      [SerializeField] private XRBaseController rightController;
      [SerializeField] private GameObject fireBall;
      [SerializeField] private Transform rightPowerSpawnPoint;
      [SerializeField] private float shootForce = 10f;

      public bool timePower;
      
      private bool _itemGrabbed;
      private bool _canFire;

      private void Awake()
      {
         _canFire = true;
         timePower = false;
         _inputActions = new XRIDefaultInputActions();
      }

      public void SetItemGrabTrue()
      { _itemGrabbed = true; Debug.Log("ITEM GRABBED");}

      public void SetItemGrabFalse()
      { _itemGrabbed = false; Debug.Log("ITEM DROPPED");}

      private void OnEnable()
      {
         //fireActionReference.action.Enable();
         _inputActions.Enable();
         //timeActionReference.action.Enable();
      }

      private void OnDisable()
      {
         //fireActionReference.action.Disable();
         _inputActions.Disable();
         //timeActionReference.action.Disable();
      }
   
      private void Update()
      {
         StartCoroutine(nameof(Fire));
         StartCoroutine(nameof(TimeForward));
      }

      private IEnumerator Fire()
      {
         if (_inputActions.XRIPower.Fire_Power.triggered && !_itemGrabbed && _canFire)
         {
            _canFire = false;
            Debug.Log("FIRE");
            TriggerHaptic(rightController);
            GameObject fireBallShot = Instantiate(fireBall, rightPowerSpawnPoint.position, Quaternion.identity);
            fireBallShot.GetComponent<Rigidbody>().AddForce(rightPowerSpawnPoint.transform.forward * shootForce, ForceMode.Impulse);
            yield return new WaitForSeconds(1);
            _canFire = true;
         }
      }

      //Time Power
      private IEnumerator TimeForward()
      {
         if (_inputActions.XRIPower.Time_Power.triggered && timePower)
         {
            Debug.Log("RAH RHA RAH RAH RAH ARHA RAH ");
            leftController.SendHapticImpulse(1, 5);
            rightController.SendHapticImpulse(1, 5);
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
