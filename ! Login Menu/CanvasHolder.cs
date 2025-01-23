using Firebase.Auth;
using Firebase;
using Firebase.Database;
using Firebase.Firestore;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Firebase.Extensions;

public class CanvasHolder : MonoBehaviour
{
    [Header("Login Panels")]
    public GameObject LoadingPanel;
    public GameObject LoginPanel;
    public GameObject MainMenuPanel;
    public GameObject StartGamePanel;
    public GameObject CharSelectPanel;
    public GameObject LevelSelectPanel;
    public GameObject StoryboardPanel;
    public GameObject OptionsPanel;
    public GameObject JournalsPanel;
    public Slider ProgressBar;
    public SavedStateSO SavedData;

    [Header("Chapter Panels")]
    public GameObject Chp1P;
    public GameObject Chp2P;
    public GameObject Chp3P;

    [Header("Settings")]
    public Slider MasterVolume;
    public Slider VoiceOver;
    public Slider Environment;

    [Header("Something Cool")]
    public GameObject InvEmlPswPref;
    public GameObject ForgotPref;
    public GameObject InvEmlPswLoca;

    [Header("SSSS")]
    public InputField EmailInputField;
    public InputField PasswordInputField;
    public Button LoginButton;
    public Button ForgotCreds;

    [Header("Firebase Auth")]
    public DependencyStatus FirebaseStatus;
    public FirebaseAuth FirebaseAuth;
    public FirebaseUser FirebaseUser;
    public FirebaseFirestore FirebaseFirestore;
    public DatabaseReference databaseReference;

    [Header("SubScripts")]
    public CanvasHandler CanvasHandler;
    public AssessmentSync AssessmentSync;
    public PlayerSync PlayerSync;
    public QuizSync QuizSync;
    public StryBoard StryBoard;
    public Journals Journals;
    public LockButtons LockButtons;

    [Header("Quiz SO")]
    public QuizSO Chp1Lvl1Quiz;
    public QuizSO Chp1Lvl2Quiz;
    public QuizSO Chp2Lvl3Quiz;
    public QuizSO Chp2Lvl4Quiz;
    public QuizSO Chp3Lvl5Quiz;

    [Header("Player Data")]
    public SavedStateSO PlayerData;

    private void Start()
    {
        CanvasHandler = new CanvasHandler(this);
        AssessmentSync = new AssessmentSync(this);
        PlayerSync = new PlayerSync(this);
        QuizSync = new QuizSync(this);
        StryBoard = new StryBoard(this);
        Journals = new Journals(this);
        LockButtons = new LockButtons(this);
        CanvasHandler.Start();

        Chp1Lvl1Quiz = Resources.Load<QuizSO>("Scriptable Objects/QuizList/Quiz1");
        Chp1Lvl2Quiz = Resources.Load<QuizSO>("Scriptable Objects/QuizList/Quiz2");
        Chp2Lvl3Quiz = Resources.Load<QuizSO>("Scriptable Objects/QuizList/Quiz3");
        Chp2Lvl4Quiz = Resources.Load<QuizSO>("Scriptable Objects/QuizList/Quiz4");
        Chp3Lvl5Quiz = Resources.Load<QuizSO>("Scriptable Objects/QuizList/Quiz5");
        PlayerData = Resources.Load<SavedStateSO>("Scriptable Objects/Player Data/Saved Data");


        Journals.Start();

        SyncToDatabase();
        GetRequiredComponents();
    }

    private void SyncToDatabase()
    {
        FirebaseApp.CheckAndFixDependenciesAsync().ContinueWithOnMainThread(task =>
        {
            FirebaseApp App = FirebaseApp.DefaultInstance;

            if (task.IsCompleted)
            {
                Debug.Log("Firebase Initialized");
                FirebaseFirestore = FirebaseFirestore.DefaultInstance;
            }
        });
    }

    private void GetRequiredComponents()
    {
        // Game Objects
        LoadingPanel = GameObject.Find("LoadingPanel");
        LoginPanel = GameObject.Find("Login Panel");
        MainMenuPanel = GameObject.Find("Main Menu");
        StartGamePanel = GameObject.Find("StartMenuPanel");
        CharSelectPanel = GameObject.Find("CharacterSelectPanel");
        LevelSelectPanel = GameObject.Find("LevelSelectPanel");
        OptionsPanel = GameObject.Find("OptionsPanel");
        StoryboardPanel = GameObject.Find("StoryBoardPanel");
        JournalsPanel = GameObject.Find("JournalPanel");
        ProgressBar = GameObject.Find("LoadingProgress").GetComponent<Slider>();

        // Volume Slider
        MasterVolume = GameObject.Find("MasterVolSlider").GetComponent<Slider>();
        VoiceOver = GameObject.Find("VoiceVolSlider").GetComponent<Slider>();
        Environment = GameObject.Find("EnvVolSlider").GetComponent<Slider>();

        InvEmlPswLoca = GameObject.Find("WrongHandler");
        InvEmlPswPref = Resources.Load<GameObject>("CanvasPrefabs/InvalidEmail");
        ForgotPref = Resources.Load<GameObject>("CanvasPrefabs/ForgotCreds");

        Chp1P = GameObject.Find("Chp1Pnl");
        Chp2P = GameObject.Find("Chp2Pnl");
        Chp3P = GameObject.Find("Chp3Pnl");

        // Buttons
        LoginButton = GameObject.Find("LogBtn").GetComponent<Button>();
        ForgotCreds = GameObject.Find("ForgCredBtn").GetComponent<Button>();

        Chp3P.SetActive(false);
        JournalsPanel.SetActive(false);
        LoadingPanel.SetActive(false);
        StartGamePanel.SetActive(false);
        CharSelectPanel.SetActive(false);
        LevelSelectPanel.SetActive(false);
        OptionsPanel.SetActive(false);
        MainMenuPanel.SetActive(false);
        StoryboardPanel.SetActive(false);

        EmailInputField = GameObject.Find("LogSTDNum").GetComponent<InputField>();
        PasswordInputField = GameObject.Find("LogPass").GetComponent<InputField>();
        LoginButton.onClick.AddListener(TryLoginUser);
        ForgotCreds.onClick.AddListener(ForgotCredits);

        MasterVolume.onValueChanged.AddListener(MasterVol);
        VoiceOver.onValueChanged.AddListener(VoiceOv);
        Environment.onValueChanged.AddListener(EnvVol);

        StartCoroutine(nya());
    }

    IEnumerator nya ()
    {
        yield return new WaitForSeconds(1);
        if (PlayerData.PlayerEmail != "" && PlayerData.PlayerPassw != "")
        {
            TryLoginUser2(PlayerData.PlayerEmail, PlayerData.PlayerPassw);
        }
    }

    private void MasterVol(float value)
    {
        PlayerData.MasterVolume = Convert.ToInt32(value);
    }

    private void VoiceOv(float value)
    {
        PlayerData.VoiceOver = Convert.ToInt32(value);
    }

    private void EnvVol(float value)
    {
        PlayerData.Environment = Convert.ToInt32(value);
    }

    private void ForgotCredits()
    {
        GameObject Inv = Instantiate(ForgotPref);
        Inv.transform.SetParent(InvEmlPswLoca.transform, false);
        Destroy(Inv, 3f);
    }

    private void TryLoginUser()
    {
        string UsernameInput = EmailInputField.text;
        string PasswordInput = PasswordInputField.text;

        FirebaseFirestore.Collection("users").WhereEqualTo("email", UsernameInput).GetSnapshotAsync().ContinueWithOnMainThread(task =>
        {
            if (task.IsCompleted)
            {
                QuerySnapshot snapshot = task.Result;
                if (snapshot.Count > 0)
                {
                    foreach (DocumentSnapshot doc in snapshot.Documents)
                    {
                        Dictionary<string, object> userData = doc.ToDictionary();

                        if (userData.ContainsKey("email") && userData.ContainsKey("password"))
                        {
                            if (UsernameInput ==  userData["email"].ToString() && PasswordInput == userData["password"].ToString())
                            {
                                EmailInputField.text = "";
                                PasswordInputField.text = "";
                                PlayerData.PlayerEmail = userData["email"].ToString();
                                PlayerData.PlayerName = userData["name"].ToString();
                                PlayerData.PlayerPassw = userData["password"].ToString();
                                AssessmentSync.SyncUser();
                                CanvasHandler.ProceedToMainMenu();
                                QuizSync.SyncQuizFromDatabase();
                                PlayerSync.UserSync();
                                return;
                            }
                            else
                            {
                                GameObject Inv = Instantiate(InvEmlPswPref);
                                Inv.transform.SetParent(InvEmlPswLoca.transform,false);
                                Destroy(Inv, 3f);
                                return;
                            }
                        }
                    }
                }
            }
        });
    }

    private void TryLoginUser2(string Email, string Password)
    {
        string UsernameInput = Email;
        string PasswordInput = Password;

        FirebaseFirestore.Collection("users").WhereEqualTo("email", UsernameInput).GetSnapshotAsync().ContinueWithOnMainThread(task =>
        {
            if (task.IsCompleted)
            {
                QuerySnapshot snapshot = task.Result;
                if (snapshot.Count > 0)
                {
                    foreach (DocumentSnapshot doc in snapshot.Documents)
                    {
                        Dictionary<string, object> userData = doc.ToDictionary();

                        if (userData.ContainsKey("email") && userData.ContainsKey("password"))
                        {
                            if (UsernameInput ==  userData["email"].ToString() && PasswordInput == userData["password"].ToString())
                            {
                                EmailInputField.text = "";
                                PasswordInputField.text = "";
                                PlayerData.PlayerEmail = userData["email"].ToString();
                                PlayerData.PlayerName = userData["name"].ToString();
                                AssessmentSync.SyncUser();
                                CanvasHandler.ProceedToMainMenu();
                                QuizSync.SyncQuizFromDatabase();
                                PlayerSync.UserSync();
                                return;
                            }
                            else
                            {
                                GameObject Inv = Instantiate(InvEmlPswPref);
                                Inv.transform.SetParent(InvEmlPswLoca.transform, false);
                                Destroy(Inv, 3f);
                                return;
                            }
                        }
                    }
                }
            }
        });
    }

    public IEnumerator Chapter1Scene()
    {
        AsyncOperation AsyncLoad = SceneManager.LoadSceneAsync("Chapter1Scene");
        AsyncLoad.allowSceneActivation = true;

        while (!AsyncLoad.isDone)
        {
            float Progress = Mathf.Clamp01(AsyncLoad.progress / 0.9f);
            if (ProgressBar != null)
            {
                ProgressBar.value = Progress;
            }

            if (AsyncLoad.progress >= 0.9f)
            {
                AsyncLoad.allowSceneActivation = true;
            }

            yield return null;
        }
    }

    public IEnumerator Chapter2Scene()
    {
        AsyncOperation AsyncLoad = SceneManager.LoadSceneAsync("Chapter2Scene");
        AsyncLoad.allowSceneActivation = true;

        while (!AsyncLoad.isDone)
        {
            float Progress = Mathf.Clamp01(AsyncLoad.progress / 0.9f);
            if (ProgressBar != null)
            {
                ProgressBar.value = Progress;
            }

            if (AsyncLoad.progress >= 0.9f)
            {
                AsyncLoad.allowSceneActivation = true;
            }

            yield return null;
        }
    }

    public IEnumerator Chapter3Scene()
    {
        AsyncOperation AsyncLoad = SceneManager.LoadSceneAsync("Chapter3Scene");
        AsyncLoad.allowSceneActivation = true;

        while (!AsyncLoad.isDone)
        {
            float Progress = Mathf.Clamp01(AsyncLoad.progress / 0.9f);
            if (ProgressBar != null)
            {
                ProgressBar.value = Progress;
            }

            if (AsyncLoad.progress >= 0.9f)
            {
                AsyncLoad.allowSceneActivation = true;
            }

            yield return null;
        }
    }
}
