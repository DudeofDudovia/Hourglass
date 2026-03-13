using System;
using System.IO;
using System.Security.Cryptography;
using TMPro;
using TreeEditor;
using UnityEditor;
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
    //public Toggle ADVFRMTTogg;

    /*public bool ADVFMT = true;
    public void ADVFMTFunc(bool tog)
    {
        ADVFMT = tog;
    }*/
    public bool MSTimer = true;
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

    //tmp
    public bool makefile = false;
    public bool readfile = false;
    public string FileName = "Test.txt";
    public string FileContents = "Hmm";
    public string ObjIndex = "Saves/";


    public int dataind = 0;
    public int Objind = 0;
    public string DataToSave = "erg";



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
        *3:Misc
        *4:Milli
        *5:Background
        *6:Animatino
        *7:LoggedTimes
     *added times will be in Object[0]
     *Things saved with the current save() func will be in Object[1]
    */
    /*public void MKfile(int Objindex,int DataLength, string[] data)
    {
            if (!Directory.Exists(Application.dataPath + @"\Savedata\")) { Directory.CreateDirectory(Application.dataPath + @"\Savedata\"); }
            //if (!File.Exists(Application.dataPath + FileName)) { File.Create(Application.dataPath + @"\Savedata\" + FileName); }

            //File.WriteAllText(Application.dataPath + @"\Savedata\" + FileName, JsonUtility.ToJson(FileContents));
            SaveObject saveobj = new SaveObject();
            saveobj.datas = new string[DataLength];
            for (int i = 0; i < DataLength; i++)
            {
                saveobj.datas[i] = data[i];
            }
            SaveObjectList sol = new SaveObjectList();
            sol.Objects = new SaveObject[Objindex+1];
            sol.Objects[Objindex] = saveobj;
            JsonUtility.ToJson(saveobj);
            Debug.Log(sol);
            File.WriteAllText(Application.dataPath + @"\Savedata\Profile" + Profile.ToString() + ".json", JsonUtility.ToJson(sol)); 

            //JsonUtility.ToJson(FileContents);


    }*/
    public void MKfile(int Objindex, int Dataindex, string Data)
    {
            MKfile(Objindex, Dataindex, Data,-1);
    }
    public void MKfile(int Objindex, int Dataindex, string Data, int MaxData)
    {
        MKfile(Objindex, Dataindex, Data, MaxData , Profile);
    }
    public void MKfile(int Objindex, int Dataindex, string Data, int MaxData, int Prof)
    {
        if (!Directory.Exists(Application.dataPath + @"\Savedata\")) { Directory.CreateDirectory(Application.dataPath + @"\Savedata\"); }

        SaveObjectList sol = new SaveObjectList();
        
        if (File.Exists(Application.dataPath + @"\Savedata\Profile" + Prof.ToString() + ".json")) {
            int ObjLength = JsonUtility.FromJson<SaveObjectList>(File.ReadAllText(Application.dataPath + @"\Savedata\Profile" + Prof.ToString() + ".json")).Objects.Length;
            sol.Objects = new SaveObject[ObjLength];
            if (Objindex+1 >= ObjLength)
            {
                sol.Objects = new SaveObject[Objindex+1];
            }
            
            for (int i = 0; i < sol.Objects.Length; i++)
            {
                SaveObject saveobj = new SaveObject();
                int SaveObjIndex = 1;
                if (i == Objindex && MaxData > 0)
                {
                    saveobj.datas = new string[MaxData];
                }
                else
                {
                    try
                    {
                        SaveObjIndex = JsonUtility.FromJson<SaveObjectList>(File.ReadAllText(Application.dataPath + @"\Savedata\Profile" + Prof.ToString() + ".json")).Objects[i].datas.Length;
                    }
                    catch {  }
                    if (Dataindex + 1  > SaveObjIndex) { SaveObjIndex = Dataindex + 1; }
                    saveobj.datas = new string[SaveObjIndex + 1];
                }
                
                SaveObjIndex = saveobj.datas.Length-1;
                for (int j = 0; j < SaveObjIndex; j++)
                {
                    try
                    {
                        saveobj.datas[j] = JsonUtility.FromJson<SaveObjectList>(File.ReadAllText(Application.dataPath + @"\Savedata\Profile" + Prof.ToString() + ".json")).Objects[i].datas[j];
                    }
                    catch { saveobj.datas[j] = "#"; }
                    
                }
                if (i == Objindex) { saveobj.datas[Dataindex] = Data; } // Debug.Log("Then Why" + i + ":" + Data + ":" + Dataindex); }
                /*if (i == 1 && Objindex == i) //{ Debug.Log("eawsgaseru8i9gaguiolerhgherg"); }
                if (i == 2) { Debug.Log("anything"); sol.Objects[i] = saveobj;  Debug.Log(saveobj); }
                if (i == 2) { Debug.Log("anything at all"); sol.Objects[i] = saveobj; Debug.Log(saveobj); }*/
                sol.Objects[i] = saveobj;
                //Debug.Log(i);
            }
            /*sol.Objects = new SaveObject[Objindex + 1];
            sol.Objects[Objindex] = saveobj;*/

            //File.WriteAllText(Application.dataPath + @"\Savedata\COPYProfile" + Profile.ToString() + ".json", JsonUtility.ToJson(sol));
        }
        else
        {
            SaveObject saveobj = new SaveObject();
            saveobj.datas = new string[Dataindex+1];
            saveobj.datas[Dataindex] = Data;
            sol.Objects = new SaveObject[Objindex + 1];
            sol.Objects[Objindex] = saveobj;
            //JsonUtility.ToJson(saveobj);
            

        }


        File.WriteAllText(Application.dataPath + @"\Savedata\Profile" + Prof.ToString() + ".json", JsonUtility.ToJson(sol));
    }
    public string RDfile(int Objindex, int Dataindex)
    {  
        string read = JsonUtility.FromJson<SaveObjectList>(File.ReadAllText(Application.dataPath + @"\Savedata\Profile" + Profile.ToString() + ".json")).Objects[Objindex].datas[Dataindex];
        return read;
    }
    public string RDfile(int Objindex, int Dataindex,int Prof)
    {
        string read = JsonUtility.FromJson<SaveObjectList>(File.ReadAllText(Application.dataPath + @"\Savedata\Profile" + Prof.ToString() + ".json")).Objects[Objindex].datas[Dataindex];
        return read;
    }
    public bool CheckFile()
    {
        return CheckFile(Profile);
    }
    public bool CheckFile(int Prof)
    {
        if (File.Exists(Application.dataPath + @"\Savedata\Profile" + Prof.ToString() + ".json")) { return true; }
        return false;
    }
    void Update()
    {
        /*
        if (makefile)
        {
            string[] d = new string[1];
            d[0] = "3";
            MKfile(Objind, dataind, DataToSave);
            makefile = false;
        }
        if (readfile)
        {
            //RDfile();
            readfile = false;
        }*/
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
        AddProfDelay -= 1;
        if (AddProfDelay == 0)
        {
            AddProfPtwo(AddProf);
            AddProf = -1;
            AddProfDelay = -1;

        }
    }
    public void SavePlayerPrefs(int Prof)
    {
        Profile = Prof;
        /*if (RunTimer) { PlayerPrefs.SetString("TimerRunning" + Prof.ToString(), TicksWhenTimerStarted.ToString()); }
        else { PlayerPrefs.SetString("TimerRunning" + Prof.ToString(), (-1).ToString()); }*/

        if (RunTimer) { MKfile(1, 0, TicksWhenTimerStarted.ToString(), -1); }
        else { MKfile(1, 0, (-1).ToString(), -1);Debug.Log("Culprit"); }
        //1,0 = TimerRunning

        PlayerPrefs.SetFloat("DefaultTime" + Prof.ToString(), DefaultTime);
        PlayerPrefs.SetFloat("UsedTime" + Prof.ToString(), UsedTime);
        PlayerPrefs.SetFloat("TotalTime" + Prof.ToString(), TotalTime);
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
                MKfile(0, i, TimeMarkers[i].GetComponent<AddedTimesScript>().MinutesAdded.ToString(), objs);
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
        PlayerPrefs.SetInt("Profiles", Profiles);
        //int ADVFRMT = 0;
        //if (ADVFRMTTogg.isOn) {ADVFRMT = 1; }
        //PlayerPrefs.SetInt("AdvancedFormatting",ADVFRMT);

        PlayerPrefs.Save();
        //Load(Prof);
    }
    public void Save(int Prof)
    {
        Profile = Prof;
        /*if (RunTimer) { PlayerPrefs.SetString("TimerRunning" + Prof.ToString(), TicksWhenTimerStarted.ToString()); }
        else { PlayerPrefs.SetString("TimerRunning" + Prof.ToString(), (-1).ToString()); }*/

        if (RunTimer) { MKfile(1, 0, TicksWhenTimerStarted.ToString(), 4, Prof); }
        else { MKfile(1, 0, (-1).ToString(), 4, Prof);  }
        //1,0 = TimerRunning

        MKfile(1, 1, DefaultTime.ToString(), 4, Prof);
        MKfile(1, 2, UsedTime.ToString(), 4, Prof);
        MKfile(1, 3, TotalTime.ToString(), 4,Prof);
        PlayerPrefs.SetFloat("DefaultTime" + Prof.ToString(), DefaultTime);
        PlayerPrefs.SetFloat("UsedTime" + Prof.ToString(), UsedTime);
        PlayerPrefs.SetFloat("TotalTime"    + Prof.ToString(), TotalTime);
        /*int objso = PlayerPrefs.GetInt("MarkersCount" + Prof.ToString(), -1);
        for (int i = 0; i < objso; i++)
        {
            PlayerPrefs.DeleteKey(i.ToString() + ("Marker") + "MinutesAdded" + Prof.ToString());
        }*/
        GameObject[] TimeMarkers = GameObject.FindGameObjectsWithTag("DelonReset");
        int objs = TimeMarkers.Length;
        //PlayerPrefs.SetInt("MarkersCount" + Prof.ToString(), objs);
        for (int i = 0; i < objs; i++)
        {
            if (TimeMarkers[i].GetComponent<AddedTimesScript>().MinutesAdded != 0)
            {
                MKfile(0,i, TimeMarkers[i].GetComponent<AddedTimesScript>().MinutesAdded.ToString(),objs);
                //PlayerPrefs.SetFloat(i.ToString() + ("Marker") + "MinutesAdded" + Prof.ToString(), TimeMarkers[i].GetComponent<AddedTimesScript>().MinutesAdded);
            }
            //Debug.Log(TimeMarkers[i].GetComponent<AddedTimesScript>().gameObject.name);
            //if (PlayerPrefs.HasKey((i + 1).ToString() + ("Marker") + "MinutesAdded" + Prof.ToString())) { PlayerPrefs.DeleteKey((i + 1).ToString() + ("Marker") + "MinutesAdded" + Prof.ToString()); }
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
        
        //PlayerPrefs.Save();
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
        if (CheckFile(Prof))
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
                int objs = JsonUtility.FromJson<SaveObjectList>(File.ReadAllText(Application.dataPath + @"\Savedata\Profile" + Profile.ToString() + ".json")).Objects[0].datas.Length;

                for (int i = 0; i < objs; i++)
                {
                    //ContentView.GetComponent<TimesAdded>().TimesAppeneded += 1;
                    try
                    {

                        Instantiate(TimetoAdd, new Vector3(float.Parse(RDfile(0, i)), transform.position.y, 0), transform.rotation, ContentView.transform);
                    }

                    catch
                    {

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
            long timerrunning = -1;
            try { 
                timerrunning = long.Parse(RDfile(1, 0));
            }
            catch {  }
            
            if (timerrunning >= 0) { RunTimer = true; WasRunTimer = true; TicksWhenTimerStarted = timerrunning; }
            else
            {
                RunTimer = false; WasRunTimer = false;
            }
            ProfileName = PlayerPrefs.GetString("Profile" + Prof.ToString() + "Name", "Profile " + Prof);
            Profile = Prof;
        }
    }
    public void LoadPlayerPrefs(int Prof, bool Internal)
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
                    //Instantiate(TimetoAdd, new Vector3(PlayerPrefs.GetFloat(i.ToString() + ("Marker") + "MinutesAdded" + Prof.ToString(), -2), transform.position.y, 0), transform.rotation, ContentView.transform);
                    Instantiate(TimetoAdd, new Vector3(float.Parse(RDfile(0, i)), transform.position.y, 0), transform.rotation, ContentView.transform);

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
