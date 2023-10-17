using System;
using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Serialization;
using UnityEngine.XR.Interaction.Toolkit;

   public class PowerManager : MonoBehaviour
   {
      [SerializeField] private DayManager dayManager;

      [SerializeField]
      private InputActionMap _inputAction;
      [SerializeField] private InputActionReference fireActionReference;
      [SerializeField] private float intensity, duration = 0.5F;
      [SerializeField] private XRBaseController leftController;
      [SerializeField] private XRBaseController rightController;
      [SerializeField] private GameObject fireBall;
      [SerializeField] private Transform rightPowerSpawnPoint;
      [SerializeField] private float shootForce = 10f;

      public bool timePower;
      
      private bool _itemGrabbed;
      [SerializeField] private bool _canFire;

      private InputAction fireAction;
      private InputAction timeAction;
      private void Awake()
      {
         _canFire = true;
         timePower = false;
         _inputAction["Fire_Power"].performed += Fire;
         _inputAction["Time_Power"].performed += TimeForward;
      }

      public void SetItemGrabTrue()
      { _itemGrabbed = true; Debug.Log("ITEM GRABBED");}

      public void SetItemGrabFalse()
      { _itemGrabbed = false; Debug.Log("ITEM DROPPED");}

      private void OnEnable()
      {
        _inputAction.Enable();
        //timeActionReference.action.Enable();
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
         _inputAction.Disable();
      }
   
      private void Update()
      {
        // StartCoroutine(nameof(FireCoroutine));
        // StartCoroutine(nameof(TimeForward));
      }

      private IEnumerator FireCoroutine(InputAction.CallbackContext context)
      {
         if ( context.performed && !_itemGrabbed && _canFire)
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
      private IEnumerator TimeForwardCoroutine(InputAction.CallbackContext context)
      {
         if (context.performed && timePower)
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
