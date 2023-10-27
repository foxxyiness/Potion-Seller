using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace Helpers
{

    [Serializable]
    public class ScriptableEvent 
    {
        public string eventName;
        public UnityEvent unityEvent;
  
    }
}