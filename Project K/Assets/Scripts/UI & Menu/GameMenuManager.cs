using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Audio;

public class GameMenuManager : MonoBehaviour
{

    public AudioMixer audioMixer;

    
    public Transform head;
    public float spawnDistance = 2;
    
    public GameObject menu;
    public InputActionProperty showButton;

    //allows player to adjust volume for master volume
    public void SetVolume(float volume)
    {
        audioMixer.SetFloat("volume", volume);
    }



    // Update is called once per frame
    void Update()
    {
        if (showButton.action.WasPressedThisFrame())
        {
            menu.SetActive(!menu.activeSelf);

            menu.transform.position = head.position + new Vector3(head.forward.x, 0, head.forward.z).normalized * spawnDistance;

        }

        menu.transform.LookAt(new Vector3(head.position.x, menu.transform.position.y, head.position.y));
        //menu.transform.forward *= -1;
    }
}
