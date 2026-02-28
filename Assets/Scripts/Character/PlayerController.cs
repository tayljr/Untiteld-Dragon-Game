using System;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Serialization;

public class PlayerController : MonoBehaviour, IPauseable
{
    public CharacterMovement characterMovement;
    public CharacterStateMachine stateMachine;
    public PlayerAnimation playerAnimation;

    //could have a list of attacks, like ratchet and clank
    [FormerlySerializedAs("punch")] public AttackBase attackPunch;
    [FormerlySerializedAs("fireBreath")] public AttackBase attackFireBreath;

    public Interactor interactor;
    
    public InputSystem_Actions playerControls;

    private InputAction move;
    private InputAction jump;
    private InputAction look;
    private InputAction sprint;
    private InputAction crouch;
    private InputAction attack;
    private InputAction fireBreath;
    private InputAction interact;
    private InputAction glide;

    [Range(0.05f, 0.8f)]
    [SerializeField] private float lookSensitivity;

    private void Awake()
    {
        playerControls = new InputSystem_Actions();
    }
    public void SetSensitivity(float value)
    {
        lookSensitivity = value;
    }
    public void OnPause()
    {
        //isPaused = true;
        enabled = false;
        characterMovement.enabled = false;
        stateMachine.enabled = false;
        playerAnimation.enabled = false;
    }
    public void OnResume()
    {
        //isPaused = false;
        enabled = true;
        characterMovement.enabled = true;
        stateMachine.enabled = true;
        playerAnimation.enabled = true;
    }

    private void OnEnable()
    {
        move = playerControls.Player.Move;
        move.Enable();
        move.performed += Move;
        move.canceled += Move;

        sprint = playerControls.Player.Sprint;
        sprint.Enable();
        sprint.performed += Sprint;
        sprint.canceled += StopSprint;

        crouch = playerControls.Player.Crouch;
        crouch.Enable();
        crouch.performed += Crouch;
        crouch.canceled += StopCrouch;

        jump = playerControls.Player.Jump;
        jump.Enable();
        jump.performed += Jump;
        jump.canceled += StopJump;

        look = playerControls.Player.Look;
        look.Enable();
        look.performed += Look;
        look.canceled += StopLook;

        attack = playerControls.Player.Attack;
        attack.Enable();
        attack.performed += Attack;
        attack.canceled += StopAttack;
        
        
        fireBreath = playerControls.Player.FireBreath;
        fireBreath.Enable();
        fireBreath.performed += FireBreath;
        fireBreath.canceled += StopFireBreath;

        interact = playerControls.Player.Interact;
        interact.Enable();
        interact.performed += Interact;
        interact.canceled += StopInteract;

        glide = playerControls.Player.Glide;
        glide.Enable();
        glide.performed += Glide;
        glide.canceled += StopGlide;

        lookSensitivity = PlayerPrefs.GetFloat("LookSensitivity", 0.2f);
    }


    //could have a list of attacks, like ratchet and clank

    private void Glide(InputAction.CallbackContext obj)
    {
        if (characterMovement != null) characterMovement.Glide(true);
        if (stateMachine != null) stateMachine.Glide(true);
    }

    private void StopGlide(InputAction.CallbackContext obj)
    {
        if (characterMovement != null) characterMovement.Glide(false);
        if (stateMachine != null) stateMachine.Glide(false);
    }

    private void Interact(InputAction.CallbackContext obj)
    {
        if (interactor != null) interactor.Interact(true);
    }

    private void StopInteract(InputAction.CallbackContext obj)
    {
        if (interactor != null) interactor.Interact(false);
    }

    private void Attack(InputAction.CallbackContext obj)
    {
        if (attackPunch != null) attackPunch.StartAttack();
    }

    private void StopAttack(InputAction.CallbackContext obj)
    {
        if (attackPunch != null) attackPunch.StopAttack();
    }

    private void FireBreath(InputAction.CallbackContext obj)
    {
        if (attackFireBreath != null) attackFireBreath.StartAttack();
    }
    
    private void StopFireBreath(InputAction.CallbackContext obj)
    {
        if (attackFireBreath != null) attackFireBreath.StopAttack();
    }
    private void Crouch(InputAction.CallbackContext obj)
    {
        if (characterMovement != null) characterMovement.Crouch(true);
    }
    private void StopCrouch(InputAction.CallbackContext obj)
    {
        if (characterMovement != null) characterMovement.Crouch(false);
    }

    
    private void Sprint(InputAction.CallbackContext obj)
    {
        if (characterMovement != null) characterMovement.Sprint(true);
        if (stateMachine != null) stateMachine.Sprint(true);
    }

    private void StopSprint(InputAction.CallbackContext obj)
    {
        if (characterMovement != null) characterMovement.Sprint(false);
    }

   
    private void Look(InputAction.CallbackContext obj)
    {
        if (characterMovement != null) characterMovement.Look(lookSensitivity * obj.ReadValue<Vector2>(), true);
        if (stateMachine != null) stateMachine.Look(lookSensitivity * obj.ReadValue<Vector2>(), true);
        //Debug.Log(obj.ReadValue<Vector2>());
    }

    private void StopLook(InputAction.CallbackContext obj)
    {
        if (characterMovement != null) characterMovement.Look(lookSensitivity * obj.ReadValue<Vector2>(), false);
        if (stateMachine != null) stateMachine.Look(lookSensitivity * obj.ReadValue<Vector2>(), false);
    }

    private void Jump(InputAction.CallbackContext obj)
    {
        //Debug.Log("Jump");
        if (characterMovement != null) characterMovement.Jump();
        if (stateMachine != null) stateMachine.Jump(true);
    }

    private void StopJump(InputAction.CallbackContext obj)
    {
        if (stateMachine != null) stateMachine.Jump(false);
    }
    private void Move(InputAction.CallbackContext obj)
    {
        //Debug.Log(obj.ReadValue<Vector2>());

        if (characterMovement != null) characterMovement.Move(obj.ReadValue<Vector2>());
        if (stateMachine != null) stateMachine.Move(obj.ReadValue<Vector2>());
    }

    private void OnDisable()
    {
        move.Disable();
        move.performed -= Move;
        move.canceled -= Move;
        sprint.Disable();
        sprint.performed -= Sprint;
        sprint.canceled -= StopSprint;
        crouch.Disable();
        crouch.performed -= Crouch;
        crouch.canceled -= StopCrouch;
        jump.Disable();
        jump.performed -= Jump;
        jump.canceled -= StopJump;
        look.Disable();
        look.performed -= Look;
        look.canceled -= StopLook;
        attack.performed -= Attack;
        attack.canceled -= StopAttack;
        interact.performed -= Interact;
        interact.canceled -= StopInteract;
        glide.performed -= Glide;
        glide.canceled -= StopGlide;
    }


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
