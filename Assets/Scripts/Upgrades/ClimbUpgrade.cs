using System;
using UnityEngine;


/// <summary>
/// when climb hitbox is colliding with vine, tell character controller they can climb
/// </summary>
public class ClimbUpgrade : UpgradeMonoBehaviour
{
    public ColliderEvents climbTrigger;
    public CharacterMovement characterMovement;
    public string climbableTag = "Climbable";

    private int climbCount = 0;

    private void OnEnable()
    {

        climbTrigger.OnTriggerEnterEvent += TouchingClimbable;
        climbTrigger.OnTriggerExitEvent += NotTouchingClimbable;
    }

    //todo add filter for climbables
    private void TouchingClimbable(GameObject self, Collider other)
    {
        //print(other.tag);
        if (other.tag == climbableTag)
        {
            climbCount++;
        }
        if (climbCount > 0)
        {
            characterMovement.Climb(true);
        }
    }

    private void NotTouchingClimbable(GameObject self, Collider other)
    {
        if (other.tag == climbableTag)
        {
            climbCount--;
        }
        if (climbCount <= 0)
        {
            climbCount = 0;
            characterMovement.Climb(false);
        }
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
