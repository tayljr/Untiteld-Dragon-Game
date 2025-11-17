using System;
using System.Collections.Generic;
using UnityEngine;

public class KillQuestBase : QuestBase
{
    private List<GameObject> enemiesKilled =  new List<GameObject>();
    
    public List<GameObject> enemiesToKill = new List<GameObject>();

    private void OnEnable()
    {
        HealthBase.OnDeath += HealthBase_OnDeath;
    }

    private void OnDisable()
    {
        HealthBase.OnDeath -= HealthBase_OnDeath;
    }
    
    private void HealthBase_OnDeath(string tag, GameObject obj)
    {
        if(currentState == QuestState.doing)
        {
            if (enemiesToKill.Contains(obj))
            {
                enemiesKilled.Add(obj);
            }

            if (enemiesKilled.Count == enemiesToKill.Count)
            {
                FinishedQuest();
            }
        }
    }
}
