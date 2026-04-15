using System;
using System.IO;
using System.Linq.Expressions;
using System.Security.Cryptography;
using TMPro;
using TreeEditor;
using UnityEditor;
using UnityEditor.Rendering;
using UnityEditor.U2D.Aseprite;
using UnityEngine;
using UnityEngine.Diagnostics;
using UnityEngine.Events;
using UnityEngine.UI;
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


    void Awake()
    {
        Profile = int.Parse(RDfile(0,0,true));
        if (Profile == -792) { Profile = 0; }
        Load(Profile);
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
     *added times will be in Object[0]
     *Things saved with the current save() func will be in Object[1]
     *Settings[0]:CurrentProfile[0-1] Countup[2] & Misc[3-5] & Animation[6-8] & all Other SuperProfile Settings[8+]
     */

    public void MKfile(int Objindex, int Dataindex, string Data)
    {
            MKfile(Objindex, Dataindex, Data,-1);
    }
    public void MKfile(int Objindex, int Dataindex, string Data, bool Default)
    {
        MKfile(Objindex, Dataindex, Data, -1, Default);
    }
    public void MKfile(int Objindex, int Dataindex, string Data, int MaxData)
    {
        MKfile(Objindex, Dataindex, Data, MaxData , Profile);
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
        if (!Directory.Exists(Application.dataPath + @"\Savedata\")) { Directory.CreateDirectory(Application.dataPath + @"\Savedata\"); }

        SaveObjectList sol = new SaveObjectList();
        string FilePath = (@"\Savedata\Profile" + Prof.ToString() + ".json");
        if (Default) { FilePath = (@"\Savedata\Settings.json"); }

        if (File.Exists(Application.dataPath + FilePath))
            {
                int ObjLength = JsonUtility.FromJson<SaveObjectList>(File.ReadAllText(Application.dataPath + FilePath)).Objects.Length;
                sol.Objects = new SaveObject[ObjLength];
                if (Objindex + 1 >= ObjLength)
                {
                    sol.Objects = new SaveObject[Objindex + 1];
                }
                for (int i = 0; i < sol.Objects.Length; i++)
                {
                SaveObject saveobj = new SaveObject();
                int SaveObjIndex = 1;
                try
                {
                    SaveObjIndex = JsonUtility.FromJson<SaveObjectList>(File.ReadAllText(Application.dataPath + FilePath)).Objects[i].datas.Length;
                }
                catch { }
                saveobj.datas = new string[SaveObjIndex];

                SaveObjIndex = saveobj.datas.Length;
                for (int j = 0; j < SaveObjIndex; j++)
                {
                    try
                    {
                        saveobj.datas[j] = JsonUtility.FromJson<SaveObjectList>(File.ReadAllText(Application.dataPath + FilePath)).Objects[i].datas[j];
                    }
                    catch (Exception e4) { saveobj.datas[j] = "#"; Debug.Log(e4); }

                }
                try
                {
                    if (i == Objindex)
                    {
                        saveobj.datas[Dataindex] = Data;
                    }
                }
                catch (Exception e)
                {
                    if (DebugOutput)
                    {
                        Debug.Log(e);
                    }
                    saveobj = new SaveObject();
                    SaveObjIndex = 1;
                    if (i == Objindex && MaxData > 0)
                    {
                        saveobj.datas = new string[MaxData + 1];
                    }
                    else
                    {
                        try
                        {
                            SaveObjIndex = JsonUtility.FromJson<SaveObjectList>(File.ReadAllText(Application.dataPath + FilePath)).Objects[i].datas.Length;
                        }
                        catch { }
                        if (Dataindex + 1 > SaveObjIndex && i == Objindex) { SaveObjIndex = Dataindex + 1; }
                        saveobj.datas = new string[SaveObjIndex + 1];
                    }

                    SaveObjIndex = saveobj.datas.Length;
                    if (Default && Objindex == 0) { Debug.Log(Dataindex); }
                    for (int j = 0; j < SaveObjIndex; j++)
                    {
                        try
                        {
                            saveobj.datas[j] = JsonUtility.FromJson<SaveObjectList>(File.ReadAllText(Application.dataPath + FilePath)).Objects[i].datas[j];
                        }
                        catch (Exception e2)
                        {

                            saveobj.datas[j] = "#";
                            if (DebugOutput)
                            {
                                Debug.Log(e2); 
                            }
                        }

                    }
                    if (i == Objindex) { saveobj.datas[Dataindex] = Data; }
                }

                if (i == Objindex && MaxData > 0)
                    {
                        SaveObject saveobjtwo = new SaveObject();
                        saveobjtwo.datas = new string[MaxData];
                        for (int j = 0; j < saveobjtwo.datas.Length; j++)
                        {
                            try
                            {
                                saveobjtwo.datas[j] = saveobj.datas[j];
                        }
                            catch (Exception e3) {
                            if (DebugOutput)
                            {
                                Debug.Log(e3);
                            }
                        }

                    }
                    if (i == Objindex && MaxData > 0)
                    {
                        saveobj.datas = new string[MaxData];
                        if (Dataindex + 1 > MaxData && i == Objindex) { saveobj.datas = new string[Dataindex + 1]; }
                    }
                    sol.Objects[i] = saveobjtwo;
                }
                else
                {
                    sol.Objects[i] = saveobj;
                }
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
        File.WriteAllText(Application.dataPath + FilePath, JsonUtility.ToJson(sol));
    }
   
    public string RDfile(int Objindex, int Dataindex)
    {  
        return RDfile(Objindex,Dataindex,Profile);
    }
    public string RDfile(int Objindex, int Dataindex,bool Default)
    {
        return RDfile(Objindex, Dataindex, Profile,Default);
    }
    public string RDfile(int Objindex, int Dataindex,int Prof)
    {
        return RDfile(Objindex, Dataindex, Prof,false);
    }
    public string RDfile(int Objindex, int Dataindex, int Prof, bool Default)
    {
        string read = "-792";
        string FilePath = (@"\Savedata\Profile" + Prof.ToString() + ".json");
        if (Default) { FilePath = (@"\Savedata\Settings.json"); }
        try
        {
            read = JsonUtility.FromJson<SaveObjectList>(File.ReadAllText(Application.dataPath + FilePath)).Objects[Objindex].datas[Dataindex];
            if (Default) { read = JsonUtility.FromJson<SaveObjectList>(File.ReadAllText(Application.dataPath + FilePath)).Objects[Objindex].datas[Dataindex]; }
        }
        catch (Exception e) {
            if (DebugOutput && Default) {
                Debug.LogError(e);
                Debug.Log(Objindex); 
                Debug.Log(Dataindex);
                Debug.Log(JsonUtility.FromJson<SaveObjectList>(File.ReadAllText(Application.dataPath + FilePath)).Objects[Objindex].datas.Length);
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
        if (File.Exists(Application.dataPath + @"\Savedata\Profile" + Prof.ToString() + ".json")) { return true; }
        return false;
    }
    void Update()
    {
        FillProfileButtonCollection();
        Application.targetFrameRate = (int)Screen.currentResolution.refreshRateRatio.value;
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
        RunningTime = (float)(CurrentTick - TicksWhenTimerStarted) / 600000000;
        if (!RunTimer) { RunningTime = 0; }
        UsedTimeLastTick = UsedTime;
        if (RunTimer && !WasRunTimer) { 
            WasRunTimer = true; 
            TicksWhenTimerStarted = CurrentTick;
            Save(Profile);
        }
        if (!RunTimer && WasRunTimer)
        {
            UsedTime += (float)(CurrentTick - TicksWhenTimerStarted) / 600000000;
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
        if (ResetValuesTimer == 0)
        {
            ResetValues = false;
        }
        ResetValuesTimer -= 1;
    }
    public void Save(int Prof)
    {
        int Profs = 0;
        try
        {
            Profs = JsonUtility.FromJson<SaveObjectList>(File.ReadAllText(Application.dataPath + @"\Savedata\Settings.json")).Objects[1].datas.Length;

        }
        catch
        {
            Profs = 1;
        }
        Profile = Prof;
        if (Prof > Profs - 1) {  }
        if (RunTimer) { MKfile(1, 0, TicksWhenTimerStarted.ToString(), -1, Prof); }
        else { MKfile(1, 0, (-1).ToString(), -1, Prof);  }
        MKfile(1, 1, DefaultTime.ToString(), -1, Prof);
        MKfile(1, 2, UsedTime.ToString(), -1, Prof);
        MKfile(1, 3, TotalTime.ToString(), -1,Prof);
        GameObject[] TimeMarkers = GameObject.FindGameObjectsWithTag("DelonReset");
        int objs = TimeMarkers.Length;
        //PlayerPrefs.SetInt("MarkersCount" + Prof.ToString(), objs);
        for (int i = 0; i < objs; i++)
        {
            if (TimeMarkers[i].GetComponent<AddedTimesScript>().MinutesAdded != 0)
            {
                MKfile(0,i, TimeMarkers[i].GetComponent<AddedTimesScript>().MinutesAdded.ToString(),objs);
            }
        }
        if (objs == 0)
        {
            MKfile(0, 0, 0.ToString(), 0,Prof);
        }
        MKfile(0, 0, Prof.ToString(), true);
    }
    public UnityEvent LoadCalled;
    public void Load() { 
    Load(Profile,false);
    }
    public void Load(int Prof)
    {
        Load(Prof, false);
    }
    public void Load(int Prof, bool Internal)
    {
        LoadCalled.Invoke();
        int Profs = 0;
        try
        {
            Profs = JsonUtility.FromJson<SaveObjectList>(File.ReadAllText(Application.dataPath + @"\Savedata\Settings.json")).Objects[1].datas.Length;
            
        }
        catch
        {
            Profs = 1;
        }
        Profiles = Profs;
        ProfilesView.GetComponent<TimesAdded>().TimesAppeneded = 0;
        if (CheckFile(Prof))
        {
            if (!Internal)
            {

                /*
                GameObject[] DeleteProf = GameObject.FindGameObjectsWithTag("ProfileDelete");

                
                foreach (GameObject Obj in DeleteProf)
                {
                    Debug.Log(Obj.name);
                    Destroy(Obj);
                }
                */
                foreach (GameObject Obj in ProfileButtonCollection)
                {
                    Destroy(Obj);
                    //ProfileButtonCollection = new GameObject[1];
                }

                int objsProf = Profiles;

                ProfileButtonCollection = new GameObject[objsProf];
                for (int i = 0; i < objsProf; i++)
                {
                    //ContentView.GetComponent<TimesAdded>().TimesAppeneded += 1;
                     ProfileButtonCollection[i] = Instantiate(ProfileToAdd, new Vector3(0, transform.position.y, 0), transform.rotation, ProfilesView.transform);
                     
                    
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
                        /*if (objs == 1) { Debug.Log("HALT");
                            GameObject[] LastOne = GameObject.FindGameObjectsWithTag("BeingRemoved");
                            Debug.Log(LastOne.Length);
                            if (LastOne.Length == 1) { continue; }
                        }*/
                        if (float.Parse(RDfile(0, i)) != 0)
                        {
                            Instantiate(TimetoAdd, new Vector3(float.Parse(RDfile(0, i)), transform.position.y, 0), transform.rotation, ContentView.transform);
                        }
                        
                    }

                    catch
                    {

                    }
                    //Instantiate(TimetoAdd, new Vector3(PlayerPrefs.GetFloat(i.ToString() + ("Marker") + "MinutesAdded" + Prof.ToString(), -2), transform.position.y, 0), transform.rotation, ContentView.transform);
                    //Debug.Log((PlayerPrefs.GetFloat(i.ToString() + ("Marker") + "MinutesAdded" + Prof.ToString(), 0)));

                    //Debug.Log((PlayerPrefs.GetFloat(i.ToString() + ("Marker") + "MinutesAdded" + Prof.ToString(), -2))==0);
                }
            }

            DefaultTime = float.Parse(RDfile(1, 1));
            if (DefaultTime == -792) { DefaultTime = 180; }
            UsedTime = float.Parse(RDfile(1, 2));
            if (UsedTime == -792) { UsedTime = 1; }
            TotalTime = float.Parse(RDfile(1, 3));
            if ( TotalTime == -792) { TotalTime = 180; }
            ContentView.GetComponent<TimesAdded>().TimesAppeneded = 0;
            try
            {
                int CountUpTog = int.Parse(RDfile(0, 0));
                if (CountUpTog == 1) { CountUp = true; }
                if (CountUpTog == 0) { CountUp = false; }
            }
            catch { CountUp = false; }

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
            //ProfileName = PlayerPrefs.GetString("Profile" + Prof.ToString() + "Name", "Profile " + Prof);
            ProfileName = RDfile(1, Prof, true);
            string ProfName = ("Profile" + Prof.ToString()).ToString();
            try {if (int.Parse(RDfile(1, Prof, true)) == -792) { ProfileName = ProfName; } }
            catch { }
            
            Profile = Prof;
            MKfile(0, 0, Prof.ToString(), true);
            //Debug.Log(Profs);
            //Debug.Log(Prof);
            MKfile(1, Prof, ProfileName, true);
        }
        else
        {
            ResetTime(DefaultTime,true);
            string ProfName = ("Profile" + (Prof+1).ToString()).ToString();
            if (int.Parse(RDfile(1, Prof, true)) == -792) { ProfileName = ProfName; }
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
        /*
        int HighestProf = Profiles - 1;
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
                Load(NewProf + 1, true); //1., load prof above
                /*Debug.Log(("Prof = " + Prof));
                Debug.Log(("NewProf = " + NewProf));
                Debug.Log(("NewProf+1 = " + NewProf+1)); /*(TURN IT AROUND)
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
                    
                }


                int objs = PlayerPrefs.GetInt("MarkersCount" + (NewProf + 1).ToString(), 0);
                PlayerPrefs.SetInt("MarkersCount" + NewProf.ToString(), objs);
                for (int j = 0; j < objs; j++)
                {
                    //ContentView.GetComponent<TimesAdded>().TimesAppeneded += 1;
                    if (!(PlayerPrefs.GetFloat(j.ToString() + ("Marker") + "MinutesAdded" + Prof.ToString(), -2) == -2) && !(PlayerPrefs.GetFloat(j.ToString() + ("Marker") + "MinutesAdded" + NewProf.ToString(), -2) == 0))
                    {
                        // Instantiate(TimetoAdd, new Vector3(PlayerPrefs.GetFloat(j.ToString() + ("Marker") + "MinutesAdded" + NewProf.ToString(), -2), transform.position.y, 0), transform.rotation, ContentView.transform);

                        //Debug.Log(PlayerPrefs.GetFloat(j.ToString() + ("Marker") + "MinutesAdded" + NewProf.ToString(), PlayerPrefs.GetFloat(j.ToString() + ("Marker") + "MinutesAdded" + NewProf.ToString(), -2)));
                        PlayerPrefs.SetFloat(j.ToString() + ("Marker") + "MinutesAdded" + NewProf.ToString(), PlayerPrefs.GetFloat(j.ToString() + ("Marker") + "MinutesAdded" + (NewProf + 1).ToString(), -2));
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
        PlayerPrefs.Save();*/
        DeleteProf(Prof);
        Profile = 0;
        Load(Profile);
    }

    public void DeleteProf(int Prof)
    {
        int Profs;
        try
        {
            Profs = JsonUtility.FromJson<SaveObjectList>(File.ReadAllText(Application.dataPath + @"\Savedata\Settings.json")).Objects[1].datas.Length;

        }
        catch
        {
            Profs = 1;
        }

        if (Profs == 1)
        {
            File.Delete(Application.dataPath + @"\Savedata\Profile" + Prof.ToString() + ".json");
            return;
            //There's only one profile, no need to do more
        }
        //int DifferenceProfs = Profs - Prof; // How Many Iterations are we going to need MAYBE:
        int TopProf = Profs-1;
        /*for (; TopProf > Prof + 1; TopProf--)
        {
            try
            {
                MKfile(1, TopProf - 1, RDfile(1, TopProf, true), Profs - 1, true);
            }
            catch { }
        }*/
        for (; Prof < Profs +1; Prof++)
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
                    Debug.Log("AOK");
                    Debug.Log(TopProf);
                    Debug.Log(TopProf - 1);
                }
                else if (Prof == TopProf) {
                    MKfile(1, Prof, RDfile(1, Prof + 1, true), TopProf, true);
                }
            }
            catch { }
            try
            {
                //Debug.Log("MKFILE");
                if (!Directory.Exists(Application.dataPath + @"\Savedata\")) { Directory.CreateDirectory(Application.dataPath + @"\Savedata\"); }

                SaveObjectList sol = new SaveObjectList();
                string FilePath = (@"\Savedata\Profile" + Prof.ToString() + ".json");
                if (File.Exists(Application.dataPath + FilePath))
                {
                    int ObjLength = JsonUtility.FromJson<SaveObjectList>(File.ReadAllText(Application.dataPath + FilePath)).Objects.Length;
                    sol.Objects = new SaveObject[ObjLength];
                    /*if (Objindex + 1 >= ObjLength)
                    {
                        sol.Objects = new SaveObject[Objindex + 1];
                    }*/

                    for (int i = 0; i < sol.Objects.Length; i++)
                    {
                        SaveObject saveobj = new SaveObject();
                        int SaveObjIndex = 1;
                        /*if (i == Objindex && MaxData > 0)
                        {
                            saveobj.datas = new string[MaxData];
                        }
                        else*/
                        {
                            try
                            {
                                SaveObjIndex = JsonUtility.FromJson<SaveObjectList>(File.ReadAllText(Application.dataPath + FilePath)).Objects[i].datas.Length;
                                //Debug.Log(SaveObjIndex);
                            }
                            catch { }
                           // if (Dataindex + 1 > SaveObjIndex && i == Objindex) { SaveObjIndex = Dataindex + 1; }
                            saveobj.datas = new string[SaveObjIndex];
                        }

                        SaveObjIndex = saveobj.datas.Length;
                        for (int j = 0; j < SaveObjIndex; j++)
                        {
                            try
                            {
                                saveobj.datas[j] = JsonUtility.FromJson<SaveObjectList>(File.ReadAllText(Application.dataPath + FilePath)).Objects[i].datas[j];
                            }
                            catch { saveobj.datas[j] = "#"; }

                        }
                        try
                        {
                          //  if (i == Objindex) { saveobj.datas[Dataindex] = Data; }
                        }
                        catch (Exception e)
                        {
                            Debug.Log(e);
                          //  Debug.Log("i:" + i);
                          //  Debug.Log("ObjIND:" + Objindex);
                           // Debug.Log("DATIND:" + Dataindex);


                            saveobj = new SaveObject();
                            SaveObjIndex = 1;
                           /* if (i == Objindex && MaxData > 0)
                            {
                                saveobj.datas = new string[MaxData];
                            }
                            else*/
                            {
                                try
                                {
                                    SaveObjIndex = JsonUtility.FromJson<SaveObjectList>(File.ReadAllText(Application.dataPath + FilePath)).Objects[i].datas.Length;
                                    //Debug.Log(SaveObjIndex);
                                }
                                catch { }
                                //if (Dataindex + 1 > SaveObjIndex && i == Objindex) { SaveObjIndex = Dataindex + 1; }
                                saveobj.datas = new string[SaveObjIndex];
                            }

                            SaveObjIndex = saveobj.datas.Length;
                            for (int j = 0; j < SaveObjIndex; j++)
                            {
                                try
                                {
                                    saveobj.datas[j] = JsonUtility.FromJson<SaveObjectList>(File.ReadAllText(Application.dataPath + FilePath)).Objects[i].datas[j];
                                }
                                catch { saveobj.datas[j] = "#"; }

                            }


                            //if (i == Objindex) { saveobj.datas[Dataindex] = Data; }
                        }

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
                    saveobj.datas = new string[1];
                    saveobj.datas[1] = "1";
                    sol.Objects = new SaveObject[1];
                    sol.Objects[1] = saveobj;
                    //JsonUtility.ToJson(saveobj);


                }



                //File.WriteAllText(Application.dataPath + FilePath, JsonUtility.ToJson(sol));
            }
            catch { }
        
        }
        //3/6
        //So We need to delete 3's File, then we need to save 4 to 3 5 to 4 6 to 5 and set the limit to 5
        //Start with doing this for the Index in Settings, then move to the whole File;
        
        //MKfile(1, Prof, ProfileName, true);
        //ProfileName = RDfile(1, Prof, true);
        






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
        MKfile(1, NewProfile, ProfileName,Profiles+1, true);
        Profile = NewProfile;
        pendingreset = true;
        ResetTime();
        AddProf = NewProfile;
        AddProfDelay = 50;
        Save(Profile);
        Load(Profile);
    }
    public void AddProfPtwo(int NewProfile)
    {
        Debug.Log(NewProfile);
        Save(NewProfile);
        Load(NewProfile);
        pendingreset = true;
        ResetTime();
    }
    public void ResetTime(float ResetToTime)
    {
        ResetTime(ResetToTime, false);
    }
    public void ResetTime(float ResetToTime,bool Immeadiate)
    {
        resettime = 0.4167f;
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
            MKfile(1, 0, (-1).ToString(), -1, false);
            RunTimer = false;
            WasRunTimer = false;
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
            Directory.Delete(Application.dataPath + @"\Savedata\",true);
            Directory.CreateDirectory(Application.dataPath + @"\Savedata\");
            ResetTime();
            Save(0);
            Load(0);
            ResetValuesTimer = 1;
            ResetValues = true;
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
        int tog = 0;
        if (CountUp) { tog = 1; }
        MKfile(0, 2, tog.ToString(),-1,Profile,true);
    }
}