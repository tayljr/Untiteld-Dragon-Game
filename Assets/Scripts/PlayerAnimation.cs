using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerAnimation : MonoBehaviour
{

    private Animator animator;

    private PlayerController playerController;
    private CharacterMovement characterMovement;

    [SerializeField] bool IsIdle;
    [SerializeField] bool IsFalling;
    [SerializeField] bool IsSprinting;
    [SerializeField] bool IsClimbing;
    [SerializeField] bool IsGliding;

    public bool IsTalking;

    [SerializeField] private InputActionReference move;
    [SerializeField] private InputActionReference jump;
    [SerializeField] private InputActionReference sprint;
    [SerializeField] private InputActionReference punch;
    [SerializeField] private InputActionReference fire;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        animator = GetComponent<Animator>();
        playerController = GetComponentInParent<PlayerController>();
        characterMovement = GetComponentInParent<CharacterMovement>();
    }
    private void OnEnable()
    {
        move.action.performed += Move;
        move.action.canceled += Move_canceled;
        jump.action.performed += Jump;
        sprint.action.performed += Sprint;
        punch.action.performed += Punch;
        fire.action.performed += Fire;

        fire.action.canceled += StopFire;

    }

    private void StopFire(InputAction.CallbackContext obj)
    {
        animator.SetBool("IsFire", false);
    }

    private void Fire(InputAction.CallbackContext obj)
    {
        animator.SetBool("IsFire",true);
    }

    private void Move_canceled(InputAction.CallbackContext obj)
    {

        animator.SetFloat("Input.x", 0);
        animator.SetFloat("Input.y", 0);
    }

    private void Punch(InputAction.CallbackContext obj)
    {
        animator.SetTrigger("Punch");
        
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
        move.action.canceled -= Move_canceled;
        jump.action.performed -= Jump;
        sprint.action.performed -= Sprint;
        punch.action.performed -= Punch;
        fire.action.performed -= Fire;
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

        if (IsTalking)
        {
            jump.action.performed -= Jump;
            int index = animator.GetLayerIndex("Interaction");
            animator.SetLayerWeight(index, 1f);

        }
        else
        {
            jump.action.performed += Jump;
            int index = animator.GetLayerIndex("Interaction");
            animator.SetLayerWeight(index, 0f);
        }

        if (!move.action.phase.IsInProgress())
        {
            IsIdle = true;
        }

        if (!sprint.action.phase.IsInProgress())
        {
            IsSprinting = false;
        }

        IsFalling = !characterMovement.grounded;
        IsGliding = characterMovement.isGliding;
        IsClimbing = characterMovement.isClimbing;

        animator.SetBool("IsIdle", IsIdle);
        animator.SetBool("IsFalling", IsFalling);
        animator.SetBool("IsSprinting",IsSprinting);
        animator.SetBool("IsGliding", IsGliding);
        animator.SetBool("IsClimbing", IsClimbing);
    }
    void OnAnimatorIK(int layerIndex)
    {

        if (IsGliding || IsClimbing)
        {
            animator.SetLookAtWeight(0);
        }
        else
            animator.SetLookAtWeight(1f);

        if (IsGliding || IsClimbing  || IsTalking)
        {
        animator.SetLookAtWeight(0f,0f);

        }
        else
        {
        animator.SetLookAtWeight(1f, 1f);
        }
            animator.SetLookAtPosition(characterMovement.head.transform.position + characterMovement.head.transform.forward * 10f);
    }
}
