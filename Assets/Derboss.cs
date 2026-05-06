//Dude_of_dudovia vERBOS logging System = Derboss.
using UnityEngine;

public static class Derboss
{
    private static LoggingConsoleControllerScript cachedConsole;
    private static int cachedFrame;
    private static int cachedTimeIndex;
    private static bool initialized;
    private static void Handlelog(string logString, string stackTrace, LogType type)
    {
        string AppVer = Application.version;
        if (AppVer.Contains("x") || AppVer.Contains("X") || AppVer.Contains("rc"))
        {
            Application.SetStackTraceLogType(LogType.Log, StackTraceLogType.Full);
            Application.SetStackTraceLogType(LogType.Warning, StackTraceLogType.Full);
            Application.SetStackTraceLogType(LogType.Error, StackTraceLogType.Full);
        }
        if (cachedConsole == null)
        {
            GameObject[] Consoles = GameObject.FindGameObjectsWithTag("LoggingConsole");
            if (Consoles.Length > 0)
            {
                cachedConsole = Consoles[0].GetComponent<LoggingConsoleControllerScript>();
            }
        }
        if (!cachedConsole.LogMessages)
        {
            return;
        }
        string filteredStackTrace = GetFileLine(stackTrace);
        if (cachedFrame == Time.frameCount)
        {
            cachedTimeIndex++;
            if (cachedConsole != null)
            {
                cachedConsole.ConsoleLogs += "\n[" + (cachedTimeIndex + 1) + "] " + logString + "\n" + filteredStackTrace;
            }
        }
        else
        {
            cachedFrame = Time.frameCount;
            cachedTimeIndex = 0;

            if (cachedConsole != null)
            {
                cachedConsole.ConsoleLogs += "\n[" + System.DateTime.Now.ToString() + "] " + logString + "\n" + filteredStackTrace;
            }
     
        }
    }
    private static string GetFileLine(string stackTrace)
    {
        foreach (var line in stackTrace.Split('\n'))
        {
            int idx = line.IndexOf(".cs");
            if (idx != -1)
            {
                return line.Substring(line.IndexOf("in ") + 3).Trim();
            }
        }
        return "";
    }
    [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.SubsystemRegistration)]
    private static void AutoInit()
    {
        Init();
    }
    public static void Init()
    {
        if ( initialized)
        {
            return;
        }
        Application.logMessageReceived += Handlelog;
        initialized = true;
    }
    /*
    public static void Log(string message)
    {
        if (cachedConsole == null)
        {
            GameObject[] Consoles = GameObject.FindGameObjectsWithTag("LoggingConsole");
            if (Consoles.Length > 0)
            {
                cachedConsole = Consoles[0].GetComponent<LoggingConsoleControllerScript>();
            }
        }

        if (cachedTime == System.DateTime.Now.ToBinary())
        {
            cachedTimeIndex++;
            if (cachedConsole != null)
            {
                cachedConsole.ConsoleLogs += "\n" + (cachedTimeIndex + 1) + message;
            }
            if (Application.isEditor)
            {
                Debug.Log(message);
            }
        }
        else
        {
            cachedTime = System.DateTime.Now.ToBinary();
            cachedTimeIndex = 0;

            if (cachedConsole != null)
            {
                cachedConsole.ConsoleLogs += "\n" + System.DateTime.FromBinary(cachedTime).ToString() + message;
            }
            if (Application.isEditor)
            {
                Debug.Log(message);
            }
        }
    }
    public static void Log(object MSG)
    {
        string message = MSG.ToString() ?? "null";
        if (cachedConsole == null)
        {
            GameObject[] Consoles = GameObject.FindGameObjectsWithTag("LoggingConsole");
            if (Consoles.Length > 0)
            {
                cachedConsole = Consoles[0].GetComponent<LoggingConsoleControllerScript>();
            }
        }

        if (cachedTime == System.DateTime.Now.ToBinary())
        {
            cachedTimeIndex++;
            if (cachedConsole != null)
            {
                cachedConsole.ConsoleLogs += "\n" + (cachedTimeIndex+1) + message;
            }
            if (Application.isEditor)
            {
                Debug.Log(message);
            }
        }
        else
        {
            cachedTime = System.DateTime.Now.ToBinary();
            cachedTimeIndex = 0;

            if (cachedConsole != null)
            {
                cachedConsole.ConsoleLogs += "\n" + System.DateTime.FromBinary(cachedTime).ToString() + message;
            }
            if (Application.isEditor)
            {
                Debug.Log(message);
            }
        }
    }
    */
}
