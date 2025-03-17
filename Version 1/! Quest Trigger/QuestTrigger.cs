using Firebase.Auth;
using Firebase.Database;
using Firebase.Firestore;
using Firebase;
using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using Firebase.Extensions;
using UnityEngine.UI;

public class QuestTrigger : MonoBehaviour
{
    [Header("Assigned Quest")]
    public QuestSO AssignedSO;
    public bool IsTriggered;
    public bool canTrigger = false;

    public AudioSource AudSource;

    [Header("Quest Trigger Maker")]
    public GameObject QTrigger;
    public GameObject QTriggerText;
    public GameObject QTriggerLocation;

    public GameObject ShowableDials;
    public GameObject ShowableDialLocation; 
    
    public GameObject CompletedQuest;
    public GameObject CompletedQuestLocation;

    public GameObject MarkerTrigger;
    public GameObject MarkerLocation;

    public GameObject PickerTrigger;
    public GameObject PickerLocation;

    public GameObject AcvTrigger;
    public GameObject AcvLocation;

    public Slider ProgSlider;
    public TMP_Text ProgText;

    [Header("This Object")]
    public GameObject thisGameObject;

    [Header("Firebase Auth")]
    public DependencyStatus FirebaseStatus;
    public FirebaseAuth FirebaseAuth;
    public FirebaseUser FirebaseUser;
    public FirebaseFirestore FirebaseFirestore;
    public DatabaseReference databaseReference;

    protected CVMain CVMain;

    private void Start()
    {
        this.thisGameObject = this.gameObject;
        CVMain = GameObject.Find("CanvasC").GetComponent<CVMain>();

        this.QTrigger = Resources.Load<GameObject>("World Prefabs/QuestTrigger");
        this.QTriggerLocation = GameObject.Find("SideQPanel");
        this.QTriggerText = Resources.Load<GameObject>("CanvasPrefabs/QuestMPanel");

        this.ShowableDials = Resources.Load<GameObject>("CanvasPrefabs/ShowableDial");
        this.ShowableDialLocation = GameObject.Find("Showables");

        this.CompletedQuest = Resources.Load<GameObject>("CanvasPrefabs/QuestCompleted");
        this.CompletedQuestLocation = GameObject.Find("Completed");

        this.MarkerTrigger = Resources.Load<GameObject>("CanvasPrefabs/Locator");
        this.MarkerLocation = GameObject.Find("LocatorP");

        this.PickerTrigger = Resources.Load<GameObject>("CanvasPrefabs/PickedUp");
        this.PickerLocation = GameObject.Find("ComP");

        this.AcvTrigger = Resources.Load<GameObject>("CanvasPrefabs/ACP");
        this.AcvLocation = GameObject.Find("AcvPnl");

        this.ProgSlider = GameObject.Find("StrSld").GetComponent<Slider>();
        this.ProgText = GameObject.Find("StrTMP").GetComponent<TMP_Text>();

        this.AudSource = GameObject.Find("Player").GetComponent<AudioSource>();

        StartCoroutine(nya());
    }

    IEnumerator nya()
    {
        yield return new WaitForSeconds(0.5f);
        canTrigger = true;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!canTrigger) { return; }
        if (!other.CompareTag("Player")) { return; }
        if (this.AssignedSO == null) { return; }

        if (!this.IsTriggered)
        {
            this.IsTriggered = true;    
            ResetItems();
            SynctoDatabase();
            ResetMarker();

            if (AssignedSO.PickUpItemName != "")
            {
                GameObject PickTrigger = Instantiate(PickerTrigger);
                PickTrigger.transform.SetParent(PickerLocation.transform, false);

                Image ing = PickTrigger.transform.Find("PckImg").GetComponent<Image>();
                TMP_Text tmp = PickTrigger.transform.Find("PckTmp").GetComponent<TMP_Text>();

                Sprite itemSprite = Resources.Load<Sprite>($"Inventory/{AssignedSO.PickUpItemName}");

                ing.sprite = itemSprite;
                tmp.text = AssignedSO.PickUpItemName + "\n" + AssignedSO.PickUpItemDescription;

                Destroy(PickTrigger, 2.5f);

                for (int i = 0; i < CVMain.Inventory.InventorySlots.Length; i++)
                {
                    if (string.IsNullOrEmpty(CVMain.Inventory.InventorySlots[i]))
                    {
                        CVMain.Inventory.InventorySlots[i] = AssignedSO.PickUpItemName;
                        break;
                    }
                }
            }

            if (!string.IsNullOrWhiteSpace(AssignedSO.AchievementName) && !string.IsNullOrWhiteSpace(AssignedSO.AchievementDescription))
            {
                GameObject ach = Instantiate(AcvTrigger);
                ach.transform.SetParent(AcvLocation.transform, false);

                TMP_Text tmp = ach.transform.Find("AcName").GetComponent <TMP_Text>();
                tmp.text = AssignedSO.AchievementName;

                Destroy(ach, 2.5f);

                SyncAchievements(AssignedSO.AchievementName, AssignedSO.AchievementDescription);
            }

            if (AssignedSO.EventToggle != EventToToggle.None) 
            {
                CVMain.EventToggle.CurrentEvent = AssignedSO.EventToggle;
            }

            if (AssignedSO.IsLastQuest)
            {
                GameObject CreateCompleted = Instantiate(CompletedQuest);
                CreateCompleted.transform.SetParent(CompletedQuestLocation.transform, false);

                StartCoroutine(StartQuiz());

                Destroy(CreateCompleted, 3.5f);
                return;
            }

            ProgSlider.value = this.AssignedSO.QuestPercentage;
            ProgText.text = "Story Progress: (" +  this.AssignedSO.QuestPercentage + "%)";
            UpdateQuestDictionary();

            if (this.AssignedSO.DialogueAudio != null)
            {
                this.AudSource.clip = this.AssignedSO.DialogueAudio;
                this.AudSource.Play();
            }

            if (AssignedSO.PrevPrefabName != "") 
            {
                GameObject objToRemove = GameObject.Find(AssignedSO.PrevPrefabName + " QuestPrefab");
                Destroy(objToRemove);
            }

            if (AssignedSO.ShowableDialogue != "")
            {
                GameObject CreateShowable = Instantiate(ShowableDials);
                CreateShowable.transform.SetParent(ShowableDialLocation.transform, false);
                FLT flt = CreateShowable.GetComponent<FLT>();
                flt.StartTextAnim(AssignedSO.ShowableDialogue); 
            }

            if (AssignedSO.Prefab != null)
            {
                GameObject NPCPerson = Instantiate(AssignedSO.Prefab);
                NPCPerson.name = AssignedSO.PrefabName + " QuestPrefab";

                if (AssignedSO.PrefabPosition != Vector3.zero)
                {
                    NPCPerson.transform.localPosition = AssignedSO.PrefabPosition;
                }
                else
                {
                    NPCPerson.transform.localPosition = AssignedSO.NextQuest.QuestLocation;
                }

                NPCPerson.transform.localRotation = Quaternion.Euler(AssignedSO.PrefabRotation);
            }

            CreateQuestTrigger();
        }
    }

    private void UpdateQuestDictionary()
    {
        CVMain.FinishedQuest.QuestDictionary.Add(this.AssignedSO.QuestDescription, this.AssignedSO.QuestToDO);
    }

    private void CreateQuestTrigger()
    {
        GameObject QuestTriggerObj = CVMain.Instantiate(QTrigger);
        QuestTriggerObj.name = AssignedSO.NextQuest.name;
        QuestTriggerObj.transform.localPosition = AssignedSO.NextQuest.QuestLocation;

        GameObject QuestTriggerText = CVMain.Instantiate(QTriggerText);
        QuestTriggerText.transform.SetParent(QTriggerLocation.transform, false);
        TMP_Text QP = QuestTriggerText.transform.Find("QuestMTMP").GetComponent<TMP_Text>();
        QP.text = AssignedSO.NextQuest.QuestToDO;

        QuestTrigger QuestTrigScript = QuestTriggerObj.GetComponent<QuestTrigger>();
        QuestTrigScript.AssignedSO = AssignedSO.NextQuest;

        GameObject LocatorObj = Instantiate(MarkerTrigger);
        LocatorObj.transform.SetParent(MarkerLocation.transform, false);
        Markers mark = LocatorObj.GetComponent<Markers>();
        mark.Target = QuestTriggerObj.transform;
    }

    private void ResetItems()
    {
        foreach (Transform child in QTriggerLocation.transform)
        {
            CVMain.Destroy(child.gameObject);
        }
    }

    private void SynctoDatabase()
    {
        FirebaseFirestore = FirebaseFirestore.DefaultInstance;
        string currentEmail = CVMain.SavedData.PlayerEmail;

        FirebaseFirestore.Collection("userAssessments")
            .WhereEqualTo("email", currentEmail)
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
                            DocumentReference docRef = doc.Reference;
                            Dictionary<string, object> updatedData = new Dictionary<string, object>
                            {
                                { "playerx", this.transform.position.x },
                                { "playery", this.transform.position.y },
                                { "playerz", this.transform.position.z },
                                { "savedprogress", this.AssignedSO.QuestSaveName },
                                { "selectedchar", CVMain.SavedData.CharacterSelected.ToString() }
                            };

                            docRef.UpdateAsync(updatedData).ContinueWithOnMainThread(updateTask =>
                            {
                                if (updateTask.IsCompleted)
                                {
                                    if (AssignedSO.QuizToAssign == null)
                                    {
                                        Destroy(this.gameObject);
                                    }
                                }
                            });
                        }
                    }
                }
            });
    }

    private void SyncAchievements(string acvName, string acvDesc)
    {
        FirebaseFirestore = FirebaseFirestore.DefaultInstance;
        string currentEmail = CVMain.SavedData.PlayerEmail;

        FirebaseFirestore.Collection("userAssessments")
            .WhereEqualTo("email", currentEmail)
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
                            DocumentReference docRef = doc.Reference;

                            docRef.GetSnapshotAsync().ContinueWithOnMainThread(docTask =>
                            {
                                if (docTask.IsCompleted)
                                {
                                    DocumentSnapshot docSnapshot = docTask.Result;

                                    Dictionary<string, object> achievements = docSnapshot.ContainsField("achievements") ?
                                        docSnapshot.GetValue<Dictionary<string, object>>("achievements") :
                                        new Dictionary<string, object>();

                                    if (!achievements.ContainsKey(acvName))
                                    {
                                        achievements[acvName] = new Dictionary<string, object>
                                        {
                                            { "acvDesc", acvDesc }
                                        };

                                        docRef.UpdateAsync(new Dictionary<string, object> { { "achievements", achievements } });
                                    }
                                }
                            });
                        }
                    }
                }
            });
    }

    private IEnumerator StartQuiz()
    {
        yield return new WaitForSeconds(4f);
        CVMain.CVMMenu.OpenQuizMenu();
        CVMain.QuizHandler.QuizBeingTaken = AssignedSO.QuizToAssign;
        CVMain.QuizHandler.CreateQuestionPanel();
        Destroy(this.thisGameObject);
    }

    private void ResetMarker()
    {
        foreach (Transform child in MarkerLocation.transform)
        {
            CVMain.Destroy(child.gameObject);
        }
    }
}
