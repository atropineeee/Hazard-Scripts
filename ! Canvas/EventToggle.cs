using Cinemachine;
using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

[Serializable]
public class EventToggle
{
    #region
    public CVMain CVMain;
    public EventToggle(CVMain CVmain)
    {
        CVMain = CVmain;    
    }
    #endregion

    public EventToToggle CurrentEvent;

    [Header("Rain Event")]
    public GameObject RainObject;
    public GameObject FogObject;
    public CinemachineVirtualCamera VirtualCamera;
    public Vector3 RainOffset = new Vector3(0f, 25f, 0f);

    public GameObject WarnLoc;
    public GameObject WarnPref;

    public bool Triggered = false;

    public void Start()
    {
        RainObject = GameObject.Find("Rain System");
        FogObject = GameObject.Find("Fog System");
        VirtualCamera = GameObject.Find("PVCam").GetComponent<CinemachineVirtualCamera>();

        WarnLoc = GameObject.Find("Warning SHOW");
        WarnPref = Resources.Load<GameObject>("CanvasPrefabs/WPanel");
    }

    public void Update ()
    {
        if (CurrentEvent == EventToToggle.None)
        {
            var noise = VirtualCamera.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();

            noise.m_AmplitudeGain = 0f;
            noise.m_FrequencyGain = 0f;
        }

        if (CurrentEvent == EventToToggle.Rain)
        {
            if (!Triggered)
            {
                GameObject go = CVMain.Instantiate(WarnPref);
                go.transform.SetParent(WarnLoc.transform, false);

                TMP_Text txt = go.transform.Find("Wrn").GetComponent<TMP_Text>();

                txt.text = "Heavy Rain Incoming!";
                CVMain.Destroy(go, 3f);
            }

            Triggered = true;

            RainObject.transform.position = new Vector3(CVMain.Player.transform.position.x, RainOffset.y, CVMain.Player.transform.position.z);
        }

        if (CurrentEvent == EventToToggle.EarthQuake) 
        {
            if (!Triggered)
            {
                GameObject go = CVMain.Instantiate(WarnPref);
                go.transform.SetParent(WarnLoc.transform, false);

                TMP_Text txt = go.transform.Find("Wrn").GetComponent<TMP_Text>();

                txt.text = "Massive Earthquake Incoming!";
                CVMain.Destroy(go, 3f);
            }

            Triggered = true;

            var noise = VirtualCamera.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();

            noise.m_AmplitudeGain = 0.5f;
            noise.m_FrequencyGain = 1.5f;
        }

        if (CurrentEvent == EventToToggle.Volcano)
        {
            if (!Triggered)
            {
                GameObject go = CVMain.Instantiate(WarnPref);
                go.transform.SetParent(WarnLoc.transform, false);

                TMP_Text txt = go.transform.Find("Wrn").GetComponent<TMP_Text>();

                txt.text = "Volcano Eruption Incoming!";

                CVMain.Destroy(go, 3f);
            }
            Triggered = true;

            FogObject.transform.position = new Vector3(CVMain.Player.transform.position.x, CVMain.Player.transform.position.y, CVMain.Player.transform.position.z);
        }
    }
}
