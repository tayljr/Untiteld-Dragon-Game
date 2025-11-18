using UnityEngine;

public class NPCQuest : QuestBase
{
    public void NPCFinished()
    {
        if(currentState == QuestState.doing)
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
