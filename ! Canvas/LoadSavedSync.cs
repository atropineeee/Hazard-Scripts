    using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class LoadSavedSync
{
    #region
    public CVMain CVmain;
    public LoadSavedSync(CVMain cvmain)
    {
        CVmain = cvmain;
    }
    #endregion

    // Level 1
    private QuestSO Quest1;
    private QuestSO Quest2;
    private QuestSO Quest3;
    private QuestSO Quest4;
    private QuestSO Quest5;

    // Level 2
    private QuestSO Quest6;
    private QuestSO Quest7;
    private QuestSO Quest8;
    private QuestSO Quest9;
    private QuestSO Quest10;
    private QuestSO Quest11;
    private QuestSO Quest12;

    // Level 3
    private QuestSO Quest13;
    private QuestSO Quest14;
    private QuestSO Quest15;
    private QuestSO Quest16;
    private QuestSO Quest17;

    // Level 4
    private QuestSO Quest18;
    private QuestSO Quest19;
    private QuestSO Quest20;
    private QuestSO Quest21;

    // Level 5

    private QuestSO Quest22;
    private QuestSO Quest23;
    private QuestSO Quest24;
    private QuestSO Quest25;
    private QuestSO Quest26;
    private QuestSO Quest27;
    private QuestSO Quest28;
    private QuestSO Quest29;
    private QuestSO Quest30;
    private QuestSO Quest31;


    public void Start()
    {
        // Level 1
        Quest1 = Resources.Load<QuestSO>("Scriptable Objects/Quest List/Quest 1/Quest1");
        Quest2 = Resources.Load<QuestSO>("Scriptable Objects/Quest List/Quest 1/Quest1 2");
        Quest3 = Resources.Load<QuestSO>("Scriptable Objects/Quest List/Quest 1/Quest1 3");
        Quest4 = Resources.Load<QuestSO>("Scriptable Objects/Quest List/Quest 1/Quest1 4");
        Quest5 = Resources.Load<QuestSO>("Scriptable Objects/Quest List/Quest 1/Quest1 5");

        // Level 2
        Quest6 = Resources.Load<QuestSO>("Scriptable Objects/Quest List/Quest 2/Quest2");
        Quest7 = Resources.Load<QuestSO>("Scriptable Objects/Quest List/Quest 2/Quest2 1");
        Quest8 = Resources.Load<QuestSO>("Scriptable Objects/Quest List/Quest 2/Quest2 2");
        Quest9 = Resources.Load<QuestSO>("Scriptable Objects/Quest List/Quest 2/Quest2 3");
        Quest10 = Resources.Load<QuestSO>("Scriptable Objects/Quest List/Quest 2/Quest2 4");
        Quest11 = Resources.Load<QuestSO>("Scriptable Objects/Quest List/Quest 2/Quest2 5");
        Quest12 = Resources.Load<QuestSO>("Scriptable Objects/Quest List/Quest 2/Quest2 6");

        // Level 3
        Quest13 = Resources.Load<QuestSO>("Scriptable Objects/Quest List/Quest 3/Quest3");
        Quest14 = Resources.Load<QuestSO>("Scriptable Objects/Quest List/Quest 3/Quest3 1");
        Quest15 = Resources.Load<QuestSO>("Scriptable Objects/Quest List/Quest 3/Quest3 2");
        Quest16 = Resources.Load<QuestSO>("Scriptable Objects/Quest List/Quest 3/Quest3 3");
        Quest17 = Resources.Load<QuestSO>("Scriptable Objects/Quest List/Quest 3/Quest3 4");

        // Level 4
        Quest18 = Resources.Load<QuestSO>("Scriptable Objects/Quest List/Quest 4/Quest4");
        Quest19 = Resources.Load<QuestSO>("Scriptable Objects/Quest List/Quest 4/Quest4 1");
        Quest20 = Resources.Load<QuestSO>("Scriptable Objects/Quest List/Quest 4/Quest4 2");
        Quest21 = Resources.Load<QuestSO>("Scriptable Objects/Quest List/Quest 4/Quest4 3");

        // Level 5
        Quest22 = Resources.Load<QuestSO>("Scriptable Objects/Quest List/Quest 5/Quest5");
        Quest23 = Resources.Load<QuestSO>("Scriptable Objects/Quest List/Quest 5/Quest5 1");
        Quest24 = Resources.Load<QuestSO>("Scriptable Objects/Quest List/Quest 5/Quest5 2");
        Quest25 = Resources.Load<QuestSO>("Scriptable Objects/Quest List/Quest 5/Quest5 3");
        Quest26 = Resources.Load<QuestSO>("Scriptable Objects/Quest List/Quest 5/Quest5 4");
        Quest27 = Resources.Load<QuestSO>("Scriptable Objects/Quest List/Quest 5/Quest5 5");
        Quest28 = Resources.Load<QuestSO>("Scriptable Objects/Quest List/Quest 5/Quest5 6");
        Quest29 = Resources.Load<QuestSO>("Scriptable Objects/Quest List/Quest 5/Quest5 7");
        Quest30 = Resources.Load<QuestSO>("Scriptable Objects/Quest List/Quest 5/Quest5 8");
        Quest31 = Resources.Load<QuestSO>("Scriptable Objects/Quest List/Quest 5/Quest5 9");


        SyncPlayer();
    }

    public void SyncPlayer()
    {
        // Level 1

        if (CVmain.SavedData.PlayerProgress == "C1P1")
        {
            GameObject QuestTriggerObj = CVMain.Instantiate(CVmain.QuestPrefab);
            QuestTriggerObj.name = Quest1.name;
            QuestTriggerObj.transform.localPosition = Quest1.QuestLocation;
            QuestTrigger qt = QuestTriggerObj.GetComponent<QuestTrigger>();
            qt.AssignedSO = Quest1;

            CVmain.Player.transform.localPosition = CVmain.SavedData.PlayerPosition;
        }

        if (CVmain.SavedData.PlayerProgress == "C1P2")
        {
            GameObject QuestTriggerObj = CVMain.Instantiate(CVmain.QuestPrefab);
            QuestTriggerObj.name = Quest2.name;
            QuestTriggerObj.transform.localPosition = Quest2.QuestLocation;
            QuestTrigger qt = QuestTriggerObj.GetComponent<QuestTrigger>();
            qt.AssignedSO = Quest2;

            CVmain.Player.transform.localPosition = CVmain.SavedData.PlayerPosition;
        }

        if (CVmain.SavedData.PlayerProgress == "C1P3")
        {
            GameObject QuestTriggerObj = CVMain.Instantiate(CVmain.QuestPrefab);
            QuestTriggerObj.name = Quest3.name;
            QuestTriggerObj.transform.localPosition = Quest3.QuestLocation;
            QuestTrigger qt = QuestTriggerObj.GetComponent<QuestTrigger>();
            qt.AssignedSO = Quest3;

            CVmain.Player.transform.localPosition = CVmain.SavedData.PlayerPosition;
        }

        if (CVmain.SavedData.PlayerProgress == "C1P4")
        {
            GameObject QuestTriggerObj = CVMain.Instantiate(CVmain.QuestPrefab);
            QuestTriggerObj.name = Quest4.name;
            QuestTriggerObj.transform.localPosition = Quest4.QuestLocation;
            QuestTrigger qt = QuestTriggerObj.GetComponent<QuestTrigger>();
            qt.AssignedSO = Quest4;

            CVmain.Player.transform.localPosition = CVmain.SavedData.PlayerPosition;
        }

        if (CVmain.SavedData.PlayerProgress == "C1P5" && CVmain.SavedData.Level1 == "No Save")
        {
            GameObject QuestTriggerObj = CVMain.Instantiate(CVmain.QuestPrefab);
            QuestTriggerObj.name = Quest5.name;
            QuestTriggerObj.transform.localPosition = Quest5.QuestLocation;
            QuestTrigger qt = QuestTriggerObj.GetComponent<QuestTrigger>();
            qt.AssignedSO = Quest5;

            CVmain.Player.transform.localPosition = CVmain.SavedData.PlayerPosition;
        }

        if (CVmain.SavedData.PlayerProgress == "C1P5" && CVmain.SavedData.Level1 != "No Save")
        {
            GameObject QuestTriggerObj = CVMain.Instantiate(CVmain.QuestPrefab);
            QuestTriggerObj.name = Quest6.name;
            QuestTriggerObj.transform.localPosition = Quest6.QuestLocation;
            QuestTrigger qt = QuestTriggerObj.GetComponent<QuestTrigger>();
            qt.AssignedSO = Quest6;

            CVmain.Player.transform.localPosition = CVmain.SavedData.PlayerPosition;
        }

        // Level 2

        if (CVmain.SavedData.PlayerProgress == "C1P6")
        {
            GameObject QuestTriggerObj = CVMain.Instantiate(CVmain.QuestPrefab);
            QuestTriggerObj.name = Quest6.name;
            QuestTriggerObj.transform.localPosition = Quest6.QuestLocation;
            QuestTrigger qt = QuestTriggerObj.GetComponent<QuestTrigger>();
            qt.AssignedSO = Quest6;

            CVmain.Player.transform.localPosition = CVmain.SavedData.PlayerPosition;
        }

        if (CVmain.SavedData.PlayerProgress == "C1P7")
        {
            GameObject QuestTriggerObj = CVMain.Instantiate(CVmain.QuestPrefab);
            QuestTriggerObj.name = Quest7.name;
            QuestTriggerObj.transform.localPosition = Quest7.QuestLocation;
            QuestTrigger qt = QuestTriggerObj.GetComponent<QuestTrigger>();
            qt.AssignedSO = Quest7;

            CVmain.Player.transform.localPosition = CVmain.SavedData.PlayerPosition;
        }

        if (CVmain.SavedData.PlayerProgress == "C1P8")
        {
            GameObject QuestTriggerObj = CVMain.Instantiate(CVmain.QuestPrefab);
            QuestTriggerObj.name = Quest8.name;
            QuestTriggerObj.transform.localPosition = Quest8.QuestLocation;
            QuestTrigger qt = QuestTriggerObj.GetComponent<QuestTrigger>();
            qt.AssignedSO = Quest8;

            CVmain.Player.transform.localPosition = CVmain.SavedData.PlayerPosition;
        }

        if (CVmain.SavedData.PlayerProgress == "C1P9")
        {
            GameObject QuestTriggerObj = CVMain.Instantiate(CVmain.QuestPrefab);
            QuestTriggerObj.name = Quest9.name;
            QuestTriggerObj.transform.localPosition = Quest9.QuestLocation;
            QuestTrigger qt = QuestTriggerObj.GetComponent<QuestTrigger>();
            qt.AssignedSO = Quest9;

            CVmain.Player.transform.localPosition = CVmain.SavedData.PlayerPosition;
        }

        if (CVmain.SavedData.PlayerProgress == "C1P10")
        {
            GameObject QuestTriggerObj = CVMain.Instantiate(CVmain.QuestPrefab);
            QuestTriggerObj.name = Quest10.name;
            QuestTriggerObj.transform.localPosition = Quest10.QuestLocation;
            QuestTrigger qt = QuestTriggerObj.GetComponent<QuestTrigger>();
            qt.AssignedSO = Quest10;

            CVmain.Player.transform.localPosition = CVmain.SavedData.PlayerPosition;
        }

        if (CVmain.SavedData.PlayerProgress == "C1P11")
        {
            GameObject QuestTriggerObj = CVMain.Instantiate(CVmain.QuestPrefab);
            QuestTriggerObj.name = Quest11.name;
            QuestTriggerObj.transform.localPosition = Quest11.QuestLocation;
            QuestTrigger qt = QuestTriggerObj.GetComponent<QuestTrigger>();
            qt.AssignedSO = Quest11;

            CVmain.Player.transform.localPosition = CVmain.SavedData.PlayerPosition;
        }

        if (CVmain.SavedData.PlayerProgress == "C1P12" && CVmain.SavedData.Level2 == "No Save")
        {
            GameObject QuestTriggerObj = CVMain.Instantiate(CVmain.QuestPrefab);
            QuestTriggerObj.name = Quest12.name;
            QuestTriggerObj.transform.localPosition = Quest12.QuestLocation;
            QuestTrigger qt = QuestTriggerObj.GetComponent<QuestTrigger>();
            qt.AssignedSO = Quest12;

            CVmain.Player.transform.localPosition = CVmain.SavedData.PlayerPosition;
        }

        if (CVmain.SavedData.PlayerProgress == "C1P12" && CVmain.SavedData.Level2 != "No Save")
        {
            GameObject QuestTriggerObj = CVMain.Instantiate(CVmain.QuestPrefab);
            QuestTriggerObj.name = Quest13.name;
            QuestTriggerObj.transform.localPosition = Quest13.QuestLocation;
            QuestTrigger qt = QuestTriggerObj.GetComponent<QuestTrigger>();
            qt.AssignedSO = Quest13;

            CVmain.Player.transform.localPosition = CVmain.SavedData.PlayerPosition;
        }

        // Level 3

        if (CVmain.SavedData.PlayerProgress == "C2P1")
        {
            GameObject QuestTriggerObj = CVMain.Instantiate(CVmain.QuestPrefab);
            QuestTriggerObj.name = Quest13.name;
            QuestTriggerObj.transform.localPosition = Quest13.QuestLocation;
            QuestTrigger qt = QuestTriggerObj.GetComponent<QuestTrigger>();
            qt.AssignedSO = Quest13;

            CVmain.Player.transform.localPosition = CVmain.SavedData.PlayerPosition;
        }

        if (CVmain.SavedData.PlayerProgress == "C2P2")
        {
            GameObject QuestTriggerObj = CVMain.Instantiate(CVmain.QuestPrefab);
            QuestTriggerObj.name = Quest14.name;
            QuestTriggerObj.transform.localPosition = Quest14.QuestLocation;
            QuestTrigger qt = QuestTriggerObj.GetComponent<QuestTrigger>();
            qt.AssignedSO = Quest14;

            CVmain.Player.transform.localPosition = CVmain.SavedData.PlayerPosition;
        }

        if (CVmain.SavedData.PlayerProgress == "C2P3")
        {
            GameObject QuestTriggerObj = CVMain.Instantiate(CVmain.QuestPrefab);
            QuestTriggerObj.name = Quest15.name;
            QuestTriggerObj.transform.localPosition = Quest15.QuestLocation;
            QuestTrigger qt = QuestTriggerObj.GetComponent<QuestTrigger>();
            qt.AssignedSO = Quest15;

            CVmain.Player.transform.localPosition = CVmain.SavedData.PlayerPosition;
        }

        if (CVmain.SavedData.PlayerProgress == "C2P4")
        {
            GameObject QuestTriggerObj = CVMain.Instantiate(CVmain.QuestPrefab);
            QuestTriggerObj.name = Quest16.name;
            QuestTriggerObj.transform.localPosition = Quest16.QuestLocation;
            QuestTrigger qt = QuestTriggerObj.GetComponent<QuestTrigger>();
            qt.AssignedSO = Quest16;

            CVmain.Player.transform.localPosition = CVmain.SavedData.PlayerPosition;
        }

        if (CVmain.SavedData.PlayerProgress == "C2P5" && CVmain.SavedData.Level3 == "No Save")
        {
            GameObject QuestTriggerObj = CVMain.Instantiate(CVmain.QuestPrefab);
            QuestTriggerObj.name = Quest17.name;
            QuestTriggerObj.transform.localPosition = Quest17.QuestLocation;
            QuestTrigger qt = QuestTriggerObj.GetComponent<QuestTrigger>();
            qt.AssignedSO = Quest17;

            CVmain.Player.transform.localPosition = CVmain.SavedData.PlayerPosition;
        }

        if (CVmain.SavedData.PlayerProgress == "C2P5" && CVmain.SavedData.Level3 != "No Save")
        {
            GameObject QuestTriggerObj = CVMain.Instantiate(CVmain.QuestPrefab);
            QuestTriggerObj.name = Quest18.name;
            QuestTriggerObj.transform.localPosition = Quest18.QuestLocation;
            QuestTrigger qt = QuestTriggerObj.GetComponent<QuestTrigger>();
            qt.AssignedSO = Quest18;

            CVmain.Player.transform.localPosition = CVmain.SavedData.PlayerPosition;
        }

        // Level 4
        if (CVmain.SavedData.PlayerProgress == "C2P6")
        {
            GameObject QuestTriggerObj = CVMain.Instantiate(CVmain.QuestPrefab);
            QuestTriggerObj.name = Quest18.name;
            QuestTriggerObj.transform.localPosition = Quest18.QuestLocation;
            QuestTrigger qt = QuestTriggerObj.GetComponent<QuestTrigger>();
            qt.AssignedSO = Quest18;

            CVmain.Player.transform.localPosition = CVmain.SavedData.PlayerPosition;
        }

        if (CVmain.SavedData.PlayerProgress == "C2P7")
        {
            GameObject QuestTriggerObj = CVMain.Instantiate(CVmain.QuestPrefab);
            QuestTriggerObj.name = Quest19.name;
            QuestTriggerObj.transform.localPosition = Quest19.QuestLocation;
            QuestTrigger qt = QuestTriggerObj.GetComponent<QuestTrigger>();
            qt.AssignedSO = Quest19;

            CVmain.Player.transform.localPosition = CVmain.SavedData.PlayerPosition;
        }

        if (CVmain.SavedData.PlayerProgress == "C2P8")
        {
            GameObject QuestTriggerObj = CVMain.Instantiate(CVmain.QuestPrefab);
            QuestTriggerObj.name = Quest20.name;
            QuestTriggerObj.transform.localPosition = Quest20.QuestLocation;
            QuestTrigger qt = QuestTriggerObj.GetComponent<QuestTrigger>();
            qt.AssignedSO = Quest20;

            CVmain.Player.transform.localPosition = CVmain.SavedData.PlayerPosition;
        }

        if (CVmain.SavedData.PlayerProgress == "C2P9" && CVmain.SavedData.Level4 == "No Save")
        {
            GameObject QuestTriggerObj = CVMain.Instantiate(CVmain.QuestPrefab);
            QuestTriggerObj.name = Quest21.name;
            QuestTriggerObj.transform.localPosition = Quest21.QuestLocation;
            QuestTrigger qt = QuestTriggerObj.GetComponent<QuestTrigger>();
            qt.AssignedSO = Quest21;

            CVmain.Player.transform.localPosition = CVmain.SavedData.PlayerPosition;
        }

        if (CVmain.SavedData.PlayerProgress == "C2P9" && CVmain.SavedData.Level4 != "No Save")
        {
            GameObject QuestTriggerObj = CVMain.Instantiate(CVmain.QuestPrefab);
            QuestTriggerObj.name = Quest22.name;
            QuestTriggerObj.transform.localPosition = Quest22.QuestLocation;
            QuestTrigger qt = QuestTriggerObj.GetComponent<QuestTrigger>();
            qt.AssignedSO = Quest22;

            CVmain.Player.transform.localPosition = CVmain.SavedData.PlayerPosition;
        }

        // Level 5

        if (CVmain.SavedData.PlayerProgress == "C3P1")
        {
            GameObject QuestTriggerObj = CVMain.Instantiate(CVmain.QuestPrefab);
            QuestTriggerObj.name = Quest22.name;
            QuestTriggerObj.transform.localPosition = Quest22.QuestLocation;
            QuestTrigger qt = QuestTriggerObj.GetComponent<QuestTrigger>();
            qt.AssignedSO = Quest22;

            CVmain.Player.transform.localPosition = CVmain.SavedData.PlayerPosition;
        }

        if (CVmain.SavedData.PlayerProgress == "C3P2")
        {
            GameObject QuestTriggerObj = CVMain.Instantiate(CVmain.QuestPrefab);
            QuestTriggerObj.name = Quest23.name;
            QuestTriggerObj.transform.localPosition = Quest23.QuestLocation;
            QuestTrigger qt = QuestTriggerObj.GetComponent<QuestTrigger>();
            qt.AssignedSO = Quest23;

            CVmain.Player.transform.localPosition = CVmain.SavedData.PlayerPosition;
        }

        if (CVmain.SavedData.PlayerProgress == "C3P3")
        {
            GameObject QuestTriggerObj = CVMain.Instantiate(CVmain.QuestPrefab);
            QuestTriggerObj.name = Quest24.name;
            QuestTriggerObj.transform.localPosition = Quest24.QuestLocation;
            QuestTrigger qt = QuestTriggerObj.GetComponent<QuestTrigger>();
            qt.AssignedSO = Quest24;

            CVmain.Player.transform.localPosition = CVmain.SavedData.PlayerPosition;
        }

        if (CVmain.SavedData.PlayerProgress == "C3P4")
        {
            GameObject QuestTriggerObj = CVMain.Instantiate(CVmain.QuestPrefab);
            QuestTriggerObj.name = Quest25.name;
            QuestTriggerObj.transform.localPosition = Quest25.QuestLocation;
            QuestTrigger qt = QuestTriggerObj.GetComponent<QuestTrigger>();
            qt.AssignedSO = Quest25;

            CVmain.Player.transform.localPosition = CVmain.SavedData.PlayerPosition;
        }

        if (CVmain.SavedData.PlayerProgress == "C3P5")
        {
            GameObject QuestTriggerObj = CVMain.Instantiate(CVmain.QuestPrefab);
            QuestTriggerObj.name = Quest26.name;
            QuestTriggerObj.transform.localPosition = Quest26.QuestLocation;
            QuestTrigger qt = QuestTriggerObj.GetComponent<QuestTrigger>();
            qt.AssignedSO = Quest26;

            CVmain.Player.transform.localPosition = CVmain.SavedData.PlayerPosition;
        }

        if (CVmain.SavedData.PlayerProgress == "C3P6")
        {
            GameObject QuestTriggerObj = CVMain.Instantiate(CVmain.QuestPrefab);
            QuestTriggerObj.name = Quest27.name;
            QuestTriggerObj.transform.localPosition = Quest27.QuestLocation;
            QuestTrigger qt = QuestTriggerObj.GetComponent<QuestTrigger>();
            qt.AssignedSO = Quest27;

            CVmain.Player.transform.localPosition = CVmain.SavedData.PlayerPosition;
        }

        if (CVmain.SavedData.PlayerProgress == "C3P7")
        {
            GameObject QuestTriggerObj = CVMain.Instantiate(CVmain.QuestPrefab);
            QuestTriggerObj.name = Quest28.name;
            QuestTriggerObj.transform.localPosition = Quest28.QuestLocation;
            QuestTrigger qt = QuestTriggerObj.GetComponent<QuestTrigger>();
            qt.AssignedSO = Quest28;

            CVmain.Player.transform.localPosition = CVmain.SavedData.PlayerPosition;
        }

        if (CVmain.SavedData.PlayerProgress == "C3P8")
        {
            GameObject QuestTriggerObj = CVMain.Instantiate(CVmain.QuestPrefab);
            QuestTriggerObj.name = Quest29.name;
            QuestTriggerObj.transform.localPosition = Quest29.QuestLocation;
            QuestTrigger qt = QuestTriggerObj.GetComponent<QuestTrigger>();
            qt.AssignedSO = Quest29;

            CVmain.Player.transform.localPosition = CVmain.SavedData.PlayerPosition;
        }

        if (CVmain.SavedData.PlayerProgress == "C3P9")
        {
            GameObject QuestTriggerObj = CVMain.Instantiate(CVmain.QuestPrefab);
            QuestTriggerObj.name = Quest30.name;
            QuestTriggerObj.transform.localPosition = Quest30.QuestLocation;
            QuestTrigger qt = QuestTriggerObj.GetComponent<QuestTrigger>();
            qt.AssignedSO = Quest30;

            CVmain.Player.transform.localPosition = CVmain.SavedData.PlayerPosition;
        }

        if (CVmain.SavedData.PlayerProgress == "C3P10" && CVmain.SavedData.Level5 == "No Save")
        {
            GameObject QuestTriggerObj = CVMain.Instantiate(CVmain.QuestPrefab);
            QuestTriggerObj.name = Quest31.name;
            QuestTriggerObj.transform.localPosition = Quest31.QuestLocation;
            QuestTrigger qt = QuestTriggerObj.GetComponent<QuestTrigger>();
            qt.AssignedSO = Quest31;

            CVmain.Player.transform.localPosition = CVmain.SavedData.PlayerPosition;
        }
    }
}
