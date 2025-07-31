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
    private bool isCrouching = false;

    public float jumpForce = 2f;
    public bool fastFalling = true;
    public float fallingModifier = 1.5f;
    public float gravity = 10f;


    private Vector3 moveDir = Vector3.zero;
    private float speedModifier = 1f;
    [SerializeField]
    private float verticalVelocity = 0f;

    [SerializeField]
    private bool grounded = false;
    private int groundCount = 0;

    [SerializeField]
    private Vector2 currentHeadDir = Vector2.zero;
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
            if (currentHeadDir.y > minMaxHeadTilt.y)
            {
                currentHeadDir.y = minMaxHeadTilt.y;
            }
            else if (currentHeadDir.y < minMaxHeadTilt.x)
            {
                currentHeadDir.y = minMaxHeadTilt.x;
            }
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

        head.transform.localRotation = Quaternion.Euler(-currentHeadDir.y, currentHeadDir.x, 0);
        transform.localRotation = Quaternion.Euler(0, currentCharacterDir.x, 0);
    }
    public void Move(Vector2 dir)
    {
        moveDir = new Vector3(dir.x, moveDir.y, dir.y);
        currentCharacterDir.x += currentHeadDir.x;
        //transform.localRotation = Quaternion.Euler(0, currentHeadDir.y, 0);
        currentHeadDir.x = 0;
        head.transform.localRotation = Quaternion.Euler(-currentHeadDir.y, currentHeadDir.x, 0);

        transform.localRotation = Quaternion.Euler(0, currentCharacterDir.x, 0);
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
        Debug.Log(grounded);
        if (grounded)
        {
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

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
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
    private void Grounded( Collider other)
    {
        if (other.gameObject != gameObject)
        {
            verticalVelocity = 0;
            grounded = true;
            groundCount++;
        }
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
        Vector3 worldMoveDir = head.transform.TransformDirection(moveDir);
        worldMoveDir = worldMoveDir * speed * speedModifier;
        worldMoveDir.y = verticalVelocity;
        controller.Move(worldMoveDir * Time.deltaTime);
        if (!grounded)
        {
            if (fastFalling && verticalVelocity < 0)
            {
                verticalVelocity -= gravity * fallingModifier * Time.deltaTime;
            }
            else
            {
                verticalVelocity -= gravity * Time.deltaTime;
            }
        }
        
    }
}
