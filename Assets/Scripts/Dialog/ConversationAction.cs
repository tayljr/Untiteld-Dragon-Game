using System;
using Unity.Behavior;
using UnityEngine;
using Action = Unity.Behavior.Action;
using Unity.Properties;
using TMPro;
using UnityEngine.InputSystem;
using UnityEngine.UI;
using Unity.VisualScripting;

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

    [Tooltip("The Action to perform to go through the conversation")]
    [SerializeReference] public BlackboardVariable<InputActionReference> advanceConversationAction;

    [Tooltip("The other TMP")]
    [SerializeReference] public BlackboardVariable<TextMeshProUGUI> targetDialogBox;

    [Tooltip("The targets name")]
    [SerializeReference] public BlackboardVariable<TextMeshProUGUI> targetName;

    [Tooltip("The targets portrait box")]
    [SerializeReference] public BlackboardVariable<Image> targetPortrait;

    [Tooltip("The targets animator")]
    [SerializeReference] public BlackboardVariable<Animator> animator;

    private int currentMessage = 0;
    
    private PlayerController playerController;
    
    protected override Status OnStart()
    {
        currentMessage = 0;
        
        agentName.Value.text = Agent.Value.charName;
        agentName.Value.color = Agent.Value.textColour;
        agentDialogBox.Value.color = Agent.Value.textColour;
        agentPortrait.Value.sprite = Agent.Value.charPortrait;

        targetName.Value.text = Target.Value.charName;
        targetName.Value.color = Target.Value.textColour;
        targetDialogBox.Value.color = Target.Value.textColour;
        targetPortrait.Value.sprite = Target.Value.charPortrait;

        advanceConversationAction.Value.action.Enable();
        advanceConversationAction.Value.action.performed += OnAction;

        playerController = Target.Value.gameObject.GetComponent<PlayerController>();
        if (playerController != null)
        {
            playerController.playerControls.Disable();
            playerController.playerAnimation.IsTalking = true;
        }
        //todo: replace this with changing the action map
        //Target.Value.gameObject.GetComponent<PlayerController>().enabled = false;

        return Status.Running;
    }

    private void OnAction(InputAction.CallbackContext obj)
    {
        //pause check
        if(Time.timeScale != 0)
        {
            currentMessage++;
        }
    }

    protected override Status OnUpdate()
    {
        if (currentMessage == Conversation.Value.myMessageList.message.Length)
        {
            currentMessage = 0;
            
            if (playerController != null)
            {
                playerController.playerControls.Enable();
            }
            //todo: replace this with changing the action map
            //Target.Value.gameObject.GetComponent<PlayerController>().enabled = true;

            //DONE by paul :3
            return Status.Success;
        }
        if (!Conversation.Value.myMessageList.message[currentMessage].targetResponding)
        {
            //npc talking
            agentDialogBox.Value.text = Conversation.Value.myMessageList.message[currentMessage].saying;
            animator.Value.SetTrigger("talk");
            targetDialogBox.Value.text = "";
        }
        else
        {

            //player talking
            agentDialogBox.Value.text = "";
            targetDialogBox.Value.text = Conversation.Value.myMessageList.message[currentMessage].saying;
        }
        return Status.Running;
    }

    protected override void OnEnd()
    {
        playerController.playerAnimation.IsTalking = false;
        advanceConversationAction.Value.action.Disable();
    }
}

