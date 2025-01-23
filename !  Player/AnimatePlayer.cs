using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class AnimatePlayer
{
    #region
    public Player Player;
    public AnimatePlayer (Player player)
    {
        Player = player;
    }
    #endregion

    public void Update()
    {
        if (Player.MovePlayer.StoryMode)
        {
            Player.Animator.SetBool("Walking", false);
            Player.Animator.SetBool("Running", false);
            return;
        }

        if (Player.MovePlayer.PlayerState == PlayerMovementState.Idle)
        {
            Player.Animator.SetBool("Walking", false);
            Player.Animator.SetBool("Running", false);
        }

        if (Player.MovePlayer.PlayerState == PlayerMovementState.Walking)
        {
            Player.Animator.SetBool("Walking", true);
            Player.Animator.SetBool("Running", false);
        }

        if (Player.MovePlayer.PlayerState == PlayerMovementState.Running)
        {
            Player.Animator.SetBool("Walking", false);
            Player.Animator.SetBool("Running", true);
        }
    }
}
