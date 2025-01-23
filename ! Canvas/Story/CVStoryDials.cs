using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

[Serializable]
public class CVStoryDials
{
    #region
    public CVMain CVMain;
    public CVStoryDials (CVMain cvMain)
    {
        CVMain = cvMain;
    }
    #endregion
    [Header("Current Story SO")]
    public StorySO CurrentStorySO;

    [Header("Speaker Number")]
    public int DialogueNumber;
    public int SpeakerCount;

    [Header("Quest Trigger Maker")]
    public GameObject QTrigger;
    public GameObject QTriggerText;
    public GameObject QTriggerLocation;
    public GameObject MarkerTrigger;
    public GameObject MarkerLocation;
    public void Start()
    {
        this.QTrigger = Resources.Load<GameObject>("World Prefabs/QuestTrigger");
        this.QTriggerLocation = GameObject.Find("SideQPanel");
        this.QTriggerText = Resources.Load<GameObject>("CanvasPrefabs/QuestMPanel");

        this.MarkerTrigger = Resources.Load<GameObject>("CanvasPrefabs/Locator");
        this.MarkerLocation = GameObject.Find("LocatorP");
    }
    
    public void CreateCharacters()
    {
        DeactivateCamera();
        foreach (StoryChars chars in CurrentStorySO.CharactersList)
        {
            GameObject Character = CVMain.Instantiate(chars.CharacterPrefab);
            Character.name = chars.CharacterName + " OnStory";
            Character.transform.localPosition = chars.CharacterPosition;
            Character.transform.localRotation = Quaternion.Euler(chars.CharacterRotation);
        }
    }

    private void DeactivateCamera()
    {
        CVMain.CVStoryMain.PlayerCam.SetActive(false);
        CVMain.CVStoryMain.PlayerGO.SetActive(false);
        CVMain.CVStoryMain.MiniCam.SetActive(false);
        CVMain.CVStoryMain.StoryCam.SetActive(true);
    }

    public void StartDialogues()
    {
        if (CurrentStorySO == null) { return; }
        SpeakerCount = CurrentStorySO.StoryDialogues.Length;

        if (DialogueNumber != SpeakerCount)
        {
            CVMain.CVStoryMain.SpeakerName.text = CurrentStorySO.StoryDialogues[DialogueNumber].SpeakerName;
        }

        if (CVMain.CVStoryMain.CanTap)
        {
            if (CVMain.CVStoryMain.IsTyping)
            {
                CVMain.StopCoroutine("TypeDialogues");
                CVMain.CVStoryMain.SpeakerName.text = CurrentStorySO.StoryDialogues[DialogueNumber - 1].SpeakerName;
                CVMain.CVStoryMain.SpeakerDialogues.text = CurrentStorySO.StoryDialogues[DialogueNumber - 1].SpeakerDialogues;
                CVMain.CVStoryMain.IsTyping = false;
                return;
            }

            if (DialogueNumber == SpeakerCount)
            {
                CVMain.CVStoryMain.CanTap = false;
                EndStory();
                return;
            }

            if (DialogueNumber > 0)
            {
                GameObject sample = GameObject.Find(CurrentStorySO.StoryDialogues[DialogueNumber - 1].SpeakerName + " OnStory");

                if (sample != null)
                {
                    AudioSource u1 = sample.GetComponent<AudioSource>();
                    u1.Stop();
                }
            }

            if (CurrentStorySO.StoryDialogues[DialogueNumber].SpeakerAudio != null)
            {
                AudioSource u = GameObject.Find(CurrentStorySO.StoryDialogues[DialogueNumber].SpeakerName + " OnStory").GetComponent<AudioSource>();
                u.clip = CurrentStorySO.StoryDialogues[DialogueNumber].SpeakerAudio;
                u.Play();
            }
            
            if (CurrentStorySO.StoryDialogues[DialogueNumber].CameraPosition != Vector3.zero || CurrentStorySO.StoryDialogues[DialogueNumber].CameraRotation != Vector3.zero)
            {
                CVMain.CVStoryMain.StoryCam.transform.localPosition = CurrentStorySO.StoryDialogues[DialogueNumber].CameraPosition;
                CVMain.CVStoryMain.StoryCam.transform.localRotation = Quaternion.Euler(CurrentStorySO.StoryDialogues[DialogueNumber].CameraRotation);
            }

            CVMain.CVStoryMain.IsTyping = false;
            CVMain.StartCoroutine(CVMain.CVStoryMain.TypeDialogues(CurrentStorySO.StoryDialogues[DialogueNumber].SpeakerDialogues));
            DialogueNumber++;
        }
    }

    private void EndStory()
    {
        ResetMarker();
        CreateQuestTrigger();
        RemoveCharacters();
        ActivateCamera();
        CurrentStorySO = null;
        DialogueNumber = 0;
        SpeakerCount = 0;
        CVMain.Player.MovePlayer.StoryMode = false;
        CVMain.CVMMenu.CloseDialogue();
    }

    private void RemoveCharacters()
    {
        foreach (StoryChars Characters in CurrentStorySO.CharactersList) 
        {
            GameObject DestroyCharacter = GameObject.Find(Characters.CharacterName + " OnStory");
            CVMain.Destroy(DestroyCharacter);
        }
    }

    private void CreateQuestTrigger()
    {
        GameObject QuestTriggerObj = CVMain.Instantiate(QTrigger);
        QuestTriggerObj.name = CurrentStorySO.NewQuest.name;
        QuestTriggerObj.transform.localPosition = CurrentStorySO.NewQuest.QuestLocation;

        GameObject QuestTriggerText = CVMain.Instantiate(QTriggerText);
        QuestTriggerText.transform.SetParent(QTriggerLocation.transform, false);
        TMP_Text QP = QuestTriggerText.transform.Find("QuestMTMP").GetComponent<TMP_Text>();
        QP.text = CurrentStorySO.NewQuest.QuestToDO;

        QuestTrigger QuestTrigScript = QuestTriggerObj.GetComponent<QuestTrigger>();
        QuestTrigScript.AssignedSO = CurrentStorySO.NewQuest;

        GameObject LocatorObj = CVMain.Instantiate(MarkerTrigger);
        LocatorObj.transform.SetParent(MarkerLocation.transform, false);
        Markers mark = LocatorObj.GetComponent<Markers>();
        mark.Target = QuestTriggerObj.transform;
    }

    private void ActivateCamera()
    {
        CVMain.CVStoryMain.PlayerCam.SetActive(true);
        CVMain.CVStoryMain.PlayerGO.SetActive(true);
        CVMain.CVStoryMain.MiniCam.SetActive(true);
        CVMain.CVStoryMain.StoryCam.SetActive(false);
    }

    private void ResetItems()
    {
        foreach (Transform child in QTriggerLocation.transform)
        {
            CVMain.Destroy(child.gameObject);
        }
    }

    private void ResetMarker()
    {
        foreach (Transform child in MarkerLocation.transform)
        {
            CVMain.Destroy(child.gameObject);
        }
    }
}
