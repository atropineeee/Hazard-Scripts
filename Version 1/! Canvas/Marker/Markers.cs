using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Markers : MonoBehaviour
{
    [Header("Offsets")]
    [SerializeField] public Image img;
    [SerializeField] public Transform Target;
    [SerializeField] public Player Player;
    [SerializeField] private float xOffset = 400f;
    [SerializeField] private float yOffset = 200f;

    private void Start()
    {
        this.img = this.gameObject.GetComponent<Image>();
        this.Player = GameObject.Find("Player").GetComponent<Player>();
    }

    public void Update()
    {
        float minX = this.img.GetPixelAdjustedRect().width / 2 + xOffset;
        float maxX = Screen.width - minX;

        float minY = this.img.GetPixelAdjustedRect().height / 2 + yOffset;
        float maxY = Screen.height - minY;

        Vector2 pos = Camera.main.WorldToScreenPoint(Target.position);

        if (Vector3.Dot((Target.position - Player.transform.position), Player.transform.forward) < 0)
        {
            if (pos.x < Screen.width / 2)
            {
                pos.x = maxX;
            }
            else
            {
                pos.x = minX;
            }
        }

        pos.x = Mathf.Clamp(pos.x, minX, maxX);
        pos.y = Mathf.Clamp(pos.y, minY, maxY);

        img.transform.position = pos;
    }
}
