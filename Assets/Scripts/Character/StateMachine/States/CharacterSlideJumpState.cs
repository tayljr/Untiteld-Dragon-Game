using UnityEngine;

public class CharacterSlideJumpState : CharacterStateBase
{
    public CharacterSlideJumpState(CharacterStateMachine currentContext, CharacterStateFactory characterStateFactory) 
        : base(currentContext, characterStateFactory)
    {
    }

    public override void EnterState()
    {
        //Debug.Log("Entering Slide Jump State");
        HandleJump();
    }

    public override void UpdateState()
    {
        CheckSwitchStates();
        DoJump();
    }

    public override void FixedUpdateState()
    {
        
    }

    public override void ExitState()
    {
        //Debug.Log("Exiting Slide Jump State");
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
        _context.jumpCount++;
        _context.jumpVelocity = _context.slopeNormal * _context.jumpForce;
    }
    
    void DoJump()
    {
        if (_context.jumpVelocity.magnitude > 0)
        {
            _context.canSlopeJump = false;
            _context.isSliding = false;
            _context.slopeJump = true;
            //grounded = false;
            _context.worldMoveDir.x += _context.jumpVelocity.x;
            _context.worldMoveDir.z += _context.jumpVelocity.z;
            _context.verticalVelocity = _context.jumpVelocity.y;
            Debug.DrawRay(_context.transform.position, _context.jumpVelocity, Color.blue, 1f);
            
            _context.jumpVelocity = Vector3.MoveTowards(_context.jumpVelocity, Vector3.zero, _context.currentGravity * Time.deltaTime);
        }
        else
        {
            _context.slopeJump = false;
            _context.jumpVelocity = Vector3.zero;
            _context.worldMoveDir.y = 0;
        }
    }
}
