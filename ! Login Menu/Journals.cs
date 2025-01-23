using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

[Serializable]
public class Journals
{
    #region
    public CanvasHolder CanvasHolder;
    public Journals (CanvasHolder holder)
    {
        CanvasHolder = holder;
    }
    #endregion

    public TMP_Text TitleText;

    public GameObject T1;
    public GameObject T2;
    public GameObject T3;
    public GameObject T4;
    public GameObject T5;

    public Button T1B;
    public Button T2B;
    public Button T3B;
    public Button T4B;
    public Button T5B;

    public void Start()
    {
        T1 = GameObject.Find("TypPnl");
        T2 = GameObject.Find("FldPnl");
        T3 = GameObject.Find("ErqPnl");
        T4 = GameObject.Find("TsuPnl");
        T5 = GameObject.Find("VolPnl");

        T1B = GameObject.Find("TypBtn").GetComponent<Button>();
        T2B = GameObject.Find("FldBtn").GetComponent<Button>();
        T3B = GameObject.Find("ErqBtn").GetComponent<Button>();
        T4B = GameObject.Find("TsuBtn").GetComponent<Button>();
        T5B = GameObject.Find("VolBtn").GetComponent<Button>();

        TitleText = GameObject.Find("TxtLp").GetComponent<TMP_Text>();

        TitleText.text = "Typhoon";

        T2.SetActive(false);
        T3.SetActive(false);
        T4.SetActive(false);
        T5.SetActive(false);

        T1B.onClick.AddListener(TypPnl);
        T2B.onClick.AddListener(FldPnl);
        T3B.onClick.AddListener(ErqPnl);
        T4B.onClick.AddListener(TsuPnl);
        T5B.onClick.AddListener(VolPnl);
    }

    private void TypPnl()
    {
        TitleText.text = "Typhoon";

        T1.SetActive(true);
        T2.SetActive(false);
        T3.SetActive(false);
        T4.SetActive(false);
        T5.SetActive(false);
    }

    private void FldPnl()
    {
        TitleText.text = "Flood & Landslide";

        T1.SetActive(false);
        T2.SetActive(true);
        T3.SetActive(false);
        T4.SetActive(false);
        T5.SetActive(false);
    }

    private void ErqPnl()
    {
        TitleText.text = "Earthquake";

        T1.SetActive(false);
        T2.SetActive(false);
        T3.SetActive(true);
        T4.SetActive(false);
        T5.SetActive(false);
    }

    private void TsuPnl()
    {
        TitleText.text = "Tsunami";

        T1.SetActive(false);
        T2.SetActive(false);
        T3.SetActive(false);
        T4.SetActive(true);
        T5.SetActive(false);
    }

    private void VolPnl()
    {
        TitleText.text = "Volcanic Eruption";

        T1.SetActive(false);
        T2.SetActive(false);
        T3.SetActive(false);
        T4.SetActive(false);
        T5.SetActive(true);
    }
}
