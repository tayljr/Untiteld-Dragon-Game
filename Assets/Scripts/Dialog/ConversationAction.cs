using System;
using Unity.Behavior;
using UnityEngine;
using Action = Unity.Behavior.Action;
using Unity.Properties;
using TMPro;
using UnityEngine.UI;

[Serializable, GeneratePropertyBag]
[NodeDescription(name: "Conversation", story: "[Agent] and [Target] are in Conversation", category: "Action", id: "db6a1d0bc8b5f037e168e1fa2d36623d")]
public partial class ConversationAction : Action
{
    [Tooltip("The character who is talking")]
    [SerializeReference] public BlackboardVariable<CharacterBase> Agent;

    [Tooltip("Who they are talking to")]
    [SerializeReference] public BlackboardVariable<CharacterBase> Target;
    
    [Tooltip("list of messages and who is responding")]
    [SerializeReference] public BlackboardVariable<JSONReader> Conversation;

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

    [Tooltip("The targets portrait box")]
    [SerializeReference] public BlackboardVariable<Image> targetPortrait;

    private int currentMessage = 0;

    protected override Status OnStart()
    {
        agentName.Value.text = Agent.Value.charName;
        agentName.Value.color = Agent.Value.textColour;
        agentDialogBox.Value.color = Agent.Value.textColour;
        agentPortrait.Value.sprite = Agent.Value.charPortrait;

        targetName.Value.text = Target.Value.charName;
        targetName.Value.color = Target.Value.textColour;
        targetDialogBox.Value.color = Target.Value.textColour;
        targetPortrait.Value.sprite = Target.Value.charPortrait;


        return Status.Running;
    }

    protected override Status OnUpdate()
    {
        if (currentMessage == Conversation.Value.myMessageList.message.Length)
        {
            return Status.Success;
        }
        if (!Conversation.Value.myMessageList.message[currentMessage].targetResponding)
        {
            agentDialogBox.Value.text = Conversation.Value.myMessageList.message[currentMessage].saying;
            targetDialogBox.Value.text = "";
        }
        else
        {
            agentDialogBox.Value.text = "";
            targetDialogBox.Value.text = Conversation.Value.myMessageList.message[currentMessage].saying;
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            currentMessage++;
        }
        return Status.Running;
    }

    protected override void OnEnd()
    {
    }
}

