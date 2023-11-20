using UnityEngine;

public class MainMenuPowerManager : MonoBehaviour
{
      public bool timePower;
      public bool sunPower;
      public bool canFire;
      private Camera _camera;
      private bool _isCameraNotNull;

      private void Awake()
      {
         _isCameraNotNull = _camera != null;
         _camera = Camera.main;
         canFire = false;
         sunPower = false;
         timePower = false;
         //inputAction["Sun_Power"].performed += SunPower;
         /*_defaultAction = new XRIDefaultInputActions();
         _defaultAction.XRIPower.Fire_Power.performed += Fire;
         _defaultAction.XRIPower.Time_Power.performed += TimeForward;*/
      }
     
   }