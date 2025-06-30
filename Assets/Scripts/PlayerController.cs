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
        jump = playerControls.Player.Jump;
        jump.Enable();
        jump.performed += Jump;
        look = playerControls.Player.Look;
        look.Enable();
        look.performed += Look;
    }

    private void Look(InputAction.CallbackContext obj)
    {
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
