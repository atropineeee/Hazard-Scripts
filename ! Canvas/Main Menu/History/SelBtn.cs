using Firebase.Auth;
using Firebase.Database;
using Firebase.Firestore;
using Firebase;
using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Firebase.Extensions;
using Unity.VisualScripting;

public class SelBtn : MonoBehaviour
{
    public Button thisButton;

    public QuizSO AssignedQuiz;
    public SavedStateSO PlayerData;

    public GameObject QQLoc;
    public GameObject QQPref;

    public string Chapter;
    public string Level;

    [Header("Firebase Auth")]
    public DependencyStatus FirebaseStatus;
    public FirebaseAuth FirebaseAuth;
    public FirebaseUser FirebaseUser;
    public FirebaseFirestore FirebaseFirestore;
    public DatabaseReference databaseReference;

    private void Start()
    {
        GameObject thisGO = this.gameObject;
        this.thisButton = thisGO.GetComponent<Button>();

        this.QQLoc = GameObject.Find("JBotScrol");
        this.QQPref = Resources.Load<GameObject>("CanvasPrefabs/NyaBot");

        this.PlayerData = Resources.Load<SavedStateSO>("Scriptable Objects/Player Data/Saved Data");

        this.thisButton.onClick.AddListener(ViewScore);
    }

    private void ViewScore()
    {
        ResetList();

        this.Chapter = this.AssignedQuiz.QuizChapter;
        this.Level = this.AssignedQuiz.QuizLevel;

        for (int i = 0; i < this.AssignedQuiz.QuizQuestionList.Length; i++)
        {
            FirebaseFirestore = FirebaseFirestore.DefaultInstance;

            GameObject Nya = Instantiate(QQPref);
            TMP_Text qstn = Nya.transform.Find("QuestionNM").GetComponent<TMP_Text>();
            TMP_Text ansr = Nya.transform.Find("AnswerNM").GetComponent<TMP_Text>();
            Nya.transform.SetParent(QQLoc.transform, false);

            qstn.text = this.AssignedQuiz.QuizQuestionList[i].Question;

            string email = this.PlayerData.PlayerEmail;
            int currentIndex = i;

            FirebaseFirestore.Collection("userAssessments").WhereEqualTo("email", email).GetSnapshotAsync().ContinueWithOnMainThread(task =>
            {
                if (task.IsCompleted)
                {
                    QuerySnapshot snapshot = task.Result;

                    if (snapshot.Count > 0)
                    {
                        foreach (DocumentSnapshot doc in snapshot.Documents)
                        {
                            if (doc.TryGetValue(Chapter, out object chapterObj) && chapterObj is Dictionary<string, object> chapterData)
                            {
                                if (chapterData.TryGetValue(Level, out object levelObj) && levelObj is Dictionary<string, object> levelData)
                                {
                                    if (levelData.TryGetValue("answers", out object answersObj) && answersObj is List<object> answersList)
                                    {
                                        string[] answers = answersList.ConvertAll(a => a.ToString()).ToArray();

                                        if (answers != null && currentIndex < answers.Length)
                                        {
                                            ansr.text = "<b>Your Answer:</b> " + answers[currentIndex] + "      <b>Right Answer:</b> " + this.AssignedQuiz.QuizQuestionList[currentIndex].RightAnswer.ToString();
                                        }
                                        else
                                        {
                                            ansr.text = "Not Taken Yet";
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
            });
        }
    }

    private void ResetList()
    {
        foreach (Transform child in QQLoc.transform)
        {
            Destroy(child.gameObject);
        }
    }
}
