using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    public CharacterMovement characterMovement;
    

    //could have a list of attacks, like ratchet and clank
    public AttackBase punch;
    public AttackBase fireBreath;

    public InputSystem_Actions playerControls;

    private InputAction move;
    private InputAction jump;
    private InputAction look;
    private InputAction sprint;
    private InputAction crouch;
    private InputAction attack;
    private InputAction interact;

    [Range(0.05f, 0.8f)]
    [SerializeField] private float lookSensitivity;

    private void Awake()
    {
        playerControls = new InputSystem_Actions();
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

        look = playerControls.Player.Look;
        look.Enable();
        look.performed += Look;

        attack = playerControls.Player.Attack;
        attack.Enable();
        attack.performed += Attack;
        attack.canceled += StopAttack;

        interact = playerControls.Player.Interact;
        interact.Enable();
        interact.performed += Interact;
        interact.canceled += StopInteract;
    }


    //could have a list of attacks, like ratchet and clank

    private void Interact(InputAction.CallbackContext obj)
    {
        fireBreath.StartAttack();
    }

    private void StopInteract(InputAction.CallbackContext obj)
    {
        fireBreath.StopAttack();
    }

    private void Attack(InputAction.CallbackContext obj)
    {
        punch.StartAttack();
    }

    private void StopAttack(InputAction.CallbackContext obj)
    {
        punch.StopAttack();
    }

    private void Crouch(InputAction.CallbackContext obj)
    {
        characterMovement.Crouch(true);
    }
    private void StopCrouch(InputAction.CallbackContext obj)
    {
        characterMovement.Crouch(false);
    }

    
    private void Sprint(InputAction.CallbackContext obj)
    {
        characterMovement.Sprint(true);
    }

    private void StopSprint(InputAction.CallbackContext obj)
    {
        characterMovement.Sprint(false);
    }

   
    private void Look(InputAction.CallbackContext obj)
    {

        characterMovement.Look(lookSensitivity * obj.ReadValue<Vector2>());
        //Debug.Log(obj.ReadValue<Vector2>());
    }

    private void Jump(InputAction.CallbackContext obj)
    {
        Debug.Log("Jump");
        characterMovement.Jump();
    }
    private void Move(InputAction.CallbackContext obj)
    {
        //Debug.Log(obj.ReadValue<Vector2>());

        characterMovement.Move(obj.ReadValue<Vector2>());
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
        look.Disable();
        look.performed -= Look;
        attack.performed -= Attack;
        attack.canceled -= StopAttack;
    }


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
