using UnityEngine;

public class CharacterFallingState : CharacterStateBase
{
    public CharacterFallingState(CharacterStateMachine currentContext, CharacterStateFactory characterStateFactory) 
        : base(currentContext, characterStateFactory)
    {
        
    }

    public override void EnterState()
    {
        //Debug.Log("Entering Character Falling State");
        _context.currentGravity = _context.gravity;
        _context.currentTerminalVelocity = _context.terminalVelociy;
    }

    public override void UpdateState()
    {
        HandleGravity();
        CheckSwitchStates();
    }

    public override void FixedUpdateState()
    {
        
    }

    public override void ExitState()
    {
        //Debug.Log("Exiting Character Falling State");
    }

    public override void CheckSwitchStates()
    {
        
    }

    public override void InitializeSubState()
    {
        
    }
    

    void HandleGravity()
    {
        if (_context.fastFalling && _context.verticalVelocity < 0)
        {
            _context.currentGravity = _context.gravity * _context.fallingModifier;
        }
        else
        {
            //_context.currentGravity = _context.gravity;
        }
    }
}
