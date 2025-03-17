using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

[Serializable]
public class FinishedQuest
{
    #region
    public CVMain CVMain;
    public FinishedQuest (CVMain CVmain)
    {
        CVMain = CVmain;
    }
    #endregion

    public Dictionary<string, string> QuestDictionary = new Dictionary<string, string>();

    public GameObject QstLoc;
    public GameObject QstPref;

    public void UpdateQuestList()
    {
        QstLoc = GameObject.Find("SCL");
        QstPref = Resources.Load<GameObject>("CanvasPrefabs/FinishedQuests");

        Reset();

        foreach (KeyValuePair<string, string> quest in QuestDictionary)
        {
            GameObject GO = CVMain.Instantiate(QstPref);
            GO.transform.SetParent(QstLoc.transform, false);
            TMP_Text title = GO.transform.Find("QuestNumN").GetComponent<TMP_Text>();
            TMP_Text desc = GO.transform.Find("QuestDescN").GetComponent<TMP_Text>();

            title.text = quest.Key;
            desc.text = quest.Value;
        }
    }

    public void Reset()
    {
        foreach (Transform child in QstLoc.transform) 
        {
            CVMain.Destroy(child.gameObject);
        }
    }
}
