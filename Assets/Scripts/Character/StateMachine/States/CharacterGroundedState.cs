using System;
using UnityEngine;

public class CharacterGroundedState : CharacterStateBase
{
    public CharacterGroundedState(CharacterStateMachine currentContext, CharacterStateFactory characterStateFactory) 
        : base(currentContext, characterStateFactory)
    {
        isRootState = true;
        InitializeSubState();
    }

    

    public override void EnterState()
    {
        //Debug.Log("Entering Character Grounded State");
    }

    public override void UpdateState()
    {
        CheckSwitchStates();
        //HandleGravity();
        DoMove();
        CheckSlope();
    }

    public override void FixedUpdateState()
    {
    }

    public override void ExitState()
    {
        //Debug.Log("Exiting Character Grounded State");
    }

    public override void CheckSwitchStates()
    {
        if (_context.isClimbing)
        {
            SwitchState(_factory.Climbing());
        }
        else if (!_context.grounded || _context.isJumpPressed)
        {
            SwitchState(_factory.InAir());
        }

        if (_context.isSliding)
        {
            SetSubState(_factory.Sliding());
        }
        else if (_context.isWalkPressed)
        {
            if (_context.isRunPressed)
            {
                SetSubState(_factory.Run());
            }
            else
            {
                SetSubState(_factory.Walk());
            }
        }
        else
        {
            SetSubState(_factory.Idle());
        }
    }

    public override void InitializeSubState()
    {
        if (_context.isSliding)
        {
            SetSubState(_factory.Sliding());
        }
        else if (_context.isWalkPressed)
        {
            if (_context.isRunPressed)
            {
                SetSubState(_factory.Run());
            }
            else
            {
                SetSubState(_factory.Walk());
            }
        }
        else
        {
            SetSubState(_factory.Idle());
        }
    }

    public void DoMove()
    {
        //Debug.Log(_context.moveDir);
        _context.worldMoveDir = _context.transform.TransformDirection(_context.moveDir);
        _context.worldMoveDir = _context.worldMoveDir * _context.speed * _context.speedModifier;
        //_context.worldMoveDir.y = _context.verticalVelocity;
    }
    
    public void CheckSlope()
    {
        if (_context.slopeAngle <= _context.controller.slopeLimit + 0.01f)
        {
            _context.canSlopeJump = false;
            _context.isSliding = false;
            _context.wasSliding = false;
            _context.jumpCount = 0;
            //_context.grounded = true;
            //isGliding = false;
        }
        else if (_context.slopeAngle < 89.5f)
        {
            //todo move to slide state
            _context.isSliding = true;
            _context.wasSliding = true;
            //isGliding = false;
            _context.StartCoroutine(_context.SlideCoyoteTime());
        }
        else
        {
            _context.isSliding = false;
            _context.wasSliding = false;
            _context.canSlopeJump = false;
            //grounded = false;
            //isGliding = false;
            _context.slopeNormal = Vector3.up;
        }
        /*
        _context.slopeNormal = Vector3.up;
        if (_context.grounded || _context.isSliding || _context.wasSliding)
        {
            Vector3 rayDir = Vector3.down;
            float rayLength = Vector3.Distance(_context.transform.position, _context.groundTrigger.transform.position);
            Physics.SphereCast(_context.transform.position, 0.3f, rayDir, out _context.slopeHit, rayLength + 0.1f,
                Int32.MaxValue, QueryTriggerInteraction.Ignore);
            //Physics.Raycast(groundTrigger.gameObject.transform.position, rayDir, out hit, 2f, Int32.MaxValue, QueryTriggerInteraction.Ignore);
            if (_context.slopeHit.collider != null)
            {
                _context.slopeNormal = _context.slopeHit.normal;
                _context.slopeAngle = Vector3.Angle(_context.slopeNormal, Vector3.up);

                //Debug.Log(angle);

            }

            Debug.DrawRay(_context.slopeHit.point, _context.slopeHit.normal, Color.red, 1f);
        }*/
        
        //todo fix project on plane for slopes
        
        if(_context.worldMoveDir.magnitude > 0)
        {
            _context.worldMoveDir = Vector3.ProjectOnPlane(_context.worldMoveDir, _context.slopeNormal);
        }
        
        _context.verticalVelocity = _context.worldMoveDir.y;
    }
}
