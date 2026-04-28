using System;
using UnityEngine;
using System.Collections.Generic;
using System.Collections;
using UnityEngine.TextCore.Text;

public class MovingPlatform : MonoBehaviour
{
    [SerializeField]
    private Waypointpath waypointPath;

    [SerializeField]
    private float speed;

    private int targetWaypointIndex;

    private Transform previouseWaypoint;
    private Transform targetWaypoint;

    private float timeToWaypoint;
    private float fixedTimeToWaypoint;
    private float elapsedTime;
    private float fixedElapsedTime;
    
    private Vector3 moveDistance;
    private List<CharacterMovement> characters = new List<CharacterMovement>();
    private List<CharacterStateMachine> characterMachines = new List<CharacterStateMachine>();
    
    //todo remove fixed update stuff
    float lastDeltaTime;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        TargetNextWaypoint();
    }

    public void AddCharacter(CharacterStateMachine character)
    {
        if (character != null && !characterMachines.Contains(character))
        {
            characterMachines.Add(character);
        }
    }

    public void RemoveCharacter(CharacterStateMachine character)
    {
        if (character != null && characterMachines.Contains(character))
        {
            characterMachines.Remove(character);
        }
    }
    // Update is called once per frame
    private void FixedUpdate()
    {
        float fixedDelta = Time.fixedDeltaTime/lastDeltaTime;
        foreach (CharacterMovement character in characters)
        {
            character.PlatformMove(moveDistance * fixedDelta);
        }
    }

    private void Update()
    {
        lastDeltaTime = Time.deltaTime;
        elapsedTime += Time.deltaTime;

        float elapsedPercentage = elapsedTime / timeToWaypoint;
        elapsedPercentage = Mathf.SmoothStep(0, 1, elapsedPercentage);
        
        moveDistance = Vector3.Lerp(previouseWaypoint.position, targetWaypoint.position, elapsedPercentage) - transform.position;
        foreach (CharacterStateMachine character in characterMachines)
        {
            character.PlatformMove(moveDistance);
        }
        
        transform.position = Vector3.Lerp(previouseWaypoint.position, targetWaypoint.position, elapsedPercentage);
        transform.rotation = Quaternion.Lerp(previouseWaypoint.rotation, targetWaypoint.rotation, elapsedPercentage);

        if (elapsedPercentage >= 1)
        {
            TargetNextWaypoint();
        }
        
        characterMachines.Clear();
    }

    private void TargetNextWaypoint()
    {
        previouseWaypoint = waypointPath.GetWaypoint(targetWaypointIndex);
        targetWaypointIndex = waypointPath.GetNextWaypointIndex(targetWaypointIndex);
        targetWaypoint = waypointPath.GetWaypoint(targetWaypointIndex);

        elapsedTime = 0;

        float distanceToWaypoint = Vector3.Distance(previouseWaypoint.position, targetWaypoint.position);
        timeToWaypoint = distanceToWaypoint / speed;
    }

    public Transform GetPos()
    {
        return transform;
    }
    
    private void OnTriggerEnter(Collider other)
    {
        CharacterMovement charMove = other.gameObject.GetComponentInParent<CharacterMovement>();
        //CharacterStateMachine charMach = other.gameObject.GetComponentInParent<CharacterStateMachine>();
        if (charMove != null && other.CompareTag("GroundCheck"))
        {
            characters.Add(charMove);
        }
        /*
        if (charMach != null && other.CompareTag("GroundCheck"))
        {
            characterMachines.Add(charMach);
        }*/
        //other.transform.SetParent(transform);
    }
    private void OnTriggerExit(Collider other)
    {
        CharacterMovement charMove = other.gameObject.GetComponentInParent<CharacterMovement>();
        //CharacterStateMachine charMach = other.gameObject.GetComponentInParent<CharacterStateMachine>();
        if (charMove != null && other.CompareTag("GroundCheck"))
        {
            characters.Remove(charMove);
        }

        /*
        if (charMach != null && other.CompareTag("GroundCheck"))
        {
            characterMachines.Remove(charMach);
        }*/
        //other.transform.SetParent(null);
    }
}
