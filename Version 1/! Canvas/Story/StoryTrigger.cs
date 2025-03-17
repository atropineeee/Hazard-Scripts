using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoryTrigger : MonoBehaviour
{
    private CVMain CVMain;
    private Player Player;

    [Header("Assigned Story SO")]
    public StorySO AssignedStorySO;
    public bool isTriggered;

    private void Awake()
    {
        CVMain = GameObject.Find("CanvasC").GetComponent<CVMain>();
        Player = GameObject.Find("Player").GetComponent<Player>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Player")) { return; }
        if (this.AssignedStorySO == null) { return; }

        if (!this.isTriggered) 
        { 
            this.isTriggered = true;
            Player.MovePlayer.StoryMode = true;
            StartCoroutine(StartStory());
        }
    }

    IEnumerator StartStory()
    {
        yield return new WaitForSeconds(0.2f);
        CVMain.CVMMenu.OpenDialogue();
        CVMain.CVStoryMain.CanTap = true;
        Player.MovePlayer.JoyStickInput = Vector2.zero;
        Player.PlayerJoyStick.input = Vector2.zero;
        Player.PlayerJoyStick.IsUsingJoyStick = false;
        Player.PlayerJoyStick.handle.anchoredPosition = Vector2.zero;
        CVMain.CVStoryDials.CurrentStorySO = AssignedStorySO;
        CVMain.CVStoryDials.CreateCharacters();
        CVMain.CVStoryDials.StartDialogues();
        Destroy(this.gameObject);
    }
}
