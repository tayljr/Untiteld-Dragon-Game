using UnityEngine;

public class CharacterGlideState : CharacterStateBase
{
    public CharacterGlideState(CharacterStateMachine currentContext, CharacterStateFactory characterStateFactory) 
        : base(currentContext, characterStateFactory)
    {
        
    }

    public override void EnterState()
    {
        //Debug.Log("Entering Character Glide State");
        //HandleGlide();
    }

    public override void UpdateState()
    {
        CheckSwitchStates();
        HandleGravity();
        DoGlide();
    }

    public override void FixedUpdateState()
    {
        
    }

    public override void ExitState()
    {
        //Debug.Log("Exiting Character Glide State");
    }

    public override void CheckSwitchStates()
    {
        if (_context.grounded)
        {
            SwitchState(_factory.Grounded());
        }
    }

    public override void InitializeSubState()
    {
        throw new System.NotImplementedException();
    }

    //todo don't know if i need this
    void HandleGlide()
    {
        _context.jumpCount++;
        //Debug.Log(slopeAngle);
        if(_context.canSlopeJump)
        {
            _context.jumpVelocity = _context.slopeNormal * _context.jumpForce;
        }
        else
        {
            _context.jumpVelocity = Vector3.up * _context.jumpForce;
        }
    }

    //todo change glide gravity
    void HandleGravity()
    {
        _context.currentGravity = _context.glideGrav;
        _context.currentTerminalVelocity = _context.teminalGlideVel;
    }

    void DoGlide()
    {
        _context.verticalVelocity = _context.moveDir.y;
        _context.worldMoveDir = _context.transform.TransformDirection(_context.moveDir.x * _context.glideSidewaysSpeed, _context.moveDir.y, _context.glideForwardSpeed);
        _context.LockHead();
    }
}
