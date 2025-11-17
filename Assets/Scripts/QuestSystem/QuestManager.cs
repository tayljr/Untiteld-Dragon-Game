using System;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class QuestManager : MonoBehaviour
{
    public static QuestManager instance;
    
    public delegate void QuestDelegate();
    public event QuestDelegate OnQuestsFinished;
    
    public List<QuestBase> quests = new List<QuestBase>();
    public int currentQuest = 0;
    
    public TMP_Text questDescription;
    public TMP_Text questTitle;
    public Image questIcon;

    private void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            instance = this;
        }
    }
    
    public void SetQuests(List<QuestBase> newQuests)
    {
        currentQuest = 0;
        quests = newQuests;
        foreach (QuestBase quest in quests)
        {
            quest.OnQuestFinished += QuestOnOnQuestFinished;
        }
        if (quests.Count > 0)
        {
            quests[currentQuest].BeginQuest();
            if (questTitle != null) questTitle.text = quests[currentQuest].name;
            if (questDescription != null) questDescription.text = quests[currentQuest].description;
            if (questIcon != null) questIcon.sprite = quests[currentQuest].icon;
        }
    }

    private void OnDisable()
    {
        foreach (QuestBase quest in quests)
        {
            quest.OnQuestFinished -= QuestOnOnQuestFinished;
        }
    }

    private void QuestOnOnQuestFinished(QuestBase quest)
    {
        quest.OnQuestFinished -= QuestOnOnQuestFinished;
        currentQuest++;
        if (currentQuest < quests.Count)
        {
            quests[currentQuest].BeginQuest();
            if (questTitle != null) questTitle.text = quests[currentQuest].name;
            if (questDescription != null) questDescription.text = quests[currentQuest].description;
            if (questIcon != null) questIcon.sprite = quests[currentQuest].icon;
        }
        else
        {
            AllQuestsFinished();
        }
    }

    private void AllQuestsFinished()
    {
        //Debug.Log("All Quests Done");
        OnQuestsFinished?.Invoke();
        if (questTitle != null) questTitle.text = "All Quests Complete!";
        if (questDescription != null) questDescription.text = "Well Done";
        if (questIcon != null) questIcon.sprite = null; 
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
       
    }

    // Update is called once per frame
    void Update()
    {
        List<QuestBase> toRemoveQuests = new List<QuestBase>();
        foreach (QuestBase quest in quests)
        {
            if (quest == null)
            {
                toRemoveQuests.Add(quest);
            }
        }
        foreach (QuestBase quest in toRemoveQuests)
        {
            quests.Remove(quest);
        }
        
        if (quests.Count == 0)
        {
            AllQuestsFinished();
        }
    }
}
