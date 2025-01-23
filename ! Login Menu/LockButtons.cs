using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class LockButtons
{
    #region
    public CanvasHolder CVHolder;
    public LockButtons (CanvasHolder CVholder)
    {
        CVHolder = CVholder;    
    }
    #endregion

    public void SyncButtons()
    {
        CVHolder.CanvasHandler.Chp1Lvl1Btn.interactable = true;

        if (CVHolder.PlayerData.Level1 != "No Save")
        {
            CVHolder.CanvasHandler.Chp1Lvl2Btn.interactable = true;
        }
        else
        {
            CVHolder.CanvasHandler.Chp1Lvl2Btn.interactable = false;
            CVHolder.CanvasHandler.Chp2Lvl3Btn.interactable = false;
            CVHolder.CanvasHandler.Chp2Lvl4Btn.interactable = false;
            CVHolder.CanvasHandler.Chp3Lvl5Btn.interactable = false;
            return;
        }

        if (CVHolder.PlayerData.Level2 != "No Save")
        {
            CVHolder.CanvasHandler.Chp2Lvl3Btn.interactable = true;
        }
        else
        {
            CVHolder.CanvasHandler.Chp2Lvl3Btn.interactable = false;
            CVHolder.CanvasHandler.Chp2Lvl4Btn.interactable = false;
            CVHolder.CanvasHandler.Chp3Lvl5Btn.interactable = false;
            return;
        }

        if (CVHolder.PlayerData.Level3 != "No Save")
        {
            CVHolder.CanvasHandler.Chp2Lvl4Btn.interactable = true;
        }
        else
        {
            CVHolder.CanvasHandler.Chp2Lvl4Btn.interactable = false;
            CVHolder.CanvasHandler.Chp3Lvl5Btn.interactable = false;
            return;
        }

        if (CVHolder.PlayerData.Level4 != "No Save")
        {
            CVHolder.CanvasHandler.Chp3Lvl5Btn.interactable = true;
        }
        else
        {
            CVHolder.CanvasHandler.Chp3Lvl5Btn.interactable = false;
        }
    }
}
