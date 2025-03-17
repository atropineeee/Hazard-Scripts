using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class QuizPanel : MonoBehaviour
{
    private GameObject thisParent;
    public QuizSO QuizNumber;

    [SerializeField] public TMP_Text QuizQuestion;
    [SerializeField] public TMP_Text AChoiceTMP;
    [SerializeField] public TMP_Text BChoiceTMP;
    [SerializeField] public TMP_Text CChoiceTMP;
    [SerializeField] public TMP_Text DChoiceTMP;

    [SerializeField] public Button AChoiceButton;
    [SerializeField] public Button BChoiceButton;
    [SerializeField] public Button CChoiceButton;
    [SerializeField] public Button DChoiceButton;

    [SerializeField] public GameObject AChoiceButtonG;
    [SerializeField] public GameObject BChoiceButtonG;
    [SerializeField] public GameObject CChoiceButtonG;
    [SerializeField] public GameObject DChoiceButtonG;

    public string Qstn;
    public string QC1;
    public string QC2;
    public string QC3;
    public string QC4;
    public string QstNum;

    public RightAnswer ThisRightAnswer;
    public SelectedChoice ThisSelectedChoice;

    public bool ChoiceSelected;
    public bool isSubmitted;

    protected CVMain CVMain;

    private void Start()
    {
        thisParent = this.gameObject;
        CVMain = GameObject.Find("CanvasC").GetComponent<CVMain>();

        this.QuizQuestion = thisParent.transform.Find("QuestionTMP").GetComponent<TMP_Text>();
        this.AChoiceTMP = thisParent.transform.Find("ACBtn/ACTmp").GetComponent<TMP_Text>();
        this.BChoiceTMP = thisParent.transform.Find("BCBtn/BCTmp").GetComponent<TMP_Text>();
        this.CChoiceTMP = thisParent.transform.Find("CCBtn/CCTmp").GetComponent<TMP_Text>();
        this.DChoiceTMP = thisParent.transform.Find("DCBtn/DCTmp").GetComponent<TMP_Text>();


        this.AChoiceButton = thisParent.transform.Find("ACBtn").GetComponent<Button>();
        this.BChoiceButton = thisParent.transform.Find("BCBtn").GetComponent<Button>();
        this.CChoiceButton = thisParent.transform.Find("CCBtn").GetComponent<Button>();
        this.DChoiceButton = thisParent.transform.Find("DCBtn").GetComponent<Button>();

        this.AChoiceButtonG = thisParent.transform.Find("ACBtn").gameObject;
        this.BChoiceButtonG = thisParent.transform.Find("BCBtn").gameObject;
        this.CChoiceButtonG = thisParent.transform.Find("CCBtn").gameObject;
        this.DChoiceButtonG = thisParent.transform.Find("DCBtn").gameObject;

        this.AChoiceButton.onClick.AddListener(ChoiceASelected);
        this.BChoiceButton.onClick.AddListener(ChoiceBSelected);
        this.CChoiceButton.onClick.AddListener(ChoiceCSelected);
        this.DChoiceButton.onClick.AddListener(ChoiceDSelected);

        this.QuizQuestion.text = Qstn;
        this.AChoiceTMP.text = QC1;
        this.BChoiceTMP.text = QC2;
        this.CChoiceTMP.text = QC3;
        this.DChoiceTMP.text = QC4;
    }

    private void ChoiceASelected()
    {
        if (isSubmitted) { return; }

        int QuestionNum = int.Parse(QstNum);

        if (!ChoiceSelected)
        {
            if (this.ThisRightAnswer == RightAnswer.A)
            {
                CVMain.QuizHandler.QuizScore++;
            }

            CVMain.QuizHandler.QuizAnswers[QuestionNum] = "A";

            ChoiceSelected = true;
            CVMain.QuizHandler.QuizAnswerCount++;
            this.ThisSelectedChoice = SelectedChoice.A;
            this.BChoiceButton.interactable = false;
            this.CChoiceButton.interactable = false;
            this.DChoiceButton.interactable = false;
        }
        else
        {
            if (this.ThisRightAnswer == RightAnswer.A)
            {
                CVMain.QuizHandler.QuizScore--;
            }

            ChoiceSelected = false;
            CVMain.QuizHandler.QuizAnswerCount--;
            this.ThisSelectedChoice = SelectedChoice.None;
            this.BChoiceButton.interactable = true;
            this.CChoiceButton.interactable = true;
            this.DChoiceButton.interactable = true;
        }
    }

    private void ChoiceBSelected()
    {
        if (isSubmitted) { return; }

        int QuestionNum = int.Parse(QstNum);

        if (!ChoiceSelected)
        {
            if (this.ThisRightAnswer == RightAnswer.B)
            {
                CVMain.QuizHandler.QuizScore++;
            }

            CVMain.QuizHandler.QuizAnswers[QuestionNum] = "B";

            ChoiceSelected = true;
            CVMain.QuizHandler.QuizAnswerCount++;
            this.ThisSelectedChoice = SelectedChoice.B;
            this.AChoiceButton.interactable = false;
            this.CChoiceButton.interactable = false;
            this.DChoiceButton.interactable = false;
        }
        else
        {
            if (this.ThisRightAnswer == RightAnswer.B)
            {
                CVMain.QuizHandler.QuizScore--;
            }

            CVMain.QuizHandler.QuizAnswers[QuestionNum] = string.Empty;

            ChoiceSelected = false;
            CVMain.QuizHandler.QuizAnswerCount--;
            this.ThisSelectedChoice = SelectedChoice.None;
            this.AChoiceButton.interactable = true;
            this.CChoiceButton.interactable = true;
            this.DChoiceButton.interactable = true;
        }
    }

    private void ChoiceCSelected()
    {
        if (isSubmitted) { return; }

        int QuestionNum = int.Parse(QstNum);

        if (!ChoiceSelected)
        {
            if (this.ThisRightAnswer == RightAnswer.C)
            {
                CVMain.QuizHandler.QuizScore++;
            }

            CVMain.QuizHandler.QuizAnswers[QuestionNum] = "C";

            ChoiceSelected = true;
            CVMain.QuizHandler.QuizAnswerCount++;
            this.ThisSelectedChoice = SelectedChoice.C;
            this.AChoiceButton.interactable = false;
            this.BChoiceButton.interactable = false;
            this.DChoiceButton.interactable = false;
        }
        else
        {
            if (this.ThisRightAnswer == RightAnswer.C)
            {
                CVMain.QuizHandler.QuizScore--;
            }

            CVMain.QuizHandler.QuizAnswers[QuestionNum] = string.Empty;

            ChoiceSelected = false;
            CVMain.QuizHandler.QuizAnswerCount--;
            this.ThisSelectedChoice = SelectedChoice.None;
            this.AChoiceButton.interactable = true;
            this.BChoiceButton.interactable = true;
            this.DChoiceButton.interactable = true;
        }
    }

    private void ChoiceDSelected()
    {
        if (isSubmitted) { return; }

        int QuestionNum = int.Parse(QstNum);

        if (!ChoiceSelected)
        {
            if (this.ThisRightAnswer == RightAnswer.D)
            {
                CVMain.QuizHandler.QuizScore++;
            }

            CVMain.QuizHandler.QuizAnswers[QuestionNum] = "D";

            ChoiceSelected = true;
            CVMain.QuizHandler.QuizAnswerCount++;
            this.ThisSelectedChoice = SelectedChoice.D;
            this.AChoiceButton.interactable = false;
            this.BChoiceButton.interactable = false;
            this.CChoiceButton.interactable = false;
        }
        else
        {
            if (this.ThisRightAnswer == RightAnswer.D)
            {
                CVMain.QuizHandler.QuizScore--;
            }

            CVMain.QuizHandler.QuizAnswers[QuestionNum] = string.Empty;

            ChoiceSelected = false;
            CVMain.QuizHandler.QuizAnswerCount--;
            this.ThisSelectedChoice = SelectedChoice.None;
            this.AChoiceButton.interactable = true;
            this.BChoiceButton.interactable = true;
            this.CChoiceButton.interactable = true;
        }
    }

    public void ShowRightAnswers()
    {
        if (isSubmitted) { return; }

        isSubmitted = true;

        if (this.ThisRightAnswer == RightAnswer.A)
        {
            this.AChoiceButtonG.SetActive(true);
            this.BChoiceButtonG.SetActive(false);
            this.CChoiceButtonG.SetActive(false);
            this.DChoiceButtonG.SetActive(false);
        }
        
        if (this.ThisRightAnswer == RightAnswer.B)
        {
            this.AChoiceButtonG.SetActive(false);
            this.BChoiceButtonG.SetActive(true);
            this.CChoiceButtonG.SetActive(false);
            this.DChoiceButtonG.SetActive(false);
        }
        
        if (this.ThisRightAnswer == RightAnswer.C)
        {
            this.AChoiceButtonG.SetActive(false);
            this.BChoiceButtonG.SetActive(false);
            this.CChoiceButtonG.SetActive(true);
            this.DChoiceButtonG.SetActive(false);
        }
        
        if (this.ThisRightAnswer == RightAnswer.D)
        {
            this.AChoiceButtonG.SetActive(false);
            this.BChoiceButtonG.SetActive(false);
            this.CChoiceButtonG.SetActive(false);
            this.DChoiceButtonG.SetActive(true);
        }
    }
}

public enum SelectedChoice { None, A, B, C, D } 
