using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Item : MonoBehaviour
{
    [SerializeField]
    private string type;
    [SerializeField]
    private string Name;
    [SerializeField]
    private string description;

    public string GetName()
    {
        return Name;
    }

}
