using System;
using Unity.Behavior;
using UnityEngine;
using Action = Unity.Behavior.Action;
using Unity.Properties;

[Serializable, GeneratePropertyBag]
[NodeDescription(name: "CheckInteract", story: "Check if Player Has [Interacted]", category: "Action", id: "a9d6598b10eb3cda43e3d3cec0844c30")]
public partial class CheckInteractAction : Action
{
    [SerializeReference] public BlackboardVariable<NPCInteract> Interacted;
    
    
    protected override Status OnStart()
    {
        return Status.Running;
    }

    protected override Status OnUpdate()
    {
        if (Interacted.Value.hasInteract)
        {
            Interacted.Value.hasInteract = false;
            return Status.Success;
        }
        return Status.Running;
    }

    protected override void OnEnd()
    {
    }
}

