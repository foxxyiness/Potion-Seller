using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Items;

public class Potion : MonoBehaviour
{
    private Item potion;
    [SerializeField] private GameObject toastedUpgrade;
    [SerializeField] private bool toastable;
    [SerializeField] private int roastLevel;
    [SerializeField] private string baseText;
    [SerializeField] private string strengthText;
    [SerializeField] private string flavorText;
    private void Start()
    {
        potion = GetComponent<Item>();
        roastLevel = 2;
    }

    public string GetToastable()
    {
        var answer = toastable ? "Yes" : "No";
        return answer;
    }
    public string GetBaseText()
    {
        return baseText;
    }
    public string GetStrengthText()
    {
        return strengthText;
    }
    public string GetFlavorText()
    {
        return flavorText;
    }
    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("Fire") && toastable)
        {
            roastLevel--;
            CheckRoast();
        }
    }

    private void CheckRoast()
    {
        if (roastLevel == 0)
        {
            GameObject toastedPotion = Instantiate(toastedUpgrade, transform.position, transform.rotation);
            Destroy(this.gameObject);
        }
    }
}
