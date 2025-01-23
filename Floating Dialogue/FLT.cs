using Firebase.Auth;
using Firebase.Database;
using Firebase;
using Firebase.Firestore;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;
using Firebase.Extensions;

public class FLT : MonoBehaviour
{
    [SerializeField] private TMP_Text floatingText;
    [SerializeField] private float TypingSpeed = 0.05f;
    [SerializeField] private bool isTyping;

    public AudioSource AudSource;

    [Header("Firebase Auth")]
    public DependencyStatus FirebaseStatus;
    public FirebaseAuth FirebaseAuth;
    public FirebaseUser FirebaseUser;
    public FirebaseFirestore FirebaseFirestore;
    public DatabaseReference databaseReference;

    [SerializeField] private SavedStateSO SavedData;
    [SerializeField] private CVMain CVMain;

    private void Start()
    {
        floatingText = this.gameObject.transform.Find("Floaters").GetComponent<TMP_Text>();
        SavedData = Resources.Load<SavedStateSO>("Scriptable Objects/Player Data/Saved Data");
        this.AudSource = GameObject.Find("Player").GetComponent<AudioSource>();
        CVMain = GameObject.Find("CanvasC").GetComponent<CVMain>();
    }

    public void StartTextAnim(string Dialogue)
    {
        StartCoroutine(TypeDialogue(Dialogue));
    }

    public IEnumerator TypeDialogue(string Dialoguse)
    {
        yield return new WaitForSeconds(0.5f);
        StartCoroutine(TypeDialogueAnim(Dialoguse));
    }

    public IEnumerator TypeDialogueAnim(string Dialoguse)
    {
        isTyping = true;
        floatingText.text = "";

        foreach (char letter in Dialoguse)
        {
            if (!isTyping) { yield break; }

            floatingText.text += letter;
            yield return new WaitForSeconds(TypingSpeed);
        }

        isTyping = false;
        Destroy(this.gameObject, 3f);
    }

    public void StartTextTriv(string Dialogue, string NextScene, LevelSelected levelSelected, AudioClip audio)
    {
        if (Dialogue == "") { return; }


        StartCoroutine(TypeDialogueTriv(Dialogue, NextScene, levelSelected, audio));
    }

    public IEnumerator TypeDialogueTriv(string Dialoguse, string nextsc, LevelSelected lvSel, AudioClip audio)
    {
        yield return new WaitForSeconds(0.5f);
        StartCoroutine(TypeDialogueTriv2(Dialoguse, nextsc, lvSel));

        if (Dialoguse != "") 
        {
            if (audio != null)
            {
                this.AudSource.clip = audio;
                this.AudSource.Play();
            }
        }
    }

    public IEnumerator TypeDialogueTriv2(string Dialoguse, string nextsc, LevelSelected lvSel)
    {
        isTyping = true;
        floatingText.text = "";

        foreach (char letter in Dialoguse)
        {
            if (!isTyping) { yield break; }

            floatingText.text += letter;
            yield return new WaitForSeconds(TypingSpeed);
        }

        if (nextsc != "")
        {
            StartCoroutine(nya(nextsc));
            SavedData.PlayerProgress = "";
            SavedData.LevelSelected = lvSel;
        }

        isTyping = false;
        Destroy(this.gameObject, 3f);
    }


    IEnumerator nya(string nextscene)
    {
        yield return new WaitForSeconds(2.5f);
        CVMain.CVMMenu.LoadingPanel.SetActive(true);
        SynctoDatabase();
        StartCoroutine(LoadGameAsync(nextscene));
    }

    public IEnumerator LoadGameAsync(string nextscene)
    {
        AsyncOperation AsyncLoad = SceneManager.LoadSceneAsync(nextscene);
        AsyncLoad.allowSceneActivation = true;

        while (!AsyncLoad.isDone)
        {
            float Progress = Mathf.Clamp01(AsyncLoad.progress / 0.9f);
            if (CVMain.CVMMenu.Progressbar != null)
            {
                CVMain.CVMMenu.Progressbar.value = Progress;
            }

            if (AsyncLoad.progress >= 0.9f)
            {
                AsyncLoad.allowSceneActivation = true;
            }

            yield return null;
        }
    }

    private void SynctoDatabase()
    {
        FirebaseFirestore = FirebaseFirestore.DefaultInstance;
        string currentEmail = CVMain.SavedData.PlayerEmail;

        FirebaseFirestore.Collection("userAssessments")
            .WhereEqualTo("email", currentEmail)
            .GetSnapshotAsync()
            .ContinueWithOnMainThread(task =>
            {
                if (task.IsCompleted)
                {
                    QuerySnapshot snapshot = task.Result;

                    if (snapshot.Count > 0)
                    {
                        foreach (DocumentSnapshot doc in snapshot.Documents)
                        {
                            DocumentReference docRef = doc.Reference;
                            Dictionary<string, object> updatedData = new Dictionary<string, object>
                            {
                                { "playerx", "" },
                                { "playery", "" },
                                { "playerz", "" },
                                { "savedprogress", "" },
                                { "selectedchar", "" }
                            };

                            docRef.UpdateAsync(updatedData);
                        }
                    }
                }
            });
    }
}
