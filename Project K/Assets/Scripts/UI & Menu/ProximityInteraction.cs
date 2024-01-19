using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class ProximityInteraction : MonoBehaviour
{
    [FormerlySerializedAs("DialogueWindow")] public GameObject dialogueWindow;
    public float interactionRadius = 2.5f; // The radius within which the interaction can occur.

    [SerializeField] private Transform player; // Reference to the VR player's position.

    private void Awake()
    {
        dialogueWindow = GetComponentInChildren<Canvas>().gameObject;
    }

    private void Update()
    {
        // Calculate the distance between the player and this object.
        float distance = Vector3.Distance(transform.position, player.position);

        // Check if the player is within the interaction radius.
        if (distance <= interactionRadius)
        {
            // toggle dialogue window
            dialogueWindow.SetActive(true);
        }
        else
        {
            dialogueWindow.SetActive(false);
        }
    }


}


