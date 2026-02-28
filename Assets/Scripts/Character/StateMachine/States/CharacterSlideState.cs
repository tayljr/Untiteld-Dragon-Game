using UnityEngine;

public class CharacterSlideState : CharacterStateBase
{
    public CharacterSlideState(CharacterStateMachine currentContext, CharacterStateFactory characterStateFactory) 
        : base(currentContext, characterStateFactory)
    {
        isRootState = true;
    }

    public override void EnterState()
    {
        //Debug.Log("Entering Character Slide State");
        
        HandleSlide();
    }

    public override void UpdateState()
    {
        CheckSwitchStates();
        DoSlide();
        //HandleGravity();
    }

    public override void FixedUpdateState()
    {
        
    }

    public override void ExitState()
    {
        //Debug.Log("Exiting Character Slide State");
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

    void HandleSlide()
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

    void DoSlide()
    {
        //grounded = false;
        Vector3 slideDir = Vector3.RotateTowards(_context.slopeNormal, Vector3.down, 90 * Mathf.Deg2Rad, 0f);
        slideDir = Vector3.ProjectOnPlane(new Vector3(0, _context.verticalVelocity, 0), _context.slopeNormal);
        Debug.DrawRay(_context.slopeHit.point, slideDir, Color.yellow, 1f);
        _context.worldMoveDir += slideDir.normalized * (_context.slideSpeed * Time.deltaTime);
        _context.verticalVelocity = -_context.slideSpeed * Time.deltaTime;
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
