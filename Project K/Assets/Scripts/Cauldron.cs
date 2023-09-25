using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Cualdron : MonoBehaviour
{
    private Item currentItem;
    /*[SerializeField]
    private GameObject baseFlavor, flavor, strength;*/
    [SerializeField]
    private bool allowBase, allowFlavor, allowStrength;
    [SerializeField]
    private List<Item> itemList = new List<Item>();
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
    private void Update()
    {
        /*CheckForBaseFlavor(baseFlavor);*/
        //CheckForItemCount();
    }
    /* void CheckForBaseFlavor(GameObject b)
     {

     }*/
    static bool HasItemName(Item item)
    {
        return item;
    }
    void CheckForCompleteRecipe()
    {
        string currentRecipe = "";
        foreach(Item item in itemList)
        {
            if(item != null)
            {
                currentRecipe += item.getName();
            }
            else
            {
                currentRecipe += "null";
            }
        }
        for(int i = 0; i < recipes.Length; i++)
        {
            if (recipes[i] == currentRecipe) 
            {
                SpawnPotion(i);
            }
            else
            {
                Debug.Log("Nothing has been created, items shall be returned to you at once");
                itemList.RemoveAll(HasItemName);
            }
        }
    }
    void SpawnPotion(int recipeindex)
    {
        if(recipeindex <= 5)
        {
            Instantiate(recipeResults[0], potionSpawnPoint);
            Debug.Log("Potion of Light Created");
            itemList.RemoveAll(HasItemName);
            return;
        }
    }
    void CheckForItemCount()
    {
        if (itemList.Count == 3)
        {
            CheckForCompleteRecipe();
        }
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.tag == "BaseFlavor" && allowBase)
        {
            //baseFlavor = collision.gameObject;
            Debug.Log("Base Flavor Found");
            collision.gameObject.transform.localScale = Vector3.zero;
            itemList.Add(collision.gameObject.GetComponent<Item>());
            allowBase = false;
            CheckForItemCount();
            

        }
        else if (collision.collider.tag == "Flavor" && allowFlavor)
        {
            //flavor = collision.gameObject;
            Debug.Log("Flavor Found");
            collision.gameObject.transform.localScale = Vector3.zero;
            itemList.Add(collision.gameObject.GetComponent<Item>());
            allowFlavor = false;
            CheckForItemCount();
        }
        else if (collision.collider.tag == "Strength" && allowStrength)
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
