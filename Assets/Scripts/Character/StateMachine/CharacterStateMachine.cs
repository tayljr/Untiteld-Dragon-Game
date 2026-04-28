using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

/// <summary>
/// this contains all the player information so the states can access them
/// </summary>

public class CharacterStateMachine : MonoBehaviour
{
    //State Variables
    CharacterStateBase _currentState;
    CharacterStateFactory _states;
    
    //getters and setter
    public CharacterStateBase CurrentState {get {return _currentState;} set {_currentState = value;}}
    
    public bool isJumpPressed;
    public bool isRunPressed;
    public bool isWalkPressed;
    
    public CharacterController controller;

    //public ColliderEvents groundTrigger;
    public GroundCheck groundCheck;
    
    public GameObject head;
    public Vector2 minMaxHeadTilt = new Vector2(-45f, 45f);
    public Vector2 minMaxHeadTurn = new Vector2(-45f, 45f);
    
    private Vector2 lookInput =  Vector2.zero;
    private bool isLooking = false;
    
    public float speed = 10f;
    public float sprintModifier = 1.5f;
    public float crouchModifier = 0.5f;
    
    public float coyoteTime = 0.1f;
    public int defaultJumpCount = 1;
    public int maxJumpCount = 1;
    public int jumpCount = 0;
    public float jumpForce = 2f;
    public Vector3 jumpVelocity = Vector3.zero;
    public bool isJumping = false;
    public bool fastFalling = true;
    public float fallingModifier = 1.5f;
    public float gravity = 10f;
    public float terminalVelociy = 50f;
    public Vector3 climbSpeed = new Vector3(10f, 10f, 10f);
    public float glideGrav = 5f;
    public float teminalGlideVel = 2.5f;
    public float glideForwardSpeed = 10f;
    public float glideSidewaysSpeed = 5f;
    public float slideSpeed = 500f;


    //will be private
    private bool canClimb = false;
    private bool canGlide = false;

    public Vector3 moveDir = Vector3.zero;
    public float speedModifier = 1f;
    [SerializeField] public float verticalVelocity = 0f;
    public Vector3 slopeNormal = Vector3.up;
    public float slopeAngle = 0;
    public RaycastHit slopeHit;
    public float stepAngle = 90;
    
    //please dont priv this i need for animator :(
    public bool isSliding = false;
    public bool isUpStairs = false;
    public bool grounded = false;
    public bool isCrouching = false;
    public bool isGliding = false;
    public bool isClimbing = false;
    public Vector2 currentHeadDir = Vector2.zero;

    public Transform groundObjTransform;
    public bool wasSliding = false;
    public bool canSlopeJump = false;
    public bool slopeJump = false;
    
    private Collider currentGround; 
    
    private Vector3 platformMovement = Vector3.zero;
    private Transform movingPlatform;
    
    [SerializeField]
    private int groundCount = 0;
    [SerializeField]
    private List<Collider> groundList = new List<Collider>();

    [SerializeField]
    private Vector2 currentCharacterDir = Vector2.zero;
    
    public Vector3 worldMoveDir = Vector3.zero;

    public float currentGravity;
    public float currentTerminalVelocity;

    void Awake()
    {
        //setup states
        _states = new CharacterStateFactory(this);
        _currentState = _states.InAir();
        _currentState.EnterState();
    }
    
    private void OnEnable()
    {
        //groundTrigger.OnTriggerEnterEvent += Grounded;
        //groundTrigger.OnTriggerExitEvent += NotGrounded;
        
    }
    private void OnDisable()
    {
        //groundTrigger.OnTriggerEnterEvent -= Grounded;
        //groundTrigger.OnTriggerExitEvent -= NotGrounded;
    }
    
    //todo move to dead state
    public void Teleport(Vector3 pos)
    {
        controller.enabled = false;
        transform.position = pos;
        controller.enabled = true;
    }
    
    public void Look(Vector2 dir, bool start)
    {
        lookInput = dir * Time.deltaTime;
        //lookInput = dir;
        isLooking = start;
    }
    
    private void DoLook(Vector2 dir)
    {
        Debug.Log(dir.ToString());
        currentHeadDir += dir;

        if (moveDir.x != 0f || moveDir.z != 0f)
        {
            currentCharacterDir.x += dir.x;
            currentHeadDir.x = 0;
        }
        else
        {
            if (currentHeadDir.x > minMaxHeadTurn.y)
            {
                currentHeadDir.x = minMaxHeadTurn.y;
                currentCharacterDir.x += dir.x;
            }
            else if (currentHeadDir.x < minMaxHeadTurn.x)
            {
                currentHeadDir.x = minMaxHeadTurn.x;
                currentCharacterDir.x += dir.x;
            }
        }

        if (currentHeadDir.y > minMaxHeadTilt.y)
        {
            currentHeadDir.y = minMaxHeadTilt.y;
        }
        else if (currentHeadDir.y < minMaxHeadTilt.x)
        {
            currentHeadDir.y = minMaxHeadTilt.x;
        }

        head.transform.localRotation = Quaternion.Euler(-currentHeadDir.y, currentHeadDir.x, 0);
        transform.localRotation = Quaternion.Euler(0, currentCharacterDir.x, 0);
    }
    
    private void NotGrounded(GameObject self, Collider other)
    {
        if (other.gameObject != gameObject && !other.isTrigger && groundList.Contains(other) && other.gameObject.layer != LayerMask.NameToLayer("Ignore GroundCheck"))
        {
            if (verticalVelocity < 0)
            {
                verticalVelocity = 0;
            }
            
            groundCount--;
            groundList.Remove(other);
            
            if (groundCount <= 0)
            {
                groundCount = 0;
                grounded = false;
            }
            
            StartCoroutine(CoyoteTime());
        }

    }
    
    IEnumerator CoyoteTime()
    {
        yield return new WaitForSeconds(coyoteTime);

        if (!grounded && jumpCount == 0)
        {
            jumpCount = 1;
        }
    }

    public IEnumerator SlideCoyoteTime()
    {
        yield return new WaitForSeconds(coyoteTime);

        if (isSliding)
        {
            canSlopeJump = true;
        }
    }
    
    private void Grounded(GameObject self, Collider other)
    {
        if (other.gameObject != gameObject && !other.isTrigger && !groundList.Contains(other) && other.gameObject.layer != LayerMask.NameToLayer("Ignore GroundCheck"))
        {
            verticalVelocity = 0;
            grounded = true;
            if (!wasSliding)
            {
                //jumpCount = 0;
            }
            groundCount++;
            groundList.Add(other);
            isGliding = false;
            currentGround = other;
        }
    }
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }
    
    public void Move(Vector2 dir)
    {
        if (dir != Vector2.zero)
        {
            isWalkPressed = true;
        }
        else
        {
            isWalkPressed = false;
        }
        moveDir = new Vector3(dir.x, moveDir.y, dir.y);
        
        //todo strafe mechanics (like ratchet and clank)
        LockHead();

        /*
        if (moveDir.x != 0f || moveDir.y != 0f)
        {
            currentHeadDir.x = 0;
        }

        head.transform.localRotation = Quaternion.Euler(-currentHeadDir.y, currentHeadDir.x, 0);
        */
    }
    
    //todo inherent parent from object the player is standing on
    public void PlatformMove(Vector3 dir)
    {
        platformMovement = dir;
        //controller.Move(transform.TransformDirection(dir));
    }

    public void Jump(bool pressed)
    {
        isJumpPressed = pressed;
        //isJumping = false;
        //if (pressed)
        //{
        //}
    }

    public void Sprint(bool pressed)
    {
        isRunPressed = pressed;
    }
    
    //this allows the character to climb
    public void SetCanClimb(bool enableClimb)
    {
        canClimb = enableClimb;
        isClimbing = canClimb;
    }

    public void SetCanGlide(bool enableGlide)
    {
        canGlide = enableGlide;
    }
    
    public void Glide(bool startGliding)
    {
        if (canGlide)
        {
            isGliding = startGliding;
        }else
        {
            isGliding = false;
        }
    }
    
    //double jump
    public void SetJumpCount(int newCount)
    {
        maxJumpCount = newCount;
    }
    public void AddJumpCount(int newCount)
    {
        maxJumpCount += newCount;
    }
    public void ResetJumpCount()
    {
        maxJumpCount = defaultJumpCount;
    }

    public void LockHead()
    {
        currentCharacterDir.x += currentHeadDir.x;
        //transform.localRotation = Quaternion.Euler(0, currentHeadDir.y, 0);
        currentHeadDir.x = 0;
        head.transform.localRotation = Quaternion.Euler(-currentHeadDir.y, currentHeadDir.x, 0);

        transform.localRotation = Quaternion.Euler(0, currentCharacterDir.x, 0);
    }
    
    
    private void Update()
    {
        if (GameManager.instance.isPaused)
        {
            return; 
        }
        
        
        
        //todo move to in air state
        //Debug.Log("is grounded = " + grounded);
        if (!grounded)
        {
            //StartCoroutine(CoyoteTime());
        }

        if (isLooking)
        {
            DoLook(lookInput);
        }
        
        if (groundCheck.movingGround != null)
        {
            groundCheck.movingGround.AddCharacter(this);
        }
        worldMoveDir.y = verticalVelocity;
        controller.Move(worldMoveDir * Time.deltaTime + platformMovement);
        platformMovement = Vector3.zero;
        
        //Debug.Log(_currentState.ToString());
        grounded = groundCheck.grounded;
        //groundObjTransform = groundCheck.groundObjTrans;
        //transform.parent = groundObjTransform;
        //transform.SetParent(groundObjTransform, true);
        slopeAngle = groundCheck.slopeAngle;
        slopeNormal = groundCheck.slopeNormal;
        stepAngle = groundCheck.stepAngle;
        _currentState.UpdateStates();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        _currentState.FixedUpdateStates();
        //todo change ground check to ray cast of some sort
        /*
        List<Collider> missingColliders = new List<Collider>();
        foreach (Collider collider in groundList)
        {
            if (collider == null)
            {
                missingColliders.Add(collider);
            }
        }
        
        groundList = groundList.Except(missingColliders).ToList();
        missingColliders.Clear();
        groundCount = groundList.Count;
        */
        
        
        //old test stuff vvv
        /*
        isJumpPressed = false;
        worldMoveDir = transform.TransformDirection(moveDir);
        worldMoveDir = worldMoveDir * speed * speedModifier;
        worldMoveDir.y  += verticalVelocity;

        if (jumpVelocity.magnitude > 0)
        {
            verticalVelocity = jumpForce;
            jumpVelocity = Vector3.zero;
            worldMoveDir.y  = verticalVelocity;
        }
        else
        {
            slopeJump = false;
            jumpVelocity = Vector3.zero;
        }

        controller.Move(worldMoveDir * Time.deltaTime + platformMovement);
        */
    }
}
