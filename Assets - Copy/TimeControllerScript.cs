using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class TimeControllerScript : MonoBehaviour
{
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
    public bool DEBUGLOAD = false;
    public bool DEDELCURRENTPROFILE = false;
    public bool DEBUGCLEARPLAYERPREFS = false;
    public string ProfileName = "Profile";
    public TMP_InputField ProfileNameSetter;
    public Toggle ADVFRMTTogg;
    public int AddProf;
    public int AddProfDelay;

    public GameObject ResetApp;
    public bool pendingappreset = false;
    public bool pendingtimerappreset = false;
    public float appresettime = 0.4167f;
    public bool CountUp = false;
    public bool ResetReset;

    public bool MSTimer = true;
    public bool MSAddeds = false;
    public bool MSLeftDisplay = false;
    public bool MSUsedDisplay = false;

    public bool KeepTimeInAddBox = false;
    void Awake()
    {
        Profile = PlayerPrefs.GetInt("Profile", 0);
        Load(Profile);

        if (PlayerPrefs.HasKey("TCS.KeepTimeInAddBoxTime"))
        {
            int KLAT = PlayerPrefs.GetInt("TCS.KeepTimeInAddBoxTime");
            if (KLAT == 1) { KeepTimeInAddBox = true; }
            if (KLAT == 0) { KeepTimeInAddBox = false; }

        }
        else { KeepTimeInAddBox = false; }
    }
    void Update()
    {
        Application.targetFrameRate = (int)Screen.currentResolution.refreshRateRatio.value;
        if (ResetReset)
        {
            pendingreset = false;
            pendingtimerappreset = false;
            pendingtimerreset = false;
            pendingappreset = false;
        }
        ResetReset = false;
        if (Input.GetMouseButton(0) && (pendingreset || pendingtimerappreset || pendingtimerreset || pendingappreset)) 
        { 
        //    ResetReset = true;
        }

        if (DEBUGCLEARPLAYERPREFS)
        {
            DEBUGCLEARPLAYERPREFS = false;
            PlayerPrefs.DeleteAll();
        }
        viewCurrentTick = CurrentTick;
        RemainingTime = TotalTime - UsedTime;
        // (RemainingTime < 0) { RemainingTime = 0; }
        //if (RunTimer) { UsedTime += Time.deltaTime / TimeScale; }
        //if (UsedTime > TotalTime) { UsedTime = TotalTime; }
        if (pendingreset)
        {
            resettime -= Time.deltaTime / TimeScale;
            if (resettime <= 0) { pendingreset = false; }
        }
        if (pendingtimerreset)
        {
            timerresettime -= Time.deltaTime / TimeScale;
            if (timerresettime <= 0) { pendingtimerreset = false; }
        }
        if (pendingappreset)
        {
            appresettime -= Time.deltaTime / TimeScale;
            if (appresettime <= 0) { pendingappreset = false; }
        }
        if (primesave)
        {
            Save(Profile);
            primesave = false;
        }
        if (UsedTime != UsedTimeLastTick)
        {
            primesave = true;
        }
        //Debug.Log((double)(CurrentTick - TicksWhenTimerStarted) / 600000000);
        RunningTime = (float)(CurrentTick - TicksWhenTimerStarted) / 600000000;
        if (!RunTimer) { RunningTime = 0; }
        UsedTimeLastTick = UsedTime;
        if (RunTimer && !WasRunTimer) { 
            WasRunTimer = true; 
            TicksWhenTimerStarted = CurrentTick;
            PlayerPrefs.SetString("TimerRunning" + Profile.ToString(), TicksWhenTimerStarted.ToString());
            PlayerPrefs.SetInt("IsTimerRunning" + Profile.ToString(), 1);
            Save(Profile);
            PlayerPrefs.Save();
        }
        if (!RunTimer && WasRunTimer)
        {
            PlayerPrefs.SetInt("IsTimerRunning" + Profile.ToString(), -1);
            PlayerPrefs.Save();
            UsedTime += (float)(CurrentTick - TicksWhenTimerStarted) / 600000000;
            //ContentView.GetComponent<TimesAdded>().TimesAppeneded += 1;
            Instantiate(TimetoAdd, new Vector3((float)(CurrentTick - TicksWhenTimerStarted) / 600000000, transform.position.y, 0), transform.rotation, ContentView.transform);
            WasRunTimer = false;
        }
        if (DEBUGLOAD)
        {
            Load(Profile);
            DEBUGLOAD = false;
        }
        if (DEDELCURRENTPROFILE)
        {
            DelProf(Profile);
            DEDELCURRENTPROFILE = false;
        }
        AddProfDelay -= 1;
        if (AddProfDelay == 0)
        {
            AddProfPtwo(AddProf);
            AddProf = -1;
            AddProfDelay = -1;

        }
    }
    public void Save(int Prof)
    {
        Profile = Prof;
        if (RunTimer) { PlayerPrefs.SetString("TimerRunning" + Prof.ToString(), TicksWhenTimerStarted.ToString()); }
        else { PlayerPrefs.SetString("TimerRunning" + Prof.ToString(), (-1).ToString()); }

        PlayerPrefs.SetFloat("DefaultTime" + Prof.ToString(), DefaultTime);
        PlayerPrefs.SetFloat("UsedTime" + Prof.ToString(), UsedTime);
        PlayerPrefs.SetFloat("TotalTime"    + Prof.ToString(), TotalTime);
        int objso = PlayerPrefs.GetInt("MarkersCount" + Prof.ToString(), -1);
        for (int i = 0; i < objso; i++)
        {
            PlayerPrefs.DeleteKey(i.ToString() + ("Marker") + "MinutesAdded" + Prof.ToString());
        }
        GameObject[] TimeMarkers = GameObject.FindGameObjectsWithTag("DelonReset");
        int objs = TimeMarkers.Length;
        PlayerPrefs.SetInt("MarkersCount" + Prof.ToString(), objs);
        for (int i = 0; i < objs; i++)
        {
            if (TimeMarkers[i].GetComponent<AddedTimesScript>().MinutesAdded != 0)
            {
                PlayerPrefs.SetFloat(i.ToString() + ("Marker") + "MinutesAdded" + Prof.ToString(), TimeMarkers[i].GetComponent<AddedTimesScript>().MinutesAdded);
            }
            //Debug.Log(TimeMarkers[i].GetComponent<AddedTimesScript>().gameObject.name);
            if (PlayerPrefs.HasKey((i + 1).ToString() + ("Marker") + "MinutesAdded" + Prof.ToString())) { PlayerPrefs.DeleteKey((i + 1).ToString() + ("Marker") + "MinutesAdded" + Prof.ToString()); }
        }
        PlayerPrefs.SetInt("Profile" + Prof.ToString(), 1);
        PlayerPrefs.SetString("Profile" + Prof.ToString() + "Name", ProfileName);
        PlayerPrefs.SetInt("Profile", Profile);
        bool EndofProfiles = false;
        while (!EndofProfiles)
        {
            int DoesProf = PlayerPrefs.GetInt("Profile" + Profiles, -1);
            if (DoesProf == -1) { EndofProfiles = true; }
            else { Profiles += 1; }
        }
        PlayerPrefs.SetInt("Profiles",Profiles);
        //int ADVFRMT = 0;
        //if (ADVFRMTTogg.isOn) {ADVFRMT = 1; }
        //PlayerPrefs.SetInt("AdvancedFormatting",ADVFRMT);

        PlayerPrefs.Save();
        //Load(Prof);
    }
    public void Load() { 
    Load(Profile,false);
    }
    public void Load(int Prof)
    {
        Load(Prof, false);
    }
    public void Load(int Prof, bool Internal)
    {
        int DoesThisProf = PlayerPrefs.GetInt("Profile" + Prof, -1);
        //if (DoesThisProf == -1 && Initiate) { Save(Prof); }
        //if (DoesThisProf == -1 && Initiate) { Save(Prof); }
        Profiles = 0;
        ProfilesView.GetComponent<TimesAdded>().TimesAppeneded = 0;
        bool EndofProfiles = false;
        while (!EndofProfiles)
        {
            int DoesProf = PlayerPrefs.GetInt("Profile" + Profiles, -1);
            if (DoesProf == -1) { EndofProfiles = true; }
            else { Profiles += 1; }
        }
        if (!Internal)
        {


            GameObject[] DeleteProf = GameObject.FindGameObjectsWithTag("ProfileDelete");
            foreach (GameObject Obj in DeleteProf)
            {
                Destroy(Obj);
            }
            int objsProf = Profiles;
            for (int i = 0; i < objsProf; i++)
            {
                //ContentView.GetComponent<TimesAdded>().TimesAppeneded += 1;
                Instantiate(ProfileToAdd, new Vector3(0, transform.position.y, 0), transform.rotation, ProfilesView.transform);
            }



            GameObject[] Delete = GameObject.FindGameObjectsWithTag("DelonReset");
            foreach (GameObject Obj in Delete)
            {
                Destroy(Obj);
            }

            int objs = PlayerPrefs.GetInt("MarkersCount" + Prof.ToString(), 0);

            for (int i = 0; i < objs; i++)
            {
                //ContentView.GetComponent<TimesAdded>().TimesAppeneded += 1;
                if (!(PlayerPrefs.GetFloat(i.ToString() + ("Marker") + "MinutesAdded" + Prof.ToString(), -2) == -2) && !(PlayerPrefs.GetFloat(i.ToString() + ("Marker") + "MinutesAdded" + Prof.ToString(), -2) == 0))
                {
                    Instantiate(TimetoAdd, new Vector3(PlayerPrefs.GetFloat(i.ToString() + ("Marker") + "MinutesAdded" + Prof.ToString(), -2), transform.position.y, 0), transform.rotation, ContentView.transform);
                    //Debug.Log((PlayerPrefs.GetFloat(i.ToString() + ("Marker") + "MinutesAdded" + Prof.ToString(), -2)));
                }
                //Instantiate(TimetoAdd, new Vector3(PlayerPrefs.GetFloat(i.ToString() + ("Marker") + "MinutesAdded" + Prof.ToString(), -2), transform.position.y, 0), transform.rotation, ContentView.transform);
                //Debug.Log((PlayerPrefs.GetFloat(i.ToString() + ("Marker") + "MinutesAdded" + Prof.ToString(), 0)));

                //Debug.Log((PlayerPrefs.GetFloat(i.ToString() + ("Marker") + "MinutesAdded" + Prof.ToString(), -2))==0);
            }
        }

        UsedTime = PlayerPrefs.GetFloat("UsedTime" + Prof.ToString(), 0);
        TotalTime = PlayerPrefs.GetFloat("TotalTime" + Prof.ToString(), DefaultTime);
        DefaultTime = PlayerPrefs.GetFloat("DefaultTime" + Prof.ToString(), DefaultTime);
        ContentView.GetComponent<TimesAdded>().TimesAppeneded = 0;


        long timerrunning = long.Parse(PlayerPrefs.GetString("TimerRunning" + Prof.ToString(), CurrentTick.ToString()));
        int istimerrunning = PlayerPrefs.GetInt("IsTimerRunning" + Prof.ToString(), -1);
        if (istimerrunning == 1) { RunTimer = true; WasRunTimer = true; TicksWhenTimerStarted = timerrunning; }
        else
        {
            RunTimer = false; WasRunTimer = false;
        }
        ProfileName = PlayerPrefs.GetString("Profile" + Prof.ToString() + "Name", "Profile " + Prof);
        Profile = Prof;
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
        int HighestProf = Profiles-1;
        if (HighestProf == 0)
        {
            pendingreset = true;
            ResetTime(DefaultTime);
            ProfileName = ProfileNameSetter.text;
            if (ProfileName == "") { ProfileName = "Profile 1"; }
            Save(0);
        }
        else if (Prof == HighestProf)
        {
            DeleteProf(Prof);
        }
        else
        {
            //1. load Prof above() to be Deleted()
            //2. Save Prof above to prof to be deleted, thereby deleting it prof to be deled
            //3. Repeat until highest prof is reached



            int NumAbove = HighestProf - Prof;
            for (int i = 0; i < NumAbove; i++)
            {
                int NewProf = Prof + i;
                
                //Load(Prof + i+1,false);
                Load(NewProf+1,true); //1., load prof above
                /*Debug.Log(("Prof = " + Prof));
                Debug.Log(("NewProf = " + NewProf));
                Debug.Log(("NewProf+1 = " + NewProf+1));*/
                if (RunTimer) { PlayerPrefs.SetString("TimerRunning" + NewProf.ToString(), TicksWhenTimerStarted.ToString()); }
                else { PlayerPrefs.SetString("TimerRunning" + NewProf.ToString(), (-1).ToString()); }

                PlayerPrefs.SetFloat("DefaultTime" + NewProf.ToString(), DefaultTime);
                PlayerPrefs.SetFloat("UsedTime" + NewProf.ToString(), UsedTime);
                PlayerPrefs.SetFloat("TotalTime" + NewProf.ToString(), TotalTime);

                /*
                GameObject[] TimeMarkers = GameObject.FindGameObjectsWithTag("DelonReset");
                int objs = TimeMarkers.Length;
                PlayerPrefs.SetInt("MarkersCount" + NewProf.ToString(), objs);
                for (int k = 0; k < objs; k++)
                {
                    Debug.Log((k.ToString() + ("Marker") + "MinutesAdded" + NewProf.ToString(), TimeMarkers[k].GetComponent<AddedTimesScript>().MinutesAdded));
                    PlayerPrefs.SetFloat(k.ToString() + ("Marker") + "MinutesAdded" + NewProf.ToString(), TimeMarkers[k].GetComponent<AddedTimesScript>().MinutesAdded);
                    if (PlayerPrefs.HasKey((k + 1).ToString() + ("Marker") + "MinutesAdded" + NewProf.ToString())) { PlayerPrefs.DeleteKey((k + 1).ToString() + ("Marker") + "MinutesAdded" + NewProf.ToString()); }
                    
                }*/


                int objs = PlayerPrefs.GetInt("MarkersCount" + (NewProf+1).ToString(), 0);
                PlayerPrefs.SetInt("MarkersCount" + NewProf.ToString(), objs);
                for (int j = 0; j < objs; j++)
                {
                    //ContentView.GetComponent<TimesAdded>().TimesAppeneded += 1;
                    if (!(PlayerPrefs.GetFloat(j.ToString() + ("Marker") + "MinutesAdded" + Prof.ToString(), -2) == -2) && !(PlayerPrefs.GetFloat(j.ToString() + ("Marker") + "MinutesAdded" + NewProf.ToString(), -2) == 0))
                    {
                       // Instantiate(TimetoAdd, new Vector3(PlayerPrefs.GetFloat(j.ToString() + ("Marker") + "MinutesAdded" + NewProf.ToString(), -2), transform.position.y, 0), transform.rotation, ContentView.transform);

                        //Debug.Log(PlayerPrefs.GetFloat(j.ToString() + ("Marker") + "MinutesAdded" + NewProf.ToString(), PlayerPrefs.GetFloat(j.ToString() + ("Marker") + "MinutesAdded" + NewProf.ToString(), -2)));
                        PlayerPrefs.SetFloat(j.ToString() + ("Marker") + "MinutesAdded" + NewProf.ToString(), PlayerPrefs.GetFloat(j.ToString() + ("Marker") + "MinutesAdded" + (NewProf+1).ToString(), -2));
                        //if (PlayerPrefs.HasKey((k + 1).ToString() + ("Marker") + "MinutesAdded" + NewProf.ToString())) { PlayerPrefs.DeleteKey((k + 1).ToString() + ("Marker") + "MinutesAdded" + NewProf.ToString()); }



                    }

                    //Debug.Log((k.ToString() + ("Marker") + "MinutesAdded" + NewProf.ToString(), TimeMarkers[k].GetComponent<AddedTimesScript>().MinutesAdded));
                    //PlayerPrefs.SetFloat(k.ToString() + ("Marker") + "MinutesAdded" + NewProf.ToString(), TimeMarkers[k].GetComponent<AddedTimesScript>().MinutesAdded);
                 //   if (PlayerPrefs.HasKey((k + 1).ToString() + ("Marker") + "MinutesAdded" + NewProf.ToString())) { PlayerPrefs.DeleteKey((k + 1).ToString() + ("Marker") + "MinutesAdded" + NewProf.ToString()); }




                    //Instantiate(TimetoAdd, new Vector3(PlayerPrefs.GetFloat(i.ToString() + ("Marker") + "MinutesAdded" + Prof.ToString(), -2), transform.position.y, 0), transform.rotation, ContentView.transform);
                    //Debug.Log((PlayerPrefs.GetFloat(i.ToString() + ("Marker") + "MinutesAdded" + Prof.ToString(), 0)));

                    //Debug.Log((PlayerPrefs.GetFloat(i.ToString() + ("Marker") + "MinutesAdded" + Prof.ToString(), -2))==0);
                }








                // GameObject[] TimeMarkers = GameObject.FindGameObjectsWithTag("DelonReset");
                //int objs = TimeMarkers.Length;
                //PlayerPrefs.SetInt("MarkersCount" + NewProf.ToString(), objs);

                PlayerPrefs.SetInt("Profile" + NewProf, 1);
                PlayerPrefs.SetString("Profile" + NewProf.ToString() + "Name", ProfileName);
            }
            DeleteProf(HighestProf);
            
        }
        PlayerPrefs.Save();
        Profile = 0;
        Load(Profile);
    }
    public void DeleteProf(int Prof)
    {
        PlayerPrefs.DeleteKey("TimerRunning" + Prof.ToString());
        PlayerPrefs.DeleteKey("DefaultTime" + Prof.ToString());
        PlayerPrefs.DeleteKey("UsedTime" + Prof.ToString());
        PlayerPrefs.DeleteKey("TotalTime" + Prof.ToString());
        int objs = PlayerPrefs.GetInt("MarkersCount" + Prof.ToString(), 0);
        PlayerPrefs.DeleteKey("MarkersCount" + Prof.ToString());
        for (int i = 0; i < objs; i++)
        {
            PlayerPrefs.DeleteKey(i.ToString() + ("Marker") + "MinutesAdded" + Prof.ToString());
        }
        PlayerPrefs.DeleteKey("Profile" + Prof.ToString());
        PlayerPrefs.DeleteKey("Profile" + Prof.ToString() + "Name");
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
        Profile = NewProfile;
        pendingreset = true;
        ResetTime();
        AddProf = NewProfile;
        AddProfDelay = 50;

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
        resettime = 0.4167f;
        if (pendingreset)
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

            //
           // Save(Profile);
            return;
        }

        pendingreset = true;
    }
    public void ResetTime()
    {
        ResetTime(DefaultTime);
        
    }
    public void ResetTimer()
    {
        timerresettime = 0.4167f;
        if (pendingtimerreset)
        {
            TicksWhenTimerStarted = CurrentTick;
            RunTimer = false;
            WasRunTimer = false;
            PlayerPrefs.SetInt("IsTimerRunning" + Profile.ToString(), -1);
            return;
        }

        pendingtimerreset = true;
    }

    public void AppReset()
    {
        appresettime = 0.2167f;
        if (pendingappreset)
        {
            pendingappreset = false;
            pendingreset = true;
            PlayerPrefs.DeleteAll();
            ResetTime();
            Load(0);
            Save(0);
            return;
        }

        pendingappreset = true;
    }

    public float Truncate(float number, int digits)
    {
        number *= Mathf.Pow(10, digits);
        number = (long)number;
        //number = (float)number;
        number /= Mathf.Pow(10, digits);
        return number;
    }
    public string TruncateFS(float number, int digits)
    {
        string Numstring = number.ToString();
        number *= Mathf.Pow(10, digits);
        number = (long)number;
        //number = (float)number;
        number /= Mathf.Pow(10, digits);
        Numstring = number.ToString();
        if (number < 10 && number >= 0) { Numstring = "0" + Numstring; }
        return Numstring;
    }
    public string TruncateForSeconds(float number, int digits)
    {
        
        string NumString = number.ToString();
        float TheFloat = Truncate(number, digits);
        
        NumString = TheFloat.ToString();
        if (((TheFloat * 10) % 1) < 0.03f && (TheFloat != (long)TheFloat)) { NumString += "0"; }
        if (((TheFloat * 10) % 1) < 0.03f && (TheFloat == (long)TheFloat)) { NumString += ".00"; }
        if (((TheFloat * 10) % 1) > 0.98f ) { NumString += "0"; }
        if (TheFloat < 10 && TheFloat >= 0) { NumString = "0" + NumString; }
        return NumString;
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
    }

    
}
