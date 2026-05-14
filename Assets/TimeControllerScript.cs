using System;
using System.Collections;
using System.IO;
using TMPro;


//using Unity.Notifications.iOS;
using UnityEngine;

using UnityEngine.Events;
using System.Reflection;
using UnityEditor.ShaderKeywordFilter;



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
        }
        Derboss.Init();
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
        
        Debug.Log(Application.platform);

        StartCoroutine(RequestPerms());
        NotificationSetup();
        #if UNITY_ANDROID
                AndroidNotificationCenter.CancelAllScheduledNotifications();
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
    public bool NotificationsEnabledOld = true;
    public bool NotificationsEnabled = true;
    public bool NotificationWarningsEnabled = true;
    public bool NotificationWarningsEnabledOld = true;
    public bool NotificationWarningHalfTimeEnabled = true;
    public bool NotificationWarningHalfTimeEnabledOld = true;
    IEnumerator RequestPerms()
    {
#if UNITY_ANDROID || UNITY_IOS
        Debug.Log("Is Android");
        string perm = "android.permission.POST_NOTIFICATIONS";
        if (Permission.ShouldShowRequestPermissionRationale(perm))
        {
            Debug.Log("Should Show Rationale");
        }
        if (!Permission.HasUserAuthorizedPermission(perm))
        {
            Debug.Log("Not Authorized On Android");
        }

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
        Debug.Log("Not Android or iOS");
        yield break;
#endif
    }
    private string NotificationChannel = "V2";
    public void NotificationSetup()
    {
#if UNITY_ANDROID
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
        if (Application.platform == RuntimePlatform.IPhonePlayer)
        {
            Debug.Log(":(");
        }
        if (Application.platform == RuntimePlatform.WindowsPlayer)
        {
            Debug.Log("MS Windows");
        }
        if (Application.platform == RuntimePlatform.WindowsEditor)
        {
            Debug.Log("MS Windows Editor");
        }
    }
    int[] ChannelIDs = new int[0];
    int SavedNotifIDIndex(SaveObjectList data)
    {
        if (data?.Objects == null || data.Objects.Length <= 6)
        {             return 0;
        }
        if (data.Objects[6] == null) { return 0; }
        if (data.Objects[6].datas == null) { return 0; }
        if (data.Objects[6].datas.Length == 0) { return 0; }
        if (data.Objects[6].datas[0] == "-792") { return 0; }
        return data.Objects[6].datas.Length;
    }
    int[] SavedNotifIDs(SaveObjectList data)
    {
        int[] IDIndex = new int[1];
        if (data?.Objects == null || data.Objects.Length <= 6)
        {
            IDIndex[0] = 0;
            return IDIndex;
        }

        return IDIndex;
    }
    public float WarningTime = 5f;
    public void WarningTimeFunc(float flot)
    {
        WarningTime = flot;
    }

    public void Notify(float FireMinutes,string NotifTitle,string Notiftext)
    {
#if UNITY_ANDROID
        Debug.Log("Notification Deployed for:" + DateTime.Now.AddMinutes(FireMinutes));
        AndroidJavaClass unityPlayerAndr = new AndroidJavaClass("com.unity3d.player.UnityPlayer");
        AndroidJavaObject activityAndr = unityPlayerAndr.GetStatic<AndroidJavaObject>("currentActivity");
        AndroidJavaObject contextAndr = activityAndr.Call<AndroidJavaObject>("getApplicationContext");
        AndroidJavaObject alarmManager = contextAndr.Call<AndroidJavaObject>("getSystemService", "alarm");
        AndroidNotification notification = new AndroidNotification();
        notification.Title = NotifTitle;
        notification.Text = Notiftext;
        notification.FireTime = DateTime.Now.AddMinutes(FireMinutes);
        notification.ShowTimestamp = true;
        notification.Color = new Color(0.027f, 0.267f, 0.016f);
        notification.ShouldAutoCancel = true;
        notification.UsesStopwatch = true;
        int id = AndroidNotificationCenter.SendNotification(notification, "Hourglass_Channel" + NotificationChannel);
        
        int DatInd = 0;
        var data = JsonUtility.FromJson<SaveObjectList>(File.ReadAllText(Path.Combine(Application.persistentDataPath, "Savedata", "Profile" + Profile.ToString() + ".json")));
        DatInd = SavedNotifIDIndex(data);
        DatInd = int.Parse(RDfile(6, 0, true));
        if (DatInd == -792) DatInd = 0;
        MKfile(6, DatInd, id.ToString(), false);
#endif
    }
    public void CancelNotifications()
    {
        Debug.Log("Notifs Canceled");
#if UNITY_ANDROID
        var data = JsonUtility.FromJson<SaveObjectList>(File.ReadAllText(Path.Combine(Application.persistentDataPath, "Savedata", "Profile" + Profile.ToString() + ".json")));
        int[] NotifIndex = SavedNotifIDs(data);
        foreach (int ID in NotifIndex)
        {
            AndroidNotificationCenter.CancelNotification(ID);
        }
        MKfile(6, 0, "", 0, false);
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
        *6:NotificationIds
     *Settings[Default]
        *9:EnableNotification [NotificationSettings Starts]
        *11:EnableNotificationWarning
        *13:EnableNotificationWarningAtHalfTime
        *12:WarningTime [NotificationSettings Ends]
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
            Debug.Log("Small Obj or Data" + Objindex + ":" + Dataindex);
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
                Debug.Log(e);
                Debug.Log(Objindex);
                Debug.Log(Dataindex);
                // Debug.Log(JsonUtility.FromJson<SaveObjectList>(File.ReadAllText(Application.persistentDataPath + FilePath)).Objects[Objindex].datas.Length);
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
        if (DebugOutput)
        {
            Debug.Log(UsedTime);
        }
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
        }
        RunningTime = (float)(CurrentTick - TicksWhenTimerStarted) / 600000000;
        if (!RunTimer) { RunningTime = 0; }
        UsedTimeLastTick = UsedTime;
        if (RunTimer && !WasRunTimer)
        {
            WasRunTimer = true;
            TicksWhenTimerStarted = CurrentTick;
            if (RemainingTime >= 0)
            {
                if (NotificationsEnabled)
                {
                    if (RemainingTime > 0.2f)
                    {
                        Notify(RemainingTime-0.2f, "Time has run out", "The timer has exceeded the total time.");
                    }
                    else
                    {
                        Notify(RemainingTime, "Time has run out", "The timer has exceeded the total time.");
                    }
                    if (WarningTime - 0.2f > RemainingTime)
                    {
                        Notify(RemainingTime - ((WarningTime-0.2f) / 60), WarningTime.ToString() + " minutes left!", "The timer has " + ((WarningTime - 0.2f) / 60).ToString() + " minutes remaining.");
                    }
                    else if (NotificationWarningsEnabled && RemainingTime > (WarningTime / 60))
                    {
                        Notify(RemainingTime - (WarningTime/60), WarningTime.ToString() + " minutes left!", "The timer has " + (WarningTime / 60).ToString() + " minutes remaining.");
                    }
                    if (NotificationWarningHalfTimeEnabled)
                    {
                        Notify(RemainingTime/2, (RemainingTime/2).ToString() + " minutes left!", "The timer has " + (RemainingTime / 2).ToString() + " minutes remaining.");

                    }
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
            Debug.Log("shouldCancel");
            CancelNotifications();
        }
        if (!NotificationsEnabled && NotificationsEnabledOld)
        {
            CancelNotifications();
        }
        NotificationsEnabledOld = NotificationsEnabled;
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
            if (TimeMarkers[i].GetComponent<AddedTimesScript>().MinutesAdded != 0)
            {
                MKfile(0, i, TimeMarkers[i].GetComponent<AddedTimesScript>().MinutesAdded.ToString(), objs, Prof);
                MKfile(5, i, TimeMarkers[i].GetComponent<AddedTimesScript>().TimeWhenAdded.ToString(), objs, Prof);
            }
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
                        if (float.Parse(RDfile(0, i)) != 0)
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
            catch (Exception e) { Debug.Log(e); }

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
#if UNITY_ANDROID
        AndroidNotificationCenter.CancelAllScheduledNotifications();
#endif
        //resettime = 0.4167f;
        resettime = 100f;
        if (pendingreset || Immeadiate)
        {
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
        /*
        int PowTen = (int)Math.Pow(10, digits);
        string Numstring = number.ToString();
        double Dnum = number * PowTen;
        Dnum = (long)Dnum;
        Dnum /= PowTen;
        Numstring = Dnum.ToString();
        if (Dnum < 10 && Dnum >= 0) { Numstring = "0" + Numstring; }
        return Numstring;*/
        float truncatednum = Truncate(number, digits);

        string formatstring = "F" + digits;
        return truncatednum.ToString(formatstring);
    }
    public string TruncateForSeconds(float number, int digits)
    {
        /* This kinda hurts ngl
        string NumString = number.ToString();
        //float TheFloat = Truncate(number, digits);
        float TheFloat = Mathf.Round(number*Mathf.Pow(10,digits))/Mathf.Pow(10,digits);


        NumString = TheFloat.ToString();
        if (((TheFloat * 10) % 1) < 0.03f && (TheFloat != (long)TheFloat)) { NumString += "0"; Debug.Log("Ah"); }
        if (((TheFloat * 10) % 1) < 0.03f && (TheFloat == (long)TheFloat)) { NumString += ".00"; }
        if (((TheFloat * 10) % 1) > 0.98f) { NumString += "0"; }
        if (TheFloat < 10 && TheFloat >= 0) { NumString = "0" + NumString; }

        return NumString;
        */
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