using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Story", menuName = "Story lines / Create New Story")]
public class StorySO : ScriptableObject
{
    [Header("Story Characters")]
    public StoryChars[] CharactersList;

    [Header("Story Lines")]
    public StoryDials[] StoryDialogues;

    [Header("Proceed Quest")]
    public QuestSO NewQuest;

    [Header("Start Location")]
    public Vector3 Postion;
}
