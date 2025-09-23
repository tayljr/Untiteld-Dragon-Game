using System;
using Unity.Behavior;
using UnityEngine;
using Action = Unity.Behavior.Action;
using Unity.Properties;

[Serializable, GeneratePropertyBag]
[NodeDescription(name: "BeginConversation", story: "[Player] has begun a conversation with [self]", category: "Action", id: "a9d6598b10eb3cda43e3d3cec0844c30")]
public partial class BeginConversationAction : Action, IInteractable
{
    [SerializeReference] public BlackboardVariable<CharacterBase> Self;
    private bool hasInteract = false;
    
    protected override Status OnStart()
    {
        hasInteract = false;
        return Status.Running;
    }

    protected override Status OnUpdate()
    {
        if (hasInteract)
        {
            hasInteract = false;
            return Status.Success;
        }
        return Status.Running;
    }

    protected override void OnEnd()
    {
    }

    public void StartInteract()
    {
        hasInteract = true;
    }

    public void StopInteract()
    {
        hasInteract = false;
    }
}

