using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class StoryDials
{
    [Header("Speaker Name")]
    public string SpeakerName;
    public string SpeakerDialogues;
    public AudioClip SpeakerAudio;

    [Header("Camera Positions")]
    public Vector3 CameraPosition;
    public Vector3 CameraRotation;
}
