using System;
using System.Collections;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.IO.Enumeration;
using TMPro;

using UnityEditor;
//using Unity.Notifications.iOS;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Rendering.Universal;
using System.Globalization;






#if UNITY_ANDROID
using Unity.Notifications.Android;
using UnityEngine.Android;
# endif
public class TimeControllerScript : MonoBehaviour
{
    public bool DebugOutput;
    //Minutes, not hours
    public float DefaultTime = 180;
    public float TotalTime;
    public float RemainingTime;
    public float CurrentTime;
    public float UsedTime;
    public float UsedTimeLastTick;
    public float TimeScale = 60f;
    public float resettime = 0.4167f;
    public float timerresettime = 0.4167f;
    public bool pendingreset = false;
    public bool pendingtimerreset = false;
    public bool RunTimer = false;
    public bool WasRunTimer = false;
    public float RunningTime = 0f;
    public GameObject ContentView;
    public GameObject TimetoAdd;
    public float TimeWhenTimerStarted;
    public long CurrentTick => DateTime.Now.Ticks;
    public long viewCurrentTick = 0;
    public long TicksWhenTimerStarted = DateTime.Now.Ticks;
    public bool primesave;
    public int Profile;
    public int Profiles;
    public GameObject ProfilesView;
    public GameObject ProfileToAdd;
    public GameObject[] ProfileButtonCollection = new GameObject[1];
    public string ProfileName = "Profile";
    public TMP_InputField ProfileNameSetter;

    public int AddProf;
    public int AddProfDelay;

    public GameObject ResetApp;
    public bool pendingappreset = false;
    public bool pendingtimerappreset = false;
    public float appresettime = 0.4167f;
    public bool CountUp = false;
    public bool ResetReset;
    public bool MSTimer = true;
    public bool ResetValues = false;
    public int ResetValuesTimer = 1;
    public bool BigReset = false;
    public bool NotificationPerms = false;
    public bool IsOnAndroid = false;


    public void MSTimerFunc(bool tog)
    {
        MSTimer = tog;
    }
    public bool MSAddeds = false;
    public void MSAddedsFunc(bool tog)
    {
        MSAddeds = tog;
    }
    public bool MSLeftDisplay = false;
    public void MSLeftDisplayFunc(bool tog)
    {
        MSLeftDisplay = tog;
    }
    public bool MSUsedDisplay = false;
    public void MSUsedDisplayFunc(bool tog)
    {
        MSUsedDisplay = tog;
    }

    public bool KeepTimeInAddBox = false;
    public void KeepTimeInAddBoxFunc(bool tog)
    {
        KeepTimeInAddBox = tog;
    }
    public int DeletedProfLifeTime = 120;
    public void DeletedProfLifeTimeFunc(int inte)
    {
        DeletedProfLifeTime = inte;
    }

    public float BackgroundHue = 0.3333333f;
    public void BackgroundHueFunc(float flot)
    {
        BackgroundHue = flot;
    }
    public float BackgroundSat = 1;
    public void BackgroundSatFunc(float flot)
    {
        BackgroundSat = flot;
    }
    public float BackgroundVal = 1;
    public void BackgroundValFunc(float flot)
    {
        BackgroundVal = flot;
    }

    public float AddedTimesHue;
    public void AddedTimesHueFunc(float flot)
    {
        AddedTimesHue = flot;
    }
    public float AddedTimesSat;
    public void AddedTimesSatFunc(float flot)
    {
        AddedTimesSat = flot;
    }
    public float AddedTimesVal;
    public void AddedTimesValFunc(float flot)
    {
        AddedTimesVal = flot;
    }

    public float AddedTimesFadeOutHue;
    public void AddedTimesFadeOutHueFunc(float flot)
    {
        AddedTimesFadeOutHue = flot;
    }
    public float AddedTimesFadeOutSat;
    public void AddedTimesFadeOutSatFunc(float flot)
    {
        AddedTimesFadeOutSat = flot;
    }
    public float AddedTimesFadeOutVal;
    public void AddedTimesFadeOutValFunc(float flot)
    {
        AddedTimesFadeOutVal = flot;
    }
    public bool AddedTimesRainbow = false;
    public void AddedTimesRainbowFunc(bool tog)
    {
        AddedTimesRainbow = tog;
    }

    public int Reset = -1;

    void Awake()
    {

        string AppVer = Application.version;
        if (AppVer.Contains("x") || AppVer.Contains("X") || AppVer.Contains("rc"))
        {
            Application.SetStackTraceLogType(LogType.Log, StackTraceLogType.Full);
            Application.SetStackTraceLogType(LogType.Warning, StackTraceLogType.Full);
            Application.SetStackTraceLogType(LogType.Error, StackTraceLogType.Full);
#if !(PLATFORM_STANDALONE_WIN || UNITY_EDITOR_WIN)
            Derboss.Init();
#endif
        }
        Profile = 0;
        try { Profile = int.Parse(RDfile(0, 0, true)); }
        catch { Profile = 0; }
        if (Profile == -792) { Profile = 0; }
        if (!CheckFile(Profile))
        {
            Load(Profile, true);
            return;
        }
        Load(Profile);

    }
    private void Start()
    {

#if UNITY_ANDROID && !UNITY_EDITOR
        StartCoroutine(RequestPerms());
        NotificationSetup();
#endif
    }
    public void NotificationsEnabledfFunc(bool tog)
    {
        NotificationsEnabled = tog;
    }
    public void NotificationWarningsEnabledfFunc(bool tog)
    {
        NotificationsEnabled = tog;
    }
    public void NotificationWarningHalfTimeEnabledfFunc(bool tog)
    {
        NotificationWarningHalfTimeEnabled = tog;
    }
    public void NotificationCustomTimeWarningFunc(bool tog)
    {
        NotificationCustomTimeWarning = tog;
    }
    public bool NotificationsEnabledOld = true;
    public bool NotificationsEnabled = true;
    public bool NotificationLowWarningsEnabled = true;
    public bool NotificationLowWarningsEnabledOld = true;
    public bool NotificationWarningHalfTimeEnabled = true;
    public bool NotificationWarningHalfTimeEnabledOld = true;
    public bool NotificationCustomTimeWarning = true;
    public bool NotificationCustomTimeWarningOld = true;
    IEnumerator RequestPerms()
    {
#if UNITY_ANDROID && !UNITY_EDITOR
        string perm = "android.permission.POST_NOTIFICATIONS";
        if (!Permission.HasUserAuthorizedPermission(perm))
        {
            Permission.RequestUserPermission(perm);
        }
        float timeout = 5f;
        while (!Permission.HasUserAuthorizedPermission(perm) && timeout >0f)
        {
            timeout -= Time.deltaTime;
            yield return null;
        }
        NotificationPerms = Permission.HasUserAuthorizedPermission(perm);
#else
        yield break;
#endif
    }
    private string NotificationChannel = "V2";
    public void NotificationSetup()
    {
#if UNITY_ANDROID && !UNITY_EDITOR
            var channel = new AndroidNotificationChannel()
            {
                Id = "Hourglass_Channel" + NotificationChannel,
                Name = "Timer Channel",
                Importance = Importance.High,
                Description = "Hourglass Notifications",
                EnableVibration = true,
                CanShowBadge = true,
                LockScreenVisibility = LockScreenVisibility.Public,
                VibrationPattern = new long[] { 0, 500, 200, 500 },// wait, vibrate, pause, vibrate
            };
            AndroidNotificationCenter.RegisterNotificationChannel(channel);
        IsOnAndroid = true;
#endif
    }
    int[] ChannelIDs = new int[0];
    int SavedNotifIDIndex(SaveObjectList data, int channel)
    {
        if (data?.Objects == null || data.Objects.Length <= channel)
        { return 0;
        }
        if (data.Objects[channel] == null) { return 0; }
        if (data.Objects[channel].datas == null) { return 0; }
        if (data.Objects[channel].datas.Length == 0) { return 0; }
        if (data.Objects[channel].datas[0] == "-792") { return 0; }
        return data.Objects[channel].datas.Length;
    }
    int[] SavedNotifIDs(SaveObjectList data, int channel)
    {

        if (data?.Objects == null || data.Objects.Length <= channel)
        {
            int[] IDIndex = new int[1];
            IDIndex[0] = 0;
            return IDIndex;
        }
        int length = data.Objects[channel].datas.Length;
        int[] IDIndexS = new int[length];
        for (int i = 0; i < length; i++)
        {
            IDIndexS[i] = data.Objects[channel].datas[i] != null ? int.TryParse(data.Objects[channel].datas[i], out int result) ? result : 0 : 0;
        }
        return IDIndexS;
    }
    public float LowWarningTime = 5f;
    public void LowWarningTimeFunc(float flot)
    {
        LowWarningTime = flot;
    }
    public float CustomWarningTime = 5f;
    public void CustomWarningTimeFunc(float flot)
    {
        CustomWarningTime = flot;
    }
    public void Notify(float FireMinutes, string NotifTitle, int channel, string Notiftext)
    {
        Notify(FireMinutes, NotifTitle, Notiftext, channel, false);
    }
    public void Notify(float FireMinutes, string NotifTitle, string Notiftext, int channel, bool UseStopWatch)
    {
        Notify(FireMinutes, NotifTitle, Notiftext, channel, UseStopWatch, false);
    }
    public void Notify(float FireMinutes, string NotifTitle, string Notiftext, int channel, bool UseStopWatch, bool ForTimer)
    {
#if DEBUG
        //Notiftext += "\nProfile: " + (ProfileName).ToString() + "\nChannel:" + channel.ToString();
        if (FireMinutes > 1.5 && DebugOutput)
        {
            Notify(0, NotifTitle, Notiftext + "\n***\nFireMinutes: " + FireMinutes +"\nFireTime: "+DateTime.Now.AddMinutes(FireMinutes) +"\n***", channel, UseStopWatch, ForTimer);
        }
        Notiftext += "\nChannel:" + channel.ToString();
#endif
        if (Profiles > 1) { Notiftext += "\nProfile: " + (ProfileName).ToString(); }

        if (channel < 6 || channel > 11) { channel = 8; }
#if UNITY_ANDROID
        AndroidJavaClass unityPlayerAndr = new AndroidJavaClass("com.unity3d.player.UnityPlayer");
        AndroidJavaObject activityAndr = unityPlayerAndr.GetStatic<AndroidJavaObject>("currentActivity");
        AndroidJavaObject contextAndr = activityAndr.Call<AndroidJavaObject>("getApplicationContext");
        AndroidJavaObject alarmManager = contextAndr.Call<AndroidJavaObject>("getSystemService", "alarm");
        AndroidNotification notification = new AndroidNotification();
        notification.Title = NotifTitle;
        notification.Text = Notiftext;
        notification.FireTime = DateTime.Now.AddMinutes(FireMinutes);
        notification.ShowTimestamp = true;
        notification.ShouldAutoCancel = ForTimer;
        notification.UsesStopwatch = UseStopWatch;
        notification.Color = Color.HSVToRGB(BackgroundHue, BackgroundSat, BackgroundVal);
        
        notification.Style = NotificationStyle.BigPictureStyle;
        int id = AndroidNotificationCenter.SendNotification(notification, "Hourglass_Channel" + NotificationChannel);
        int DatInd = 0;
        var data = JsonUtility.FromJson<SaveObjectList>(File.ReadAllText(Path.Combine(Application.persistentDataPath, "Savedata", "Profile" + Profile.ToString() + ".json")));
        DatInd = SavedNotifIDIndex(data,channel);
        //DatInd = int.Parse(RDfile(6, 0, true));
        //if (DatInd == -792) DatInd = 0;
        /*if (ForTimer)
        {
            MKfile(7, DatInd, id.ToString(), false);
        }
        else */
        if (channel != 6) { MKfile(6, DatInd, id.ToString(), false); }
        MKfile(channel, DatInd, id.ToString(), false);
#elif PLATFORM_STANDALONE_WIN || UNITY_EDITOR_WIN
        //Notif Id = Profile + Channel + ChannelIndex//
        Notiftext = NotifTitle + "\n" + Notiftext;
        int DatInd = 0;
        var data = JsonUtility.FromJson<SaveObjectList>(File.ReadAllText(Path.Combine(Application.persistentDataPath, "Savedata", "Profile" + Profile.ToString() + ".json")));
        DatInd = SavedNotifIDIndex(data, channel);
        string NotifId = Profile + channel + DatInd.ToString();
        if (FireMinutes == 0)
        {
            Process.Start(new ProcessStartInfo()//
            {
                FileName = "powershell",
                Arguments = "-Command \"[Windows.UI.Notifications.ToastNotificationManager, Windows.UI.Notifications, ContentType = WindowsRuntime] > $null;" +
                "$template = [Windows.UI.Notifications.ToastTemplateType]::ToastText02;" +
                "$xml = [Windows.UI.Notifications.ToastNotificationManager]::GetTemplateContent($template);" +
                "$texts = $xml.GetElementsByTagName('text');" +
                "$texts[0].AppendChild($xml.CreateTextNode('" + Notiftext + "')) > $null;" +
                "$texts[1].AppendChild($xml.CreateTextNode('Notification Test')) > $null;" +
                "$toast = [Windows.UI.Notifications.ToastNotification]::new($xml);" +
                "$toast.Id = '" + NotifId + "';" +
                "$notifier = [Windows.UI.Notifications.ToastNotificationManager]::CreateToastNotifier('HourGlass');" +
                "$notifier.Show($toast);\""
                ,
                CreateNoWindow = true,
                UseShellExecute = false,

            });
        }
        else
        {
            float FireTime = (FireMinutes + .2f);
            Process.Start(new ProcessStartInfo()
            {
                FileName = "powershell",
                Arguments = "-Command \"[Windows.UI.Notifications.ToastNotificationManager, Windows.UI.Notifications, ContentType = WindowsRuntime] > $null;" +
                "$template = [Windows.UI.Notifications.ToastTemplateType]::ToastText02;" +
                "$xml = [Windows.UI.Notifications.ToastNotificationManager]::GetTemplateContent($template);" +
                "$texts = $xml.GetElementsByTagName('text');" +
                "$texts[0].AppendChild($xml.CreateTextNode('" + Notiftext + "')) > $null;" +
                "$texts[1].AppendChild($xml.CreateTextNode('HourGlass')) > $null;" +
                "$date = [DateTimeOffset]::Now.AddMinutes(" + (FireTime).ToString(CultureInfo.InvariantCulture) + ");" +
                "$xml.DocumentElement.SetAttribute('launch', 'openapp');" +
                "$scheduledToast = [Windows.UI.Notifications.ScheduledToastNotification]::new($xml,$date);" +
                "$scheduledToast.Id = '" + NotifId + "';" +
                "$notifier = [Windows.UI.Notifications.ToastNotificationManager]::CreateToastNotifier('HourGlass');" +
                "$notifier.AddToSchedule($scheduledToast);\""
                ,
                CreateNoWindow = true,
                UseShellExecute = false,
            });
        }

#else

#endif
    }
    /*public int CancelNotifications(bool ReturnVals)
    {
        if (ReturnVals) { } 
        else { CancelNotifications();  }
        return 0;
    }*/
    public void CancelMostNotifications()
    {
      
         CancelNotifications(Profile, 8);
         CancelNotifications(Profile, 9);
         CancelNotifications(Profile, 10);
         CancelNotifications(Profile, 11);
    }
    public void CancelNotifications(int channel)
    {
        CancelNotifications(Profile, channel, false);
    }
    public void CancelNotifications(int prof, int channel)
    {
        CancelNotifications(prof, channel, false);
    }
    public void CancelNotifications(int Prof, int channel ,bool CancelAll)
    {
        if (channel < 6 || channel > 11) { channel = 6; }
        //UnityEngine.Debug.Log("Notifs Canceled" + channel);
        //if (channel == 7) { CancelTimerNotif(Prof); return; }
#if UNITY_ANDROID && !UNITY_EDITOR
        if (CancelAll)
        {
            AndroidNotificationCenter.CancelAllNotifications();
            return;
        }
        if (File.Exists(File.ReadAllText(Path.Combine(Application.persistentDataPath, "Savedata", "Profile" + Profile.ToString() + ".json"))))
            {
            var data = JsonUtility.FromJson<SaveObjectList>(File.ReadAllText(Path.Combine(Application.persistentDataPath, "Savedata", "Profile" + Profile.ToString() + ".json")));
            int[] NotifIndex = SavedNotifIDs(data, channel);
            foreach (int ID in NotifIndex)
            {
                AndroidNotificationCenter.CancelNotification(ID);
            }
        }
        
        

        
       
        MKfile(channel, 0, "", 0, false);
#elif PLATFORM_STANDALONE_WIN || UNITY_EDITOR
        //                                    "$notifier.GetScheduledToastNotifications() | Select Id, DeliveryTime;" +
        CancelAll = true;
        if (CancelAll)
        {
            Process.Start(new ProcessStartInfo()
            {
                FileName = "powershell",
                Arguments =
                 "-Command \"[Windows.UI.Notifications.ToastNotificationManager, Windows.UI.Notifications, ContentType = WindowsRuntime] > $null;" +
                    "$notifier = [Windows.UI.Notifications.ToastNotificationManager]::CreateToastNotifier('HourGlass');" +
                    "$scheduled = $notifier.GetScheduledToastNotifications();" +
                    "foreach ($toast in $scheduled) {" +
                    "$notifier.RemoveFromSchedule($toast);" +
                    "}\""
                    ,
                CreateNoWindow = true,
                UseShellExecute = false,

            });
            return;
        }
        int DatInd = 0;
        string NotifId = "sad";
        if (File.Exists(File.ReadAllText(Path.Combine(Application.persistentDataPath, "Savedata", "Profile" + Profile.ToString() + ".json"))))
        {
            var data = JsonUtility.FromJson<SaveObjectList>(File.ReadAllText(Path.Combine(Application.persistentDataPath, "Savedata", "Profile" + Profile.ToString() + ".json")));
            DatInd = SavedNotifIDIndex(data, channel);
            NotifId = Prof + channel + DatInd.ToString();
        }
        Process.Start(new ProcessStartInfo()
        {
            FileName = "powershell",
            Arguments = 
                "-Command \"[Windows.UI.Notifications.ToastNotificationManager, Windows.UI.Notifications, ContentType = WindowsRuntime] > $null;" +
                "$notifier = [Windows.UI.Notifications.ToastNotificationManager]::CreateToastNotifier('HourGlass');" +
                "$scheduled = $notifier.GetScheduledToastNotifications();" +
                "foreach ($toast in $scheduled) {" +
                "if ($toast.Id -eq '" + NotifId +  "') {" +
                "$notifier.RemoveFromSchedule($toast);" +
                "}" +
                "}\""
                ,
            CreateNoWindow = false,
            UseShellExecute = false,

        });
#else
#endif
    }
    [System.Serializable]
    public class SaveObjectList
    {
        public SaveObject[] Objects;
    }
    [System.Serializable]
    public class SaveObject
    {
        public string[] datas;
    }
    /*One Master File to contain profiles(Profiles.txt for example, just needs to store the int of how many there are)
     *Each Profile will be is own file
     *Follow Options: Sections -> Value
        *Therefore We See:
        *2:Milli
        *3:Background
        *4:LoggedTimes
        *5:LoggedTimesTime
        *6:[All]NotificationIds
        *7:TimerNotifID;
        *8:OutNotificationIds;
        *9:WarningIDs;
        *10:HalfTimeIDs;
        *11:CustomWarningIDs;
        *12:Notification Settings
        * -Execpt the Big Toggle.
        * 0:Enable Low Time Warning
        * 1:Low Time Warning Time
        * 2:Enable Notification Warning Time 
        * 3:Custom Warning Time
        * 4:Enable Notification Warning Half Time (1)
     *Settings[Default]
        *9:EnableNotification [NotificationSettings Starts]
        *10 - 13 are old values, no longer necessary
        *10:EnableNotificationWarning
        *11:EnableNotificationWarningAtHalfTime
        *12:LowWarningTime 
        *13:CustomWarningTime [NotificationSettings Ends]
     *added times will be in Object[0]
     *Things saved with the current save() func will be in Object[1]
     *Settings[0]:CurrentProfile[0-1] Countup[2] & Misc[3-5] & Animation[6-8] & all Other SuperProfile Settings[8+]
     */

    public void MKfile(int Objindex, int Dataindex, string Data, bool Default)
    {
        MKfile(Objindex, Dataindex, Data, -1, Default);
    }

    public void MKfile(int Objindex, int Dataindex, string Data, int MaxData, bool Default)
    {
        MKfile(Objindex, Dataindex, Data, MaxData, Profile, Default);
    }
    public void MKfile(int Objindex, int Dataindex, string Data, int MaxData, int Prof)
    {
        MKfile(Objindex, Dataindex, Data, MaxData, Prof, false);
    }
    public void MKfile(int Objindex, int Dataindex, string Data, int MaxData, int Prof, bool Default)
    {
        if (Objindex < 0 || Dataindex < 0)
        {
            UnityEngine.Debug.Log("Small Obj or Data" + Objindex + ":" + Dataindex);
            return;
        }
        if (!Directory.Exists(Path.Combine(Application.persistentDataPath, "Savedata"))) { Directory.CreateDirectory(Path.Combine(Application.persistentDataPath, "Savedata")); }
        SaveObjectList sol = new SaveObjectList();
        string TotalFilePath = Path.Combine(Application.persistentDataPath, "Savedata", "Profile" + Prof.ToString() + ".json");
        if (Default) {  TotalFilePath = Path.Combine(Application.persistentDataPath, "Savedata", "Settings.json");}
        if (File.Exists(TotalFilePath))
            {
            string FileContents = File.ReadAllText(TotalFilePath);
            sol = JsonUtility.FromJson<SaveObjectList>(FileContents);

            if (sol.Objects == null)
            {
                sol.Objects = new SaveObject[Objindex + 1];
            }
            if (Objindex >= sol.Objects.Length)
            {
                Array.Resize(ref sol.Objects, Objindex + 1);
            }
            if (sol.Objects[Objindex] == null)
            {
                sol.Objects[Objindex] = new SaveObject();
            }

            if (sol.Objects[Objindex].datas == null)
                {
                sol.Objects[Objindex].datas = new string[Mathf.Max(Dataindex+1,1)];
            }
            if (MaxData >= 0)
            {
                if (Dataindex <= MaxData)
                {
                    if (sol.Objects[Objindex].datas.Length < Dataindex + 1)
                    {
                        Array.Resize(ref sol.Objects[Objindex].datas, Dataindex + 1);
                    }
                    sol.Objects[Objindex].datas[Dataindex] = Data;
                }
                if (sol.Objects[Objindex].datas.Length != MaxData)
                {
                    Array.Resize(ref sol.Objects[Objindex].datas, MaxData);
                }
            }
            else
            {
                if (sol.Objects[Objindex].datas.Length <= Dataindex)
                        {
                    Array.Resize(ref sol.Objects[Objindex].datas, Dataindex + 1);
                }
                sol.Objects[Objindex].datas[Dataindex] = Data;
            }
        }
        else
        {
            SaveObject saveobj = new SaveObject();
            saveobj.datas = new string[Dataindex + 1];
            saveobj.datas[Dataindex] = Data;
            sol.Objects = new SaveObject[Objindex + 1];
            sol.Objects[Objindex] = saveobj;
        }
        File.WriteAllText(TotalFilePath, JsonUtility.ToJson(sol));
    }

    public string RDfile(int Objindex, int Dataindex)
    {
        return RDfile(Objindex, Dataindex, Profile);
    }
    public string RDfile(int Objindex, int Dataindex, bool Default)
    {
        return RDfile(Objindex, Dataindex, Profile, Default);
    }
    public string RDfile(int Objindex, int Dataindex, int Prof)
    {
        return RDfile(Objindex, Dataindex, Prof, false);
    }
    public string RDfile(int Objindex, int Dataindex, int Prof, bool Default)
    {
        string read = "-792";
        string FilePath = (@"\Savedata\Profile" + Prof.ToString() + ".json");
        string TotalFilePath = Path.Combine(Application.persistentDataPath, "Savedata", "Profile" + Prof.ToString() + ".json");
        if (Default) { 
            FilePath = (@"\Savedata\Settings.json");
            TotalFilePath = Path.Combine(Application.persistentDataPath, "Savedata", "Settings.json");
        }

        //if (!File.Exists(Application.persistentDataPath + FilePath)) {return read; }
        if (!File.Exists(TotalFilePath)) {return read; }
        try
        {
            //read = JsonUtility.FromJson<SaveObjectList>(File.ReadAllText(Application.persistentDataPath + FilePath)).Objects[Objindex].datas[Dataindex];
            //if (Default) { read = JsonUtility.FromJson<SaveObjectList>(File.ReadAllText(Application.persistentDataPath + FilePath)).Objects[Objindex].datas[Dataindex]; }
            read = JsonUtility.FromJson<SaveObjectList>(File.ReadAllText(TotalFilePath)).Objects[Objindex].datas[Dataindex];
        }
        catch (Exception e)
        {
            if (DebugOutput && Default)
            {
                UnityEngine.Debug.Log(e);
                UnityEngine.Debug.Log(Objindex);
                UnityEngine.Debug.Log(Dataindex);
                // UnityEngine.Debug.Log(JsonUtility.FromJson<SaveObjectList>(File.ReadAllText(Application.persistentDataPath + FilePath)).Objects[Objindex].datas.Length);
            }
        }
        return read;
    }
    public bool CheckFile()
    {
        return CheckFile(Profile);
    }
    public bool CheckFile(int Prof)
    {
        string TotalFilePath = Path.Combine(Application.persistentDataPath, "Savedata", "Profile" + Prof.ToString() + ".json");
        //if (File.Exists(Application.persistentDataPath + @"\Savedata\Profile" + Prof.ToString() + ".json")) { return true; }
        if (File.Exists(TotalFilePath)) { return true; }
        return false;
    }
    private void FixedUpdate()
    {
        if (!RunTimer) { timerresettime = 0; }
        if (pendingreset)
        {
            resettime -= 1;
            if (resettime <= 0) { pendingreset = false; }
        }
        if (pendingtimerreset)
        {
            timerresettime -= 1;
            if (timerresettime <= 0) { pendingtimerreset = false; }
        }
        if (pendingappreset)
        {
            appresettime -= 1;
            if (appresettime <= 0) { pendingappreset = false; }
        }

        if (ResetValuesTimer == 0)
        {
            ResetValues = false;
            BigReset = false;
        }
        AddProfDelay -= 1;
        if (AddProfDelay == 0)
        {
            AddProfPtwo(AddProf);
            AddProf = -1;
            AddProfDelay = -1;

        }
        ResetValuesTimer -= 1;

    }
    public void NotificationSender(float WorkingRemTime)
    {
        if (WorkingRemTime > 0.2f)
        {
            Notify(WorkingRemTime - 0.2f, "Time has run out!", "The timer has exceeded the total time.", 8, false);
            if (NotificationLowWarningsEnabled && WorkingRemTime > (LowWarningTime) && WorkingRemTime > 0.2f)
            {
                //Why do I need to do this if persision isn't within 13 seconds.
                if (LowWarningTime > 1) // Working Remaining Time Factored In
                {
                    float workingWarnTime = WorkingRemTime - LowWarningTime;
                    if (workingWarnTime > 0)
                    {
                        if (LowWarningTime > 1)
                        {
                            if (workingWarnTime > 1)
                            {
                                Notify(workingWarnTime, Mathf.Floor(LowWarningTime).ToString() + " minutes left!", Mathf.Round(workingWarnTime).ToString() + " minutes have been used.", 9, false);
                            }
                            else
                            {
                                Notify(workingWarnTime, Mathf.Floor(LowWarningTime).ToString() + " minutes left!", Mathf.Round(workingWarnTime * 60).ToString("00") + " seconds have been used.", 9, false);
                            }
                        }
                        else
                        {
                            if (workingWarnTime > 1)
                            {
                                Notify(workingWarnTime, Mathf.Floor(LowWarningTime * 60).ToString("00") + " seconds left!", Mathf.Round(workingWarnTime).ToString() + " minutes have been used.", 9, false);
                            }
                            else
                            {
                                Notify(workingWarnTime, Mathf.Floor(LowWarningTime * 60).ToString("00") + " seconds left!", Mathf.Round(workingWarnTime * 60).ToString("00") + " seconds have been used.", 9, false);
                            }
                        }
                    }


                }
            }
            if (NotificationWarningHalfTimeEnabled && WorkingRemTime > 5) // Working Remaining Time Factored In
            {
                float workingRemTime = UsedTime + (WorkingRemTime / 2);
                Notify(WorkingRemTime / 2, Mathf.Floor(WorkingRemTime / 2).ToString() + " minutes left!", Mathf.Round(workingRemTime).ToString() + " minutes have been used.", 10, false);
            }
            if (NotificationCustomTimeWarning && CustomWarningTime < WorkingRemTime) // Needs help for Working Remaining Time
            {
                float workingUsedTime = UsedTime + (CustomWarningTime);
                float workingRemainingTime = WorkingRemTime - CustomWarningTime;

                if (workingUsedTime > 1)
                {
                    if (CustomWarningTime > 1)
                    {
                        if (workingRemainingTime > 1)
                        {
                            Notify(CustomWarningTime, Mathf.Round(CustomWarningTime).ToString() + " minutes have elapsed!", Mathf.Round(workingUsedTime).ToString() + " minutes have been used." +
                                "\n" + Mathf.Floor(workingRemainingTime).ToString() + " minutes remain.", 11, false);
                        }
                        else
                        {
                            Notify(CustomWarningTime, Mathf.Round(CustomWarningTime).ToString() + " minutes have elapsed!", Mathf.Round(workingUsedTime).ToString() + " minutes have been used." +
                                "\n" + Mathf.Floor(workingRemainingTime * 60).ToString("00") + " seconds remain.", 11, false);
                        }
                    }
                    else
                    {
                        if (workingRemainingTime > 1)
                        {
                            Notify(CustomWarningTime, Mathf.Round(CustomWarningTime * 60).ToString("00") + " seconds have elapsed!", Mathf.Round(workingUsedTime).ToString() + " minutes have been used." +
                                "\n" + Mathf.Floor(workingRemainingTime).ToString() + " minutes remain.", 11, false);
                        }
                        else
                        {
                            Notify(CustomWarningTime, Mathf.Round(CustomWarningTime * 60).ToString("00") + " seconds have elapsed!", Mathf.Round(workingUsedTime).ToString() + " minutes have been used." +
                                "\n" + Mathf.Floor(workingRemainingTime * 60).ToString("00") + " seconds remain.", 11, false);
                        }
                    }
                }
                if (workingUsedTime <= 1)
                {
                    if (CustomWarningTime > 1)
                    {
                        //Notify(CustomWarningTime, Mathf.Round(CustomWarningTime).ToString() + " minutes have elapsed!", Mathf.Round(workingUsedTime*60).ToString("00") + " seconds have been used.", 11, false);
                        if (workingRemainingTime > 1)
                        {
                            Notify(CustomWarningTime, Mathf.Round(CustomWarningTime).ToString() + " minutes have elapsed!", Mathf.Round(workingUsedTime * 60).ToString("00") + " seconds have been used." +
                                "\n" + Mathf.Floor(workingRemainingTime).ToString() + " minutes remain.", 11, false);
                        }
                        else
                        {
                            Notify(CustomWarningTime, Mathf.Round(CustomWarningTime).ToString() + " minutes have elapsed!", Mathf.Round(workingUsedTime * 60).ToString("00") + " seconds have been used." +
                                "\n" + Mathf.Floor(workingRemainingTime * 60).ToString("00") + " seconds remain.", 11, false);
                        }
                    }
                    else
                    {
                        //Notify(CustomWarningTime, Mathf.Round(CustomWarningTime*60).ToString("00") + " seconds have elapsed!", Mathf.Round(workingUsedTime*60).ToString("00") + " seconds have been used.", 11, false);
                        if (workingRemainingTime > 1)
                        {
                            Notify(CustomWarningTime, Mathf.Round(CustomWarningTime * 60).ToString("00") + " seconds have elapsed!", Mathf.Round(workingUsedTime * 60).ToString("00") + " seconds have been used." +
                                "\n" + Mathf.Floor(workingRemainingTime).ToString() + " minutes remain.", 11, false);
                        }
                        else
                        {
                            Notify(CustomWarningTime, Mathf.Round(CustomWarningTime * 60).ToString("00") + " seconds have elapsed!", Mathf.Round(workingUsedTime * 60).ToString("00") + " seconds have been used." +
                                "\n" + Mathf.Floor(workingRemainingTime * 60).ToString("00") + " seconds remain.", 11, false);
                        }
                    }
                }


            }
        }
        else
        {
            Notify(WorkingRemTime, "Time has run out!", "The timer has exceeded the total time.", 8, false);
        }
    }
    public void ResendNotifs()
    {
        if (RunTimer)
        {
            CancelMostNotifications();
            float timeattoggled = RemainingTime;
            long tickdifference = (CurrentTick - TicksWhenTimerStarted);
            float tickD = (float)tickdifference / 600000000;
            float WorkingRemTime = RemainingTime - tickD;
            if (WorkingRemTime > 0) { NotificationSender(WorkingRemTime); }
            /*
            if (WorkingRemTime >= 0)
            {
                if (NotificationsEnabled)
                {
                    if (WorkingRemTime > 0.2f)
                    {
                        Notify(WorkingRemTime - 0.2f, "Time has run out", "The timer has exceeded the total time.", 8, false);
                        if (NotificationLowWarningsEnabled && WorkingRemTime > (LowWarningTime) && WorkingRemTime > 0.2f)
                        {
                            if (WorkingRemTime - ((LowWarningTime - 0.2f)) > 1) { Notify(WorkingRemTime - ((LowWarningTime - 0.2f)), Mathf.Round(LowWarningTime).ToString() + " minutes left!", "The timer has " + Mathf.Round((LowWarningTime - 0.2f)).ToString() + " minutes remaining.", 9, false); }
                            else { Notify(WorkingRemTime - ((LowWarningTime - 0.2f)), Mathf.Round(LowWarningTime*60).ToString("00") + " Seconds left!", "The timer has " + Mathf.Round(LowWarningTime).ToString() + " Seconds remaining.", 9, false); }
                        }

                        else if (NotificationLowWarningsEnabled && WorkingRemTime > (LowWarningTime))
                        {
                            if (LowWarningTime > 1)
                            {
                                float workingUsedTime = WorkingRemTime - (LowWarningTime);
                                if (WorkingRemTime - (LowWarningTime) > 1)
                                {
                                    Notify(WorkingRemTime - (LowWarningTime), Mathf.Round(LowWarningTime).ToString() + " minutes left!", Mathf.Round(workingUsedTime).ToString() + " minutes have been used.", 9, false);

                                }
                                else
                                {
                                    Notify(WorkingRemTime - (LowWarningTime), Mathf.Round(LowWarningTime*60).ToString("00") + " Seconds left!", Mathf.Round(workingUsedTime).ToString() + " seconds have been used.", 9, false);

                                }
                                
                            }
                        }
                        if (NotificationWarningHalfTimeEnabled && WorkingRemTime > 5)
                        {
                            Notify(WorkingRemTime / 2, Mathf.Round(WorkingRemTime / 2).ToString() + " minutes used!", "The timer has " + Mathf.Round(WorkingRemTime / 2).ToString() + " minutes remaining.", 10, false);
                        }
                        if (NotificationCustomTimeWarning && CustomWarningTime < WorkingRemTime)
                        {
                            float WRemTime = WorkingRemTime - (CustomWarningTime / 60);
                            if (CustomWarningTime > 1)
                            {
                                Notify(CustomWarningTime, Mathf.Round(CustomWarningTime).ToString() + " minutes used!", Mathf.Round(WRemTime).ToString() + " minutes have been used.", 11, false);
                            }
                            else {
                                Notify(CustomWarningTime, Mathf.Round(CustomWarningTime).ToString("00") + " seconds used!", Mathf.Round(WRemTime).ToString() + " minutes have been used.", 11, false);
                            }
                            
                        }
                    }
                    else
                    {
                        Notify(WorkingRemTime, "Time has run out", "The timer has exceeded the total time.", 8, false);
                    }
                }
            }
      */
        }

    }
    void Update()
    {
        FillProfileButtonCollection();
        Application.targetFrameRate = (int)Screen.currentResolution.refreshRateRatio.value;
        if (!Application.isFocused) { Application.targetFrameRate = 24; }
        if (RunTimer) { Application.runInBackground = true; }
        else { Application.runInBackground = false; }
        if (ResetReset)
        {
            pendingreset = false;
            pendingtimerappreset = false;
            pendingtimerreset = false;
            pendingappreset = false;
        }
        ResetReset = false;
        viewCurrentTick = CurrentTick;
        RemainingTime = TotalTime - UsedTime;

        if (primesave)
        {
            Save(Profile);
            primesave = false;
        }
        if (UsedTime != UsedTimeLastTick)
        {
            primesave = true;
            ResendNotifs();
        }
        RunningTime = (float)(CurrentTick - TicksWhenTimerStarted) / 600000000;
        if (!RunTimer) { RunningTime = 0; }
        UsedTimeLastTick = UsedTime;
        if (RunTimer && !WasRunTimer)
        {
            WasRunTimer = true;
            TicksWhenTimerStarted = CurrentTick;
            if (NotificationsEnabled)
            {
                if (RemainingTime > 1)
                {
                    Notify(0, "Timer Running", "The timer is running.\n" + Mathf.Floor(RemainingTime).ToString() + " minutes remained when the timer was started", 7, true, true);
                }
                else if (RemainingTime >= 0)
                {
                    Notify(0, "Timer Running", "The timer is running.\n" + Mathf.Floor(RemainingTime*60).ToString() + " seconds remained when the timer was started", 7, true, true);
                }
                else if (RemainingTime < 0 && RemainingTime >= -1)
                {
                    Notify(0, "Timer Running", "The timer is running.\n" + Mathf.Floor(RemainingTime * 60).ToString() + " seconds over when the timer was started", 7, true, true);
                }
                else if (RemainingTime < -1)
                {
                    Notify(0, "Timer Running", "The timer is running.\n" + Mathf.Floor(RemainingTime).ToString() + " minutes over when the timer was started", 7, true, true);
                }
                /*
                if (RemainingTime >= 0)
                {
                    if (RemainingTime > 0.2f)
                    {
                        Notify(RemainingTime - 0.2f, "Time has run out", "The timer has exceeded the total time.", 8, false);
                        if (NotificationLowWarningsEnabled && RemainingTime > (LowWarningTime) && RemainingTime > 0.2f)
                        {
                            //Why do I need to do this if persision isn't within 13 seconds.
                            if (LowWarningTime > 1) // Working Remaining Time Factored In
                            {
                                float workingWarnTime = RemainingTime - LowWarningTime;
                                if (workingWarnTime > 0)
                                {
                                    if (LowWarningTime > 1)
                                    {
                                        Derboss.Log("case1");
                                        if (workingWarnTime > 1)
                                        {
                                            Derboss.Log("case1A");
                                            Notify(workingWarnTime, Mathf.Floor(LowWarningTime).ToString() + " minutes left!", Mathf.Round(workingWarnTime).ToString() + " minutes have been used.", 9, false);
                                        }
                                        else
                                        {
                                            Derboss.Log("case1B");
                                            Notify(workingWarnTime, Mathf.Floor(LowWarningTime).ToString() + " minutes left!", Mathf.Round(workingWarnTime * 60).ToString("00") + " seconds have been used.", 9, false);
                                        }
                                    }
                                    else
                                    {
                                        Derboss.Log("case2");
                                        if (workingWarnTime > 1)
                                        {
                                            Derboss.Log("case2A");
                                            Notify(workingWarnTime, Mathf.Floor(LowWarningTime * 60).ToString("00") + " seconds left!", Mathf.Round(workingWarnTime).ToString() + " minutes have been used.", 9, false);
                                        }
                                        else
                                        {
                                            Derboss.Log("case2B");
                                            Notify(workingWarnTime, Mathf.Floor(LowWarningTime * 60).ToString("00") + " seconds left!", Mathf.Round(workingWarnTime * 60).ToString("00") + " seconds have been used.", 9, false);
                                        }
                                    }
                                }
     

                            }
                        }
                        if (NotificationWarningHalfTimeEnabled && RemainingTime > 5) // Working Remaining Time Factored In
                        {
                            float workingRemTime = UsedTime + (RemainingTime/2);
                            Notify(RemainingTime / 2, Mathf.Floor(RemainingTime / 2).ToString() + " minutes left!",Mathf.Round(workingRemTime).ToString() + " minutes have been used.", 10, false);
                        }
                        if (NotificationCustomTimeWarning && CustomWarningTime < RemainingTime) // Needs help for Working Remaining Time
                        {
                            float workingUsedTime = UsedTime + (CustomWarningTime);
                            float workingRemainingTime = RemainingTime - CustomWarningTime;

                            if (workingUsedTime > 1)
                            {
                                if (CustomWarningTime > 1)
                                {
                                    if (workingRemainingTime > 1)
                                    {
                                        Notify(CustomWarningTime, Mathf.Round(CustomWarningTime).ToString() + " minutes have elapsed!", Mathf.Round(workingUsedTime).ToString() + " minutes have been used." +
                                            "\n" + Mathf.Floor(workingRemainingTime).ToString() + " minutes remain.", 11, false);
                                    }
                                    else
                                    {
                                        Notify(CustomWarningTime, Mathf.Round(CustomWarningTime).ToString() + " minutes have elapsed!", Mathf.Round(workingUsedTime).ToString() + " minutes have been used." +
                                            "\n" + Mathf.Floor(workingRemainingTime*60).ToString("00") + " seconds remain.", 11, false);
                                    }
                                }
                                else
                                {
                                    if (workingRemainingTime > 1)
                                    {
                                        Notify(CustomWarningTime, Mathf.Round(CustomWarningTime * 60).ToString("00") + " seconds have elapsed!", Mathf.Round(workingUsedTime).ToString() + " minutes have been used." +
                                            "\n" + Mathf.Floor(workingRemainingTime).ToString() + " minutes remain.", 11, false);
                                    }
                                    else
                                    {
                                        Notify(CustomWarningTime, Mathf.Round(CustomWarningTime * 60).ToString("00") + " seconds have elapsed!", Mathf.Round(workingUsedTime).ToString() + " minutes have been used." +
                                            "\n" + Mathf.Floor(workingRemainingTime * 60).ToString("00") + " seconds remain.", 11, false);
                                    }
                                }
                            }
                            if (workingUsedTime <= 1)
                            {
                                if (CustomWarningTime > 1)
                                {
                                    //Notify(CustomWarningTime, Mathf.Round(CustomWarningTime).ToString() + " minutes have elapsed!", Mathf.Round(workingUsedTime*60).ToString("00") + " seconds have been used.", 11, false);
                                    if (workingRemainingTime > 1)
                                    {
                                        Notify(CustomWarningTime, Mathf.Round(CustomWarningTime).ToString() + " minutes have elapsed!", Mathf.Round(workingUsedTime * 60).ToString("00") + " seconds have been used." +
                                            "\n" + Mathf.Floor(workingRemainingTime).ToString() + " minutes remain.", 11, false);
                                    }
                                    else
                                    {
                                        Notify(CustomWarningTime, Mathf.Round(CustomWarningTime).ToString() + " minutes have elapsed!", Mathf.Round(workingUsedTime * 60).ToString("00") + " seconds have been used." +
                                            "\n" + Mathf.Floor(workingRemainingTime * 60).ToString("00") + " seconds remain.", 11, false);
                                    }
                                }
                                else
                                {
                                    //Notify(CustomWarningTime, Mathf.Round(CustomWarningTime*60).ToString("00") + " seconds have elapsed!", Mathf.Round(workingUsedTime*60).ToString("00") + " seconds have been used.", 11, false);
                                    if (workingRemainingTime > 1)
                                    {
                                        Notify(CustomWarningTime, Mathf.Round(CustomWarningTime * 60).ToString("00") + " seconds have elapsed!", Mathf.Round(workingUsedTime * 60).ToString("00") + " seconds have been used." +
                                            "\n" + Mathf.Floor(workingRemainingTime).ToString() + " minutes remain.", 11, false);
                                    }
                                    else
                                    {
                                        Notify(CustomWarningTime, Mathf.Round(CustomWarningTime * 60).ToString("00") + " seconds have elapsed!", Mathf.Round(workingUsedTime * 60).ToString("00") + " seconds have been used." +
                                            "\n" + Mathf.Floor(workingRemainingTime * 60).ToString("00") + " seconds remain.", 11, false);
                                    }
                                }
                            }


                        }
                    }
                    else
                    {
                        Notify(RemainingTime, "Time has run out", "The timer has exceeded the total time.",8,false);
                    }

                }
            */
                if (RemainingTime >= 0)
                {
                    NotificationSender(RemainingTime);
                }
            }
            Save(Profile);
        }
        if (!RunTimer && WasRunTimer)
        {
            long tickdifference = (CurrentTick - TicksWhenTimerStarted);
            float tickD = (float)tickdifference / 600000000;
            //Instantiate(TimetoAdd, new Vector3((float)(CurrentTick - TicksWhenTimerStarted) / 600000000, transform.position.y, 0), transform.rotation, ContentView.transform);
            var logs = Instantiate(TimetoAdd, new Vector3(0, transform.position.y, 0), transform.rotation, ContentView.transform);
            UsedTime += tickD;
            logs.GetComponent<AddedTimesScript>().MinutesToAdd = tickD;
            logs.GetComponent<AddedTimesScript>().BeingMade = true;
            WasRunTimer = false;
            CancelNotifications(Profile,7);
            CancelMostNotifications();
        }
        if (!NotificationsEnabled && NotificationsEnabledOld)
        {
            CancelNotifications(0,6,true);
        }
        NotificationsEnabledOld = NotificationsEnabled;
        NotificationLowWarningsEnabledOld = NotificationLowWarningsEnabled;
        NotificationWarningHalfTimeEnabledOld = NotificationWarningHalfTimeEnabled;
    }
    public void Save(int Prof)
    {
        int Profs = 0;
        try
        {
            //Profs = JsonUtility.FromJson<SaveObjectList>(File.ReadAllText(    Path.Combine(Application.persistentDataPath, "Savedata", "Settings.json"))).Objects[1].datas.Length;
            Profs = JsonUtility.FromJson<SaveObjectList>(File.ReadAllText(    Path.Combine(Application.persistentDataPath, "Savedata", "Settings.json"))).Objects[1].datas.Length;
           // Path.Combine(Application.persistentDataPath, "Savedata", "Profile" + Prof.ToString() + ".json")
            //Path.Combine(Application.persistentDataPath, "Savedata", "Settings.json")
        }
        catch
        {
            Profs = 1;
        }
        Profile = Prof;
        if (Prof > Profs - 1) { }
        if (RunTimer) { MKfile(1, 0, TicksWhenTimerStarted.ToString(), -1, Prof); }
        else { MKfile(1, 0, (-1).ToString(), -1, Prof); }
        MKfile(1, 1, DefaultTime.ToString(), -1, Prof);
        MKfile(1, 2, UsedTime.ToString(), -1, Prof);
        MKfile(1, 3, TotalTime.ToString(), -1, Prof);
        GameObject[] TimeMarkers = GameObject.FindGameObjectsWithTag("DelonReset");
        int objs = TimeMarkers.Length;
        for (int i = 0; i < objs; i++)
        {
            //
                MKfile(0, i, TimeMarkers[i].GetComponent<AddedTimesScript>().MinutesAdded.ToString(), objs, Prof);
                MKfile(5, i, TimeMarkers[i].GetComponent<AddedTimesScript>().TimeWhenAdded.ToString(), objs, Prof);
        }
        if (objs == 0)
        {
            MKfile(0, 0, 0.ToString(), 0, Prof);
            MKfile(5, 0, (-792).ToString(), 0, objs);
        }
        MKfile(0, 0, Prof.ToString(), true);
    }
    public UnityEvent LoadCalled;
    public void Load(int Prof)
    {
        Load(Prof, false);
    }
    public void Load(int Prof, bool Nofile)
    {
        LoadCalled.Invoke();
        int Profs = 0;
        if (!Nofile)
        {
            try
            {
                //Profs = JsonUtility.FromJson<SaveObjectList>(File.ReadAllText(Application.persistentDataPath + @"\Savedata\Settings.json")).Objects[1].datas.Length;
                Profs = JsonUtility.FromJson<SaveObjectList>(File.ReadAllText(    Path.Combine(Application.persistentDataPath, "Savedata", "Settings.json"))).Objects[1].datas.Length;
            }
            catch
            {
                Profs = 1;
            }
        }
        Profiles = Profs;
        ProfilesView.GetComponent<TimesAdded>().TimesAppeneded = 0;
        if (CheckFile(Prof) && !Nofile)
        {
                foreach (GameObject Obj in ProfileButtonCollection)
                {
                    Destroy(Obj);
                    
                }

                int objsProf = Profiles;

                ProfileButtonCollection = new GameObject[objsProf];
                for (int i = 0; i < objsProf; i++)
                {
                    ProfileButtonCollection[i] = Instantiate(ProfileToAdd, new Vector3(0, transform.position.y, 0), transform.rotation, ProfilesView.transform);
                }
                GameObject[] Delete = GameObject.FindGameObjectsWithTag("DelonReset");
                foreach (GameObject Obj in Delete)
                {
                    Destroy(Obj);
                }
                //int objs = JsonUtility.FromJson<SaveObjectList>(File.ReadAllText(Application.persistentDataPath + @"\Savedata\Profile" + Profile.ToString() + ".json")).Objects[0].datas.Length;
                int objs = JsonUtility.FromJson<SaveObjectList>(File.ReadAllText(Path.Combine(Application.persistentDataPath, "Savedata", "Profile" + Prof.ToString() + ".json"))).Objects[0].datas.Length;
                for (int i = 0; i < objs; i++)
                {
                    try
                    {
                        //if (float.Parse(RDfile(0, i)) != 0)
                        {
                           var logs = Instantiate(TimetoAdd, new Vector3(float.Parse(RDfile(0, i)), transform.position.y, 0), transform.rotation, ContentView.transform);
                           logs.GetComponent<AddedTimesScript>().BeingMade = true;
                           logs.GetComponent<AddedTimesScript>().MinutesToAdd = float.Parse(RDfile(0, i));
                    }

                    }

                    catch
                    {

                    }
                }

            DefaultTime = float.Parse(RDfile(1, 1));
            if (DefaultTime == -792) { DefaultTime = 180; }
            UsedTime = float.Parse(RDfile(1, 2));
            if (UsedTime == -792) { UsedTime = 0; }
            TotalTime = float.Parse(RDfile(1, 3));
            if (TotalTime == -792) { TotalTime = 180; }
            ContentView.GetComponent<TimesAdded>().TimesAppeneded = 0;
            try
            {
                int CountUpTog = int.Parse(RDfile(0, 2));
                if (CountUpTog == 1) { CountUp = true;  }
                if (CountUpTog == 0) { CountUp = false;  }

            }
            catch { CountUp = false; }

            long timerrunning = -1;
            try
            {
                timerrunning = long.Parse(RDfile(1, 0));
            }
            catch { }

            if (timerrunning >= 0) { RunTimer = true; WasRunTimer = true; TicksWhenTimerStarted = timerrunning; }
            else
            {
                RunTimer = false; WasRunTimer = false;
            }
            ProfileName = RDfile(1, Prof, true);
            string ProfName = ("Profile" + Prof.ToString()).ToString();
            try { if (int.Parse(RDfile(1, Prof, true)) == -792) { ProfileName = ProfName; } }
            catch { ProfileName = ProfName; }

            Profile = Prof;
            MKfile(0, 0, Prof.ToString(), true);
            MKfile(1, Prof, ProfileName, true);
        }
        else
        {
            ResetTime(DefaultTime, true);
            string ProfName = ("Profile" + (Prof + 1).ToString()).ToString();
            try { if (int.Parse(RDfile(1, Prof, true)) == -792) { ProfileName = ProfName; } }
            catch { ProfileName = ProfName; }
            if (Nofile) {
                foreach (GameObject Obj in ProfileButtonCollection)
                {
                    Destroy(Obj);

                }
                Profiles = 1;
                int objsProf = Profiles;
                ProfileButtonCollection = new GameObject[objsProf];
                ProfileButtonCollection[0] = Instantiate(ProfileToAdd, new Vector3(0, transform.position.y, 0), transform.rotation, ProfilesView.transform);
                GameObject[] Delete = GameObject.FindGameObjectsWithTag("DelonReset");
                foreach (GameObject Obj in Delete)
                {
                    Destroy(Obj);
                }
                DefaultTime = 180;
                UsedTime = 0;
                TotalTime = 180;
                ContentView.GetComponent<TimesAdded>().TimesAppeneded = 0;
                CountUp = false; 
                RunTimer = false; WasRunTimer = false;
                ProfileName = "Profile0";

            }
            Profile = Prof;
            MKfile(0, 0, Prof.ToString(), true);
            MKfile(1, Prof, ProfileName, true);
        }
    }
    public void FillProfileButtonCollection()
    {
        GameObject[] Profs = GameObject.FindGameObjectsWithTag("ProfileDelete");
        if (Profs.Length > 0)
        {
            ProfileButtonCollection = new GameObject[Profs.Length];
            for (int i = 0; i < Profs.Length; i++)
            {
                ProfileButtonCollection[i] = Profs[i];

            }
        }
    }

    public void RemoTime(float TimeToRemove)
    {
        UsedTime -= TimeToRemove;
        Save(Profile);
        Load(Profile);
    }
    public void DelProf()
    {
        DelProf(Profile);
    }
    public void DelProf(int Prof)
    {
        DeleteProf(Prof);
        Profile = 0;
        Load(Profile);
    }

    public void DeleteProf(int Prof)
    {
        CancelNotifications(Prof,6);
        CancelNotifications(Prof,7);
        int Profs;
        try
        {
            Profs = JsonUtility.FromJson<SaveObjectList>(File.ReadAllText(    Path.Combine(Application.persistentDataPath, "Savedata", "Settings.json"))).Objects[1].datas.Length;
        }
        catch
        {
            Profs = 1;
        }
        File.Delete(Application.persistentDataPath + @"\Savedata\Profile" + Prof.ToString() + ".json");
        if (Profs == 1)
        {
            return;
        }
        int TopProf = Profs - 1;

        for (; Prof < Profs + 1; Prof++)
        {
            try
            {
                if (Prof < TopProf - 1)
                {
                    MKfile(1, Prof, RDfile(1, Prof + 1, true), TopProf + 1, true);
                }
                else if (Prof == TopProf - 1)
                {
                    MKfile(1, Prof, RDfile(1, TopProf, true), Profs - 1, true);
                }
                else if (Prof == TopProf)
                {
                    MKfile(1, Prof, RDfile(1, Prof + 1, true), TopProf, true);
                }
            }
            catch { }
            try
            {
                if (!Directory.Exists(Path.Combine(Application.persistentDataPath, "Savedata"))) { Directory.CreateDirectory(Path.Combine(Application.persistentDataPath, "Savedata")); }

                SaveObjectList sol = new SaveObjectList();
                string FilePath = (@"\Savedata\Profile" + Prof.ToString() + ".json");
                string TotalFilePath = Path.Combine(Application.persistentDataPath, "Savedata", "Profile" + Prof.ToString() + ".json");
                string TotalNextFilePath = Path.Combine(Application.persistentDataPath, "Savedata", "Profile" + (Prof+1).ToString() + ".json");
                string TotalTopFilePath = Path.Combine(Application.persistentDataPath, "Savedata", "Profile" + TopProf.ToString() + ".json");
                //File.Copy(Application.persistentDataPath + @"\Savedata\Profile" + (Prof + 1).ToString() + ".json", Application.persistentDataPath + FilePath, true);
                //File.Delete(Application.persistentDataPath + @"\Savedata\Profile" + TopProf.ToString() + ".json");

                File.Copy(TotalNextFilePath, TotalFilePath, true);
                File.Delete(TotalTopFilePath);
            }
            catch (Exception e) { UnityEngine.Debug.Log(e); }

        }
    }
    public void UpdateProfile(int Prof)
    {
        Save(Profile);
        Profile = Prof;
        Load(Prof);
    }
    public void IncrementProfile()
    {
        Profile += 1;
        UpdateProfile(Profile);
    }
    public void DecrementProfile()
    {
        Profile -= 1;
        UpdateProfile(Profile);
    }
    public void AddProfile(string Name)
    {
        Save(Profile);
        int NewProfile = Profiles;
        ProfileName = Name;
        MKfile(1, NewProfile, ProfileName, Profiles + 1, true);
        Profile = NewProfile;
        pendingreset = true;
        ResetTime();
        AddProf = NewProfile;
        AddProfDelay = 100;
        Save(Profile);
        Load(Profile);
    }
    public void AddProfPtwo(int NewProfile)
    {
        Save(NewProfile);
        Load(NewProfile);
        pendingreset = true;
        ResetTime();
    }
    public void ResetTime(float ResetToTime)
    {
        ResetTime(ResetToTime, false);
    }
    public void ResetTime()
    {
        ResetTime(DefaultTime);
    }
    public void ResetTime(bool Immeaiate)
    {
        ResetTime(DefaultTime, Immeaiate);
    }
    public void ResetTime(float ResetToTime, bool Immeadiate)
    {
        //resettime = 0.4167f;
        resettime = 100f;
        if (pendingreset || Immeadiate)
        {
            CancelNotifications(Profile, 7);
            CancelMostNotifications();
            UsedTime = 0;
            TotalTime = ResetToTime;

            GameObject[] Delete = GameObject.FindGameObjectsWithTag("DelonReset");
            foreach (GameObject Obj in Delete)
            {
                Destroy(Obj);
            }
            ContentView.GetComponent<TimesAdded>().TimesAppeneded = 0;
            pendingreset = false;
            RunTimer = false;
            WasRunTimer = false;
            TicksWhenTimerStarted = CurrentTick;
            Save(Profile);
            return;
        }

        pendingreset = true;
    }
    public void ResetTimer()
    {

        //timerresettime = 0.4167f;
        timerresettime = 100f;
        if (pendingtimerreset)
        {
            CancelNotifications(Profile,7);
            CancelMostNotifications();
            TicksWhenTimerStarted = CurrentTick;
            MKfile(1, 0, (-1).ToString(), -1, false);
            RunTimer = false;
            WasRunTimer = false;
            return;
        }

        pendingtimerreset = true;
    }

    public void AppReset()
    {
        //appresettime = 0.2167f;
        appresettime = 100f;
        if (pendingappreset)
        {
            CancelNotifications(0,6, true);
            pendingappreset = false;
            pendingreset = true;
            if (Directory.Exists(Application.persistentDataPath + @"\Savedata\"))
            {
                Directory.Delete(Application.persistentDataPath + @"\Savedata\", true);
            }
            Directory.CreateDirectory(Application.persistentDataPath + @"\Savedata\");
            pendingtimerappreset = true;
            ResetTime(true);
            Load(0,true);
            ResetValuesTimer = 1;
            ResetValues = true;
            BigReset = true;
            return;
        }

        pendingappreset = true;
    }

    public float Truncate(float number, int digits)
    {
        long PowTen = (long)Math.Pow(10, digits);
        double Dnum = number * PowTen;
        Dnum = (long)Dnum;
        Dnum /= PowTen;
        return (float)Dnum;
    }
    public string TruncateFS(float number, int digits)
    {
        float truncatednum = Truncate(number, digits);

        string formatstring = "F" + digits;
        return truncatednum.ToString(formatstring);
    }
    public string TruncateForSeconds(float number, int digits)
    {
        string formatstring = "F" + digits;
        return number.ToString(formatstring);
    }
    public string TruncateForSecondsNM(float number, int digits)
    {

        string NumString = number.ToString();
        float TheFloat = Truncate(number, digits);

        NumString = TheFloat.ToString();
        if (TheFloat < 10) { NumString = "0" + NumString; }
        return NumString;
    }
    public bool TildeEqual(float f1, float f2)
    {
        return TildeEqual(f1, f2, 0.05f);
    }
    public bool TildeEqual(float f1, float f2, float percent)
    {

        float GPerf1 = f1 + (f1 * percent);
        float LPerf1 = f1 - (f1 * percent);

        if ((f2 < GPerf1 && f2 > LPerf1) || f1 == f2) { return true; }

        return false;
    }
    public float addtime(float timeToAdd)
    {
        UsedTime += timeToAdd;
        return TotalTime;
    }
    public void toggletime()
    {
        RunTimer = !RunTimer;
    }
    public void CancelTimer()
    {
        CancelNotifications(Profile, 7);
        CancelMostNotifications();
        TicksWhenTimerStarted = CurrentTick;
        RunTimer = false;
        WasRunTimer = false;
    }
    public void ToggleCountUp()
    {
        CountUp = !CountUp;
        int tog = 0;
        if (CountUp) { tog = 1; }
        MKfile(0, 2, tog.ToString(), -1, Profile, true);
    }
}