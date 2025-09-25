using System;
using Unity.Behavior;
using UnityEngine;
using Action = Unity.Behavior.Action;
using Unity.Properties;

[Serializable, GeneratePropertyBag]
[NodeDescription(name: "BeginConversation", story: "[Player] has begun a conversation with [self]", category: "Action", id: "a9d6598b10eb3cda43e3d3cec0844c30")]
public partial class BeginConversationAction : Action
{
    [SerializeReference] public BlackboardVariable<CharacterBase> Self;
    protected override Status OnStart()
    {
        return Status.Running;
    }

    protected override Status OnUpdate()
    {
        if (Self.Value.nearPlayer == true && Input.GetKeyDown(KeyCode.E))
        {
            return Status.Success;
        }
        return Status.Failure;
    }

    protected override void OnEnd()
    {
    }
}

