using System;
using UnityEngine;


/// <summary>
/// when climb hitbox is colliding with vine, tell character controller they can climb
/// </summary>
public class ClimibUpdgrade : MonoBehaviour
{
    public ColliderEvents climbTrigger;
    public CharacterMovement characterMovement; 

    private void OnEnable()
    {

        climbTrigger.OnTriggerEnterEvent += TouchingClimbable;
        climbTrigger.OnTriggerExitEvent += NotTouchingClimbable;
    }

    //todo add filter for climbables
    private void NotTouchingClimbable(Collider other)
    {
        characterMovement.Climb(false);
    }

    private void TouchingClimbable(Collider other)
    {
        characterMovement.Climb(true);
    }

    private void OnDisable()
    {
        climbTrigger.OnTriggerEnterEvent -= TouchingClimbable;
        climbTrigger.OnTriggerExitEvent -= TouchingClimbable;
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
