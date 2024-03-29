using System;
using System.Collections;
using System.Collections.Generic;
using System.Net;
using Unity.VisualScripting;
using UnityEngine;
using Random = System.Random;

public class GrowthStage : MonoBehaviour
{
    [Header("Time & Random Time")]
    [SerializeField] private DayManager _dayManager;

    [SerializeField] private ItemManager itemManager;
    [SerializeField] private int minValue, maxValue;

    [Header("Growth Numbers")] 
    public int currentProgression = 0;

    public float currentTimeBeforeStage;
    public int timeBetweenGrowths;
    [SerializeField] private int maxGrowth;
    [SerializeField] private GameObject crop;
    [SerializeField] private AudioSource audioSource;
    
    public AudioClip FinishedSound;
    public Transform[] stages;
    public List<Transform> crops = new List<Transform>();
    public bool isCropEnabled;


    // Start is called before the first frame update
    void Start()
    {
        minValue = 60;
        maxValue = 120;
        SelectRandomTimeBetweenGrowth();
        //loops growth function till finished growing
        //InvokeRepeating("Growth", timeBetweenGrowths, timeBetweenGrowths);
    }

    private void SelectRandomTimeBetweenGrowth()
    {
        var rand = new Random();
        timeBetweenGrowths = rand.Next(minValue, maxValue);
        currentTimeBeforeStage = timeBetweenGrowths;
    }

    private void Update()
    {
        Growth();
    }

    private void Growth()
    {
        if (isCropEnabled)
        {
            currentTimeBeforeStage -= Time.deltaTime;
            if (currentTimeBeforeStage <= 0.0f)
            {
                SelectRandomTimeBetweenGrowth();
                Progression();
            }
        }
      
    }

    public void EnableCrop()
    {
        if (isCropEnabled)
        {
            isCropEnabled = false;
            //itemManager.currentEnabledCrops.Remove(gameObject);
        }
        else if (!isCropEnabled)
        {
            isCropEnabled = true;
            //itemManager.currentEnabledCrops.Add(gameObject);
        }
    }
    private void Progression()
    {
        currentProgression++;
        if (currentProgression == 1)
        {
            stages[0].gameObject.SetActive(true);
        }
        else if (currentProgression == 2)
        {
            stages[0].gameObject.SetActive(false);
            stages[1].gameObject.SetActive(true);
        }
        else if (currentProgression == 3)
        {
            stages[1].gameObject.SetActive(false);
            stages[2].gameObject.SetActive(true);
        }
        else if (currentProgression > 3)
        {
            stages[2].gameObject.SetActive(false);
            GameObject temp = Instantiate(crop,transform.position,Quaternion.identity);
            crops.Add(temp.transform);
            //infinate crop for now?
            currentProgression = 0;
        }
    }

    int CountCrops()
    {
        for (int n = crops.Count - 1; n > 0; n--)
        {
            if (crops[n] == null)
                crops.RemoveAt(n);
        }

        return crops.Count;
    }
}
