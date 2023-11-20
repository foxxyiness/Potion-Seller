using System.Collections;
using UnityEngine;

//Orgininally Found on Stack Overflow: https://stackoverflow.com/questions/67704820/how-do-i-print-unitys-debug-log-to-the-screen-gui
namespace UI___Menu
{
    public class LogUI : MonoBehaviour
    {
        uint qsize = 15;  // number of messages to keep
        Queue myLogQueue = new Queue();

        void Start()
        {
            Debug.Log("Started up logging.");
        }

        void OnEnable()
        {
            Application.logMessageReceived += HandleLog;
        }

        void OnDisable()
        {
            Application.logMessageReceived -= HandleLog;
        }

        void HandleLog(string logString, string stackTrace, LogType type)
        {
            myLogQueue.Enqueue("[" + type + "] : " + logString);
            if (type == LogType.Exception)
                myLogQueue.Enqueue(stackTrace);
            while (myLogQueue.Count > qsize)
                myLogQueue.Dequeue();
        }

        void OnGUI()
        {
            GUILayout.BeginArea(new Rect(Screen.width - 200, 0, 400, Screen.height));
            GUILayout.Label("\n" + string.Join("\n", myLogQueue.ToArray()));
            GUILayout.EndArea();
        }
    }
}
