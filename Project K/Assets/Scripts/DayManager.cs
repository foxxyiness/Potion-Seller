using Orders;
using UnityEngine;

public class DayManager : MonoBehaviour
{
    [SerializeField] 
    private OrderManager orderManager;
    
    [SerializeField]
    private int totalMin = 1440;

    [SerializeField]
    private float timer;

    [SerializeField]
    private int hour, min;

    private int _totalDays;
    
    public int getTotalDays => _totalDays;
    // Start is called before the first frame update
    
    //for every 1.5 seconds, subtract 1 from _totalMin
    
    // Update is called once per frame
    private void Update()
    {
        Timer();
    }

    private void Timer()
    {
        //Timer cycle for Day
        timer += Time.deltaTime;
        while (timer >= 1.5f)
        {
            timer = 0f;
            totalMin--;
            min++;
        }
        DayTime();
    }
    private void DayTime()
    {
        //Resets Minute and add Hour
        if (min >= 60)
        {
            min = 0;
            hour++;
        }

        //Resets Day and Hour
        if (hour < 24) return;
        hour = 0;
        totalMin = 1440;
        AddDay();
    }

    private void AddDay()
    {
        //Adds day and checks and iterates difficulty
        _totalDays++;
        if (_totalDays % 3 == 0 && orderManager.getCurrentState == OrderManager.Difficulty.VeryHard)
        {
            orderManager.AddDifficulty();
        }
        StartCoroutine(orderManager.StartOfDay());
    }
}
