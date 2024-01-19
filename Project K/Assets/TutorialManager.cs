using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using Items;
using Player;
using TMPro;
using UI___Menu;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.XR.Interaction.Toolkit;
using Random = System.Random;

public class TutorialManager : MonoBehaviour
{
    [SerializeField] private PowerManager powerManager;
    [SerializeField] private int currentState;
    private string contentString;
    [SerializeField] private TextMeshProUGUI textContent;
    [SerializeField] private GameObject nextButton;
    [SerializeField] private GameObject orderUIContent;
    TextMeshProUGUI text; 
    void Start()
    {
        currentState = 0;
        UpdateState();
        nextButton.SetActive(false);
    }

    void AddState()
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
        var playerStart = powerManager.gameObject.transform.position;
        
         void Update()
        {
            var player = powerManager.gameObject.transform.position;
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
                contentString =
                    "You've been empowered by your godly powers, lets try your fire ability.\n Lift your right hand, " +
                    "hold the grip button, and tap the trigger.";
                textContent.text = contentString;
                text = Instantiate(textContent, orderUIContent.transform.position, orderUIContent.transform.rotation, orderUIContent.transform);
                StartCoroutine(TimeDelay());
                break;
            }
            case 4:
            {
                contentString = "Great, you'll be able to toast certain potions with that ability.";
                textContent.text = contentString;
                text = Instantiate(textContent, orderUIContent.transform.position, orderUIContent.transform.rotation, orderUIContent.transform);
                StartCoroutine(TimeDelay());
                break;
            }
            case 5:
            {
                contentString =
                    "Lets try your Sun power, harnessing the laws of photosynthesis.\n Lift your left hand, " +
                    "hold the grip button, and tap the trigger.";
                textContent.text = contentString;
                text = Instantiate(textContent, orderUIContent.transform.position, orderUIContent.transform.rotation, orderUIContent.transform);
                StartCoroutine(TimeDelay());
                break;
            }
            case 6:
            {

                contentString = "Great, you'll be able to use this ability to promote growth on your crops.";
                textContent.text = contentString;
                text = Instantiate(textContent, orderUIContent.transform.position, orderUIContent.transform.rotation, orderUIContent.transform);
                StartCoroutine(TimeDelay());
                break;
            }
            case 7:
            {
                contentString = "You can make a potion by putting the correct combination into the cauldron. \n " +
                                "For each potion, you must include 1 base orb, 1 flavor orb, and 1 strength orb.";
                //contentString = "Lets try making a potion.";
                textContent.text = contentString;
                text = Instantiate(textContent, orderUIContent.transform.position, orderUIContent.transform.rotation, orderUIContent.transform);
                StartCoroutine(TimeDelay());
                break;
            }

            default:
            {
                contentString = "Great, ready to begin. Press Play to load in.";
                textContent.text = contentString;
                text = Instantiate(textContent, orderUIContent.transform.position, orderUIContent.transform.rotation, orderUIContent.transform);
                nextButton.SetActive(true);
                break;
            }
            // case 9:
            // {
            //     contentString =
            //         "Lets try it. Look behind you and put the ingredients into the cauldron. Use your trigger button to grab an item.";
            //     textContent.text = contentString;
            //     text = Instantiate(textContent, orderUIContent.transform.position, orderUIContent.transform.rotation, orderUIContent.transform);
            //     break;
            // }

        }
        
    }
   
}
