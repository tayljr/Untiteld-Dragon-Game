using UnityEngine;

public class CharacterRuningState : CharacterStateBase
{
    public CharacterRuningState(CharacterStateMachine currentContext, CharacterStateFactory characterStateFactory) 
        : base(currentContext, characterStateFactory)
    {
    }

    public override void EnterState()
    {
        _context.speedModifier = _context.sprintModifier;
        _context.isCrouching = false;
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
        _context.speedModifier = 1;
    }

    public override void CheckSwitchStates()
    {
        if(!_context.isRunPressed)
        {
            //todo change to is walking bool
            if (_context.moveDir.magnitude > 0)
            {
                SwitchState(_factory.Walk());
            }
            else
            {
                SwitchState(_factory.Idle());
            }
        }
    }

    public override void InitializeSubState()
    {
        throw new System.NotImplementedException();
    }
}
