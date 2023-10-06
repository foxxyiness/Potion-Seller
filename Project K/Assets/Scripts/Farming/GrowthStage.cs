using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrowthStage : MonoBehaviour
{

    private int currentProgression = 0;
    public int timeBetweenGrowths;
    public int maxGrowth;
    public GameObject crop;
    [SerializeField]
    private AudioSource audioSource;
    
    public AudioClip FinishedSound;
    public Transform[] stages;
    public List<Transform> crops = new List<Transform>();


    // Start is called before the first frame update
    void Start()
    {
        //loops growth function till finished growing
        InvokeRepeating("Growth", timeBetweenGrowths, timeBetweenGrowths);

    }

    // Update is called once per frame
    void Update()
    {
        
    }


    public void Growth()
    {
        
        if (currentProgression != maxGrowth && currentProgression < stages.Length)
        {
            stages[currentProgression].gameObject.SetActive(true);
        }
        if (currentProgression > 0 && currentProgression < maxGrowth) 
        {
            stages[currentProgression -1].gameObject.SetActive(false);
        }
        if (currentProgression < maxGrowth)
        {
            currentProgression++;
        }
         else if(currentProgression == maxGrowth && CountCrops() < 3) 
        {
            GameObject temp = Instantiate(crop,transform.position,Quaternion.identity);
            crops.Add(temp.transform);
            stages[currentProgression-1].gameObject.SetActive(false);
            //infinate crop for now?
            currentProgression = currentProgression - maxGrowth;


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
