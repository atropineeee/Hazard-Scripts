using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class RainMov
{
    #region
    public Player Player;
    public RainMov(Player player)
    {
        Player = player;
    }
    #endregion
}
