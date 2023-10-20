using Helpers;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UIElements;
using Interactor;




namespace Dialogue
{

    public class DialogueHandler : MonoBehaviour
    {
        [SerializeField] private DialogueTreeObject dialogueTree;
        [SerializeField] DialogueUI dialogueUI;
        [SerializeField] UnityEvent onDialogueEnd;
        [SerializeField] ScriptableEvent[] scriptableEvents;


        private void Start()
        {

            dialogueTree.ResetCallbacks();
            foreach (var scriptableEvent in scriptableEvents)
            {
                dialogueTree.RegisterScriptableCallback(scriptableEvent.eventName,()  => scriptableEvent.unityEvent.Invoke());
            }
            dialogueTree.SetUpDialogueUnitsDict();
            dialogueTree.continueCallback += dialogueUI.ContinueDialogue;
            dialogueTree.continueCallback += ContinueDialogue;
            dialogueTree.endDialogueCallback += dialogueUI.EndDialogue;
            dialogueTree.endDialogueCallback += EndDialogue;

        }


        public void OnInteract(Interactor interactor)
        {
            var dialogueState = interactor.GetComponent<DialogueState>();
            if (dialogueState == null)
                return;

            dialogueTree.SetUpDialogueState(dialogueState);
            ContinueDialogue();

        }

        public void ContinueDialogue()
        {
            HandleDialogue(dialogueTree.GetNextDialogueUnit());
        }

        public void EndDialogue()
        {
            onDialogueEnd.Invoke();
        }

        private void HandleDialogue(DialogueUnit dialogueUnit)
        {
            dialogueUI.SetNpcName(dialogueTree.npcName);
            dialogueUI.SetSentences(dialogueUnit.sentences);
            dialogueUI.SetDialogueOptions(dialogueUnit.options, dialogueTree.defaultOption);
            dialogueUI.ContinueDialogue();
        }

        // Update is called once per frame
        void Update()
        {

        }
    }
}
