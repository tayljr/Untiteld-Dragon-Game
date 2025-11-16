using System;
using Unity.AppUI.UI;
using Unity.Behavior;
using UnityEngine;
using Action = Unity.Behavior.Action;
using Unity.Properties;

[Serializable, GeneratePropertyBag]
[NodeDescription(name: "FinishQuest", story: "Player Finished [Quest]", category: "Action", id: "a1aaf35038a96af06f7129e4e8e4fc5c")]
public partial class FinishQuestAction : Action
{
    [SerializeReference] public BlackboardVariable<NPCQuest> Quest;
    public delegate void DialogQuest(QuestBase quest);
    public DialogQuest QuestFinished;
    
    protected override Status OnStart()
    {
        Quest.Value.NPCFinished();
        return Status.Success;
    }

    protected override Status OnUpdate()
    {
        return Status.Success;
    }

    protected override void OnEnd()
    {
    }
}

