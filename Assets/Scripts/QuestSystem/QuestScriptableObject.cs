using UnityEngine;



[CreateAssetMenu(fileName = "QuestScriptableObject", menuName = "Scriptable Objects/QuestScriptableObject")]
public class QuestScriptableObject : ScriptableObject
{
    public string QuestName;
    public string QuestDescription;

    public QuestScriptableObject[] questPrerequisites;
    
    public QuestBase questStep;
    
    
    //quest rewards???
}
