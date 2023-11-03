using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.XR;
using TMPro;


public class DialogueManager : MonoBehaviour
{

    public NPC npc;

    bool isTalking = false;

    float distance;
    float curResponseTracker = 0;

    public GameObject player;
    public GameObject dialogueUI;

    public TextMeshProUGUI npcName;
    public TextMeshProUGUI npcDialogueBox;
    public TextMeshProUGUI playerResponse;


    // Start is called before the first frame update
    void Start()
    {
        dialogueUI.SetActive(false);
    }

    void OnMouseOver()
    {
        distance = Vector3.Distance(player.transform.position, this.transform.position);
        if(distance <= 2.5f)
        {
            //trigger dialogue
            if (Input.GetKeyDown(KeyCode.E) && isTalking==false)
            {
                StartConversation();
            }
            else if(Input.GetKeyDown(KeyCode.E) && isTalking== true) 
            {
                EndDialogue();
            }
        }

    }

    void StartConversation()
    {
        isTalking = true;
        curResponseTracker = 0;
        dialogueUI.SetActive(true);
        npcName.text = npc.name;
        npcDialogueBox.text = npc.dialogue[0];
    }

    void EndDialogue()
    {
        isTalking = false;
        dialogueUI.SetActive(false);
    }

}
