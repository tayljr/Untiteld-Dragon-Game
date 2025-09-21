using UnityEngine;

public class CharacterMovement : MonoBehaviour
{
    public CharacterController controller;

    public ColliderEvents groundTrigger;

    public GameObject head;
    public Vector2 minMaxHeadTilt = new Vector2(-45f, 45f);
    public Vector2 minMaxHeadTurn = new Vector2(-45f, 45f);

    public float speed = 10f;
    public float sprintModifier = 1.5f;
    public float crouchModifier = 0.5f;

    public int defaultJumpCount = 1;
    public int maxJumpCount = 1;
    private int jumpCount = 0;
    public float jumpForce = 2f;
    public bool fastFalling = true;
    public float fallingModifier = 1.5f;
    public float gravity = 10f;
    public float terminalVelociy = 50f;
    public Vector3 climbSpeed = new Vector3(10f, 10f, 10f);
    public float glideGrav = 5f;
    public float teminalGlideVel = 2.5f;
    public float glideForwardSpeed = 10f;
    public float glideSidewaysSpeed = 5f;


    //will be private
    private bool canClimb = false;
    private bool canGlide = false;

    private Vector3 moveDir = Vector3.zero;
    private float speedModifier = 1f;
    [SerializeField]
    private float verticalVelocity = 0f;

    //please dont priv this i need for animator :(
    public bool grounded = false;
    public bool isCrouching = false;
    public bool isGliding = false;
    public Vector2 currentHeadDir = Vector2.zero;


    [SerializeField]
    private int groundCount = 0;

    [SerializeField]
    private Vector2 currentCharacterDir = Vector2.zero;

    public void Look(Vector2 dir)
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
        if (jumpCount < maxJumpCount)
        {
            jumpCount++;
            verticalVelocity = jumpForce;
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
    private void NotGrounded(Collider other)
    {
        if (other.gameObject != gameObject)
        {
            groundCount--;
        }
        if (groundCount <= 0)
        {
            groundCount = 0;
            grounded = false;
        }
    }
    private void Grounded(Collider other)
    {
        if (other.gameObject != gameObject)
        {
            verticalVelocity = 0;
            grounded = true;
            jumpCount = 0;
            groundCount++;
            isGliding = false;
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
        //ground check
        /*
        RaycastHit hit;
        Vector3 feetPos = transform.position;
        feetPos.y -= controller.height / 3;
        grounded = Physics.SphereCast(feetPos, controller.height / 4, -transform.up, out hit, 0.5f);
        */

        Vector3 worldMoveDir = transform.TransformDirection(moveDir);
        worldMoveDir = worldMoveDir * speed * speedModifier;

        if (isGliding && !grounded && !canClimb)
        {
            //worldMoveDir.y = -glideGrav;
            worldMoveDir = transform.TransformDirection(moveDir.x * glideSidewaysSpeed, moveDir.y, glideForwardSpeed);
            LockHead();
        }

        worldMoveDir.y = verticalVelocity;

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


        if (!grounded)
        {
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
            Debug.Log(verticalVelocity);
        }
        
        controller.Move(worldMoveDir * Time.deltaTime);
    }
}