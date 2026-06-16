using System.IO;
using TMPro;
using UnityEngine;
using UnityEngine.Playables;
#if UNITY_EDITOR
using UnityEditor;
#endif
#if UNITY_EDITOR
[ExecuteAlways]
#endif
public class LoggingConsoleControllerScript : MonoBehaviour
{
    public TextMeshProUGUI TMP;
    public string ConsoleLogs;
    public string LastConsoleLogs;
    public TextMeshPro Changelog;
    public bool ShowConsole = false;
    public bool LogMessages = false;
    public int LastSavedFrame = 0;
    public TimeControllerScript TCS;

#if UNITY_EDITOR
private void OnEnable()
    {
        if (Application.isPlaying)
        {
            return;
        }
        Changelog.text = File.ReadAllText(Application.dataPath + "/ChangeLog.txt");
    }
#endif


    public void ShowConsoleFunc(bool tog)
    {
        ShowConsole = tog;
    }
    public void Awake()
    {
        string AppVer = Application.version;
        if (AppVer.Contains("x") || AppVer.Contains("X"))
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
                TCS.DebugOutput = true;
            }
            else { TMP.text = Changelog.text; }
        }
        if (((Time.frameCount - LastSavedFrame) / Application.targetFrameRate)  >= 10) {
            if (ConsoleLogs != LastConsoleLogs)
            {
                LastSavedFrame = Time.frameCount;
                WriteConsole();
                LastConsoleLogs = ConsoleLogs;
            }
        }
        
    }
    private static string GetDLPath()
    {
#if UNITY_ANDROID && !UNITY_EDITOR
        using (var environment = new AndroidJavaClass("android.os.Environment"))
        {
            using (var downloadsDir = environment.CallStatic<AndroidJavaObject>(
                "getExternalStoragePublicDirectory",
                environment.GetStatic<string>("DIRECTORY_DOWNLOADS")
                ))
            {
                return downloadsDir.Call<string>("getAbsolutePath");
            }
        }
#elif UNITY_STANDALONE_WIN
        return Application.persistentDataPath;
#else
        return Application.persistentDataPath;
#endif
    }
    public void WriteConsole()
    {
        if (ConsoleLogs.Length > 0||ConsoleLogs != "")
        {
            string Dir = Path.Combine(GetDLPath(), "HourglassLogs");
            if (!Directory.Exists(Dir))
            {
                Directory.CreateDirectory(Dir);
            }
            File.WriteAllText(Path.Combine(Dir, "DerbossLog.txt"), ConsoleLogs);
        }
    }
    private void OnApplicationQuit()
    {
        WriteConsole();
    }
}
