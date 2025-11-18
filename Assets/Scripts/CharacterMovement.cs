using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class CharacterMovement : MonoBehaviour
{


    public CharacterController controller;

    public ColliderEvents groundTrigger;

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
    private int jumpCount = 0;
    public float jumpForce = 2f;
    private Vector3 jumpVelocity = Vector3.zero;
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

    private Vector3 moveDir = Vector3.zero;
    private float speedModifier = 1f;
    [SerializeField]
    private float verticalVelocity = 0f;
    public Vector3 slopeAngle = Vector3.up;
    
    //please dont priv this i need for animator :(
    public bool isSliding = false;
    public bool grounded = false;
    public bool isCrouching = false;
    public bool isGliding = false;
    public bool isClimbing = false;
    public Vector2 currentHeadDir = Vector2.zero;
    
    private bool slopeJump = false;
    private Collider currentGround; 
    
    [SerializeField]
    private int groundCount = 0;
    [SerializeField]
    private List<Collider> groundList = new List<Collider>();

    [SerializeField]
    private Vector2 currentCharacterDir = Vector2.zero;

    public void Teleport(Vector3 pos)
    {
        controller.enabled = false;
        transform.position = pos;
        controller.enabled = true;
    }
    
    public void Look(Vector2 dir, bool start)
    {
        lookInput = dir;
        isLooking = start;
    }

    private void DoLook(Vector2 dir)
    {
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
    public void Move(Vector2 dir)
    {
        moveDir = new Vector3(dir.x, moveDir.y, dir.y);

        LockHead();

        /*
        if (moveDir.x != 0f || moveDir.y != 0f)
        {
            currentHeadDir.x = 0;
        }

        head.transform.localRotation = Quaternion.Euler(-currentHeadDir.y, currentHeadDir.x, 0);
        */
    }

    public void Jump()
    {
        //Debug.Log(grounded);
        if (grounded)
        {
            jumpCount = 0;
        }
        if (jumpCount < maxJumpCount)
        {
            jumpCount++;
            //Debug.Log(slopeAngle);
            jumpVelocity = slopeAngle * jumpForce;
            //verticalVelocity = jumpForce;
        }
    }

    public void Sprint(bool on)
    {
        if (on)
        {
            speedModifier = sprintModifier;
            isCrouching = false;
        }
        else if (isCrouching == false)
        {
            speedModifier = 1;
        }
    }

    public void Crouch(bool on)
    {
        if (on)
        {
            speedModifier = crouchModifier;
            isCrouching = true;
        }
        else
        {
            speedModifier = 1;
            isCrouching = false;
        }
    }

    public void Climb(bool enableClimb)
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
            //todo not stoppping
            //Move(Vector2.zero);
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



    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
    }

    private void OnEnable()
    {
        groundTrigger.OnTriggerEnterEvent += Grounded;
        groundTrigger.OnTriggerExitEvent += NotGrounded;
        
    }
    private void OnDisable()
    {
        groundTrigger.OnTriggerEnterEvent -= Grounded;
        groundTrigger.OnTriggerExitEvent -= NotGrounded;
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
    private void Grounded(GameObject self, Collider other)
    {
        if (other.gameObject != gameObject && !other.isTrigger && !groundList.Contains(other) && other.gameObject.layer != LayerMask.NameToLayer("Ignore GroundCheck"))
        {
            verticalVelocity = 0;
            grounded = true;
            jumpCount = 0;
            groundCount++;
            groundList.Add(other);
            isGliding = false;
            currentGround = other;
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
    private void LockHead()
    {
        currentCharacterDir.x += currentHeadDir.x;
        //transform.localRotation = Quaternion.Euler(0, currentHeadDir.y, 0);
        currentHeadDir.x = 0;
        head.transform.localRotation = Quaternion.Euler(-currentHeadDir.y, currentHeadDir.x, 0);

        transform.localRotation = Quaternion.Euler(0, currentCharacterDir.x, 0);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
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
        
        if (isLooking)
        {
            DoLook(lookInput);
        }
        
        Vector3 worldMoveDir = transform.TransformDirection(moveDir);
        worldMoveDir = worldMoveDir * speed * speedModifier;
        
        //ground angle check
        slopeAngle = Vector3.up;
        if (grounded || isSliding)
        {
            //grounded = false;
            // Vector3 rayDir = currentGround.ClosestPointOnBounds(groundTrigger.gameObject.transform.position) - groundTrigger.gameObject.transform.position;
            Vector3 rayDir = Vector3.down;
            float rayLength = Vector3.Distance(transform.position, groundTrigger.transform.position);
            RaycastHit hit;
            Physics.BoxCast(transform.position, new Vector3(0.4f, 0.00f, 0.4f), rayDir, out hit,
                transform.rotation, rayLength + 0.1f, Int32.MaxValue, QueryTriggerInteraction.Ignore);
            //Physics.Raycast(groundTrigger.gameObject.transform.position, rayDir, out hit, 2f, Int32.MaxValue, QueryTriggerInteraction.Ignore);
            if (hit.collider != null)
            {
                slopeAngle = hit.normal;
            }
            var angle = Vector3.Angle(slopeAngle, Vector3.up);
            
            //Debug.Log(angle);
            if (angle <= controller.slopeLimit + 0.01f)
            {
                isSliding = false;
                //grounded = true;
            }
            else if (angle < 89.5f)
            {
                //grounded = false;
                Vector3 slideDir = Vector3.RotateTowards(slopeAngle, Vector3.down, 90 * Mathf.Deg2Rad, 0f);
                Debug.DrawRay(hit.point, slideDir, Color.yellow, 1f);
                worldMoveDir += slideDir.normalized * (slideSpeed * Time.deltaTime);
                verticalVelocity = -slideSpeed * Time.deltaTime;
                isSliding = true;
            }
            else
            {
                slopeAngle = Vector3.up;
            }

            Debug.DrawRay(hit.point, hit.normal, Color.red, 1f);
        }
        
        worldMoveDir = Vector3.ProjectOnPlane(worldMoveDir, slopeAngle);
        
        if (isGliding && !grounded && !canClimb)
        {
            worldMoveDir = transform.TransformDirection(moveDir.x * glideSidewaysSpeed, moveDir.y, glideForwardSpeed);
            LockHead();
        }
        
        worldMoveDir.y  += verticalVelocity;
        
        
        if (canClimb)
        {
            //worldMoveDir.y = worldMoveDir.x;
            float forwardMoveDir = moveDir.z;
            if (!grounded)
            {
                forwardMoveDir = 0;
                verticalVelocity = 0;
            }
            worldMoveDir = Vector3.Scale(transform.TransformDirection(moveDir.x, moveDir.z, forwardMoveDir), climbSpeed);
        }
        

        float _gravity = gravity;
        float _termVel = terminalVelociy;
        if (fastFalling && verticalVelocity < 0)
        {
            _gravity = gravity * fallingModifier;
        }
        else
        {
            _gravity = gravity;
        }
        
        if (isGliding)
        {
            _gravity = glideGrav;
            _termVel = teminalGlideVel;
        }

        verticalVelocity -= _gravity * Time.deltaTime;
        if (verticalVelocity <= -_termVel)
        {
            verticalVelocity = -_termVel;
        }
        
        //add jump force
        if (jumpVelocity.magnitude > 0)
        {
            if(isSliding || slopeJump)
            {
                isSliding = false;
                slopeJump = true;
                //grounded = false;
                worldMoveDir.x += jumpVelocity.x;
                worldMoveDir.z += jumpVelocity.z;
                verticalVelocity = jumpVelocity.y;
                Debug.DrawRay(transform.position, jumpVelocity, Color.blue, 1f);
            }
            else
            {
                verticalVelocity = jumpForce;
                jumpVelocity = Vector3.zero;
            }
            jumpVelocity = Vector3.MoveTowards(jumpVelocity, Vector3.zero, _gravity * Time.deltaTime);
            //Debug.Log(jumpVelocity);
        }
        else
        {
            slopeJump = false;
            jumpVelocity = Vector3.zero;
        }
        
        controller.Move(worldMoveDir * Time.deltaTime);
    }
}