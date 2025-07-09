using Player;
using UnityEngine;
using UnityEngine.Serialization;

//Script for Fast forward power
public class TimeForward : MonoBehaviour
{
    [FormerlySerializedAs("_dayManager")] [SerializeField] private DayManager dayManager;
    [SerializeField] private PowerManager powerManager;

    public void DoTimeForward()
    {
        StartCoroutine(powerManager.TimeForwardCoroutine());
    }

}
