
using TMPro;
using UdonSharp;
using UnityEngine;
using VRC.SDKBase;
using VRC.Udon;

[DefaultExecutionOrder(-2), UdonBehaviourSyncMode(BehaviourSyncMode.None)]
public class Loger : UdonSharpBehaviour
{
    [SerializeField]
    TextMeshPro textLogError, textLogEvent;

    string tempErrors, tempLogs;

    private void Start()
    {
        tempLogs = "";
        tempErrors = "";
        LogEvent("log start");
    }

    public void LogError(string errorMessage)
    {
        if (tempErrors.Length > 1500)
        {
            tempErrors = "";
        }
        tempErrors += "\n";
        tempErrors += errorMessage;
        textLogError.text += tempErrors;
    }

    public void LogEvent(string eventMessage)
    {
        if (tempLogs.Length > 1500)
        {
            tempLogs = "";
        }
        tempLogs += "\n";
        tempLogs += eventMessage;
        textLogEvent.text = tempLogs;
    }
}
