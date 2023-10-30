using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProximityInteraction : MonoBehaviour
{
    public GameObject DialogueWindow;
    public float interactionRadius = 1.0f; // The radius within which the interaction can occur.

    private Transform player; // Reference to the VR player's position.

    private void Start()
    {
        player = Camera.main.transform; // Assuming the VR camera is the player's viewpoint.
    }

    private void Update()
    {
        // Calculate the distance between the player and this object.
        float distance = Vector3.Distance(transform.position, player.position);

        // Check if the player is within the interaction radius.
        if (distance <= interactionRadius)
        {
            // toggle dialogue window
            DialogueWindow.SetActive(true);
        }
        else
        {
            DialogueWindow.SetActive(false);
        }
    }


}


