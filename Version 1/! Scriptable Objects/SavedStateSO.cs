using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Create Saved Data", menuName = "Saved Data")]
public class SavedStateSO : ScriptableObject
{
    [Header("Player Position")]
    public Vector3 PlayerPosition;
    public Vector3 PlayerRotation;

    [Header("Player Email")]
    public string PlayerEmail;
    public string PlayerPassw;
    public string PlayerName;
    public string PlayerProgress;
    public string Level1 = "No Save";
    public string Level2 = "No Save";
    public string Level3 = "No Save";
    public string Level4 = "No Save";
    public string Level5 = "No Save";
    public bool newGame;

    [Header("Volumes")]
    [Range(0, 10)] public int MasterVolume = 5;
    [Range(0, 10)] public int VoiceOver = 5;
    [Range(0, 10)] public int Environment = 5;

    public CharacterType CharacterSelected;
    public LevelSelected LevelSelected;
}

public enum CharacterType { Male, Female }
public enum LevelSelected { Ch1L1, Ch1L2, Ch2L1, Ch2L2, Ch3L1, Ch3L2, }
