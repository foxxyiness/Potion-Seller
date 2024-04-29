using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnableCrop : MonoBehaviour
{
    [SerializeField] private GameObject leftHand;
    [SerializeField] private List<GrowthStage> cropGrowthStage;
    [SerializeField] private GameObject player;
    [SerializeField] private Image LeftHandManaStatus;
    [SerializeField] private Image textImage;
    private bool _enableCrop;
    private bool _isCropEnabled;
    private bool turnTemp;
    public float timer = 0;

    private void Start()
    {
        _enableCrop = false;
    }

    private void Update()
    {
         StartCrop();
         CheckPlayer();
    }
    
    //If enable crop is true, checks transform of left hand if true, starts 2 second timer, then enable crops
    public void StartCrop()
    {
        /*IsCropEnabled();
        foreach (var crop in cropGrowthStage)
        {
            crop.EnableCrop();
        }*/
        if (_enableCrop)
        {
            Debug.Log("LEFT HAND IN POSITION");
            timer += Time.deltaTime;
            LeftHandManaStatus.fillAmount = timer / 5.0f;
            if (timer >= 5)
            {
                if (!turnTemp) return;
                IsCropEnabled();
                foreach (var crop in cropGrowthStage)
                {
                    crop.EnableCrop();
                }
                turnTemp = false;
            }
        }
        else
        {
            timer = 0;
        }
    }

   private void CheckPlayer()
    {
        if (Vector3.Distance(transform.position, player.transform.position) <= 2.5)
        {
            _enableCrop = true;
            Debug.Log("Player in position of " + gameObject.name);
        }
        else
        {
            _enableCrop = false;
            turnTemp = true;
        }
    } 
    
    private void IsCropEnabled()
    {
        if (_isCropEnabled)
        {
            _isCropEnabled = false;
            textImage.color = new Color(119, 136, 255);
        }
        else if (!_isCropEnabled)
        {
            _isCropEnabled = true;
            textImage.color = new Color(255, 187, 0);
        }
            
    }
    
}
