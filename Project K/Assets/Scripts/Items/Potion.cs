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
    private void Start()
    {
        potion = GetComponent<Item>();
        roastLevel = 2;
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
