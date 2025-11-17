using System;
using NUnit.Framework;
using UnityEngine;
using System.Collections.Generic;

public class LevelQuestsMiddleMan : MonoBehaviour
{
    public List<QuestBase> quests;
    

    
    public void SetQuests()
    {
        if (quests.Count == 0)
        {
            quests = new List<QuestBase>(GetComponentsInChildren<QuestBase>());
        }
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        SetQuests();
        QuestManager.instance.SetQuests(quests);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
