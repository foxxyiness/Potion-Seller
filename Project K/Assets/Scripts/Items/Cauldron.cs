using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Items
{
    public class Cauldron : MonoBehaviour
    {
        [SerializeField]
        private bool allowBase, allowFlavor, allowStrength, potionFound;

        [SerializeField] private AudioSource audioSource;
        [SerializeField] private List<Item> itemList;
        [SerializeField] private string[] recipes;
        [SerializeField] private Item[] recipeResults;
        [SerializeField] private Transform potionSpawnPoint;
        [SerializeField] private ParticleSystem potionSpawnParticleSystem;
        [SerializeField] private ParticleSystem cauldronParticleSystem;

        private string _currentRecipe;
        private void Start()
        {
            allowBase = true;
            allowFlavor = true;
            allowStrength = true;
            potionFound = false;
            itemList = new List<Item> {null, null, null};
        }
        private void CheckForCompleteRecipe()
        {
            
            foreach(Item item in itemList)
            {
                if(item != null)
                {
                    _currentRecipe += item.GetName();
                }
                else
                {
                    _currentRecipe += "null";
                }
            }
            for(var i = 0; i < recipes.Length; i++)
            {
                if (recipes[i] == _currentRecipe)
                {
                    potionFound = true;
                    StartCoroutine(SpawnPotion(i));
                    break;
                }
               
            }

            if (potionFound)
            {
                potionFound = false;
            }
            else if (!potionFound)
            {
                Debug.Log(_currentRecipe);
                Debug.Log("Items have been returned to you");
                ReturnItems();
            }
        }
    
        //Returns All items in Items List, Calls Clear List to destroy the items in Cauldron and List
        private void ReturnItems()
        {
            foreach (var itemSpawnGameObject in itemList.Select(item => Instantiate(item.gameObject, potionSpawnPoint.transform.position, Quaternion.identity)))
            {
                //itemSpawnGameObject.transform.localScale = new Vector3(0.15f, 0.15f, 0.15f);
                itemSpawnGameObject.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.None;
            }
            
            StartCoroutine(ClearList());
        }
        
        private IEnumerator SpawnPotion(int recipeIndex)
        {
            //Plays Fire Swoosh and spawns in potion after swoosh
            audioSource.Play();
            potionSpawnParticleSystem.Play();
            yield return new WaitForSeconds(2.25F);
            Instantiate(recipeResults[recipeIndex], potionSpawnPoint);
            potionSpawnParticleSystem.Stop();
            //CheckCorrectItem(recipeResults[0]);
            Debug.Log("Potion Created");
            StartCoroutine(ClearList());
            
        }
        //Destroys Items in Cauldron and ItemList
        private IEnumerator ClearList()
        {
            yield return new WaitForSeconds(1.5F);
            foreach (var item in itemList)
            {
                Destroy(item.gameObject);
            }
            itemList.Clear();
            SetBoolFalse();
            _currentRecipe = String.Empty;
            StartCoroutine(RestoreList());
        }

        private IEnumerator RestoreList()
        {
            yield return new WaitForSeconds(.5F);
            itemList = new List<Item> {null, null, null};
        }

        private IEnumerator CauldronSmoke(Color color)
        {
            ParticleSystem.MainModule mainModule = cauldronParticleSystem.main;
            mainModule.startColor = color;
            cauldronParticleSystem.Play();
            yield return new WaitForSeconds(1f);
            cauldronParticleSystem.Stop();
        }
        
        //Checks for count of ItemList to determine if completeRecipe should be called
        private void CheckForItemCount()
        {
            //if (itemList.Count != 3) return;
            if (itemList.Count == 6)
            {
                itemList.RemoveAll(item => item == null);
                CheckForCompleteRecipe();
            }
            //CheckForCompleteRecipe();
        }

        //Resets All Booleans back to true
        private void SetBoolFalse()
        {
            allowBase = true;
            allowStrength = true;
            allowFlavor = true;
        }
        //All Collision Detections to determine flavor combinations
        private void OnCollisionEnter(Collision collision)
        {
            if (collision.collider.CompareTag("BaseFlavor") && allowBase)
            {
                //baseFlavor = collision.gameObject;
                Debug.Log("Base Flavor Found");
                collision.gameObject.transform.localScale = Vector3.zero;
                collision.rigidbody.constraints = RigidbodyConstraints.FreezeAll;
                itemList.Insert(0,collision.gameObject.GetComponent<Item>());
                StartCoroutine(CauldronSmoke(Color.blue)); //*************************************TEMP*****************//
                //itemList.Add(collision.gameObject.GetComponent<Item>());
                allowBase = false;
                CheckForItemCount();
            

            }
            else if (collision.collider.CompareTag("Flavor") && allowFlavor)
            {
                //flavor = collision.gameObject;
                Debug.Log("Flavor Found");
                collision.gameObject.transform.localScale = Vector3.zero;
                collision.rigidbody.constraints = RigidbodyConstraints.FreezeAll;
                itemList.Insert(2, collision.gameObject.GetComponent<Item>());
                StartCoroutine(CauldronSmoke(Color.green));  //*************************************TEMP*****************//
                //itemList.Add(collision.gameObject.GetComponent<Item>());
                allowFlavor = false;
                CheckForItemCount();
            }
            else if (collision.collider.CompareTag("Strength") && allowStrength)
            {
                //strength = collision.gameObject;
                Debug.Log("Strength Found");
                collision.gameObject.transform.localScale = Vector3.zero;
                collision.rigidbody.constraints = RigidbodyConstraints.FreezeAll;
                itemList.Insert(1,collision.gameObject.GetComponent<Item>());
                StartCoroutine(CauldronSmoke(Color.yellow));  //*************************************TEMP*****************//
                //itemList.Add(collision.gameObject.GetComponent<Item>());
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
