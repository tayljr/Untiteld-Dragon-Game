using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    public CharacterMovement characterMovement;
    public InputSystem_Actions playerControls;

    private InputAction move;
    private InputAction jump;
    private InputAction look;
    private InputAction sprint;
    private InputAction crouch;
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
