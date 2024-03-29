using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnableCrop : MonoBehaviour
{
    [SerializeField] private GameObject leftHand;
    [SerializeField] private List<GrowthStage> cropGrowthStage;
    [SerializeField] private GameObject player;
    private bool _enableCrop;
    private bool _isCropEnabled;
    private float timer = 2f;

    private void Start()
    {
        _enableCrop = false;
        cropGrowthStage.Add(gameObject.GetComponentInChildren<GrowthStage>());
    }

    private void Update()
    {
         StartCrop();
         CheckPlayer();
    }
    
    //If enable crop is true, checks transform of left hand if true, starts 2 second timer, then enable crops
    private void StartCrop()
    {
        if (!_enableCrop) return;
        if (leftHand.transform.rotation.x > 65f)
        {
            timer += Time.deltaTime;
            if (timer >= 2)
            {
                IsCropEnabled();
                foreach (var crop in cropGrowthStage)
                {
                    crop.EnableCrop();
                }
            }
        }
        else
        {
            timer = 0;
        }
    }

   private void CheckPlayer()
    {
        if (Vector3.Distance(transform.position, player.transform.position) <= 1)
        {
            _enableCrop = true;
            Debug.Log("Player in position of " + gameObject.name);
        }
        else
        {
            _enableCrop = false;
        }
    } 
    
    private void IsCropEnabled()
    {
        if (_isCropEnabled)
            _isCropEnabled = false;
        else if (!_isCropEnabled)
            _isCropEnabled = true;
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
