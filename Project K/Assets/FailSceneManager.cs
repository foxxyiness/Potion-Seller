using System;
using System.Collections;
using System.Collections.Generic;
using Player;
using UI___Menu;
using UnityEngine;

public class FailSceneManager : MonoBehaviour
{
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private MenuManager menuManager;
    [SerializeField] private PowerManager powerManager;


    private void Start()
    {
        audioSource.PlayDelayed(4F);
        StartCoroutine(ReturnToMenu());
        powerManager.canFire = false;
        powerManager.sunPower = false;
    }

    private IEnumerator ReturnToMenu()
    {
        yield return new WaitForSeconds(28F);
        menuManager.MainMenu();
    }
}
