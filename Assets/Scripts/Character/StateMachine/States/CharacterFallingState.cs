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
    }

    public override void UpdateState()
    {
        CheckSwitchStates();
        //HandleGravity();
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
        float _gravity = _context.gravity;
        float _termVel = _context.terminalVelociy;
        if (_context.fastFalling && _context.verticalVelocity < 0)
        {
            _gravity = _context.gravity * _context.fallingModifier;
        }
        else
        {
            _gravity = _context.gravity;
        }
        
        if (_context.isGliding)
        {
            _gravity = _context.glideGrav;
            _termVel = _context.teminalGlideVel;
        }

        _context.verticalVelocity -= _gravity * Time.deltaTime;
        if (_context.verticalVelocity <= -_termVel)
        {
            _context.verticalVelocity = -_termVel;
        }
    }
}
