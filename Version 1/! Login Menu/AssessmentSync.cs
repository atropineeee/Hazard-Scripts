using Firebase.Extensions;
using Firebase.Firestore;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class AssessmentSync
{
    #region
    public CanvasHolder CanvasHolder;
    public AssessmentSync (CanvasHolder Canvasholder)
    {
        CanvasHolder = Canvasholder;
    }
    #endregion

    public void SyncUser()
    {
        string email = CanvasHolder.PlayerData.PlayerEmail;
        string name = CanvasHolder.PlayerData.PlayerName;

        CanvasHolder.FirebaseFirestore.Collection("userAssessments").WhereEqualTo("email", email).GetSnapshotAsync().ContinueWithOnMainThread(task =>
        {
            if (task.IsCompleted) 
            {
                QuerySnapshot snapshot = task.Result;
                if (snapshot.Count > 0) { Debug.Log("User already Exists!"); return; }

                DocumentReference newUser = CanvasHolder.FirebaseFirestore.Collection("userAssessments").Document();

                Dictionary<string, object> chapter1Levels = new Dictionary<string, object>
                {
                    { "Level 1", new Dictionary<string, object> { { "score", 0 }, { "progress", 0 }, { "answers", new string[] { } } } },
                    { "Level 2", new Dictionary<string, object> { { "score", 0 }, { "progress", 0 }, { "answers", new string[] { } } } }
                };

                Dictionary<string, object> chapter2Levels = new Dictionary<string, object>
                {
                   { "Level 3", new Dictionary<string, object> { { "score", 0 }, { "progress", 0 }, { "answers", new string[] { } } } },
                   { "Level 4", new Dictionary<string, object> { { "score", 0 }, { "progress", 0 }, { "answers", new string[] { } } } }
                };

                Dictionary<string, object> chapter3Levels = new Dictionary<string, object>
                {
                    { "Level 5", new Dictionary<string, object> { { "score", 0 }, { "progress", 0 }, { "answers", new string[] { } } } },
                };

                Dictionary<string, object> userData = new Dictionary<string, object>
                {
                    { "email", email },
                    { "name", name },
                    { "savedprogress", "" },
                    { "selectedchar", "" },
                    { "playerx", "" },
                    { "playery", "" },
                    { "playerz", "" },
                    { "Chapter 1", chapter1Levels },
                    { "Chapter 2", chapter2Levels },
                    { "Chapter 3", chapter3Levels },
                    { "achievements", new object[] { } }
                };

                newUser.SetAsync(userData);
            }
        });
    }
}
