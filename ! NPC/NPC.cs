using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPC : MonoBehaviour
{
    [Header("Player")]

    [field: SerializeField] public Player Player;
    [field: SerializeField] public NpcSO AssignedNPCSO;
}
