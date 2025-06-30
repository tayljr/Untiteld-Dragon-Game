using UnityEngine;

public class CharacterMovement : MonoBehaviour
{
    public CharacterController controller;

    public float speed = 10f;
    public float sprintModifier = 1.5f;
    public float crouchModifier = 0.5f;

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

    public void Move(Vector2 dir)
    {
        moveDir = new Vector3(dir.x, moveDir.y, dir.y);
        //controller.Move(moveDir);
    }

    public void Jump()
    {
        Debug.Log(grounded);
        if (grounded)
        {
            verticalVelocity = jumpForce;
        }
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject != gameObject)
        {
            grounded = false;
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject != gameObject)
        {
            verticalVelocity = 0;
            grounded = true;
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
        moveDir.y = verticalVelocity;
        controller.Move(moveDir * speed * speedModifier * Time.deltaTime);
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
