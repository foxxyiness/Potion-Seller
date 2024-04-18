using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using Items;
using Pages;
using Player;
using TMPro;
using UI___Menu;
using Unity.Mathematics;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;
using UnityEngine.XR.Interaction.Toolkit;
using Random = System.Random;

public class TutorialManager : MonoBehaviour
{
    [Header("Tutorial Important Prefabs")]
    [SerializeField] private TutorialPowerManager tutorialPowerManager;

    [SerializeField] private TutorialCauldron tutorialCauldron;
    [SerializeField] public int currentState;
    [SerializeField] private TextMeshProUGUI textContent;
    [SerializeField] private GameObject playButton;
    [SerializeField] private GameObject nextButton;
    [SerializeField] private GameObject orderUIContent;
    
    [Header("Tutorial Instantiation Objects")] 
    [SerializeField] private GameObject cauldron;
    [SerializeField] private GameObject icepepperCrop;
    [SerializeField] private GameObject stormvineCrop;
    [SerializeField] private GameObject waterOrb, sunOrb, hairOrb, deliveryBox, darknessPotion;
    [SerializeField] private GameObject book;
    [SerializeField] private GameObject lostPage;
    [SerializeField] private AudioSource bellAudioSource;
    [SerializeField] private AudioClip bellClip, distantChime;
    [SerializeField] private Transform orbSpawnpoint;
    private string _contentString;
    
    
    public TextMeshProUGUI text; 
    void Start()
    {
        UpdateState();
        playButton.SetActive(false);
        nextButton.SetActive(false);
        cauldron.SetActive(false);
        icepepperCrop.SetActive(false);
        stormvineCrop.SetActive(false);
        book.SetActive(false);
        lostPage.SetActive(false);
        deliveryBox.SetActive(false);
    }

    public void AddState()
    {
        Destroy(text.gameObject);
        currentState++;
        UpdateState();
    }

    IEnumerator TimeDelay()
    {
        yield return new WaitForSeconds(10);
        AddState();
    }

    void CheckPlayerMovement()
    {
        var playerStart = tutorialPowerManager.gameObject.transform.position;
        
         void Update()
        {
            var player = tutorialPowerManager.gameObject.transform.position;
            if (playerStart != player)
            {
                textContent.text = "Great!";
                StartCoroutine(TimeDelay());
            }
        }
    }
    public void UpdateState()
    {
        switch (currentState)
        {
            case 0:
            {
                _contentString = "Lets begin with the tutorial.";
                textContent.text = _contentString;
                text = Instantiate(textContent, orderUIContent.transform.position, orderUIContent.transform.rotation, orderUIContent.transform);
                StartCoroutine(TimeDelay());
                break;
            }
            case 1:
            {
                _contentString = "Try moving by rotating the thumb stick on your left controller";
                textContent.text = _contentString;
                text = Instantiate(textContent, orderUIContent.transform.position, orderUIContent.transform.rotation, orderUIContent.transform);
                StartCoroutine(TimeDelay());
                break;
            }
            case 2:
            {
                _contentString = "Try rotating by using the thumb stick on your right controller.";
                textContent.text = _contentString;
                text = Instantiate(textContent, orderUIContent.transform.position, orderUIContent.transform.rotation, orderUIContent.transform);
                StartCoroutine(TimeDelay());
                break;
            }
            case 3:
            {
                tutorialPowerManager.initialFire = true;
                tutorialPowerManager.RestoreMana(2000);
                _contentString =
                    "You've been born with godly abilities, meaning you are a god. Now, lets try your fire ability.\n Lift your right hand, " +
                    "hold the grip button, and tap the trigger.";
                textContent.text = _contentString;
                text = Instantiate(textContent, orderUIContent.transform.position, orderUIContent.transform.rotation, orderUIContent.transform);
                break;
            }
            case 4:
            {
                tutorialPowerManager.initialFire = false;
                _contentString = "Great, you'll be able to toast certain potions with that ability.";
                textContent.text = _contentString;
                text = Instantiate(textContent, orderUIContent.transform.position, orderUIContent.transform.rotation, orderUIContent.transform);
                StartCoroutine(TimeDelay());
                break;
            }
            case 5:
            {
                tutorialPowerManager.initialSun = true;
                tutorialPowerManager.RestoreMana(2000);
                _contentString =
                    "Lets try your Sun power, harnessing the laws of photosynthesis.\n Lift your left hand, " +
                    "hold the grip button, and tap the trigger.";
                textContent.text = _contentString;
                text = Instantiate(textContent, orderUIContent.transform.position, orderUIContent.transform.rotation, orderUIContent.transform);
                break;
            }
            case 6:
            {
                tutorialPowerManager.initialSun = false;
                _contentString = "Great, you'll be able to use this ability to promote growth on your crops.";
                textContent.text = _contentString;
                text = Instantiate(textContent, orderUIContent.transform.position, orderUIContent.transform.rotation, orderUIContent.transform);
                StartCoroutine(TimeDelay());
                break;
            }
            case 7:
            {
                nextButton.SetActive(true);
                _contentString = "You can make a potion by putting the correct combination into the cauldron. \n " +
                                "For each potion, you must include 1 base orb, 1 flavor orb, and 1 strength orb.";
                //contentString = "Lets try making a potion.";
                textContent.text = _contentString;
                cauldron.SetActive(true);
                text = Instantiate(textContent, orderUIContent.transform.position, orderUIContent.transform.rotation, orderUIContent.transform);
                break;
            }

            case 8:
            {
                nextButton.SetActive(false);
                Instantiate(waterOrb, orbSpawnpoint.position, quaternion.identity);
                _contentString =
                    "Try it for yourself. Grab the water orb and put it into the Cauldron.";
                textContent.text = _contentString;
                text = Instantiate(textContent, orderUIContent.transform.position, orderUIContent.transform.rotation,
                    orderUIContent.transform);
                break;
            }
            
            case 9:
            {
                Instantiate(sunOrb, orbSpawnpoint.position, quaternion.identity);
                _contentString =
                    "Great. Water orbs is the base of all potions. Every potion needs water. Now grab the sun orb and put it into the Cauldron.";
                textContent.text = _contentString;
                text = Instantiate(textContent, orderUIContent.transform.position, orderUIContent.transform.rotation,
                    orderUIContent.transform);
                break;
            }

            case 10:
            {
                Instantiate(hairOrb, orbSpawnpoint.position, quaternion.identity);
                _contentString =
                    "Great. Sun orbs is the strength component of potions. Different strength ingredients are grown from your crops. \n " +
                    "Now grab the hair orb and put it into the Cauldron.";
                textContent.text = _contentString;
                text = Instantiate(textContent, orderUIContent.transform.position, orderUIContent.transform.rotation,
                    orderUIContent.transform);
                break;
            }
            
            case 11:
            {
                tutorialCauldron.initialPotionOfLight = false;
                _contentString =
                    "You have made the Potion of Light. Light Potions are unique and can be toasted using your fire ability. \n" +
                    "Lets try it now. Shoot the Potion of Light. ";
                textContent.text = _contentString;
                text = Instantiate(textContent, orderUIContent.transform.position, orderUIContent.transform.rotation,
                    orderUIContent.transform);
                StartCoroutine(TimeDelay());
                break;
            }
            
            case 12:
            {
                icepepperCrop.SetActive(true);
                stormvineCrop.SetActive(true);
                _contentString =
                    "Excellent! Now look behind you to find your crops.";
                textContent.text = _contentString;
                text = Instantiate(textContent, orderUIContent.transform.position, orderUIContent.transform.rotation,
                    orderUIContent.transform);
                StartCoroutine(TimeDelay());
                break;
            }
            
            case 13:
            {
                nextButton.SetActive(true);
                _contentString =
                    "Your farm will contains the necessary ingredients to make all potions, " +
                    "but worldly constraints prevent are preventing them from growing.";
                textContent.text = _contentString;
                text = Instantiate(textContent, orderUIContent.transform.position, orderUIContent.transform.rotation,
                    orderUIContent.transform);
                break;
            }
            case 14:
            {
                _contentString =
                    "Your godly aura can make the crops grow. Stand on top of the crop sign and look at your mana ring on your left hand.";
                textContent.text = _contentString;
                text = Instantiate(textContent, orderUIContent.transform.position, orderUIContent.transform.rotation,
                    orderUIContent.transform);
                break;
            }
            case 15:
            {
                _contentString =
                    "You mana ring on your left hand shows the status of your mana control over the crops.";
                textContent.text = _contentString;
                text = Instantiate(textContent, orderUIContent.transform.position, orderUIContent.transform.rotation,
                    orderUIContent.transform);
                break;
            }
            case 16:
            {
                tutorialPowerManager.RestoreMana(2000);
                _contentString =
                    "You sun ability is used to grow crops faster. Now try it out. Shoot one of the crops with the power of the sun.";
                textContent.text = _contentString;
                text = Instantiate(textContent, orderUIContent.transform.position, orderUIContent.transform.rotation,
                    orderUIContent.transform);
                break;
            }
            case 17:
            {
                nextButton.SetActive(true);
                _contentString =
                    "Great, this ingredient, known as Ice Pepper. Now grab it and put it into the cauldron.";
                textContent.text = _contentString;
                text = Instantiate(textContent, orderUIContent.transform.position, orderUIContent.transform.rotation,
                    orderUIContent.transform);
                break;
            }
            case 18:
            {
                _contentString =
                    "Now turn on the Storm Vine crop, use your sun ability, and put Storm Vine into the cauldron.";
                textContent.text = _contentString;
                text = Instantiate(textContent, orderUIContent.transform.position, orderUIContent.transform.rotation,
                    orderUIContent.transform);
                break;
            }
            case 19:
            {
                Instantiate(waterOrb, orbSpawnpoint.position, quaternion.identity);
                _contentString =
                    "Now add a Water Orb into the Cauldron.";
                textContent.text = _contentString;
                text = Instantiate(textContent, orderUIContent.transform.position, orderUIContent.transform.rotation,
                    orderUIContent.transform);
                break;
            }
            case 20:
            {
                deliveryBox.SetActive(true);
                nextButton.SetActive(false);
                _contentString =
                    "You have now created a Potion of Darkness. Now put it into the box to the left.";
                textContent.text = _contentString;
                text = Instantiate(textContent, orderUIContent.transform.position, orderUIContent.transform.rotation,
                    orderUIContent.transform);
                StartCoroutine(TimeDelay());
                break;
            }
            case 21:
            {
                var potion = Instantiate(darknessPotion, orbSpawnpoint.position, quaternion.identity);
                potion.transform.localScale = new Vector3(5f, 5f, 5f);
                _contentString =
                    "This is how you will deposit the potions you make that are on order. " +
                    "\n Now, lets try making that Potion of Darkness into Coffee";
                //Spawn Potion of Darkness 
                textContent.text = _contentString;
                text = Instantiate(textContent, orderUIContent.transform.position, orderUIContent.transform.rotation,
                    orderUIContent.transform);
                StartCoroutine(TimeDelay());
                break;
            }
            case 22:
            {
                _contentString =
                    "Great, breaking the coffee will give you a temporary speed boost.";
                textContent.text = _contentString;
                text = Instantiate(textContent, orderUIContent.transform.position, orderUIContent.transform.rotation,
                    orderUIContent.transform);
                StartCoroutine(TimeDelay());
                break;
            }
            case 23:
            {
                book.SetActive(true);
                nextButton.SetActive(true);
                _contentString =
                    "Now look towards your right for your recipe book. There you'll find your found potion recipes.";
                textContent.text = _contentString;
                text = Instantiate(textContent, orderUIContent.transform.position, orderUIContent.transform.rotation,
                    orderUIContent.transform);
                break;
            }
            case 24:
            {
                lostPage.SetActive(true);
                _contentString =
                    "If you see a potion on order that you've never seen before, you must find the lost page.";
                textContent.text = _contentString;
                text = Instantiate(textContent, orderUIContent.transform.position, orderUIContent.transform.rotation,
                    orderUIContent.transform);
                break;
            }
            case 25:
            {
                nextButton.SetActive(false);
                tutorialPowerManager.RestoreMana(2000);
                _contentString =
                    "Now, Shoot the lost page with your fire ability.";
                textContent.text = _contentString;
                text = Instantiate(textContent, orderUIContent.transform.position, orderUIContent.transform.rotation,
                    orderUIContent.transform);
                break;
            }
            case 26:
            {
                nextButton.SetActive(true);
                _contentString =
                    "Once shot, the lost page will added to your recipe book.";
                textContent.text = _contentString;
                text = Instantiate(textContent, orderUIContent.transform.position, orderUIContent.transform.rotation,
                    orderUIContent.transform);
                break;
            }
            case 27:
            {
                bellAudioSource.clip = bellClip;
                bellAudioSource.Play();
                _contentString =
                    "Remember this bell, it rings only 3 times a day. If you complete all orders, \n " +
                    "you will hear the single bell ring at the start of the day.";
                textContent.text = _contentString;
                text = Instantiate(textContent, orderUIContent.transform.position, orderUIContent.transform.rotation,
                    orderUIContent.transform);
                StartCoroutine(TimeDelay());
                break;
            }
            case 28:
            {
                bellAudioSource.clip = distantChime;
                bellAudioSource.Play();
                _contentString =
                    "If you hear this bell, you have failed to complete all orders, and your Earthly contract will be voided.";
                textContent.text = _contentString;
                text = Instantiate(textContent, orderUIContent.transform.position, orderUIContent.transform.rotation,
                    orderUIContent.transform);
                StartCoroutine(TimeDelay());
                break;
            }
            
            default:
            {
                nextButton.SetActive(false);
                _contentString = "You Earth-Bound contract begins now. Don't fail us.";
                textContent.text = _contentString;
                text = Instantiate(textContent, orderUIContent.transform.position, orderUIContent.transform.rotation, orderUIContent.transform);
                playButton.SetActive(true);
                break;
            }
            

        }
        
    }
   
}
