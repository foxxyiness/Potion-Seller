using Dialogue;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;



namespace UIElements
{


    public class DialogueUI : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI npcNameText;
        [SerializeField] private TextMeshProUGUI sentenceText;


        private Button[] _buttons;
        private Queue<string> _sentences;
        private DialogueOption[] _dialogueOptions;
        private DialogueOption _defaultDialogueOptions;


        private void Start()
        {
            _buttons = GetComponentsInChildren<Button>();
            _sentences = new Queue<string>();
            gameObject.SetActive(false);

        }

        public void SetNpcName(string npcName)
        {
            npcNameText.text = npcName;
        }

        public void SetSentences(IEnumerable<string> sentences)
        {
            _sentences.Clear();
            foreach (var sentence in sentences)
            {
                _sentences.Enqueue(sentence);
            }
        }
        public void SetDialogueOptions(DialogueOption[] dialogueOptions, DialogueOption defaultOption)
        {

            _dialogueOptions = dialogueOptions;
            _defaultDialogueOptions = defaultOption;

        }

        public void ContinueDialogue()
        {
            gameObject.SetActive(true);
            if (DisplaySentence())
            {
                DisplayContinueDialogueButton();
            }
            else if (_dialogueOptions.Length > 0)
            {
                DisplayDialogueOptions();
            }
            else if (_defaultDialogueOptions != null)
            {
                DisplayDefaultDialogueOption();
            }
            else
            {
                EndDialogue();
            }

        }


        public void EndDialogue()
        {
            gameObject.SetActive(false);
        }

        private bool DisplaySentence()
        {
            if (_sentences.Count <= 0)
            {
                return false;
            }
            sentenceText.text = _sentences.Dequeue();
            return _sentences.Count > 0;
        }

        private void DisplayContinueDialogueButton()
        {
            if (_buttons.Length <= 0)
            {
                return;
            }

            for (var i = 0; i < _buttons.Length; i++)
            {
                if (i == 0)
                {
                    var text = _buttons[i].GetComponentInChildren<TextMeshProUGUI>();
                    text.text = "Continue";
                    _buttons[i].onClick.RemoveAllListeners();
                    _buttons[i].onClick.AddListener(ContinueDialogue);
                    _buttons[i].gameObject.SetActive(true);

                }
                else
                {
                    _buttons[i].gameObject.SetActive(false);
                }
            }


        }


        private void DisplayDefaultDialogueOption()
        {
            if (_buttons.Length <= 0)
            {
                return;
            }
            for (var i = 0; i < _buttons.Length; i++)
            {
                if (i == 0)
                {
                    var text = _buttons[i].GetComponentInChildren<TextMeshProUGUI>();
                    text.text = _defaultDialogueOptions.buttonText;
                    _buttons[i].onClick.RemoveAllListeners();
                    _buttons[i].onClick.AddListener(_defaultDialogueOptions.actionToTrigger.Invoke);
                    _buttons[i].gameObject.SetActive(true);


                }
                else
                {
                    _buttons[i].gameObject.SetActive(false);
                }
            }
        
        }
        private void DisplayDialogueOptions()
        {
            var optionsCount = _dialogueOptions.Length;
            for (int i = 0; i < _buttons.Length; i++)
            {
                if (i < optionsCount)
                {
                    var text = _buttons[i].GetComponentInChildren<TextMeshProUGUI>();
                    text.text = _dialogueOptions[i].buttonText;
                    _buttons[i].onClick.RemoveAllListeners();
                    _buttons[i].onClick.AddListener(_dialogueOptions[i].actionToTrigger.Invoke);
                    _buttons[i].gameObject.SetActive(true);

                }
                else
                {
                    _buttons[i].gameObject.SetActive(false);
                }

            }
        }






     
    }
}