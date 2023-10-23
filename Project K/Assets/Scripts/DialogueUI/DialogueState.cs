using System.Collections;
using System.Collections.Generic;
using UnityEngine;





namespace Dialogue
{
    public class DialogueState : MonoBehaviour
    {

        public Dictionary<string, string> stateDict;

        

        private void Start()
        {
            stateDict = new Dictionary<string, string>();
        }


    }
}
