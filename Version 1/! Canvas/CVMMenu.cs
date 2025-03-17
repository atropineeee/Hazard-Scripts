using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

[Serializable]
public class CVMMenu
{
    #region
    public CVMain CVMain;
    public CVMMenu(CVMain cvmain)
    {
        CVMain = cvmain;
    }
    #endregion

    [SerializeField] protected GameObject MainMenuPanel;
    [SerializeField] protected GameObject PauseMenuPanel;
    [SerializeField] protected GameObject HistoryMenuPanel;
    [SerializeField] protected GameObject TouchScreenPanel;
    [SerializeField] protected GameObject DialoguesPanel;
    [SerializeField] protected GameObject BlackScreenPanel;
    [SerializeField] protected GameObject OptionsPanel;
    [SerializeField] protected GameObject TutorialPanel;
    [SerializeField] protected GameObject AchievePanel;
    [SerializeField] protected GameObject BackBtn;
    [SerializeField] protected GameObject InventoryPanel;
    [SerializeField] protected GameObject QuestHPanel;
    [SerializeField] public GameObject LoadingPanel;
    [SerializeField] protected GameObject QuizPanel;

    [SerializeField] protected Animator BlackScreenAnimator;

    [SerializeField] protected Button SettingsButton;

    [SerializeField] protected Button ResumeButton;
    [SerializeField] protected Button OptionsButton;
    [SerializeField] protected Button HistoryButton;
    [SerializeField] protected Button QuitButton;
    [SerializeField] protected Button BackButton;
    [SerializeField] protected Button InventoryButton;
    [SerializeField] protected Button TutorialButton;
    [SerializeField] protected Button AchieveButton;
    [SerializeField] protected Button QuestHButtonO;
    [SerializeField] protected Button QuestHButtonC;

    [SerializeField] public Slider Progressbar;

    private bool toggleOn;

    public void Start()
    {
        SettingsButton = GameObject.Find("SettingsButton").GetComponent<Button>();
        ResumeButton = GameObject.Find("ResumeButton").GetComponent<Button>();
        OptionsButton = GameObject.Find("OptionsButton").GetComponent<Button>();
        HistoryButton = GameObject.Find("HistoryButton").GetComponent<Button>();
        QuitButton = GameObject.Find("QuitButton").GetComponent<Button>();
        BackButton = GameObject.Find("BackButton").GetComponent<Button>();
        InventoryButton = GameObject.Find("AidMVButton").GetComponent<Button>();
        TutorialButton = GameObject.Find("TutorialButton").GetComponent<Button>();
        AchieveButton = GameObject.Find("AchieveButton").GetComponent<Button>();
        QuestHButtonO = GameObject.Find("QuestButton").GetComponent<Button>();
        QuestHButtonC = GameObject.Find("CloseQsPnl").GetComponent<Button>();

        MainMenuPanel = GameObject.Find("Main Menu Panel");
        PauseMenuPanel = GameObject.Find("PauseMenu");
        HistoryMenuPanel = GameObject.Find("HistoryMenu");
        OptionsPanel = GameObject.Find("OptionsMenu");
        TouchScreenPanel = GameObject.Find("TouchScreenCV");
        DialoguesPanel = GameObject.Find("DLGBox");
        BlackScreenPanel = GameObject.Find("DPanel");
        QuizPanel = GameObject.Find("QuizPanel");
        BackBtn = GameObject.Find("BackButton");
        InventoryPanel = GameObject.Find("Inventory");
        LoadingPanel = GameObject.Find("LoadingPanel");
        TutorialPanel = GameObject.Find("TutorialMenu");
        AchievePanel = GameObject.Find("AchieveMenu");
        QuestHPanel = GameObject.Find("QuestsPanel");
        Progressbar = GameObject.Find("LoadingProgress").GetComponent<Slider>();

        BlackScreenAnimator = BlackScreenPanel.GetComponent<Animator>();

        OptionsButton.onClick.AddListener(OpenOptions);
        SettingsButton.onClick.AddListener(OpenSettings);
        ResumeButton.onClick.AddListener(CloseSettings);
        HistoryButton.onClick.AddListener(OpenHistoryMenu);
        BackButton.onClick.AddListener(BackMenu);
        QuitButton.onClick.AddListener(QuitGame);
        InventoryButton.onClick.AddListener(OpenInventory);
        TutorialButton.onClick.AddListener(OpenTutorial);
        AchieveButton.onClick.AddListener(OpenAchievement);
        QuestHButtonO.onClick.AddListener(OpenQstHMenu);
        QuestHButtonC.onClick.AddListener(CloseQstHMenu);

        BlackScreenAnimator.SetTrigger("Open");

        CVMain.StartCoroutine(HideMainMenu());
        CVMain.StartCoroutine(HideBlackMenu());
    }

    private void OpenQstHMenu()
    {
        QuestHPanel.SetActive(true);
        CVMain.FinishedQuest.UpdateQuestList();
    }

    private void CloseQstHMenu()
    {
        QuestHPanel.SetActive(false);
    }

    private void OpenAchievement()
    {
        BackBtn.SetActive(true);
        AchievePanel.SetActive(true);
        CVMain.Achvments.Sync();
        PauseMenuPanel.SetActive(false);
    }

    private void OpenTutorial()
    {
        BackBtn.SetActive(true);
        TutorialPanel.SetActive(true);
        PauseMenuPanel.SetActive(false);
    }

    private void OpenInventory()
    {
        if (toggleOn)
        {
            toggleOn = false;
            InventoryPanel.SetActive(false);
        }
        else
        {
            toggleOn = true;
            InventoryPanel.SetActive(true); 
            CVMain.Inventory.RefreshInventory();
        }
    }

    private void QuitGame()
    {
        LoadingPanel.SetActive(true);
        CVMain.StartCoroutine(LoadGameAsync());
    }

    private void OpenOptions()
    {
        BackBtn.SetActive(true);
        OptionsPanel.SetActive(true);
        PauseMenuPanel.SetActive(false);
    }

    private void OpenHistoryMenu()
    {
        BackBtn.SetActive(true);
        HistoryMenuPanel.SetActive(true);
        PauseMenuPanel.SetActive(false);
        CVMain.SyncHistory.RefreshHistory();
    }

    private void BackMenu()
    {
        PauseMenuPanel.SetActive(true);
        HistoryMenuPanel.SetActive(false);
        OptionsPanel.SetActive(false);
        AchievePanel.SetActive(false);
        TutorialPanel.SetActive(false);
        BackBtn.SetActive(false);
    }

    public void OpenSettings()
    {
        MainMenuPanel.SetActive(true);
        TouchScreenPanel.SetActive(false);
    }

    public void CloseSettings()
    {
        MainMenuPanel.SetActive(false);
        TouchScreenPanel.SetActive(true);
    }

    public void OpenDialogue()
    {
        DialoguesPanel.SetActive(true);
        TouchScreenPanel.SetActive(false);
    }

    public void CloseDialogue()
    {
        TouchScreenPanel.SetActive(true);
        CVMain.Player.MovePlayer.PlayerState = PlayerMovementState.Idle;
        CVMain.Player.MovePlayer.JoyStickInput = Vector2.zero;
        CVMain.Player.PlayerJoyStick.input = Vector2.zero;
        CVMain.Player.PlayerJoyStick.handle.anchoredPosition = Vector2.zero;
        DialoguesPanel.SetActive(false);
    }

    public void OpenQuizMenu()
    {
        QuizPanel.SetActive(true);
    }

    public void CloseQuizMenu()
    {
        QuizPanel.SetActive(false);
    }

    IEnumerator HideMainMenu()
    {
        yield return new WaitForSeconds(0.25f);
        InventoryPanel.SetActive(false);
        LoadingPanel.SetActive(false);
        MainMenuPanel.SetActive(false);
        DialoguesPanel.SetActive(false);
        HistoryMenuPanel.SetActive(false);
        OptionsPanel.SetActive(false);
        TutorialPanel.SetActive(false);
        AchievePanel.SetActive(false);
        BackBtn.SetActive(false);
        QuizPanel.SetActive(false);
        QuestHPanel.SetActive(false);
    }

    IEnumerator HideBlackMenu()
    {
        yield return new WaitForSeconds(2f);
        BlackScreenPanel.SetActive(false);
    }

    IEnumerator LoadGameAsync()
    {
        AsyncOperation AsyncLoad = SceneManager.LoadSceneAsync("Login Scene");
        AsyncLoad.allowSceneActivation = true;

        while (!AsyncLoad.isDone)
        {
            float Progress = Mathf.Clamp01(AsyncLoad.progress / 0.9f);
            if (Progressbar != null)
            {
                Progressbar.value = Progress;
            }

            if (AsyncLoad.progress >= 0.9f)
            {
                AsyncLoad.allowSceneActivation = true;
            }

            yield return null;
        }
    }
}
