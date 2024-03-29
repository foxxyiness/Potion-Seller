using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnableCrop : MonoBehaviour
{
    [SerializeField] private GameObject leftHand;
    [SerializeField] private List<GrowthStage> cropGrowthStage;
    private bool _enableCrop;
    private float timer = 2f;

    private void Start()
    {
        _enableCrop = false;
    }

    private void Update()
    {
         StartCrop();
    }

    private void StartCrop()
    {
        if (_enableCrop)
        {
            if (leftHand.transform.rotation.x > 65f)
            {
                timer += Time.deltaTime;
                if (timer >= 2)
                {
                    _enableCrop = false;
                    foreach (var crop in cropGrowthStage)
                    {
                        crop.EnableCrop();
                    }
                }
            }
        }
    }
    private void OnCollisionEnter(Collision other)
    {
        if (other.collider.CompareTag("Player"))
        {
            _enableCrop = true;
        }
    }

    private void OnCollisionExit(Collision other)
    {
        if (other.collider.CompareTag("Player"))
        {
            _enableCrop = false;
        }
    }
}
