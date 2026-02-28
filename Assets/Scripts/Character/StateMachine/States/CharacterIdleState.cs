using UnityEngine;

public class CharacterIdleState : CharacterStateBase
{
    public CharacterIdleState(CharacterStateMachine currentContext, CharacterStateFactory characterStateFactory) 
        : base(currentContext, characterStateFactory)
    {
    }

    public override void EnterState()
    {
        //Debug.Log("Entering Character Idle State");
    }

    public override void UpdateState()
    {
        CheckSwitchStates();
    }

    public override void FixedUpdateState()
    {
        
    }

    public override void ExitState()
    {
        //Debug.Log("Exiting Character Idle State");
    }

    public override void CheckSwitchStates()
    {
        if (_context.isWalkPressed)
        {
            if (_context.isRunPressed)
            {
                SwitchState(_factory.Run());
            }
            else
            {
                SwitchState(_factory.Walk());
            }
        } 
    }

    public override void InitializeSubState()
    {
        throw new System.NotImplementedException();
    }
}
