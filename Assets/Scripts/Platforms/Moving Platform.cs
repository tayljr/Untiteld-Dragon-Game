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
    private float elapsedTime;
    
    private Vector3 moveDistance;
    private List<CharacterMovement> characters = new List<CharacterMovement>();


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        TargetNextWaypoint();
    }

    // Update is called once per frame
    private void FixedUpdate()
    {
        elapsedTime += Time.deltaTime;

        float elapsedPercentage = elapsedTime / timeToWaypoint;
        elapsedPercentage = Mathf.SmoothStep(0, 1, elapsedPercentage);
        
        moveDistance = Vector3.Lerp(previouseWaypoint.position, targetWaypoint.position, elapsedPercentage) - transform.position;

        foreach (CharacterMovement character in characters)
        {
            character.PlatformMove(moveDistance);
        }
        
        transform.position = Vector3.Lerp(previouseWaypoint.position, targetWaypoint.position, elapsedPercentage);
        transform.rotation = Quaternion.Lerp(previouseWaypoint.rotation, targetWaypoint.rotation, elapsedPercentage);

        if (elapsedPercentage >= 1)
        {
            TargetNextWaypoint();
        }
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
        if (charMove != null && other.CompareTag("GroundCheck"))
        {
            characters.Add(charMove);
        }
        //other.transform.SetParent(transform);
    }
    private void OnTriggerExit(Collider other)
    {
        CharacterMovement charMove = other.gameObject.GetComponentInParent<CharacterMovement>();
        if (charMove != null && other.CompareTag("GroundCheck"))
        {
            characters.Remove(charMove);
        }
        //other.transform.SetParent(null);
    }
}
