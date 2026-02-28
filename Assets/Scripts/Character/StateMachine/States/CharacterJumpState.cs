using UnityEngine;

public class CharacterJumpState : CharacterStateBase
{
    private float jumpVelocityMagnitude;
    public CharacterJumpState(CharacterStateMachine currentContext, CharacterStateFactory characterStateFactory) 
        : base(currentContext, characterStateFactory)
    {
        
    }

    public override void EnterState()
    {
        //Debug.Log("Entering Character Jump State");
        HandleJump();
    }

    public override void UpdateState()
    {
        CheckSwitchStates();
        if (_context.isJumpPressed && _context.jumpCount < _context.maxJumpCount)
        {
            HandleJump();
        }
        DoJump();
    }

    public override void FixedUpdateState()
    {
        
    }

    public override void ExitState()
    {
        
    }

    public override void CheckSwitchStates()
    {
       
    }

    public override void InitializeSubState()
    {
        throw new System.NotImplementedException();
    }

    void HandleJump()
    { 
        _context.Jump(false);
        _context.jumpCount++;
        _context.jumpVelocity = Vector3.up * _context.jumpForce;
        jumpVelocityMagnitude = _context.jumpVelocity.magnitude;
        _context.isJumping = true;
    }
    
    void DoJump()
    {
        //Debug.Log("jump velocity 2.0 = " + _context.jumpVelocity.magnitude);
        if (jumpVelocityMagnitude > 0)
        {
            _context.verticalVelocity = _context.jumpForce;
            _context.jumpVelocity = Vector3.zero;
            jumpVelocityMagnitude = 0;
            //_context.worldMoveDir.y  = _context.verticalVelocity;
            
            _context.jumpVelocity = Vector3.MoveTowards(_context.jumpVelocity, Vector3.zero, _context.currentGravity * Time.deltaTime);
            //Debug.Log("current grav 2.0 = " + _context.currentGravity);
        }
        else
        {
            _context.slopeJump = false;
            _context.jumpVelocity = Vector3.zero;
        }

        if (_context.verticalVelocity <= 0)
        {
            _context.isJumping = false;
        }
    }

    
}
