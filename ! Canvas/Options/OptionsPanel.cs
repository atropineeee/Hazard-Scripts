using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;
using UnityEngine.Rendering;
using UnityEngine.UI;
using UnityEngine.Audio;
using System;

[Serializable]
public class OptionsPanel
{
    #region
    public CVMain CVMain;
    public OptionsPanel (CVMain CVmain)
    {
        CVMain = CVmain;    
    }
    #endregion

    public SavedStateSO PlayerData;

    [Header("Graphics Settings")]
    public Button LowGraph;
    public Button MedGraph;
    public Button HighGraph;

    private UniversalRenderPipelineAsset urpAsset;

    public AudioMixer AudioMix;

    [Header("Settings")]
    public Slider MasterVolume;
    public Slider VoiceOver;
    public Slider Environment;

    public void Awake()
    {
        urpAsset = QualitySettings.renderPipeline as UniversalRenderPipelineAsset;

        PlayerData = Resources.Load<SavedStateSO>("Scriptable Objects/Player Data/Saved Data");
        MasterVolume = GameObject.Find("MasterVolSlider").GetComponent<Slider>();
        VoiceOver = GameObject.Find("VoiceVolSlider").GetComponent<Slider>();
        Environment = GameObject.Find("EnvVolSlider").GetComponent<Slider>();

        LowGraph = GameObject.Find("LowGraphBtn").GetComponent<Button>();
        MedGraph = GameObject.Find("MedGraphBtn").GetComponent<Button>();
        HighGraph = GameObject.Find("HighGraphBtn").GetComponent<Button>();

        AudioMix = Resources.Load<AudioMixer>("Audio/MasterVol");

        LowGraph.onClick.AddListener(LowSettings);
        MedGraph.onClick.AddListener(MedSettings);
        HighGraph.onClick.AddListener(HighSettings);

        MasterVolume.onValueChanged.AddListener(MasterVol);
        VoiceOver.onValueChanged.AddListener(VoiceOv);
        Environment.onValueChanged.AddListener(EnvVol);

        DoAwake();
    }

    private void DoAwake()
    {
        float MasVal = Mathf.Lerp(-20f, 20f, PlayerData.MasterVolume / 10f);
        AudioMix.SetFloat("MasterVolume", MasVal);

        float VoVVal = Mathf.Lerp(-20f, 20f, PlayerData.VoiceOver / 10f);
        AudioMix.SetFloat("Environment", VoVVal);

        float EnvVal = Mathf.Lerp(-20f, 20f, PlayerData.Environment / 10f);
        AudioMix.SetFloat("VoiceOver", EnvVal);
    }

    private void LowSettings()
    {
        urpAsset.renderScale = 0.5f;
        urpAsset.supportsHDR = false;
    }

    private void MedSettings()
    {
        urpAsset.renderScale = 1f;
        urpAsset.supportsHDR = true;
    }

    private void HighSettings()
    {
        urpAsset.renderScale = 1.5f;
        urpAsset.supportsHDR = true;
    }

    private void MasterVol(float value)
    {
        PlayerData.Environment = Convert.ToInt32(value);
        float dBValue = Mathf.Lerp(-20f, 20f, value / 10f);
        AudioMix.SetFloat("MasterVolume", dBValue);
    }

    private void VoiceOv(float value)
    {
        PlayerData.VoiceOver = Convert.ToInt32(value);
        float dBValue = Mathf.Lerp(-20f, 20f, value / 10f);
        AudioMix.SetFloat("VoiceOver", dBValue);
    }

    private void EnvVol(float value)
    {
        PlayerData.Environment = Convert.ToInt32(value);
        float dBValue = Mathf.Lerp(-20f, 20f, value / 10f);
        AudioMix.SetFloat("Environment", dBValue);
    }
}
