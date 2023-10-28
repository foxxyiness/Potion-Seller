using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Items
{
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
        /*private static bool HasItemName(Item item)
        {
            return item;
        }*/
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
                }
                else
                {
                    Debug.Log("Nothing has been created, items shall be returned to you at once");
                    //ReturnItems();
                }
               
            }
        }

        private void ReturnItems()
        {
            foreach (Item item in itemList)
            {
                GameObject itemSpawn = Instantiate(item.gameObject, potionSpawnPoint.position, Quaternion.identity);
                itemSpawn.transform.localScale = new Vector3(5f, 5f, 5f);
            }

            StartCoroutine(ClearList());
        }
        private void SpawnPotion(int recipeIndex)
        {
            if (recipeIndex <= 5)
            {
                //Potion of Light
                Instantiate(recipeResults[0], potionSpawnPoint);
                //CheckCorrectItem(recipeResults[0]);
                Debug.Log("Potion of Light Created");
                StartCoroutine(ClearList());
            }
            else if(recipeIndex is >= 6 and <= 11)
            {
                //Potion of Darkness
                Instantiate(recipeResults[1], potionSpawnPoint);
                //CheckCorrectItem(recipeResults[1]);
                Debug.Log("Potion of Darkness Created");
                StartCoroutine(ClearList());
            }
            
        }
        private IEnumerator ClearList()
        {
            yield return new WaitForSeconds(1.5F);
            itemList.Clear();
            SetBoolFalse();
        }
        private void CheckForItemCount()
        {
            if (itemList.Count != 3) return;
            CheckForCompleteRecipe();
        }

        private void SetBoolFalse()
        {
            allowBase = true;
            allowStrength = true;
            allowFlavor = true;
        }
        private void OnCollisionEnter(Collision collision)
        {
            if (collision.collider.CompareTag("BaseFlavor") && allowBase)
            {
                //baseFlavor = collision.gameObject;
                Debug.Log("Base Flavor Found");
                collision.gameObject.transform.localScale = Vector3.zero;
                collision.rigidbody.constraints = RigidbodyConstraints.FreezeAll;
                
                itemList.Add(collision.gameObject.GetComponent<Item>());
                allowBase = false;
                CheckForItemCount();
            

            }
            else if (collision.collider.CompareTag("Flavor") && allowFlavor)
            {
                //flavor = collision.gameObject;
                Debug.Log("Flavor Found");
                collision.gameObject.transform.localScale = Vector3.zero;
                collision.rigidbody.constraints = RigidbodyConstraints.FreezeAll;
                itemList.Add(collision.gameObject.GetComponent<Item>());
                allowFlavor = false;
                CheckForItemCount();
            }
            else if (collision.collider.CompareTag("Strength") && allowStrength)
            {
                //strength = collision.gameObject;
                Debug.Log("Strength Found");
                collision.gameObject.transform.localScale = Vector3.zero;
                collision.rigidbody.constraints = RigidbodyConstraints.FreezeAll;
                itemList.Add(collision.gameObject.GetComponent<Item>());
                allowStrength = false;
                CheckForItemCount();
            }
            else
            {
                // ReSharper disable once CommentTypo
                //Rejects Gameobjects Given
                collision.gameObject.GetComponent<Rigidbody>().AddForce(0, 5, 3, ForceMode.Impulse);
            }


        }
    }
}
