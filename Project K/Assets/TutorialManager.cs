using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using Items;
using Player;
using TMPro;
using UI___Menu;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;
using UnityEngine.XR.Interaction.Toolkit;
using Random = System.Random;

public class TutorialManager : MonoBehaviour
{
    [SerializeField] private TutorialPowerManager tutorialPowerManager;
    [SerializeField] public int currentState;
    private string contentString;
    [SerializeField] private TextMeshProUGUI textContent;
    [FormerlySerializedAs("nextButton")] [SerializeField] private GameObject playButton;
    [SerializeField] private GameObject nextButton;
    [SerializeField] private GameObject orderUIContent;
    
    [Header("Tutorial Instantiation Objects")] 
    [SerializeField] private GameObject cauldron;
    [SerializeField] private GameObject icepepperCrop;
    [SerializeField] private GameObject stormvineCrop;
    
    
    public TextMeshProUGUI text; 
    void Start()
    {
        currentState = 0;
        UpdateState();
        playButton.SetActive(false);
        nextButton.SetActive(false);
    }

    public void AddState()
    {
        currentState++;
        UpdateState();
    }

    IEnumerator TimeDelay()
    {
        yield return new WaitForSeconds(10);
        Destroy(text.gameObject);
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
                contentString = "Lets begin with the tutorial.";
                textContent.text = contentString;
                text = Instantiate(textContent, orderUIContent.transform.position, orderUIContent.transform.rotation, orderUIContent.transform);
                StartCoroutine(TimeDelay());
                break;
            }
            case 1:
            {
                contentString = "Try moving by rotating the thumb stick on your left controller";
                textContent.text = contentString;
                text = Instantiate(textContent, orderUIContent.transform.position, orderUIContent.transform.rotation, orderUIContent.transform);
                StartCoroutine(TimeDelay());
                break;
            }
            case 2:
            {
                contentString = "Try rotating by using the thumb stick on your right controller.";
                textContent.text = contentString;
                text = Instantiate(textContent, orderUIContent.transform.position, orderUIContent.transform.rotation, orderUIContent.transform);
                StartCoroutine(TimeDelay());
                break;
            }
            case 3:
            {
                tutorialPowerManager.initialFire = true;
                tutorialPowerManager.RestoreMana(2000);
                contentString =
                    "You've been born with godly abilities, meaning you are a god. Now, lets try your fire ability.\n Lift your right hand, " +
                    "hold the grip button, and tap the trigger.";
                textContent.text = contentString;
                text = Instantiate(textContent, orderUIContent.transform.position, orderUIContent.transform.rotation, orderUIContent.transform);
                break;
            }
            case 4:
            {
                tutorialPowerManager.initialFire = false;
                contentString = "Great, you'll be able to toast certain potions with that ability.";
                textContent.text = contentString;
                text = Instantiate(textContent, orderUIContent.transform.position, orderUIContent.transform.rotation, orderUIContent.transform);
                StartCoroutine(TimeDelay());
                break;
            }
            case 5:
            {
                tutorialPowerManager.initialSun = true;
                tutorialPowerManager.RestoreMana(2000);
                contentString =
                    "Lets try your Sun power, harnessing the laws of photosynthesis.\n Lift your left hand, " +
                    "hold the grip button, and tap the trigger.";
                textContent.text = contentString;
                text = Instantiate(textContent, orderUIContent.transform.position, orderUIContent.transform.rotation, orderUIContent.transform);
                break;
            }
            case 6:
            {
                tutorialPowerManager.initialSun = false;
                contentString = "Great, you'll be able to use this ability to promote growth on your crops.";
                textContent.text = contentString;
                text = Instantiate(textContent, orderUIContent.transform.position, orderUIContent.transform.rotation, orderUIContent.transform);
                StartCoroutine(TimeDelay());
                break;
            }
            case 7:
            {
                nextButton.SetActive(true);
                contentString = "You can make a potion by putting the correct combination into the cauldron. \n " +
                                "For each potion, you must include 1 base orb, 1 flavor orb, and 1 strength orb.";
                //contentString = "Lets try making a potion.";
                textContent.text = contentString;
                text = Instantiate(textContent, orderUIContent.transform.position, orderUIContent.transform.rotation, orderUIContent.transform);
                break;
            }

            case 8:
            {
                nextButton.SetActive(false);
                contentString =
                    "Try it for yourself. Grab the water orb and put it into the Cauldron.";
                textContent.text = contentString;
                text = Instantiate(textContent, orderUIContent.transform.position, orderUIContent.transform.rotation,
                    orderUIContent.transform);
                break;
            }
            
            case 9:
            {
                contentString =
                    "Great. Water orbs is the base of all potions. Every potion needs water. Now grab the sun orb and put it into the Cauldron.";
                textContent.text = contentString;
                text = Instantiate(textContent, orderUIContent.transform.position, orderUIContent.transform.rotation,
                    orderUIContent.transform);
                break;
            }

            case 10:
            {
                contentString =
                    "Great. Sun orbs is the strength component of potions. Different strength ingredients are grown from your crops. \n " +
                    "Now grab the hair orb and put it into the Cauldron.";
                textContent.text = contentString;
                text = Instantiate(textContent, orderUIContent.transform.position, orderUIContent.transform.rotation,
                    orderUIContent.transform);
                break;
            }
            
            case 11:
            {
                contentString =
                    "You have made the Potion of Light. Light Potions are unique and can be toasted using your fire ability. \n" +
                    "Lets try it now. Shoot the Potion of Light. ";
                textContent.text = contentString;
                text = Instantiate(textContent, orderUIContent.transform.position, orderUIContent.transform.rotation,
                    orderUIContent.transform);
                break;
            }
            
            case 12:
            {
                contentString =
                    "Excellent! Now look behind you to find your crops.";
                textContent.text = contentString;
                text = Instantiate(textContent, orderUIContent.transform.position, orderUIContent.transform.rotation,
                    orderUIContent.transform);
                break;
            }
            
            case 13:
            {
                nextButton.SetActive(true);
                contentString =
                    "Your farm will contains the necessary ingredients to make all potions, " +
                    "but worldly constraints prevent are preventing them from growing.";
                textContent.text = contentString;
                text = Instantiate(textContent, orderUIContent.transform.position, orderUIContent.transform.rotation,
                    orderUIContent.transform);
                break;
            }
            case 14:
            {
                contentString =
                    "Your godly aura can make the crops grow. Stand on top of the crop sign and look at your mana ring on your left hand.";
                textContent.text = contentString;
                text = Instantiate(textContent, orderUIContent.transform.position, orderUIContent.transform.rotation,
                    orderUIContent.transform);
                break;
            }
            case 15:
            {
                contentString =
                    "You mana ring on your left hand shows the status of your mana control over the crops.";
                textContent.text = contentString;
                text = Instantiate(textContent, orderUIContent.transform.position, orderUIContent.transform.rotation,
                    orderUIContent.transform);
                break;
            }
            case 16:
            {
                tutorialPowerManager.RestoreMana(2000);
                contentString =
                    "You sun ability is used to grow crops faster. Now try it out. Shoot one of the crops with the power of the sun.";
                textContent.text = contentString;
                text = Instantiate(textContent, orderUIContent.transform.position, orderUIContent.transform.rotation,
                    orderUIContent.transform);
                break;
            }
            case 17:
            {
                contentString =
                    "Great, this ingredient, known as Ice Pepper. Now grab it and put it into the potion.";
                textContent.text = contentString;
                text = Instantiate(textContent, orderUIContent.transform.position, orderUIContent.transform.rotation,
                    orderUIContent.transform);
                break;
            }
            case 18:
            {
                contentString =
                    "Now turn on the Storm Vine crop, use your sun ability, and put Storm Vine into the cauldron.";
                textContent.text = contentString;
                text = Instantiate(textContent, orderUIContent.transform.position, orderUIContent.transform.rotation,
                    orderUIContent.transform);
                break;
            }
            case 19:
            {
                contentString =
                    "Now add a Water Orb into the Cauldron.";
                textContent.text = contentString;
                text = Instantiate(textContent, orderUIContent.transform.position, orderUIContent.transform.rotation,
                    orderUIContent.transform);
                break;
            }
            case 20:
            {
                contentString =
                    "You have now created a Potion of Darkness. Now put it into the box to the left.";
                textContent.text = contentString;
                text = Instantiate(textContent, orderUIContent.transform.position, orderUIContent.transform.rotation,
                    orderUIContent.transform);
                break;
            }
            case 21:
            {
                contentString =
                    "This is how you will deposit the potions you make that are on order. " +
                    "\n Now, lets try making that Potion of Darkness into Coffee";
                //Spawn Potion of Darkness 
                textContent.text = contentString;
                text = Instantiate(textContent, orderUIContent.transform.position, orderUIContent.transform.rotation,
                    orderUIContent.transform);
                break;
            }
            case 22:
            {
                contentString =
                    "Great, breaking the coffee will give you a temporary speed boost.";
                textContent.text = contentString;
                text = Instantiate(textContent, orderUIContent.transform.position, orderUIContent.transform.rotation,
                    orderUIContent.transform);
                break;
            }
            case 23:
            {
                contentString =
                    "Now look towards your right for your recipe book. There you'll find your found potion recipes.";
                textContent.text = contentString;
                text = Instantiate(textContent, orderUIContent.transform.position, orderUIContent.transform.rotation,
                    orderUIContent.transform);
                break;
            }
            case 24:
            {
                contentString =
                    "If you see a potion on order that you've never seen before, you must find the lost page.";
                textContent.text = contentString;
                text = Instantiate(textContent, orderUIContent.transform.position, orderUIContent.transform.rotation,
                    orderUIContent.transform);
                break;
            }
            case 25:
            {
                tutorialPowerManager.RestoreMana(2000);
                contentString =
                    "Now, Shoot the lost page with your fire ability.";
                textContent.text = contentString;
                text = Instantiate(textContent, orderUIContent.transform.position, orderUIContent.transform.rotation,
                    orderUIContent.transform);
                break;
            }
            case 26:
            {
                contentString =
                    "Once shot, the lost page will added to your recipe book.";
                textContent.text = contentString;
                text = Instantiate(textContent, orderUIContent.transform.position, orderUIContent.transform.rotation,
                    orderUIContent.transform);
                break;
            }
            case 27:
            {
                contentString =
                    "Remember this bell, it rings only 3 times a day. If you complete all orders, \n " +
                    "you will hear the single bell ring at the start of the day.";
                textContent.text = contentString;
                text = Instantiate(textContent, orderUIContent.transform.position, orderUIContent.transform.rotation,
                    orderUIContent.transform);
                break;
            }
            case 28:
            {
                contentString =
                    "If you hear this bell, you have failed to complete all orders, and your Earthly contract will be voided.";
                textContent.text = contentString;
                text = Instantiate(textContent, orderUIContent.transform.position, orderUIContent.transform.rotation,
                    orderUIContent.transform);
                break;
            }
            
            default:
            {
                nextButton.SetActive(false);
                contentString = "You Earth-Bound contract begins now. Don't fail us.";
                textContent.text = contentString;
                text = Instantiate(textContent, orderUIContent.transform.position, orderUIContent.transform.rotation, orderUIContent.transform);
                playButton.SetActive(true);
                break;
            }
            

        }
        
    }
   
}
