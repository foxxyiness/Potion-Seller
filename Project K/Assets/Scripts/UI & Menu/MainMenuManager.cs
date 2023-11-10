using System;
using System.Collections;
using System.Collections.Generic;
using Player;
using Unity.Mathematics;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Audio;

public class MainMenuManager : MonoBehaviour
{

    [SerializeField] private PowerManager powerManager;
    public AudioMixer audioMixer;





    //allows player to adjust volume for master volume
    public void SetVolume(float volume)
    {
        audioMixer.SetFloat("volume", volume);
    }

   
}
