using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

[Serializable]
public class SyncHistory
{
    #region
    public CVMain CVMain;
    public SyncHistory (CVMain CVmain)
    {
        CVMain = CVmain;
    }
    #endregion

    public QuizSO[] QuizList;

    public GameObject QOLoc;

    public GameObject QOPref;

    public void Start()
    {
        QOLoc = GameObject.Find("JTopScrol");

        QOPref = Resources.Load<GameObject>("CanvasPrefabs/MyaTop");
     
        QuizList = Resources.LoadAll<QuizSO>("Scriptable Objects/QuizList");
    }

    public void RefreshHistory()
    {
        ResetHistory();
        foreach (QuizSO quiz in QuizList) 
        {
            GameObject QButton = CVMain.Instantiate(QOPref);
            QButton.transform.SetParent(QOLoc.transform, false);

            TMP_Text text = QButton.transform.Find("QN").GetComponent<TMP_Text>();
            text.text = quiz.name;

            SelBtn selBtn = QButton.GetComponent<SelBtn>();
            selBtn.AssignedQuiz = quiz;
        }
    }

    public void ResetHistory()
    {
        foreach(Transform child in QOLoc.transform)
        {
            CVMain.Destroy(child.gameObject);
        }
    }
}
