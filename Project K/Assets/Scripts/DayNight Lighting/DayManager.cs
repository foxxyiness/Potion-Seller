using System.Collections;
using Orders;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Serialization;

//[ExecuteInEditMode]
public class DayManager : MonoBehaviour
{
    [SerializeField] private OrderManager orderManager;
    [SerializeField] private int totalMin;
    [SerializeField] private float timer;
    [SerializeField] private float hour, clampHour;
    [SerializeField] private int min;
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private int totalDays;
    public float timerTick;
    public int getTotalDays => totalDays;
    public float getClampHour => clampHour;

    public bool doFastForward;
    // Start is called before the first frame update
    private void Awake()
    {
        totalMin = 1440;
        //Makes day cycle 6 minutes
        timerTick = .25f;
    }
    //for every 1.5 seconds, subtract 1 from _totalMin

    // Update is called once per frame
    private void Update()
    {
        Timer();
        //Continuous Check for Time Skip
        if (doFastForward)
        {
            StartCoroutine(FastForward());
        }
    }

    //Fast forward function
    // ReSharper disable Unity.PerformanceAnalysis
    private IEnumerator FastForward()
    {
        Debug.Log("FAST FORWARD");
        if (totalMin > 20)
        {
            timerTick = 0.01f;
        }
        yield return new  WaitUntil(() => totalMin < 10);
        timerTick = 0.25f;
        doFastForward = false;
        
    }

    private void Timer()
    {
        //Timer cycle for Day
        timer += Time.deltaTime;
        while (timer >= timerTick)
        {
            timer = 0f;
            totalMin--;
            min++;
            clampHour = 24f - (totalMin / 60f);
            hour = Mathf.Floor(clampHour);
        }
        DayTime();
        CheckQuarterBell();
    }

    private void CheckQuarterBell()
    {
        if (totalMin == 1080)
            PlayBellChime();
        else if(totalMin == 720)
            PlayBellChime();
        else if(totalMin == 360)
            PlayBellChime();
    }

    private void PlayBellChime()
    {
        audioSource.Play();
    }
    private void DayTime()
    {
        //Resets Minute and add Hour
        if (min >= 60)
        {
            min = 0;
        }

        //Resets Day and Hour
        if (hour >= 24)
        {
            hour = 0;
            totalMin = 1440;
            AddDay();
        }
      
    }
    

    private void AddDay()
    {
        //Adds day and checks and iterates difficulty
        totalDays++;
        if (totalDays % 3 == 0 && orderManager.getCurrentState != OrderManager.Difficulty.VeryHard)
        {
            orderManager.AddDifficulty();
        }
        StartCoroutine(orderManager.StartOfDay());
    }

    public void AddTime(int x)
    {
        totalMin += x;
        if (totalMin > 1440)
            totalDays = 1440;
    }
}
