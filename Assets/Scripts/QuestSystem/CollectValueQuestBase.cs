using System;
using UnityEngine;

public class CollectValueQuestBase : QuestBase
{
    public int targetValue = 100;
    private int amountGained = 0;

    private void OnEnable()
    {
        GemManager.OnGemAdded += GemManagerOnOnGemAdded;
    }

    private void OnDisable()
    {
        GemManager.OnGemAdded -= GemManagerOnOnGemAdded;
    }

    private void GemManagerOnOnGemAdded(int amount)
    {
        if(currentState == QuestState.doing)
        {
            amountGained += amount;
            if (amountGained >= targetValue)
            {
                FinishedQuest();
            }
        }
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        amountGained = 0;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
