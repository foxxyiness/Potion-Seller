using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Items;

public class Potion : MonoBehaviour
{
    [SerializeField] private GameObject toastedUpgrade;
    [SerializeField] private bool toastable;
    [SerializeField] private int roastLevel;
    [SerializeField] private string baseText;
    [SerializeField] private string strengthText;
    [SerializeField] private string flavorText;
    [SerializeField] private Base baseType;
    [SerializeField] private Strength strengthType;
    [SerializeField] private Flavor flavorType;
    private void Start()
    {
        GetComponent<Item>();
        roastLevel = 2;
    }

    private enum Base
    {
        Water,
        Light,
        Darkness
        
    }
    private enum Strength
    {
        Sun,
        StormVine,
        Fireshot
    }
    private enum Flavor
    {
        Hair,
        IcePepper,
        Toasted
    }
    public string GetToastable()
    {
        var answer = toastable ? "Yes" : "No";
        return answer;
    }
    public string GetBaseText()
    {
        return baseType.ToString();
    }
    public string GetStrengthText()
    {
        return strengthType.ToString();
    }
    public string GetFlavorText()
    {
        return flavorType.ToString();
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
