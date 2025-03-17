using Cinemachine;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class MoveCamera
{
    #region
    public Player Player;
    public MoveCamera(Player player)
    {
        Player = player;
    }
    #endregion

    [Header("Cinemachine Components")]
    [field: SerializeField] private CinemachineFramingTransposer CinemachineTrans;
    [field: SerializeField] private CinemachinePOV CinemachinePOV;
    [field: SerializeField] private TCScript TouchScreen;

    [Header("Camera Sensitivity")]
    [Range(1f, 10f)] public float _TouchXSens = 1f;
    [Range(1f, 10f)] public float _TouchYSens = 1f;
    [Range(0f, 5f)] public float _ZommSens = 1f;

    [Header("Camera Position & Smoothing")]
    public float minDistance = 1f;
    public float maxDistance = 6f;
    public float defaultDistance = 4.5f;
    public float Smoothing = 12f;
    public float currentDistance;
    public float currentTargetDistance = 3;
    public float zoomValue;

    public void Start()
    {
        CinemachineTrans = Player.VirtualCamera.GetCinemachineComponent<CinemachineFramingTransposer>();
        CinemachinePOV = Player.VirtualCamera.GetCinemachineComponent<CinemachinePOV>();
        TouchScreen = GameObject.Find("TouchScreenCV").GetComponent<TCScript>();
    }

    public void Update()
    {
        UpdateDistance();
        ZoomCamera();
    }

    private void ZoomCamera()
    {
        if (Player.PlayerJoyStick.IsUsingJoyStick) { return; }
        if (Input.touchCount == 2)
        {
            Touch touch_0 = Input.GetTouch(0);
            Touch touch_1 = Input.GetTouch(1);

            if (touch_0.phase == TouchPhase.Moved && touch_1.phase == TouchPhase.Moved)
            {

                Vector2 firstTouchPrev = touch_0.position - touch_0.deltaPosition;
                Vector2 secondTouchPrev = touch_1.position - touch_1.deltaPosition;

                float touchPrevPos = (firstTouchPrev - secondTouchPrev).magnitude;
                float touchCurPos = (touch_0.position - touch_1.position).magnitude;

                if (touchPrevPos > touchCurPos)
                {
                    zoomValue = 0.1f * _ZommSens;
                }
                else if (touchPrevPos < touchCurPos)
                {
                    zoomValue = -0.1f * _ZommSens;
                }
            }
            else
            {
                zoomValue = 0f;
            }
        }
        else
        {
            zoomValue = 0f;
        }

        currentTargetDistance = Mathf.Clamp(currentTargetDistance + zoomValue, minDistance, maxDistance);
        float lerpedZoomValue = Mathf.Lerp(currentDistance, currentTargetDistance, Smoothing * Time.deltaTime);
        CinemachineTrans.m_CameraDistance = lerpedZoomValue;
    }

    private void UpdateDistance()
    {
        currentDistance = CinemachineTrans.m_CameraDistance;
        CinemachinePOV.m_HorizontalAxis.m_InputAxisValue = TouchScreen.TouchDistance.x * 10f * _TouchXSens * Time.deltaTime;
        CinemachinePOV.m_VerticalAxis.m_InputAxisValue = TouchScreen.TouchDistance.y * 10f * _TouchYSens * Time.deltaTime;
        CinemachinePOV.m_HorizontalAxis.m_InputAxisName = "";
        CinemachinePOV.m_VerticalAxis.m_InputAxisName = "";
    }
}
