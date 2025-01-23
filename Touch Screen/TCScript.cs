using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UIElements;

public class TCScript : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    [SerializeField] protected Player Player;
    [SerializeField] public Vector2 TouchDistance;
    [SerializeField] public Vector2 TouchOldPos;
    [SerializeField] public int TouchPointId;
    [SerializeField] public bool IsPressed;

    private void Start()
    {
        Player = GameObject.Find("Player").GetComponent<Player>();
    }

    private void Update()
    {
        if (IsPressed)
        {
            if (Input.touchCount == 1)
            {
                if (TouchPointId >= 0 && TouchPointId < Input.touches.Length)
                {
                    TouchDistance = Input.touches[TouchPointId].position - TouchOldPos;
                    TouchOldPos = Input.touches[TouchPointId].position;
                }
                else
                {
                    TouchDistance = new Vector2(Input.mousePosition.x, Input.mousePosition.y) - TouchOldPos;
                    TouchOldPos = Input.mousePosition;
                }
            }
            else if (Player.PlayerJoyStick.IsUsingJoyStick)
            {
                if (TouchPointId >= 0 && TouchPointId < Input.touches.Length)
                {
                    TouchDistance = Input.touches[TouchPointId].position - TouchOldPos;
                    TouchOldPos = Input.touches[TouchPointId].position;
                }
                else
                {
                    TouchDistance = new Vector2(Input.mousePosition.x, Input.mousePosition.y) - TouchOldPos;
                    TouchOldPos = Input.mousePosition;
                }
            }
        } 
        else
        {
            TouchDistance = new Vector2();
        }
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        IsPressed = true;
        TouchPointId = eventData.pointerId;
        TouchOldPos = eventData.position;
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        IsPressed = false;
    }
}
