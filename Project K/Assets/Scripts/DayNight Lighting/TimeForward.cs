using Player;
using UnityEngine;
using UnityEngine.Serialization;

//Script for Fast forward power
public class TimeForward : MonoBehaviour
{
    [FormerlySerializedAs("_dayManager")] [SerializeField] private DayManager dayManager;
    [SerializeField] private PowerManager powerManager;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            powerManager.timePower = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            powerManager.timePower = false;
        }
    }
}
