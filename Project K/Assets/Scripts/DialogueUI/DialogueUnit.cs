using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;



namespace Dialogue
{
    [Serializable]
    public class DialogueUnit
    {

        public string requiredStateKey;
        [TextArea(2, 5)]
        public string[] sentences;
        public DialogueOption[] options;

    }
}