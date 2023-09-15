using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Item : MonoBehaviour
{
    [SerializeField]
    private GameObject baseFlavor, flavor, strength;

    private void Update()
    {
        CheckForBaseFlavor(baseFlavor);
    }
    void CheckForBaseFlavor(GameObject b)
    {
    
    }
    private void OnCollisionEnter(Collision collision)
    {
        if(collision.collider.tag == "BaseFlavor")
        {
            baseFlavor = collision.gameObject;
            Debug.Log("Base Flavor Found");
            Destroy(collision.gameObject);

        }
        else if (collision.collider.tag == "Flavor")
        {
            flavor = collision.gameObject;
        }
        else if(collision.collider.tag == "strength")
        {
            strength = collision.gameObject;
        }
        else
        {
            // Instantiate(collision.gameObject, this.transform, true);
            //Destroy(collision.gameObject);
            Debug.Log("Incorrect item Type");
        }

    }
}
