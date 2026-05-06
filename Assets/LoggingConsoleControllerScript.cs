using TMPro;
using UnityEngine;
using UnityEngine.Playables;

public class LoggingConsoleControllerScript : MonoBehaviour
{
    public TextMeshProUGUI TMP;
    public string ConsoleLogs;
    public TextMeshPro Changelog;
    public bool ShowConsole = false;
    public bool LogMessages = false;
    public void ShowConsoleFunc(bool tog)
    {
        ShowConsole = tog;
    }
    public void Awake()
    {
        string AppVer = Application.version;
        if (AppVer.Contains("x") || AppVer.Contains("X") || AppVer.Contains("rc"))
        {
            LogMessages = true;
            Application.SetStackTraceLogType(LogType.Log, StackTraceLogType.Full);
            Application.SetStackTraceLogType(LogType.Warning, StackTraceLogType.Full);
            Application.SetStackTraceLogType(LogType.Error, StackTraceLogType.Full);
        }
    }
    public void Update()
    {
        if (ShowConsole) { LogMessages = true; }
        if (LogMessages)
        {
            Application.SetStackTraceLogType(LogType.Log, StackTraceLogType.Full);
            Application.SetStackTraceLogType(LogType.Warning, StackTraceLogType.Full);
            Application.SetStackTraceLogType(LogType.Error, StackTraceLogType.Full);
        }
        if (TMP != null)
        {
            if (ShowConsole)
            {
                TMP.text = ConsoleLogs;
            }
            else { TMP.text = Changelog.text; }
        }
    }
}
