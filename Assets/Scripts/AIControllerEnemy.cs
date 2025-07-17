using System;
using System.Collections;
using UnityEditor;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UIElements;
[RequireComponent(typeof(NavMeshAgent))]
public class AIControllerEnemy : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private NavMeshAgent agent;
    private CharacterController characterController;
    private Rigidbody rb;
    [SerializeField] private GameObject target;
    [SerializeField] private Transform position;
    [SerializeField] private Transform foveye;
    public Transform[] patrolPoints;
    public int currentPoint;
    public bool patroling;
    public float fovdelay;

    public bool notGrounded;
    private float verticalvel;
    public float agro;
    [SerializeField,Range(0f,10f)]
    private float agromax = 4f;
    public float Gravity;
    public float minDistance;

    [Range(40f, 120f)]
    public float FOV = 65f;
    [Range (30f, 1000f)]
    public float FOVRange = 10.0f;
    public LayerMask RaycastMask;

    [SerializeField] private Vector3 agentVelocity;
    [SerializeField] private Vector3 CharControlVelocity;

    void Awake()
    {
        //grabs that boi at the start
        agent = GetComponent<NavMeshAgent>();
        characterController = GetComponent<CharacterController>();
        rb = GetComponent<Rigidbody>();

        agent.updatePosition = false;
        agent.updateRotation = false;

        agent.autoTraverseOffMeshLink = true;
        StartCoroutine(FOVCoroutine());
    }

    // Update is called once per frame
    void Update()
    {
        agentVelocity = agent.velocity;
        CharControlVelocity = characterController.velocity;

        notGrounded = !characterController.isGrounded;
        Vector3 desVelocity = agent.desiredVelocity;

        Vector3 Lookpos = agent.steeringTarget - transform.position;
        Lookpos.y = 0;
        if (Lookpos!= Vector3.zero)
        {
            Quaternion targetRot = Quaternion.LookRotation(Lookpos);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRot, Time.deltaTime * agent.angularSpeed);

        }
        if (notGrounded)
        {
            verticalvel += Gravity * Time.deltaTime;
        }
        else
        {
            verticalvel = -1f;
        }
        //move for charcontroller according to NavMeshAgent

        Vector3 move = desVelocity.normalized * agent.speed;
        move.y = verticalvel;

        characterController.Move(move * Time.deltaTime);
        agent.nextPosition = transform.position;
        
  

        
        //gravity for the CharController!


        //patrol bool
        if (patroling)
            Patrol();
        if (agro > 0)
        {
            agent.SetDestination(target.transform.position);
            agro -= Time.deltaTime;
        }
        else
        {
            //agent.ResetPath();
        }
    }

    private void Patrol()
    {
        //if agent has no path then set destination
        if (agent.remainingDistance <= minDistance)
        {
            //checks if its the last

            currentPoint++;
            if (currentPoint == patrolPoints.Length)
            {
                currentPoint = 0;
            }
            agent.SetDestination(patrolPoints[currentPoint].position);
        }

    }

    public IEnumerator FOVCoroutine()
    {
        while (true)
        {
            yield return new WaitForSeconds(fovdelay);
            //check if player is still in view
            FovCheck();
        }

    }

    private void FovCheck()
    {
        Vector3 dir = (target.transform.position - transform.position).normalized;

        float angleToTarget = Vector3.Angle(transform.forward, dir);
        if (angleToTarget > FOV / 2)
            return; //outside of view, lets get out of this function!
        RaycastHit hit;
        if (Physics.Raycast(transform.position, dir, out hit, FOVRange, RaycastMask))
        {
            Debug.DrawRay(transform.position, dir * hit.distance, Color.red);

            if (hit.transform.gameObject == target)
            {
                agro = agromax;
            }
        }
    }

    public void gotopos()
    {
        agent.SetDestination(position.transform.position);
    }
    private void OnDrawGizmos()
    {
        //null check for patrol points so no 100000 errors
        if (patrolPoints != null)
        {
            foreach (var point in patrolPoints)
            {
                //null check for poins for also no 10000 errors
                if (point != null)
                {
                    
                    Gizmos.DrawIcon(point.position, "point",true);
                }
            }
        }
        Handles.DrawWireArc(foveye.transform.position, Vector3.up,Vector3.forward,360,FOVRange);
    }
    private void OnDisable()
    {
        agent.enabled = false;
        rb.isKinematic = false;
        rb.linearVelocity = characterController.velocity;
    }
    private void OnEnable()
    {
        agent.enabled = true;
    }
}
