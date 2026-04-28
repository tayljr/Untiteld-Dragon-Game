using UnityEngine;

public class CharacterInAirState : CharacterStateBase
{
    public CharacterInAirState(CharacterStateMachine currentContext, CharacterStateFactory characterStateFactory) 
        : base(currentContext, characterStateFactory)
    {
        isRootState = true;
        InitializeSubState();
    }

    public override void EnterState()
    {
        //Debug.Log("Entering Character In Air State");
        
    }

    public override void UpdateState()
    {
        HandleGravity();
        DoMove();
        CheckSwitchStates();
        //Debug.Log("2.0 = " +_context.transform.position.y);
    }

    public override void FixedUpdateState()
    {
        
    }

    public override void ExitState()
    {
        //Debug.Log("Exiting Character In Air State");
        
        _context.jumpCount = 0;
        _context.isSliding = false;
    }

    public override void CheckSwitchStates()
    {
        if ((_context.isJumpPressed && _context.jumpCount < _context.maxJumpCount) || _context.isJumping)
        {
            SetSubState(_factory.Jump());
        }
        else if (_context.isGliding)
        {
            SetSubState(_factory.Gliding());
        }
        else
        {
            SetSubState(_factory.Falling());
        }
        
        if (_context.grounded && !_context.isJumping)
        // if (_context.grounded && _context.verticalVelocity <= 0)
        {
            SwitchState(_factory.Grounded());
        }
        else if (_context.isClimbing)
        {
            SwitchState(_factory.Climbing());
        }
    }

    public override void InitializeSubState()
    {
        //todo glide
        _context.jumpVelocity = Vector3.zero;
        if ((_context.isJumpPressed && _context.jumpCount < _context.maxJumpCount) || _context.jumpVelocity.magnitude > 0)
        {
            SetSubState(_factory.Jump());
        }
        else if (_context.isGliding)
        {
            SetSubState(_factory.Gliding());
        }
        else
        {
            SetSubState(_factory.Falling());
        }
    }
    
    void HandleGravity()
    {
        
        //_context.currentGravity = _context.gravity;
        //_context.currentTerminalVelocity = _context.terminalVelociy;
        //todo move to falling
        

        _context.verticalVelocity -= _context.currentGravity * Time.deltaTime;
        if (_context.verticalVelocity <= -_context.currentTerminalVelocity)
        {
            _context.verticalVelocity = -_context.currentTerminalVelocity;
        }
        _context.worldMoveDir.y = _context.verticalVelocity;
    }
    
    public void DoMove()
    {
        _context.worldMoveDir = _context.transform.TransformDirection(_context.moveDir);
        _context.worldMoveDir = _context.worldMoveDir * _context.speed * _context.speedModifier;
    }
}
