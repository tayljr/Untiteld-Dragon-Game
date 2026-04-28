using System;
using UnityEngine;

public class GroundCheck : MonoBehaviour
{
    //todo send this to the character state machine
    public float groundCheckRadius;
    public float groundCheckHeight;
    public float slopeCheckHeight;
    public LayerMask ignoreGroundCheck;
    public float stepHeight = 0.3f;
    public float stepDistance = 0.2f;
    
    public bool grounded;
    public Vector3 slopeNormal;
    public float slopeAngle;
    public Transform groundObjTrans;
    public MovingPlatform movingGround;
    public float stepAngle;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        slopeNormal = Vector3.up;
        slopeAngle = Vector3.Angle(slopeNormal, Vector3.up);
        groundObjTrans = null;
        grounded = false;
        CharacterController characterController = GetComponentInParent<CharacterController>();
        if (characterController != null)
        {
            stepHeight = characterController.stepOffset;
        }
    }

    // Update is called once per frame
    void Update()
    {
        movingGround = null;
        RaycastHit hit;
        LayerMask mask = ~(ignoreGroundCheck);
        Physics.SphereCast(transform.position, groundCheckRadius, Vector3.down, out hit, groundCheckHeight, mask, QueryTriggerInteraction.Ignore);
        if (hit.collider != null)
        {
            RaycastHit hit2;
            Vector3 hitDirection = hit.point - transform.position;
            Physics.Raycast(transform.position, hitDirection, out hit2, float.MaxValue, mask, QueryTriggerInteraction.Ignore);
            //Physics.Linecast(transform.position, hit.point, out hit2, mask, QueryTriggerInteraction.Ignore);
            if (hit2.collider != null)
            {
                Debug.DrawRay(hit2.point, hit2.normal * groundCheckHeight, Color.red);
                grounded = true;
                groundObjTrans = hit2.collider.transform;
                slopeNormal = hit2.normal;
                slopeAngle = Vector3.Angle(slopeNormal, Vector3.up);
                movingGround = (MovingPlatform)hit2.collider.GetComponent(typeof(MovingPlatform));
                
                RaycastHit stepHit;
                Vector3 stepDirection = hit2.point - transform.position;
                stepDirection.y = 0;
                stepDirection.Normalize();
                Vector3 stepPos = transform.position + Vector3.down * groundCheckHeight + Vector3.up * stepHeight + stepDirection * stepDistance;
                Physics.Raycast(stepPos, Vector3.down, out stepHit, stepHeight * 2, mask, QueryTriggerInteraction.Ignore);
                Debug.DrawRay(stepPos, Vector3.down * stepHeight, Color.red);
                if (stepHit.collider != null)
                {
                    stepAngle = Vector3.Angle(stepHit.normal, Vector3.up);
                    Debug.DrawRay(stepHit.point, stepHit.normal * (stepHeight * 2), Color.green);
                    slopeNormal = stepHit.normal;
                }
                else
                {
                    stepAngle = 90f;
                }
            }
            /*
            RaycastHit SlopeHit;
            Vector3 dir = hit.point - transform.position;
            Physics.SphereCast(transform.position, 0.3f, dir, out SlopeHit, dir.magnitude, Int32.MaxValue, QueryTriggerInteraction.Ignore);
            if (SlopeHit.collider != null)
            {
                slopeNormal = SlopeHit.normal;
                slopeAngle = Vector3.Angle(slopeNormal, Vector3.up);
            }*/
        }
        else
        {
            slopeNormal = Vector3.up;
            slopeAngle = Vector3.Angle(slopeNormal, Vector3.up);
            grounded = false;
        }
    }
}
