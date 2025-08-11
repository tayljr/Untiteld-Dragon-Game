using Mono.Cecil.Cil;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerAnimation : MonoBehaviour
{

    private Animator animator;

    private CharacterController characterController;
    private CharacterMovement characterMovement;

    [SerializeField] bool IsIdle;
    [SerializeField] bool IsFalling;
    [SerializeField] bool IsSprinting;


    [SerializeField] private InputActionReference move;
    [SerializeField] private InputActionReference jump;
    [SerializeField] private InputActionReference sprint;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        animator = GetComponent<Animator>();
        characterController = GetComponentInParent<CharacterController>();
        characterMovement = GetComponentInParent<CharacterMovement>();
    }
    private void OnEnable()
    {
        move.action.performed += Move;
        jump.action.performed += Jump;
        sprint.action.performed += Sprint;
    }

    private void Sprint(InputAction.CallbackContext obj)
    {
        IsSprinting = true;
    }

    private void Jump(InputAction.CallbackContext obj)
    {
        animator.SetTrigger("HasJumped");
    }

    private void OnDisable()
    {
        move.action.performed -= Move;
        jump.action.performed -= Jump;
        sprint.action.performed -= Sprint;
    }
    private void Move(InputAction.CallbackContext obj)
    {
        IsIdle = false;

        animator.SetFloat("Input.x", obj.ReadValue<Vector2>().x);
        animator.SetFloat("Input.y", obj.ReadValue<Vector2>().y);



    }

    // Update is called once per frame
    void Update()
    {
        UpdateAnimatorValues();
    }
    public void UpdateAnimatorValues()
    {

        if (!move.action.phase.IsInProgress())
        {
            IsIdle = true;
        }

        if (!sprint.action.phase.IsInProgress())
        {
            IsSprinting = false;
        }
        IsFalling = !characterMovement.grounded;

        animator.SetBool("IsIdle",IsIdle);
        animator.SetBool("IsFalling", IsFalling);
        animator.SetBool("IsSprinting",IsSprinting);


    }
}
