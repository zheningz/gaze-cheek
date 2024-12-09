using System.Collections;
using UnityEngine;
using UnityEngine.UI;

namespace Gazecheek.Scripts
{
    public class OnScreenLog : MonoBehaviour
    {
        uint qsize = 2;  // number of messages to keep
        public Text logText;
        Queue myLogQueue = new Queue();

        void Start() {
            Debug.Log("Started up logging.");
        }

        void OnEnable() {
            Application.logMessageReceived += HandleLog;
        }

        void OnDisable() {
            Application.logMessageReceived -= HandleLog;
        }

        void HandleLog(string logString, string stackTrace, LogType type) {
            myLogQueue.Enqueue("[" + type + "] : " + logString);
            if (type == LogType.Exception)
                myLogQueue.Enqueue(stackTrace);
            while (myLogQueue.Count > qsize)
                myLogQueue.Dequeue();
        
            UpdateLogText();
        }
    
        private void UpdateLogText()
        {
            if (logText == null)
                return;

            logText.text = string.Join("\n", myLogQueue.ToArray());
        }
    }
}
