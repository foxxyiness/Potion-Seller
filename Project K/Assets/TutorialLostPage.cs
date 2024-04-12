using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialLostPage : MonoBehaviour
{
    [SerializeField] private TutorialManager tutorialManager;
    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("Fire"))
        {
            tutorialManager.currentState = 26;
            tutorialManager.UpdateState();
            Debug.Log("Lost Page Hit");
            Destroy(gameObject);
        }
    }
}
