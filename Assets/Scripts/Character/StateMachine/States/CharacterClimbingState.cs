using UnityEngine;

public class CharacterClimbingState : CharacterStateBase
{
    public CharacterClimbingState(CharacterStateMachine currentContext, CharacterStateFactory characterStateFactory) 
        : base(currentContext, characterStateFactory)
    {
        //todo this should not be a root state
        isRootState =  true;
    }

    public override void EnterState()
    {
        //Debug.Log("Entering Climb State");
    }

    public override void UpdateState()
    {
        CheckSwitchStates();
        DoClimb();
    }

    public override void FixedUpdateState()
    {
        
    }

    public override void ExitState()
    {
        //Debug.Log("Exiting Climb State");
    }

    public override void CheckSwitchStates()
    {
            //todo touching ground climb state & normal climbing state
            //todo wall jumps?
        if (!_context.isClimbing)
        {
            if (_context.grounded)
            {
                SwitchState(_factory.Grounded());
            }
            else
            {
                SwitchState(_factory.InAir());
            }
        }
    }

    public override void InitializeSubState()
    {
        throw new System.NotImplementedException();
    }

    void DoClimb()
    {
        float forwardMoveDir = _context.moveDir.z;
        if (!_context.grounded)
        {
            forwardMoveDir = 0;
            _context.verticalVelocity = 0;
        }

        _context.verticalVelocity = _context.moveDir.z * _context.climbSpeed.y;
        _context.worldMoveDir = Vector3.Scale(_context.transform.TransformDirection(_context.moveDir.x, _context.moveDir.z, forwardMoveDir), _context.climbSpeed);
    }
}
