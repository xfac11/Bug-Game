using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveUI : MonoBehaviour
{
    [SerializeField] private WaveHandler WaveHandler;
    [SerializeField] private TMPro.TMP_Text WavenumberText;
    [SerializeField] private TMPro.TMP_Text DifficultyText;
    [SerializeField] private TMPro.TMP_Text TimeText;
    private int _wavenumber = 0;
    private int _difficulty = 0;
    private void Awake()
    {
        WaveHandler.OnNewWave += UpdateUI;
        WaveHandler.OnWaveFinished += DisplayTime;
    }

    private void DisplayTime()
    {
        StartCoroutine(UpdateTime());
    }

    private IEnumerator UpdateTime()
    {
        Debug.Log("Update time");
        TimeText.gameObject.SetActive(true);
        while(WaveHandler.Timer > 0)
        {
            TimeText.text = WaveHandler.Timer.ToString();
            yield return null;
        }
        TimeText.gameObject.SetActive(false);
        Debug.Log("Stop updating time");
    }

    private void UpdateUI(int Wavenumber, int Difficulty)
    {
        if(Wavenumber != _wavenumber)
        {
            LeanTween.scale(WavenumberText.gameObject,new Vector3(2f,2f,2f),0.5f).setLoopPingPong(1).setDelay(0.05f);
        }
        _wavenumber = Wavenumber;
        WavenumberText.text = _wavenumber.ToString();
        if(Difficulty != _difficulty)
        {
            LeanTween.scale(WavenumberText.gameObject, new Vector3(2f, 2f, 2f), 0.5f).setLoopPingPong(1).setDelay(0.05f);
        }
        _difficulty = Difficulty;
        DifficultyText.text = (_difficulty+1).ToString();
    }
}
