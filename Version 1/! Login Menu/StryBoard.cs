using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

[Serializable]
public class StryBoard
{
    #region
    public CanvasHolder CanvasHolder;
    public StryBoard (CanvasHolder holder)
    {
        CanvasHolder = holder;
    }
    #endregion

    public int i = 1;

    public void ChapterSB()
    {
        Image sr = GameObject.Find("SBImage").GetComponent<Image>();
        TMP_Text text = GameObject.Find("SBText").GetComponent<TMP_Text>();

        if (CanvasHolder.PlayerData.LevelSelected == LevelSelected.Ch1L1 || CanvasHolder.PlayerData.LevelSelected == LevelSelected.Ch1L2)
        {
            Sprite spr = Resources.Load<Sprite>("Storyboard/Chap1/" + i);

            text.text = "In a gripping adventure, a team of experts gathers to unravel the mysteries of a secluded island, rumored to be untouched by natural disasters until the explorations of Dr. Elias. Legends speak of hidden treasures surrounding the ominous Mt. Thesaurus, protected by ancestral spirits that unleash fierce phenomena on those who dare to steal from the island. As the team sets sail, they encounter treacherous storms and heavy rain, forcing them to navigate a series of challenges. From seeking shelter and rescuing stranded locals to gathering vital supplies and aiding the injured, players must act quickly to survive the wrath of a brewing typhoon and the chaos of landslides. Each quest brings them closer to the heart of the island’s secrets while testing their wits and teamwork in a race against nature's fury.";

            sr.sprite = spr;

            if (i > 2)
            {
                CanvasHolder.CanvasHandler.StartButton.interactable = true;
                return;
            }
        }

        if (CanvasHolder.PlayerData.LevelSelected == LevelSelected.Ch2L1 || CanvasHolder.PlayerData.LevelSelected == LevelSelected.Ch2L2)
        {
            Sprite spr = Resources.Load<Sprite>("Storyboard/Chap2/" + i);

            text.text = "As the team enters the second region, they find themselves amidst locals fleeing from the onslaught of a fierce typhoon, floods, and landslides. The regional head reveals the team’s quest for the island’s hidden treasure, setting the stage for a thrilling adventure. Suddenly, thick clouds rise from the nearby volcano, signaling an impending earthquake that sends everyone into a panic. Players must navigate collapsing structures and falling debris to reach safety while searching for survivors and restoring communication after the quake disrupts all systems. Just as they begin to regroup, the ocean betrays its calm, pulling back to reveal the seabed—a precursor to an imminent tsunami. With the pressure mounting, players must swiftly find a path to higher ground, overcoming obstacles left by previous disasters and racing against time to escape the catastrophic waves threatening to engulf them. As they reach a mountain peak, they glimpse the ruins left by the tsunami and prepare to venture toward the last island in their quest for survival and discovery.";

            sr.sprite = spr;

            if (i > 2)
            {
                CanvasHolder.CanvasHandler.StartButton.interactable = true;
                return;
            }
        }

        if (CanvasHolder.PlayerData.LevelSelected == LevelSelected.Ch3L1)
        {
            Sprite spr = Resources.Load<Sprite>("Storyboard/Chap3/" + i);

            text.text = "As thunder rumbles and rain begins to fall, the team of experts approaches thenominous Mt. Thesaurus, known to locals as The Mountain of Wrath. With thick smoke rising from its peak, they realize the volcano is on the verge of eruption. The regional head warns them that the island calls to those who seek its secrets.\r\nSuddenly, the volcano roars, unleashing ash that falls like heavy snow, prompting a frantic escape. Players must navigate through the chaos, completing quests to find protective gear, rescue trapped villagers, and ultimately reach a safe evacuation point. As they race to the harbor to secure a boat and flee, the volcano erupts violently, triggering seismic activity beneath the ocean.";

            sr.sprite = spr;

            if (i > 2)
            {
                CanvasHolder.CanvasHandler.StartButton.interactable = true;
                return;
            }
        }


        i++;
    }
}
