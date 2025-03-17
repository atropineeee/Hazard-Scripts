using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[Serializable]
public class Inventory
{
    #region
    public CVMain CVMain;
    public Inventory (CVMain CVmain)
    {
        CVMain = CVmain;
    }
    #endregion

    public string[] InventorySlots = new string[7];

    public Image Slot1;
    public Image Slot2;
    public Image Slot3;
    public Image Slot4;
    public Image Slot5;
    public Image Slot6;
    public Image Slot7;

    public void RefreshInventory()
    {
        Slot1 = GameObject.Find("Inv1").GetComponent<Image>();
        Slot2 = GameObject.Find("Inv2").GetComponent<Image>();
        Slot3 = GameObject.Find("Inv3").GetComponent<Image>();
        Slot4 = GameObject.Find("Inv4").GetComponent<Image>();
        Slot5 = GameObject.Find("Inv5").GetComponent<Image>();
        Slot6 = GameObject.Find("Inv6").GetComponent<Image>();
        Slot7 = GameObject.Find("Inv7").GetComponent<Image>();

        Image[] slots = { Slot1, Slot2, Slot3, Slot4, Slot5, Slot6, Slot7 };

        for (int i = 0; i < InventorySlots.Length; i++)
        {
            if (!string.IsNullOrEmpty(InventorySlots[i]))
            {
                Sprite itemSprite = Resources.Load<Sprite>($"Inventory/{InventorySlots[i]}");

                if (itemSprite != null)
                {
                    slots[i].sprite = itemSprite;
                    slots[i].enabled = true; 
                }
                else
                {
                    slots[i].enabled = false;
                }
            }
            else
            {
                slots[i].enabled = false;
            }
        }
    }
}
