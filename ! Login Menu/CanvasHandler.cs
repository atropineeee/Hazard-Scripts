using Firebase;
using Firebase.Auth;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;
using UnityEngine.UI;

[Serializable]
public class CanvasHandler
{
    #region
    public CanvasHolder CVHolder;
    public CanvasHandler(CanvasHolder cVHolder)
    {
        CVHolder = cVHolder;
    }
    #endregion

    [Header("Main Menu Buttons")]
    public Button StartGameButton;
    public Button OptionsButton;
    public Button JournalButton;

    [Header("Load / Start New Buttons")]
    public Button StartNewButton;
    public Button LoadPastButton;
    public Button LogOutButton;

    [Header("Level Select Buttons")]
    public Button Chp1Lvl1Btn;
    public Button Chp1Lvl2Btn;
    public Button Chp2Lvl3Btn;
    public Button Chp2Lvl4Btn;
    public Button Chp3Lvl5Btn;

    [Header("Character Select Button")]
    public Button FemaleSelectButton;
    public Button MaleSelectButton;

    [Header("Storyboard")]
    public Button StartButton;
    public Button SkipButton;

    [Header("Graphics Settings")]
    public Button LowGraph;
    public Button MedGraph;
    public Button HighGraph;

    [Header("Chapter Buttons")]
    public Button Nxt;
    public Button Prv;

    private UniversalRenderPipelineAsset urpAsset;

    public void Start()
    {
        urpAsset = QualitySettings.renderPipeline as UniversalRenderPipelineAsset;

        StartGameButton = GameObject.Find("StartGameMenuBtn").GetComponent<Button>();
        OptionsButton = GameObject.Find("OptionsMenuBtn").GetComponent<Button>();
        JournalButton = GameObject.Find("JournalMenuBtn").GetComponent<Button>();

        StartNewButton = GameObject.Find("StartNewBtn").GetComponent<Button>();
        LoadPastButton = GameObject.Find("LoadSaveBtn").GetComponent<Button>();
        LogOutButton = GameObject.Find("LogOutButton").GetComponent<Button>();

        Chp1Lvl1Btn = GameObject.Find("Chp1Lvl1").GetComponent<Button>();
        Chp1Lvl2Btn = GameObject.Find("Chp1Lvl2").GetComponent<Button>();
        Chp2Lvl3Btn = GameObject.Find("Chp2Lvl3").GetComponent<Button>();
        Chp2Lvl4Btn = GameObject.Find("Chp2Lvl4").GetComponent<Button>();
        Chp3Lvl5Btn = GameObject.Find("Chp3Lvl5").GetComponent<Button>();

        LowGraph = GameObject.Find("LowGraphBtn").GetComponent<Button>();
        MedGraph = GameObject.Find("MedGraphBtn").GetComponent<Button>();
        HighGraph = GameObject.Find("HighGraphBtn").GetComponent<Button>();

        FemaleSelectButton = GameObject.Find("FemaleCharacterSelect").GetComponent<Button>();
        MaleSelectButton = GameObject.Find("MaleCharacterSelect").GetComponent<Button>();

        StartButton = GameObject.Find("StartGame").GetComponent<Button>();
        SkipButton = GameObject.Find("SBImage").GetComponent<Button>();

        Nxt = GameObject.Find("NextPG").GetComponent<Button>();
        Prv = GameObject.Find("PrevPG").GetComponent<Button>();

        Nxt.onClick.AddListener(NextPG);
        Prv.onClick.AddListener(PrevPG);

        Prv.interactable = false;

        StartButton.interactable = false;

        SkipButton.onClick.AddListener(CVHolder.StryBoard.ChapterSB);
        StartButton.onClick.AddListener(StartGame);

        StartGameButton.onClick.AddListener(OpenGameMenu);
        OptionsButton.onClick.AddListener(OpenOptionsMenu);
        JournalButton.onClick.AddListener(OpenJournalMenu);

        StartNewButton.onClick.AddListener(OpenCharSelect);
        LoadPastButton.onClick.AddListener(LoadSavedData);
        LogOutButton.onClick.AddListener(LogOutPlayer);

        FemaleSelectButton.onClick.AddListener(() => OpenLevelSelect(CharacterType.Female));
        MaleSelectButton.onClick.AddListener(() => OpenLevelSelect(CharacterType.Male));

        Chp1Lvl1Btn.onClick.AddListener(() => SelectLevel(LevelSelected.Ch1L1));
        Chp1Lvl2Btn.onClick.AddListener(() => SelectLevel(LevelSelected.Ch1L2));
        Chp2Lvl3Btn.onClick.AddListener(() => SelectLevel(LevelSelected.Ch2L1));
        Chp2Lvl4Btn.onClick.AddListener(() => SelectLevel(LevelSelected.Ch2L2));
        Chp3Lvl5Btn.onClick.AddListener(() => SelectLevel(LevelSelected.Ch3L1));

        LowGraph.onClick.AddListener(LowSettings);
        MedGraph.onClick.AddListener(MedSettings);
        HighGraph.onClick.AddListener(HighSettings);
    }

    private void LoadSavedData()
    {
        CVHolder.PlayerData.newGame = false;

        if (CVHolder.PlayerData.PlayerProgress.StartsWith("C1"))
        {
            CVHolder.LoadingPanel.SetActive(true);
            CVHolder.StartCoroutine(CVHolder.Chapter1Scene());
        }

        if (CVHolder.PlayerData.PlayerProgress.StartsWith("C2"))
        {
            CVHolder.LoadingPanel.SetActive(true);
            CVHolder.StartCoroutine(CVHolder.Chapter2Scene());
        }

        if (CVHolder.PlayerData.PlayerProgress.StartsWith("C3"))
        {
            CVHolder.LoadingPanel.SetActive(true);
            CVHolder.StartCoroutine(CVHolder.Chapter3Scene());
        }
    }

    private void PrevPG()
    {
        CVHolder.Chp1P.SetActive(true);
        CVHolder.Chp2P.SetActive(true);
        CVHolder.Chp3P.SetActive(false);

        Prv.interactable = false;
        Nxt.interactable = true;
    }

    private void NextPG()
    {
        CVHolder.Chp1P.SetActive(false);
        CVHolder.Chp2P.SetActive(false);
        CVHolder.Chp3P.SetActive(true);

        Prv.interactable = true;
        Nxt.interactable = false;
    }

    private void LowSettings()
    {
        urpAsset.renderScale = 0.5f;
        urpAsset.supportsHDR = false;
    }

    private void MedSettings()
    {
        urpAsset.renderScale = 1f;
        urpAsset.supportsHDR = true;
    }

    private void HighSettings()
    {
        urpAsset.renderScale = 1.5f;
        urpAsset.supportsHDR = true;
    }

    private void StartGame()
    {
        CVHolder.LoadingPanel.SetActive(true);
        if (CVHolder.PlayerData.LevelSelected == LevelSelected.Ch1L1 || CVHolder.PlayerData.LevelSelected == LevelSelected.Ch1L2)
        {
            CVHolder.StartCoroutine(CVHolder.Chapter1Scene());
        }

        if (CVHolder.PlayerData.LevelSelected == LevelSelected.Ch2L1 || CVHolder.PlayerData.LevelSelected == LevelSelected.Ch2L2)
        {
            CVHolder.StartCoroutine(CVHolder.Chapter2Scene());
        }

        if (CVHolder.PlayerData.LevelSelected == LevelSelected.Ch3L1 || CVHolder.PlayerData.LevelSelected == LevelSelected.Ch3L2)
        {
            CVHolder.StartCoroutine(CVHolder.Chapter3Scene());
        }
    }

    private void SelectLevel(LevelSelected selectedLevel)
    {
        CVHolder.PlayerData.LevelSelected = selectedLevel;
        CVHolder.StoryboardPanel.SetActive(true);
        CVHolder.StryBoard.ChapterSB();
    }

    private void OpenLevelSelect(CharacterType charType)
    {
        CVHolder.PlayerData.CharacterSelected = charType;

        CVHolder.StartGamePanel.SetActive(false);
        CVHolder.OptionsPanel.SetActive(false);
        CVHolder.CharSelectPanel.SetActive(false);
        CVHolder.LevelSelectPanel.SetActive(true);
        CVHolder.LockButtons.SyncButtons();
    }

    private void OpenCharSelect()
    {
        CVHolder.PlayerData.newGame = true;

        CVHolder.StartGamePanel.SetActive(false);
        CVHolder.OptionsPanel.SetActive(false);
        CVHolder.CharSelectPanel.SetActive(true);
        CVHolder.LevelSelectPanel.SetActive(false);
    }

    private void LogOutPlayer()
    {
        CVHolder.StartGamePanel.SetActive(false);
        CVHolder.OptionsPanel.SetActive(false);
        CVHolder.CharSelectPanel.SetActive(false);
        CVHolder.LevelSelectPanel.SetActive(false);
        CVHolder.MainMenuPanel.SetActive(false);
        CVHolder.LoginPanel.SetActive(true);

        CVHolder.PlayerData.PlayerEmail = "";
        CVHolder.PlayerData.PlayerName = "";
        CVHolder.PlayerData.PlayerPassw = "";
        CVHolder.PlayerData.PlayerProgress = "";
        CVHolder.PlayerData.PlayerPosition = Vector3.zero;
        CVHolder.PlayerData.newGame = false;
        CVHolder.PlayerData.Level1 = "No Save";
        CVHolder.PlayerData.Level2 = "No Save";
        CVHolder.PlayerData.Level3 = "No Save";
        CVHolder.PlayerData.Level4 = "No Save";
        CVHolder.PlayerData.Level5 = "No Save";

        Application.Quit();
    }

    private void OpenGameMenu()
    {
        CVHolder.StartGamePanel.SetActive(true);
        CVHolder.OptionsPanel.SetActive(false);
        CVHolder.CharSelectPanel.SetActive(false);
        CVHolder.LevelSelectPanel.SetActive(false);
        CVHolder.JournalsPanel.SetActive(false);    
    }

    private void OpenOptionsMenu()
    {
        CVHolder.StartGamePanel.SetActive(false);
        CVHolder.OptionsPanel.SetActive(true);
        CVHolder.CharSelectPanel.SetActive(false);
        CVHolder.LevelSelectPanel.SetActive(false);
        CVHolder.JournalsPanel.SetActive(false);
    }

    private void OpenJournalMenu()
    {
        CVHolder.StartGamePanel.SetActive(false);
        CVHolder.OptionsPanel.SetActive(false);
        CVHolder.CharSelectPanel.SetActive(false);
        CVHolder.LevelSelectPanel.SetActive(false);
        CVHolder.JournalsPanel.SetActive(true);
    }

    public void ProceedToMainMenu()
    {
        CVHolder.LoginPanel.SetActive(false);
        CVHolder.MainMenuPanel.SetActive(true);
    }
}
