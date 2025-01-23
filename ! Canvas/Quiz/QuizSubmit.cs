using Firebase.Auth;
using Firebase.Database;
using Firebase.Firestore;
using Firebase;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Firebase.Extensions;

public class QuizSubmit
{
    #region
    public CVMain CVMain;
    public QuizSubmit (CVMain cVMain)
    {
        CVMain = cVMain;
    }
    #endregion

    public DependencyStatus FirebaseStatus;
    public FirebaseAuth FirebaseAuth;
    public FirebaseUser FirebaseUser;
    public FirebaseFirestore FirebaseFirestore;
    public DatabaseReference databaseReference;

    public void SubmitTheQuiz(int score, string quizchap, string quizlevel, string[] quizanswers)
    {
        FirebaseFirestore = FirebaseFirestore.DefaultInstance;
        string currentEmail = CVMain.SavedData.PlayerEmail;
        FirebaseFirestore.Collection("userAssessments").WhereEqualTo("email", currentEmail).GetSnapshotAsync().ContinueWithOnMainThread(task =>
        {
            if (task.IsCompleted) 
            {
                QuerySnapshot snapshot = task.Result;

                if (snapshot.Count > 0)
                {
                    foreach (DocumentSnapshot doc in snapshot.Documents) 
                    {
                        DocumentReference docRef = doc.Reference;

                        Dictionary<string, object> UpdatedScore = new Dictionary<string, object>
                        {
                            { "score", score },
                            { "progress", 100 },
                            { "answers", quizanswers }
                        };
                            
                        string updatePath = $"{quizchap}.{quizlevel}";

                        docRef.UpdateAsync(new Dictionary<string, object>
                        {
                            { updatePath, UpdatedScore },
                        });
                    }
                }
            }
        });
    }
}