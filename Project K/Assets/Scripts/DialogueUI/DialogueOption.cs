using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;


namespace Dialogue
{
    [Serializable]
    public class DialogueOption
    {

        public string buttonText;
        public UnityEvent actionToTrigger;

    }
}