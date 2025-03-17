using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

[Serializable]
public class QuizHandler
{
    #region
    public CVMain CVMain;
    public QuizHandler (CVMain cvMain)
    {
        CVMain = cvMain;
    }
    #endregion

    public QuizSO QuizBeingTaken;
    public string[] QuizAnswers;
    public int QuizAnswerCount;
    public int QuizScore;

    public TMP_Text QuizScoreTMP;
    public Button QuizSubmitButton;
    public Button ClosePanel;
    public GameObject CloseBtn;
    public GameObject QuestionPanel;
    public GameObject QuestionPanelLoc;

    protected GameObject StoryPrefab;
    protected GameObject QuestPrefab;

    protected GameObject ShowableDials;
    protected GameObject ShowableDialLocation;

    protected GameObject FinishedGame;
    protected GameObject FinishedLoc;


    public void Start()
    {
        QuizScoreTMP = GameObject.Find("ScoreTMP").GetComponent<TMP_Text>();
        QuestionPanel = Resources.Load<GameObject>("CanvasPrefabs/QuestionPanel");
        QuestionPanelLoc = GameObject.Find("QPHolder");
        QuizSubmitButton = GameObject.Find("SubmitQuiz").GetComponent<Button>();
        ClosePanel = GameObject.Find("CloseQPanel").GetComponent<Button>();
        CloseBtn = GameObject.Find("CloseQPanel");
        StoryPrefab = Resources.Load<GameObject>("World Prefabs/StoryTrigger");
        QuestPrefab = Resources.Load<GameObject>("World Prefabs/QuestTrigger");
        QuizScoreTMP.text = "";

        ShowableDials = Resources.Load<GameObject>("CanvasPrefabs/ShowableDial");
        ShowableDialLocation = GameObject.Find("Showables");
        FinishedGame = Resources.Load<GameObject>("CanvasPrefabs/GOOOO");
        FinishedLoc = GameObject.Find("Warning");

        CloseBtn.SetActive(false);
        ClosePanel.onClick.AddListener(CloseThisPanel);
        QuizSubmitButton.onClick.AddListener(SubmitQuiz);
    }

    private void CloseThisPanel()
    {
        QuizScore = 0;
        QuizAnswerCount = 0;

        CreateTrivia();

        if (QuizBeingTaken.NextStory != null)
        {
            GameObject StoryTriggerObj = CVMain.Instantiate(StoryPrefab);
            StoryTriggerObj.name = QuizBeingTaken.NextStory.name;
            StoryTriggerObj.transform.localPosition =  QuizBeingTaken.NextStory.Postion;
            StoryTrigger st = StoryTriggerObj.GetComponent<StoryTrigger>();
            st.AssignedStorySO =  QuizBeingTaken.NextStory;
        }

        if (QuizBeingTaken.NextQuest != null) 
        {
            GameObject QuestTriggerObj = CVMain.Instantiate(QuestPrefab);
            QuestTriggerObj.name = QuizBeingTaken.NextQuest.name;
            QuestTriggerObj.transform.localPosition = QuizBeingTaken.NextQuest.QuestLocation;
            QuestTrigger qt = QuestTriggerObj.GetComponent<QuestTrigger>();
            qt.AssignedSO =  QuizBeingTaken.NextQuest;
        }

        QuizBeingTaken = null;
        CVMain.EventToggle.Triggered = false;
        CVMain.FinishedQuest.QuestDictionary.Clear();
        CVMain.CVMMenu.CloseQuizMenu();
    }

    private void CreateTrivia()
    {
        if (QuizBeingTaken.Trivias != "")
        {
            GameObject CreateShowable = CVMain.Instantiate(ShowableDials);
            CreateShowable.transform.SetParent(ShowableDialLocation.transform, false);
            FLT flt = CreateShowable.GetComponent<FLT>();
            flt.StartTextTriv(QuizBeingTaken.Trivias, QuizBeingTaken.NextScene, QuizBeingTaken.LevelSelected, QuizBeingTaken.TriviaAudio);

            if (QuizBeingTaken.NextScene == "Login Scene")
            {
                GameObject finished = CVMain.Instantiate(FinishedGame);
                finished.transform.SetParent(FinishedLoc.transform, false); 

                CVMain.Destroy(finished, 4f);
            }
        }
    }

    private void SubmitQuiz()
    {
        if (QuizAnswerCount != QuizBeingTaken.QuizQuestionList.Length) { return; };

        CVMain.QuizSubmit.SubmitTheQuiz(QuizScore, QuizBeingTaken.QuizChapter, QuizBeingTaken.QuizLevel, QuizAnswers);

        foreach (Transform Panels in QuestionPanelLoc.transform) 
        {
            QuizPanel quizPanel = Panels.GetComponent<QuizPanel>();
            QuizScoreTMP.text = "Score: " + QuizScore + " of " + QuizBeingTaken.QuizQuestionList.Length;
            CloseBtn.SetActive(true);
            quizPanel.ShowRightAnswers();
        }
    }

    public void CreateQuestionPanel()
    {
        int i = 0;
        ResetQuestionList();

        QuizAnswers = new string[QuizBeingTaken.QuizQuestionList.Length];

        foreach (QuizQuestion Question in QuizBeingTaken.QuizQuestionList)
        {
            GameObject QuizList = CVMain.Instantiate(QuestionPanel);
            QuizList.transform.SetParent(QuestionPanelLoc.transform, false);

            QuizPanel thisQP = QuizList.GetComponent<QuizPanel>();
            thisQP.Qstn = i + 1 + ". " + Question.Question;
            thisQP.QstNum = i.ToString();
            thisQP.QC1 = "A. " + Question.Choices[0];
            thisQP.QC2 = "B. " + Question.Choices[1];
            thisQP.QC3 = "C. " + Question.Choices[2];
            thisQP.QC4 = "D. " + Question.Choices[3];
            thisQP.ThisRightAnswer = Question.RightAnswer;
            QuizList.name = "Question Number: " + i++;
        }
    }

    private void ResetQuestionList()
    {
        foreach (Transform child in QuestionPanelLoc.transform) 
        {
            CVMain.Destroy(child.gameObject);
        }
    }
}
