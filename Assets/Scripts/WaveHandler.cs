using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveHandler : MonoBehaviour
{
    public Action OnNewWave;
    public Action OnWaveFinished;

    [SerializeField] private GameObject SpawnerPrefab;
    [SerializeField] private int Difficulty;
    [SerializeField] private int WaveNumber;
    [SerializeField] private int TimeBetweenWaves;
}
