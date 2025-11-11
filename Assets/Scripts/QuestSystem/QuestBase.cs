using UnityEngine;

public enum QuestState
{
    notStarted,
    doing,
    completed
}

public abstract class QuestBase : MonoBehaviour
{
    public string name;
    public string description;
    public Sprite icon;

    public QuestState currentState = QuestState.notStarted;
    
    //public QuestBase[] questPrerequisites;
    
    private bool isFinished = false;
    
    public delegate void QuestDelegate(QuestBase quest);
    public event QuestDelegate OnQuestFinished;
    
    public void onStart()
    {
        currentState = QuestState.notStarted;
    }
    
    protected void FinishedQuest()
    {
        if (!isFinished)
        {
            isFinished = true;
            
            currentState = QuestState.completed;
            OnQuestFinished?.Invoke(this);
            
            //Destroy(this.gameObject);
        }
    }

    public void BeginQuest()
    {
        currentState = QuestState.doing;
    }
}
