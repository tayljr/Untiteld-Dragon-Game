using System;

using Unity.Behavior;
using UnityEngine;
using Action = Unity.Behavior.Action;
using Unity.Properties;
using Unity.Cinemachine;

[Serializable, GeneratePropertyBag]
[NodeDescription(name: "Set Camera Target", story: "Set [Cinemachine_Camera] Target to [Target]", category: "Action", id: "54a287572e9d3fafa862c6c4fa59ca49")]
public partial class SetCameraTargetAction : Action
{
    [SerializeReference] public BlackboardVariable<CinemachineCamera> cameraVar;
    [SerializeReference] public BlackboardVariable<Transform> Target;
    protected override Status OnStart()
    {
        cameraVar.Value.LookAt = Target.Value.gameObject.transform;
        return Status.Running;
    }

    protected override Status OnUpdate()
    {
        return Status.Success;
    }

    protected override void OnEnd()
    {

        

    }
}

