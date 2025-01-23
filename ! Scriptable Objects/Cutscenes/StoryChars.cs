using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class StoryChars
{
    [Header("Character Name & Prefab")]
    public string CharacterName;
    public GameObject CharacterPrefab;

    [Header("Chracter Rotation")]
    public Vector3 CharacterPosition;
    public Vector3 CharacterRotation;
}
