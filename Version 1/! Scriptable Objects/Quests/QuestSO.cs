using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Quest", menuName = "Quest List / Create New Quest")]
public class QuestSO : ScriptableObject
{
    [Header("Quest Name")]
    public string QuestToDO;
    public string QuestDescription;
    public string QuestSaveName;
    public bool IsLastQuest;
    public QuizSO QuizToAssign;

    public int QuestPercentage;

    [Header("Item To PickUp")]
    public string PickUpItemName;
    public string PickUpItemDescription;

    public string AchievementName;
    public string AchievementDescription;

    [Header("Showable Dialogue")]
    public string ShowableDialogue;
    public AudioClip DialogueAudio;

    [Header("Locator")]
    public Vector3 QuestLocation;

    [Header("Next Quest")]
    public QuestSO NextQuest;

    [Header("Event Toggle")]
    public EventToToggle EventToggle;

    [Header("Interactable NPC For Next Quest ONLY")]
    public string PrefabName;
    public GameObject Prefab;
    public Vector3 PrefabPosition;
    public Vector3 PrefabRotation;

    [Header("Remove Prefab From Prev Quest")]
    public string PrevPrefabName;
}

public enum EventToToggle { None, Rain, EarthQuake, Tsunami, Volcano }
