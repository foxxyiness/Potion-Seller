using System;
using System.Collections;
using System.Collections.Generic;
using Player;
using Unity.Mathematics;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Audio;
using UnityEngine.UI;

public class GameMenuManager : MonoBehaviour
{

    [SerializeField] private PowerManager powerManager;
    [SerializeField] private Slider slider;
    public AudioMixer audioMixer;

    
    public Transform head;
    public float spawnDistance = 2;
    
    public GameObject menu;
    public InputActionReference showButton;
    public bool isMenuActive;

    //allows player to adjust volume for master volume
    public void SetVolume()
    {
        float volume = slider.value;
        audioMixer.SetFloat("volume", Mathf.Log10(volume) * 20);
    }

    private void OnEnable()
    {
        showButton.action.Enable();
    }

    private void OnDisable()
    {
        showButton.action.Disable();
    }

    private void Start()
    {
        isMenuActive = false;
        showButton.action.performed += ToggleMenu;
    }

    // Update is called once per frame
    private void Update()
    {
        //ToggleMenu();
    }

    private void ToggleMenu(InputAction.CallbackContext context)
    {
        if ( context.performed && !isMenuActive)
        {
            isMenuActive = true;
            menu.SetActive(true);
            powerManager.canFire = false;
            powerManager.sunPower = false;
            //menu.transform.position = head.position + new Vector3(head.forward.x, 0, head.forward.z).normalized * spawnDistance;

        }
        else if (context.performed && isMenuActive)
        {
            isMenuActive = false;
            menu.SetActive(false);
            powerManager.canFire = true;
            powerManager.sunPower = true;
        }
        /*menu.transform.LookAt(new Vector3(head.position.x, menu.transform.position.y, head.position.y));
        menu.transform.rotation = quaternion.RotateY(180);*/
        //menu.transform.forward *= -1;
    }
}
