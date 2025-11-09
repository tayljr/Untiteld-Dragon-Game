using System;
using UnityEngine;

public class WaveKillQuestBase : QuestBase
{
    public Spawner waveSpawner;

    private void OnEnable()
    {
        waveSpawner.OnWavesFinished += WaveSpawnerOnOnWavesFinished;
    }

    private void OnDisable()
    {
        waveSpawner.OnWavesFinished -= WaveSpawnerOnOnWavesFinished;
    }

    private void WaveSpawnerOnOnWavesFinished()
    {
        if (currentState == QuestState.doing)
        {
            FinishedQuest();
        }
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
