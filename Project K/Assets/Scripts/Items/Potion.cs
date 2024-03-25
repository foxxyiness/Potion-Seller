using System;
using UnityEngine;
using Items;

public class Potion : MonoBehaviour
{
    [SerializeField] private GameObject toastedUpgrade;
    [SerializeField] private bool toastable;
    [SerializeField] private int roastLevel;
    [SerializeField] private string archetype;
    [SerializeField] private string baseText;
    [SerializeField] private string strengthText;
    [SerializeField] private string flavorText;
    [SerializeField] private Archetype archeType;
    [SerializeField] private Base baseType;
    [SerializeField] private Strength strengthType;
    [SerializeField] private Flavor flavorType;
    [SerializeField] private float speed;
    [SerializeField] private float velocityLimit;
    [SerializeField] private float currentVelocity;
    private Rigidbody rb;
    private bool isBreakable;
    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        speed = rb.velocity.sqrMagnitude;
        GetComponent<Item>();
        roastLevel = 2;
    }
    

    private enum Archetype
    {
        Light,
        Darkness,
        Harmony,
        Chaos,
        Coffee
    }
    private enum Base
    {
        Water,
        Light,
        Darkness,
        Harmony,
        Chaos,
        Coffee
        
    }
    private enum Strength
    {
        Sun,
        StormVine,
        PhoenixBerry,
        AshCapMushroom,
        GoblinLeaf,
        Shoot
    }
    private enum Flavor
    {
        Hair,
        IcePepper,
        BarbedBamboo,
        PixieCabbage,
        EagleBlossom,
        Fire
    }

    public string GetArchetype()
    {
        return archetype.ToString();
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
