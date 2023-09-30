using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.XR.Interaction.Toolkit.Inputs;

public class PowerManager : MonoBehaviour
{
   [SerializeField] private InputActionReference inputActionReference;

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
      inputActionReference.action.started += _ =>
      {
         Debug.Log("Shift + W Performed");
      };
      inputActionReference.action.performed += _ =>
      {
         Debug.Log("Shift + W Performed");
      };
   }
}
