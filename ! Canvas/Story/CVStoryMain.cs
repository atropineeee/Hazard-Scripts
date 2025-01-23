using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

[Serializable]
public class CVStoryMain
{
    #region
    public CVMain CVMain;
    public CVStoryMain (CVMain cvMain)
    {
        CVMain = cvMain;
    }
    #endregion

    public Button DialogueButton;

    [Header("Typing Animation")]
    public bool CanTap;
    public bool IsTyping;
    public float typingSpeed = 0.05f;

    [Header("TMP Texts")]
    public TMP_Text SpeakerName;
    public TMP_Text SpeakerDialogues;

    [Header("Player Objects")]
    public GameObject PlayerGO;
    public GameObject PlayerCam;
    public GameObject MiniCam;
    public GameObject StoryCam;

    public void Start()
    {
        DialogueButton = GameObject.Find("DLGButton").GetComponent<Button>(); 

        SpeakerName = GameObject.Find("DLGNameSpeaker").GetComponent<TMP_Text>();
        SpeakerDialogues = GameObject.Find("DLGNameDialogues").GetComponent<TMP_Text>();

        PlayerGO = GameObject.Find("Player");
        PlayerCam = GameObject.Find("PVCam");
        MiniCam = GameObject.Find("MMCam");
        StoryCam = GameObject.Find("StryCam");

        SpeakerName.text = "";
        SpeakerDialogues.text = "";

        StoryCam.SetActive(false);
        DialogueButton.onClick.AddListener(NextPhase);
    }

    private void NextPhase()
    {
        if (!CanTap) { return; }

        if (CVMain.CVStoryDials.CurrentStorySO != null)
        {
            CVMain.CVStoryDials.StartDialogues();
            return;
        }
    }

    public IEnumerator CanTapDialogue()
    {
        yield return new WaitForSeconds(1f);
        CanTap = true;
    }

    public IEnumerator TypeDialogues(string dialogues)
    {
        IsTyping = true;
        SpeakerDialogues.text = "";

        foreach (char letter in dialogues) 
        {
            if (!IsTyping)
            {
                yield break;
            }

            SpeakerDialogues.text += letter;

            yield return new WaitForSeconds(typingSpeed);
        }

        IsTyping = false;
        CanTap = true;
    }
}
