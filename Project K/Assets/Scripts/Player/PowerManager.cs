using System.Collections;
using Unity.Mathematics;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.UI;

namespace Player
{
   public class PowerManager : MonoBehaviour
   {
      [Header("Power Input Action Map")]
      [SerializeField] private InputActionMap inputAction;

      //private XRIDefaultInputActions _defaultAction;
      [Header("References for Game Objects")]
      [SerializeField] private DayManager dayManager;
      [SerializeField] private XRBaseController leftController;
      [SerializeField] private XRBaseController rightController;
      [SerializeField] private GameObject fireBall;
      [SerializeField] private GameObject sunBeam;
      [SerializeField] private Transform rightPowerSpawnPoint;
      [SerializeField] private Transform leftPowerSpawnPoint;
      [SerializeField] private XRRayInteractor rightRayInteractor;
      [SerializeField] private XRRayInteractor leftRayInteractor;
      [SerializeField] private AudioSource audioSource;
      [SerializeField] private AudioClip sunPowerSound;

      [Header("Mana UI")] 
      public Image rightHandManaStatus;
      public Image leftHandManaStatus;

      [Header("Power Amount & Level")]
      [SerializeField] private short powerAmount = 2000;
      [SerializeField] private short totalPowerAmount = 2000;
      [Header("Float Values ")] 
      [SerializeField] private float sunDelay = 1.0f;
      [SerializeField] private float intensity = 0.5f; 
      [SerializeField] private float duration = 0.5F;
      [SerializeField] private float shootForce = 10f;

      [Header("State Check Booleans")]
      public bool timePower;
      private bool _leftItemGrabbed;
      private bool _rightItemGrabbed;
      public bool sunPower;
      public bool canFire;
      private Camera _camera;
      private bool _isCameraNotNull;

      private void Awake()
      {
         _isCameraNotNull = _camera != null;
         _camera = Camera.main;
         canFire = true;
         sunPower = true;
         inputAction["Fire_Power"].performed += Fire;
         SetMana();
         
      }

      public short GetManaLevel()
      {
         return powerAmount;
      }

      public void RestoreMana(short x)
      {
         powerAmount += x;
         if (powerAmount > 2000)
            powerAmount = 2000;
         SetMana();
         CheckPowerLevelForFire();
         CheckPowerLevelForSun();
         Debug.Log("Mana Level: " + powerAmount);
      }
      void SetMana()
      {
         rightHandManaStatus.fillAmount = (float)powerAmount / totalPowerAmount;
      }
      /****************************************************************************************************************/
      //Functions for Direct Interactors
      public void SetRightItemGrabTrue()
      { _rightItemGrabbed = true; canFire = false; Debug.Log("ITEM GRABBED");}

      public void SetRightItemGrabFalse()
      { _rightItemGrabbed = false; canFire = true; Debug.Log("ITEM DROPPED");}
      
      public void SetLeftItemGrabTrue()
      { _leftItemGrabbed = true; sunPower = false; Debug.Log("ITEM GRABBED");}

      public void SetLeftItemGrabFalse()
      { _leftItemGrabbed = false; sunPower = true; Debug.Log("ITEM DROPPED");}
      
      /****************************************************************************************************************/

      private void OnEnable()
      {
        inputAction.Enable();
         //_defaultAction.Enable();
         //timeActionReference.action.Enable();
      }

      private void Update()
      {
         SunPower();
         //CheckPowerLevelForFire();
         //CheckPowerLevelForSun();
         CheckRayInteractors();
      }

      private void CheckRayInteractors()
      {
         leftRayInteractor.gameObject.SetActive(!inputAction["Left_Grip"].IsInProgress());
         rightRayInteractor.gameObject.SetActive(!inputAction["Right_Grip"].IsInProgress());
      }
      private void CheckPowerLevelForSun()
      {
         sunPower = powerAmount > 0;
      }
      private void CheckPowerLevelForFire()
      {
         //Checks power level for fire ability
         canFire = powerAmount >= 30;
      }
      private void SunPower()
      {
         StartCoroutine(SunPowerCoroutine());
      }

      private IEnumerator SunPowerCoroutine()
      {
         if ( inputAction["Sun_Power"].IsInProgress()  && sunPower && !_leftItemGrabbed)
         {
            sunPower = false;
            audioSource.clip = sunPowerSound;
            var position = leftPowerSpawnPoint.position;
            if (_isCameraNotNull)
            {
               Ray ray = _camera.ScreenPointToRay(position);
            }

            if (Physics.Raycast(position, leftPowerSpawnPoint.transform.forward,
                   out RaycastHit hitInfo, 20f))
            {
               GameObject laser = Instantiate(sunBeam, hitInfo.point, quaternion.identity);
               //laser.GetComponent<Sunbeam>().MoveTowards(hitInfo.point, speed);

               Debug.DrawRay(position, leftPowerSpawnPoint.TransformDirection(Vector3.forward) * hitInfo.distance,
                  Color.red, 2f);
               Debug.Log(hitInfo.collider.name);
               leftController.SendHapticImpulse(1, .25f);
               StartCoroutine(SunPowerSoundCoroutine());
               leftHandManaStatus.fillAmount = 1;
               powerAmount--;
               SetMana();
            }
            yield return new WaitForSeconds(sunDelay);
            sunPower = true;
            leftHandManaStatus.fillAmount = 0f;
            CheckPowerLevelForSun();
         }
         audioSource.Stop();
      }

      private IEnumerator SunPowerSoundCoroutine()
      {
         if (audioSource.isPlaying)
         {
            audioSource.Stop();
            yield return new WaitForSeconds(1F);
         }
         audioSource.Play();
         yield return new WaitForSeconds(1F);

      }
      private void Fire(InputAction.CallbackContext context)
      {
         StartCoroutine(FireCoroutine(context));
      }
      
      private void OnDisable()
      {
         inputAction.Disable();
         //_defaultAction.Disable();
      }

      private IEnumerator FireCoroutine(InputAction.CallbackContext context)
      {
         if ( context.performed && !_rightItemGrabbed && canFire)
         {
            canFire = false;
            Debug.Log("FIRE");
            TriggerHaptic(rightController);
            GameObject fireBallShot = Instantiate(fireBall, rightPowerSpawnPoint.position, Quaternion.identity);
            fireBallShot.GetComponent<Rigidbody>().AddForce(rightPowerSpawnPoint.transform.forward * shootForce, ForceMode.Impulse);
            powerAmount -= 30;
            SetMana();
            yield return new WaitForSeconds(1);
            canFire = true;
            CheckPowerLevelForFire();
         }
      }

      //Time Power
      public IEnumerator TimeForwardCoroutine()
      {
            Debug.Log("RAH RHA RAH RAH RAH ARHA RAH ");
            leftController.SendHapticImpulse(1, 10);
            rightController.SendHapticImpulse(1, 10);
            yield return new WaitForSeconds(3);
            //Reference to enable DayManager Time forward
            dayManager.doFastForward = true;
      }
      
      private void TriggerHaptic(XRBaseController controller)
      {
         controller.SendHapticImpulse(intensity, duration);
      }
   }
}
