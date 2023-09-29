using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Item : MonoBehaviour
{
    private enum Type
    {
        Light,
        Healing,
        Poison,
        Death
    }

    [SerializeField]
    private Type type = Type.Light;
    [SerializeField]
    private string name;
    [SerializeField]
    private string description;

    public string GetName()
    {
        return name;
    }
    
}
