using Firebase.Auth;
using Firebase.Database;
using Firebase.Firestore;
using Firebase;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Firebase.Extensions;
using TMPro;

[Serializable]
public class SyncAch
{
    #region
    public CVMain CVMain;
    public SyncAch(CVMain CVmain)
    {
        CVMain = CVmain;
    }
    #endregion

    public GameObject AcvPref;
    public GameObject AcvLoc;

    [Header("Firebase Auth")]
    public DependencyStatus FirebaseStatus;
    public FirebaseAuth FirebaseAuth;
    public FirebaseUser FirebaseUser;
    public FirebaseFirestore FirebaseFirestore;
    public DatabaseReference databaseReference;

    public SavedStateSO SavedData;

    public void Sync()
    {
        AcvPref = Resources.Load<GameObject>("CanvasPrefabs/ACVPnl");
        AcvLoc = GameObject.Find("ABotScrl");
        SavedData = Resources.Load<SavedStateSO>("Scriptable Objects/Player Data/Saved Data");
        Reset();

        string email = SavedData.PlayerEmail;

        FirebaseFirestore = FirebaseFirestore.DefaultInstance;

        FirebaseFirestore.Collection("userAssessments")
            .WhereEqualTo("email", email)
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
                            if (doc.TryGetValue("achievements", out Dictionary<string, object> achievements))
                            {
                                foreach (var achievement in achievements)
                                {
                                    string acvName = achievement.Key;

                                    if (achievement.Value is Dictionary<string, object> achievementDetails)
                                    {
                                        if (achievementDetails.TryGetValue("acvDesc", out object acvDesc))
                                        {
                                            GameObject Nya = CVMain.Instantiate(AcvPref);
                                            Nya.transform.SetParent(AcvLoc.transform, false);

                                            TMP_Text acvNames = Nya.transform.Find("AcvNmTMP").GetComponent<TMP_Text>();
                                            TMP_Text acvDescs = Nya.transform.Find("AcvDesTMP").GetComponent<TMP_Text>();

                                            acvNames.text = acvName;
                                            acvDescs.text = acvDesc.ToString();
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
            });
    }



    public void Reset()
    {
        foreach(Transform child in AcvLoc.transform)
        {
            CVMain.Destroy(child.gameObject);
        }
    }
}
