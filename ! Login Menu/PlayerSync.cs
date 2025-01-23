using Firebase.Extensions;
using Firebase.Firestore;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class PlayerSync
{
    #region
    public CanvasHolder CVHolder;
    public PlayerSync (CanvasHolder cVHolder)
    {
        CVHolder = cVHolder;
    }
    #endregion

    public void UserSync ()
    {
        string email = CVHolder.PlayerData.PlayerEmail;
        string name = CVHolder.PlayerData.PlayerName;

        CVHolder.PlayerData.Level1 = "No Save";
        CVHolder.PlayerData.Level2 = "No Save";
        CVHolder.PlayerData.Level3 = "No Save";
        CVHolder.PlayerData.Level4 = "No Save";
        CVHolder.PlayerData.Level5 = "No Save";

        CVHolder.FirebaseFirestore.Collection("userAssessments").WhereEqualTo("email", email).GetSnapshotAsync().ContinueWithOnMainThread(task =>
        {
            if (task.IsCompleted)
            {
                QuerySnapshot snapshot = task.Result; 

                if (snapshot.Count > 0)
                {
                    foreach (DocumentSnapshot document in snapshot.Documents)
                    {
                        if (document.Exists)
                        {
                            Dictionary<string, object> data = document.ToDictionary();

                            var savedprogress = data["savedprogress"];
                            var selectedchar = data["selectedchar"];
                            var playerx = data["playerx"];
                            var playery = data["playery"];
                            var playerz = data["playerz"];

                            
                            
                            if (savedprogress.ToString() != "")
                            {
                                CVHolder.PlayerData.PlayerProgress = savedprogress.ToString();
                                CVHolder.PlayerData.CharacterSelected = Enum.TryParse(selectedchar.ToString(), out CharacterType gender) ? gender: CharacterType.Male;
                                float x = float.Parse(playerx.ToString());
                                float y = float.Parse(playery.ToString());
                                float z = float.Parse(playerz.ToString());
                                CVHolder.PlayerData.PlayerPosition = new Vector3(x, y, z);

                                CVHolder.CanvasHandler.LoadPastButton.interactable = true;
                                for (int chapter = 1; chapter <= 3; chapter++)
                                {
                                    string chapterKey = $"Chapter {chapter}";
                                    if (data.ContainsKey(chapterKey))
                                    {
                                        var chapterData = data[chapterKey] as Dictionary<string, object>;

                                        int[] levels = chapter == 1 ? new int[] { 1, 2 } :
                                                       chapter == 2 ? new int[] { 3, 4 } :
                                                       new int[] { 5 };

                                        foreach (int level in levels)
                                        {
                                            string levelKey = $"Level {level}";
                                            if (chapterData != null && chapterData.ContainsKey(levelKey))
                                            {
                                                var levelData = chapterData[levelKey] as Dictionary<string, object>;
                                                if (levelData != null && levelData.ContainsKey("progress"))
                                                {
                                                    var levelProgress = levelData["progress"];

                                                    switch (level)
                                                    {
                                                        case 1: CVHolder.PlayerData.Level1 = levelProgress.ToString() == "0" ? "No Save" : levelProgress.ToString(); break;
                                                        case 2: CVHolder.PlayerData.Level2 = levelProgress.ToString() == "0" ? "No Save" : levelProgress.ToString(); break;
                                                        case 3: CVHolder.PlayerData.Level3 = levelProgress.ToString() == "0" ? "No Save" : levelProgress.ToString(); break;
                                                        case 4: CVHolder.PlayerData.Level4 = levelProgress.ToString() == "0" ? "No Save" : levelProgress.ToString(); break;
                                                        case 5: CVHolder.PlayerData.Level5 = levelProgress.ToString() == "0" ? "No Save" : levelProgress.ToString(); break;
                                                    }
                                                }
                                            }
                                        }
                                    }
                                }
                                return;
                            }

                            CVHolder.CanvasHandler.LoadPastButton.interactable = false;
                        }
                    }
                }
            }
        });
    }
}
