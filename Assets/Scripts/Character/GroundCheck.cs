using System;
using UnityEngine;

public class GroundCheck : MonoBehaviour
{
    //todo send this to the character state machine
    public float groundCheckRadius;
    public float groundCheckHeight;
    public float slopeCheckHeight;
    public LayerMask ignoreGroundCheck;
    
    public bool grounded;
    public Vector3 slopeNormal;
    public float slopeAngle;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        slopeNormal = Vector3.up;
        slopeAngle = Vector3.Angle(slopeNormal, Vector3.up);
        grounded = false;
    }

    // Update is called once per frame
    void Update()
    {
        RaycastHit hit;
        LayerMask mask = ~(ignoreGroundCheck);
        Physics.SphereCast(transform.position, groundCheckRadius, Vector3.down, out hit, groundCheckHeight, mask, QueryTriggerInteraction.Ignore);
        if (hit.collider != null)
        {
            grounded = true;
            slopeNormal = hit.normal;
            slopeAngle = Vector3.Angle(slopeNormal, Vector3.up);
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
