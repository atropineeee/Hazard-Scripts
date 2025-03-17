using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New NPC Dialogue", menuName = "NPC Dialogue / Create New NPC Dialogue")]
public class NpcSO : ScriptableObject
{
    [Header("NPC Name")]
    public string NPCName;

    [Header("Dialogues")]
    public string[] NPCDialogues;

    [Header("Proceed Next Quest")]
    public QuestSO NestQuestSO;
}
