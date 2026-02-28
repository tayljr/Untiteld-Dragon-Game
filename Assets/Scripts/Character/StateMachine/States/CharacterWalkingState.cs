using UnityEngine;

public class CharacterWalkingState : CharacterStateBase
{
    public CharacterWalkingState(CharacterStateMachine currentContext, CharacterStateFactory characterStateFactory) 
        : base(currentContext, characterStateFactory)
    {
    }

    public override void EnterState()
    {
        //Debug.Log("Entering Walk State");
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
        //Debug.Log("Exiting Walk State");
    }

    public override void CheckSwitchStates()
    {
        if (_context.moveDir == Vector3.zero)
        {
            SwitchState(_factory.Idle());
        }
    }

    public override void InitializeSubState()
    {
        throw new System.NotImplementedException();
    }
}
