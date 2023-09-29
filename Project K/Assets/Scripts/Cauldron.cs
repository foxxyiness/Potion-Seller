using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Cauldron : MonoBehaviour
{
    [SerializeField]
    private bool allowBase, allowFlavor, allowStrength;

    [SerializeField] 
    private List<Item> itemList;
    [SerializeField]
    private string[] recipes;
    [SerializeField]
    private Item[] recipeResults;
    [SerializeField]
    private Transform potionSpawnPoint;
    private void Start()
    {
        allowBase = true;
        allowFlavor = true;
        allowStrength = true;

    }
    /* void CheckForBaseFlavor(GameObject b)
     {

     }*/
    private static bool HasItemName(Item item)
    {
        return item;
    }
    private void CheckForCompleteRecipe()
    {
        var currentRecipe = "";
        foreach(Item item in itemList)
        {
            if(item != null)
            {
                currentRecipe += item.GetName();
            }
            else
            {
                currentRecipe += "null";
            }
        }
        for(var i = 0; i < recipes.Length; i++)
        {
            if (recipes[i] == currentRecipe) 
            {
                SpawnPotion(i);
                break;
            }
            else
            {
                Debug.Log("Nothing has been created, items shall be returned to you at once");
                itemList.RemoveAll(HasItemName);
            }
        }
    }
    private void SpawnPotion(int recipeIndex)
    {
        if (recipeIndex > 5) return;
        Instantiate(recipeResults[0], potionSpawnPoint);
        Debug.Log("Potion of Light Created");
        itemList.RemoveAll(HasItemName);
    }
    private void CheckForItemCount()
    {
        if (itemList.Count != 3) return;
        CheckForCompleteRecipe();
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.CompareTag("BaseFlavor") && allowBase)
        {
            //baseFlavor = collision.gameObject;
            Debug.Log("Base Flavor Found");
            collision.gameObject.transform.localScale = Vector3.zero;
            itemList.Add(collision.gameObject.GetComponent<Item>());
            allowBase = false;
            CheckForItemCount();
            

        }
        else if (collision.collider.CompareTag("Flavor") && allowFlavor)
        {
            //flavor = collision.gameObject;
            Debug.Log("Flavor Found");
            collision.gameObject.transform.localScale = Vector3.zero;
            itemList.Add(collision.gameObject.GetComponent<Item>());
            allowFlavor = false;
            CheckForItemCount();
        }
        else if (collision.collider.CompareTag("Strength") && allowStrength)
        {
            //strength = collision.gameObject;
            Debug.Log("Strength Found");
            collision.gameObject.transform.localScale = Vector3.zero;
            itemList.Add(collision.gameObject.GetComponent<Item>());
            allowStrength = false;
            CheckForItemCount();
        }
        else
        {
            collision.gameObject.GetComponent<Rigidbody>().AddForce(0, 5, 3, ForceMode.Impulse);
            // Instantiate(collision.gameObject, this.transform, true);
            //Destroy(collision.gameObject);
            //CheckForCompleteRecipe();
        }


    }
}
