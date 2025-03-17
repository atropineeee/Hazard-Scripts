using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CVMain : MonoBehaviour
{
    protected TMP_Text FPSTEXT;
    protected float FPSTime = 0.0f;

    public CVMMenu CVMMenu;
    public CVStoryMain CVStoryMain;
    public CVStoryDials CVStoryDials;
    public QuizHandler QuizHandler;
    public QuizSubmit QuizSubmit;
    public EventToggle EventToggle;
    public SyncHistory SyncHistory;
    public OptionsPanel OptionsPanel;
    public LoadSavedSync LoadSavedSync;
    public Inventory Inventory;
    public SyncAch Achvments;
    public FinishedQuest FinishedQuest;
    public Player Player;

    public GameObject StoryPrefab;
    public GameObject QuestPrefab;

    protected StorySO Chp1S1SO;
    protected StorySO Chp2S1SO;
    protected StorySO Chp2S2SO;
    protected StorySO Chp3S1SO;

    protected QuestSO Chp1L2SO;
    protected QuestSO Chp2L2SO;

    public SavedStateSO SavedData;

    private void Start()
    {
        FPSTEXT = GameObject.Find("FPSText").GetComponent<TMP_Text>();
        Player = GameObject.Find("Player").GetComponent<Player>();

        SavedData = Resources.Load<SavedStateSO>("Scriptable Objects/Player Data/Saved Data");
        StoryPrefab = Resources.Load<GameObject>("World Prefabs/StoryTrigger");
        QuestPrefab = Resources.Load<GameObject>("World Prefabs/QuestTrigger");

        Chp1S1SO = Resources.Load<StorySO>("Scriptable Objects/Story List/Cutscene 1&2");
        Chp2S1SO = Resources.Load<StorySO>("Scriptable Objects/Story List/Cutscene 4&5");
        Chp2S2SO = Resources.Load<StorySO>("Scriptable Objects/Story List/Cutscene 6");
        Chp3S1SO = Resources.Load<StorySO>("Scriptable Objects/Story List/Cutscene 7");


        Chp1L2SO = Resources.Load<QuestSO>("Scriptable Objects/Quest List/Quest 2/Quest2");
        Chp2L2SO = Resources.Load<QuestSO>("Scriptable Objects/Quest List/Quest 3/Quest3");

        GameObject MaleGo = GameObject.Find("MaleCharPlayer");
        GameObject FemaleGo = GameObject.Find("FemaleCharPlayer");

        if (SavedData.CharacterSelected == CharacterType.Male)
        {
            Destroy(FemaleGo);
        }
        else
        {
            Destroy(MaleGo);
        }

        CVMMenu = new CVMMenu(this);
        CVStoryMain = new CVStoryMain(this);
        CVStoryDials = new CVStoryDials(this);
        QuizHandler = new QuizHandler(this);
        QuizSubmit = new QuizSubmit(this);
        EventToggle = new EventToggle(this);
        SyncHistory = new SyncHistory(this);
        LoadSavedSync = new LoadSavedSync(this);
        OptionsPanel = new OptionsPanel(this);
        Inventory = new Inventory(this);
        Achvments = new SyncAch(this);
        FinishedQuest = new FinishedQuest(this);

        StartAll();
        LoadData();
    }

    private void LoadData()
    {
        if (SavedData.PlayerProgress != "" && !SavedData.newGame) 
        {
            LoadSavedSync.Start();
            return; 
        }

        // Chapter 1
        if (SavedData.LevelSelected == LevelSelected.Ch1L1)
        {
            GameObject StoryTriggerObj = Instantiate(StoryPrefab);
            StoryTriggerObj.name = Chp1S1SO.name;
            StoryTriggerObj.transform.localPosition = Chp1S1SO.Postion;
            
            StoryTrigger st = StoryTriggerObj.GetComponent<StoryTrigger>();
            st.AssignedStorySO = Chp1S1SO;
            return;
        }

        if (SavedData.LevelSelected == LevelSelected.Ch1L2) 
        {
            GameObject QuestTriggerObj = Instantiate(QuestPrefab);
            QuestTriggerObj.name = Chp1L2SO.name;
            QuestTriggerObj.transform.localPosition = Chp1L2SO.QuestLocation;
            QuestTrigger qt = QuestTriggerObj.GetComponent<QuestTrigger>();
            qt.AssignedSO = Chp1L2SO;

            Player.transform.localPosition = new Vector3(145.559418f, 19.9963379f, 201.731689f);
            Player.transform.localRotation = Quaternion.Euler(0f, -48.05f, 0f);
            return;
        }

        if (SavedData.LevelSelected == LevelSelected.Ch2L1)
        {
            GameObject StoryTriggerObj = Instantiate(StoryPrefab);
            StoryTriggerObj.name = Chp2S1SO.name;
            StoryTriggerObj.transform.localPosition = Chp2S1SO.Postion;

            StoryTrigger st = StoryTriggerObj.GetComponent<StoryTrigger>();
            st.AssignedStorySO = Chp2S1SO;
            return;
        }

        if (SavedData.LevelSelected == LevelSelected.Ch2L2)
        {
            GameObject StoryTriggerObj = Instantiate(StoryPrefab);
            StoryTriggerObj.name = Chp2S2SO.name;
            StoryTriggerObj.transform.localPosition = Chp2S2SO.Postion;

            StoryTrigger st = StoryTriggerObj.GetComponent<StoryTrigger>();
            st.AssignedStorySO = Chp2S2SO;

            Player.transform.localPosition = new Vector3(116.979317f, 9.991436f, 112.872742f);
            Player.transform.localRotation = Quaternion.Euler(0f, -48.05f, 0f);
            return;
        }

        if (SavedData.LevelSelected == LevelSelected.Ch3L1)
        {
            GameObject StoryTriggerObj = Instantiate(StoryPrefab);
            StoryTriggerObj.name = Chp3S1SO.name;
            StoryTriggerObj.transform.localPosition = Chp3S1SO.Postion;

            StoryTrigger st = StoryTriggerObj.GetComponent<StoryTrigger>();
            st.AssignedStorySO = Chp3S1SO;
            return;
        }
    }

    private void StartAll()
    {
        CVMMenu.Start();
        CVStoryMain.Start();
        CVStoryDials.Start();
        QuizHandler.Start();
        EventToggle.Start();
        SyncHistory.Start();
        OptionsPanel.Awake();
    }

    private void Update()
    {
        ChangeFPS();
        EventToggle.Update();
    }

    private void ChangeFPS()
    {
        Application.targetFrameRate = 60;
        FPSTime += (Time.unscaledDeltaTime - FPSTime) * 0.1f;
        float fps = 1.0f / FPSTime;
        if (FPSTEXT != null)
        {
            FPSTEXT.text = $"FPS: {Mathf.Ceil(fps)}";
        }
    }
}
