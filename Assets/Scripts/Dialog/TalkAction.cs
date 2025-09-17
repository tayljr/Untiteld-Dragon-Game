using System;
using Unity.Behavior;
using UnityEngine;
using Action = Unity.Behavior.Action;
using Unity.Properties;
using TMPro;
using UnityEngine.UI;

[Serializable, GeneratePropertyBag]
[NodeDescription(name: "Talk", story: "[agent] says [_text] to [target]", category: "Action", id: "0b898db8721d903d489af947cf5ba501")]
public partial class TalkAction : Action
{
    [Tooltip("The character who is talking")]
    [SerializeReference] public BlackboardVariable<CharacterBase> agent;

    [Tooltip("What they are saying")]
    [SerializeReference] public BlackboardVariable<string> _text;

    [Tooltip("Who they are talking to")]
    [SerializeReference] public BlackboardVariable<CharacterBase> target;

    [Tooltip("The TMP text to change")]
    [SerializeReference] public BlackboardVariable<TextMeshProUGUI> agentDialogBox;

    [Tooltip("The characters name")]
    [SerializeReference] public BlackboardVariable<TextMeshProUGUI> agentName;

    [Tooltip("The characters portrait box")]
    [SerializeReference] public BlackboardVariable<Image> agentPortrait;

    [Tooltip("The other TMP")]
    [SerializeReference] public BlackboardVariable<TextMeshProUGUI> targetDialogBox;

    [Tooltip("The targets name")]
    [SerializeReference] public BlackboardVariable<TextMeshProUGUI> targetName;

    [Tooltip("The targets portrate box")]
    [SerializeReference] public BlackboardVariable<Image> targetPortrait;

    private bool isDone = false;

    private float delayTimer;

    protected override Status OnStart()
    {
        agentName.Value.text = agent.Value.charName;
        agentName.Value.color = agent.Value.textColour;
        agentDialogBox.Value.text = _text.Value;
        agentDialogBox.Value.color = agent.Value.textColour;
        agentPortrait.Value.sprite = agent.Value.charPortrait;

        targetName.Value.text = target.Value.charName;
        targetName.Value.color = target.Value.textColour;
        targetDialogBox.Value.text = "";
        targetPortrait.Value.sprite = target.Value.charPortrait;

        delayTimer = 0.1f;

        //isDone = false;

        return Status.Running;
    }

    protected override Status OnUpdate()
    {
        delayTimer -= Time.deltaTime;

        if (delayTimer <= 0 && Input.GetKeyDown(KeyCode.Space))
        {
            isDone = true;
        }

        if (isDone)
        {
            return Status.Success;
        } else
        {
            return Status.Running;
        }
    }

    protected override void OnEnd()
    {
    }
}

